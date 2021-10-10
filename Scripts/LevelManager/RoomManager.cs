using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
	#region Exposed

	[SerializeField] private List<GameObject> m_enemies;
	[SerializeField] private int m_numberOfEnemiesPerWave;
	[SerializeField] private int m_numberOfWaves;

	[SerializeField] private GameObject[] m_doors;

	#endregion

	#region Unity API

	private void Awake()
	{
		_roomClear = false;
	}

	private void Start()
    {
		_spawnPoint = new List<Transform>(transform.Find("=== SpawnPoint ===").GetComponentsInChildren<Transform>());
		_enemies = transform.Find("=== Enemies ===");
		_enemiesPool = new Queue<GameObject>();
		for (int i = 0; i < m_enemies.Count; i++)
		{
			GameObject enemy = Instantiate(m_enemies[i], transform.position, Quaternion.identity, _enemies);
			enemy.GetComponent<EnemyController>().IsDisabled = true;
			_enemiesPool.Enqueue(enemy);
			enemy.SetActive(false);
		}

		if (m_numberOfEnemiesPerWave > _enemiesPool.Count)
			m_numberOfEnemiesPerWave = _enemiesPool.Count;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerHurtBox") && !_roomClear)
		{
			CloseDoors();
			CheckRoomState();
		}
	}

	#endregion

	#region Main Methods

	public void CheckRoomState()
	{
		int numberOfInactiveGO = 0;
		for (int i = 0; i < _enemies.childCount; i++)
		{
			if (_enemies.GetChild(i).GetComponent<EnemyController>().IsDisabled)
				numberOfInactiveGO++;
		}
		if (numberOfInactiveGO == _enemies.childCount)
			Invoke("NextWave", 0.5f);
		else
			return;
	}

	private void CloseDoors()
	{
		foreach (GameObject door in m_doors)
			door.SetActive(true);
	}

	private void OpenDoors()
	{
		foreach (GameObject door in m_doors)
			door.SetActive(false);
	}

	private void NextWave()
	{
		_currentWave++;
		if (_currentWave <= m_numberOfWaves)
			SpawnEnemies();
		else
		{
			_roomClear = true;
			OpenDoors();
		}
	}

	private void SpawnEnemies()
	{
		for (int i = 0; i < m_numberOfEnemiesPerWave; i++)
		{
			int spawnIndex = Random.Range(0, _spawnPoint.Count);
			GameObject spawnedEnemy = _enemiesPool.Dequeue();
			spawnedEnemy.transform.position = _spawnPoint[spawnIndex].position;
			spawnedEnemy.SetActive(true);
		}
	}

	public Queue<GameObject> EnemiesPool { get => _enemiesPool; set => _enemiesPool = value; }
	public bool RoomClear { get => _roomClear; set => _roomClear = value; }

	#endregion

	#region Privates

	private bool _roomClear;
	private int _currentWave;

	private Queue<GameObject> _enemiesPool;
	private List<Transform> _spawnPoint;
	private Transform _enemies;

	#endregion
}
