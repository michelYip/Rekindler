using UnityEngine;

public class PlayerDodge : Behaviour, IState
{
    #region Exposed

    [SerializeField] private float m_playerRadius = 0.4f;
    [SerializeField] private LayerMask m_wallLayerMask;

    [Range(4f,10f)]
    [SerializeField] private float m_dodgeDistance = 5f;

    [Range(0.25f,1f)]
    [SerializeField] private float m_dodgeCooldown = 0.75f;

    #endregion

    #region Unity API
    void Awake()
    {
        _controller = GetComponent<PlayerController>();

        _canDodge = true;
        _dodgeOnCooldown = false;
    }

    void Update()
    {
        if (_dodgeOnCooldown)
		{
            float alpha = (Time.time - _dodgeCooldownStart) / m_dodgeCooldown;
            if (alpha >= 1f)
			{
                _canDodge = true;
                _dodgeOnCooldown = false;
			}
		}
    }

	private void OnDrawGizmos()
	{
        if (!m_drawGizmos) return;
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, m_playerRadius);
	}

	#endregion

	#region Main Methods

	public void DoInit()
    {
        _dodgeEnded = false;
        _controller.Animator.SetTrigger("DodgeTrigger");
        _controller.Velocity = Vector2.zero;
        _dir = transform.localScale.x;
        _dodgeStart = Time.time;

        _dodgeVelocity = _dir * Vector2.right * (m_dodgeDistance / _dodgeDuration);
        //_collider.enabled = false;
        _controller.HasInvincibilityFrame = true;
        _controller.SetCollisionsLayer(true);

        _canDodge = false;
        _dodgeOnCooldown = false;
    }

    public void DoUpdate()
    {
        float alpha = (Time.time - _dodgeStart) / _dodgeDuration;
        _controller.Velocity = Vector2.Lerp(_dodgeVelocity, Vector2.zero, alpha);

        if (alpha >= 1f)
		{
            _dodgeEnded = true;
            //_collider.enabled = true;
            _dodgeCooldownStart = Time.time;
            _dodgeOnCooldown = true;
        }

        RaycastHit2D hit;
        hit = Physics2D.Raycast((Vector2)transform.position, Vector2.right * _dir, m_playerRadius, m_wallLayerMask);
        if (hit != false)
            _controller.Velocity = Vector2.zero;
    }

    public void DoExit()
    {
        _controller.HasInvincibilityFrame = false;
        _controller.SetCollisionsLayer(false);
    }

    

    public bool DodgeEnded { get => _dodgeEnded; set => _dodgeEnded = value; }
	public bool CanDodge { get => _canDodge; set => _canDodge = value; }

	#endregion

	#region Privates

	private bool _dodgeEnded;

    private float _dir;
    private float _dodgeDuration = 0.25f;
    private float _dodgeStart;

    private bool _canDodge;
    private bool _dodgeOnCooldown;
    private float _dodgeCooldownStart;

    private Vector2 _dodgeVelocity;

    private PlayerController _controller;

	#endregion
}