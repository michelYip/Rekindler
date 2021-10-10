using UnityEngine;

public class RoomMaskController : MonoBehaviour
{
    #region Exposed

    [SerializeField] private float m_fadeDuration = 0.5f;
	[SerializeField] private bool m_isStartingRoom = false;

    #endregion

    #region Unity API
    private void Awake()
    {
        _mask = GetComponent<SpriteRenderer>();
        _fadeTimer = Time.time;
		_lightOff = true;
		if (m_isStartingRoom)
			gameObject.SetActive(false);
    }

	private void Start()
	{
		_player = GameObject.Find("Player").GetComponent<PlayerStateMachine>();
	}

	private void Update()
	{
		float alpha = (Time.time - _fadeTimer) / m_fadeDuration;
		if (_lightOff)
			_mask.color = Color.Lerp(_transparent, _opaque, alpha);
		else
			_mask.color = Color.Lerp(_opaque, _transparent, alpha);
	}

	/*private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerHurtBox") && !IsInsideOfRoom(collision.transform.position))
		{
			_lightOff = false;
			_fadeTimer = Time.time;
		}
	}*/

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerHurtBox") && _lightOff)
		{
			_lightOff = false;
			_fadeTimer = Time.time;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerHurtBox") && !IsInsideOfRoom(collision.transform.position))
		{
			if (!_lightOff)
			{
				_lightOff = true;
				_fadeTimer = Time.time;
			}
		}
	}

	#endregion

	#region Main Methods

	private bool IsInsideOfRoom(Vector3 position)
	{
		float x = position.x;
		float y = position.y;
		Vector3 topLeft		= transform.position + new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2);
		Vector3 bottomRight = transform.position + new Vector3(transform.localScale.x / 2, -transform.localScale.y / 2);

		return (x > topLeft.x && x < bottomRight.x && y < topLeft.y && y > bottomRight.y);
	}

	#endregion

	#region Privates

	private SpriteRenderer _mask;
    private float _fadeTimer;
	private bool _lightOff;

	private Color _opaque = new Color(0, 0, 0, 1);
	private Color _transparent = new Color(0, 0, 0, 0);

	private PlayerStateMachine _player;

    #endregion
}
