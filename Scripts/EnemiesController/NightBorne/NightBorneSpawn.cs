using UnityEngine;

public class NightBorneSpawn : Behaviour, IState
{
	#region Exposed
	#endregion

	#region Unity API
	void Awake()
    {
		_controller = GetComponent<NightBorneController>();
    }
	#endregion

	#region Main Methods

	public void DoExit()
	{
		GetComponent<Collider2D>().enabled = true;
	}

	public void DoInit()
	{
		_controller.Velocity = Vector2.zero;
	}

	public void DoUpdate()
	{
	}

	public void Spawned()
	{
		_hasSpawned = true;
	}

	public bool HasSpawned { get => _hasSpawned; set => _hasSpawned = value; }

	#endregion

	#region Privates

	private NightBorneController _controller;

	private bool _hasSpawned;


	#endregion
}
