using UnityEngine;

public enum JumpState
{
    JUMPUP,
    JUMPMAX,
    JUMPDOWN,
    HARDLANDING,
	SOFTLANDING,
}

public class PlayerJump : AbstractStateMachine, IState
{
    #region Exposed

    [SerializeField] private float m_jumpPower = 15f;
    [SerializeField] private float m_fallSpeed = 3f;
    [SerializeField] private float m_maxFallSpeed = -15f;
	[SerializeField] private float m_hardLandingVelocityThreshold = -14f;

	[SerializeField] private ParticleSystem m_jumpParticle;

    #endregion

    #region Unity API
    void Awake()
    {
        _controller = GetComponent<PlayerController>();
		_locomotion = GetComponent<PlayerLocomotion>();
		_currentJumpState = JumpState.JUMPUP;
    }

	private void OnGUI()
	{
		if (!m_isDebug) return;
		GUI.Button(new Rect(10, 45, 150, 25), _currentJumpState.ToString());
	}

	#endregion

	#region Main Methods

	protected override void OnStateEnter()
	{
		switch (_currentJumpState)
		{
			case JumpState.JUMPUP:
				_controller.Animator.SetTrigger("JumpTrigger");
				_controller.Animator.SetFloat("MoveY", m_jumpPower);
				break;
			case JumpState.JUMPMAX:
				break;
			case JumpState.JUMPDOWN:
				_controller.Animator.SetTrigger("FallTrigger");
				break;
			case JumpState.HARDLANDING:
				Landing();
				break;
			case JumpState.SOFTLANDING:
				Landing();
				Landed();
				break;
			default:
				break;
		}
	}

	protected override void OnStateUpdate()
	{
		switch (_currentJumpState)
		{
			case JumpState.JUMPUP:
				if (_hasJumped)
				{
					Fall();
					if (_controller.Velocity.y < 0.5f)
						TransitionToState(JumpState.JUMPMAX);
				}
				break;
			case JumpState.JUMPMAX:
				Fall();
				if (_controller.Velocity.y < 0f)
					TransitionToState(JumpState.JUMPDOWN);
				if (_controller.IsGrounded())
					TransitionToState(JumpState.SOFTLANDING);
				break;
			case JumpState.JUMPDOWN:
				Fall();
				if (_controller.IsGrounded())
				{
					_controller.Velocity = new Vector2(_controller.Velocity.x, 0f);

					if (_controller.Animator.GetFloat("MoveY") <= m_hardLandingVelocityThreshold)
						TransitionToState(JumpState.HARDLANDING);
					else
						TransitionToState(JumpState.SOFTLANDING);
				}
				break;
			case JumpState.HARDLANDING:
			case JumpState.SOFTLANDING:
				break;
			default:
				break;
		}
	}

	protected override void OnStateExit()
	{
		switch (_currentJumpState)
		{
			case JumpState.JUMPUP:
				break;
			case JumpState.JUMPMAX:
				break;
			case JumpState.JUMPDOWN:
				break;
			case JumpState.HARDLANDING:
			case JumpState.SOFTLANDING:
				break;
			default:
				break;
		}
	}

	protected void TransitionToState(JumpState newState)
	{
		OnStateExit();
		_currentJumpState = newState;
		OnStateEnter();
	}

	public void DoInit()
	{
		_hasJumped = false;
		_hasLanded = false;
		_controller.Animator.ResetTrigger("JumpTrigger");
		_controller.Animator.ResetTrigger("FallTrigger");
		_controller.Animator.ResetTrigger("LandTrigger");

		if (JumpPressed)
        {
            _currentJumpState = JumpState.JUMPUP;
            OnStateEnter();
        }
		else
		{
            _currentJumpState = JumpState.JUMPDOWN;
            OnStateEnter();
        }
    }

    public void DoUpdate()
    {		
		OnStateUpdate();

		if (_currentJumpState != JumpState.HARDLANDING)
			_locomotion.Locomotion();
	}

	public void DoExit()
	{
		_hasJumped = false;
		_hasLanded = false;
	}

    public void Jump()
	{
        Vector2 currentVelocity = _controller.Velocity;
        float jumpPower = m_jumpPower;

        currentVelocity = new Vector2(_controller.Velocity.x, jumpPower);

        _controller.Velocity = currentVelocity;
		_hasJumped = true;

		if (_controller.IsGrounded())
			m_jumpParticle.Play();
    }

	private void Landing()
	{
		_controller.Animator.SetTrigger("LandTrigger");
		_controller.Velocity = Vector2.zero;
		_controller.AdjustHeight(); 
		m_jumpParticle.Play();
	}
	public void Landed()
	{
		_hasLanded = true;
	}

    public bool JumpPressed { get => _jumpPressed; set => _jumpPressed = value; }

	public bool HasLanded { get => _hasLanded; }

	private void Fall()
	{
		_controller.Velocity += Vector2.up * m_fallSpeed * Physics2D.gravity.y * Time.deltaTime;
		if (_controller.Velocity.y < m_maxFallSpeed)
			_controller.Velocity = new Vector2(_controller.Velocity.x, m_maxFallSpeed);
		_controller.Animator.SetFloat("MoveY", _controller.Velocity.y);
	}

    #endregion

    #region Privates

    private bool _jumpPressed;

	private bool _hasJumped;
	private bool _hasLanded;

    private JumpState _currentJumpState;
    private PlayerController _controller;
	private PlayerLocomotion _locomotion;

	#endregion
}
