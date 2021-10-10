using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorExit : MonoBehaviour, IUsable
{
    #region Exposed

    [SerializeField] private GameObject m_canvas;
    [SerializeField] private string m_nextLevel;
    [SerializeField] private RoomManager m_assignedRoom;
    [SerializeField] private GameObject m_particleSystem;

    #endregion

    #region Unity API

    private void Awake()
	{
        _collider = GetComponent<Collider2D>();

        _collider.enabled = false;
        m_particleSystem.SetActive(false);
        m_canvas.SetActive(false);
    }

	private void Update()
	{
		if (m_assignedRoom.RoomClear)
		{
            _collider.enabled = true;
            m_particleSystem.SetActive(true);
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHurtBox"))
            m_canvas.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHurtBox"))
            m_canvas.SetActive(false);
    }

    #endregion

    #region Main Methods

    public void Use()
    {
        SceneManager.LoadScene(m_nextLevel);
    }

    #endregion

    #region Privates

    private Collider2D _collider;

    #endregion
}
