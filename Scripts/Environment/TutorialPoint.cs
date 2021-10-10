using UnityEngine;

public class TutorialPoint : MonoBehaviour
{
    #region Exposed

    [SerializeField] private GameObject m_canvas;

	#endregion

	#region Unity API

	private void Awake()
	{
        m_canvas.SetActive(false);
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
    #endregion

    #region Privates



    #endregion
}
