using UnityEngine;

public class CameraMovementController : Behaviour
{
    #region Exposed

    [SerializeField] private Transform m_player;
    [Header("Horizontal Parameters")]
    [SerializeField] private float m_horizontalOffset = 1.5f;
    [SerializeField] private float m_xLerpValue = 1f;
    [Header("Vertical Parameters")]
    [SerializeField] private float m_yWindow;
    [SerializeField] private float m_yLerpValue = 0.05f;


    #endregion

    #region Unity API
    private void Awake()
    {
        _PoI = new Vector2(_currentCameraX, _currentCameraY);
    }

	private void Update()
	{
        _currentCameraY = Mathf.Lerp(_currentCameraY, m_player.position.y, m_yLerpValue);
        _currentCameraX = Mathf.Lerp(_currentCameraX, m_player.position.x, m_xLerpValue);

        _PoI = new Vector2(_currentCameraX, _currentCameraY);
    }

	private void LateUpdate()
    {
        transform.position = (Vector3)_PoI + Vector3.forward * -10f;
    }

    
    private void OnDrawGizmos()
    {
        if (!m_isDebug) return;
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_PoI, 0.2f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(m_player.position + Vector3.up * 3, m_player.position + Vector3.up * -3);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(m_player.position + Vector3.right * m_horizontalOffset, m_player.position + Vector3.right * -m_horizontalOffset);
        }
    }
    

    #endregion

    #region Main Methods
    #endregion

    #region Privates

    private Vector2 _PoI;

    private float _currentCameraX;
    
    private float _currentCameraY;

    #endregion
}
