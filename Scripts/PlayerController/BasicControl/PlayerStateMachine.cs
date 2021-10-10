using UnityEngine;

public class PlayerStateMachine : AbstractStateMachine
{
	#region Exposed
	#endregion

	#region Unity API

	private void Awake()
	{
		_controller = GetComponent<PlayerController>();
		_locomotion = GetComponent<PlayerLocomotion>();
		_jump = GetComponent<PlayerJump>();
		_dodge = GetComponent<PlayerDodge>();
		_attack = GetComponent<PlayerAttack>();
		_ability = GetComponent<PlayerAbility>();
		_die = GetComponent<PlayerDie>();
		_hit = GetComponent<PlayerHit>();
	}

	private void OnGUI()
	{
		if (!m_isDebug) return;
		GUI.Button(new Rect(10, 500, 150, 25), _currentState.ToString());
	}

	#endregion

	#region Main Methods

	protected override void OnStateEnter()
	{
		switch (_currentState)
		{
			case State.LOCOMOTION:
				_locomotion.DoInit();
				break;
			case State.JUMP:
				_jump.DoInit();
				break;
			case State.DODGE:
				_dodge.DoInit();
				break;
			case State.ATTACK:
				_attack.DoInit();
				break;
			case State.ABILITY:
				_ability.DoInit();
				break;
			case State.HIT:
				_hit.DoInit();
				break;
			case State.DIE:
				_die.DoInit();
				break;
			default:
				break;
		}
	}

    protected override void OnStateUpdate()
    {
		switch (_currentState)
		{
			case State.LOCOMOTION:
				_locomotion.DoUpdate();
				
				if (_controller.IsGrounded())
				{
					if (Input.GetButtonDown("Jump"))
					{
						if (_controller.IsOnPlatform() && Input.GetAxisRaw("Vertical") < 0f)
						{
							_controller.StartCoroutine(_controller.DisablePlayformLayer());
						}
						else
						{
							_jump.JumpPressed = true;
							TransitionToState(State.JUMP);
						}
					}

					if (Input.GetButtonDown("Dodge") && _dodge.CanDodge)
						TransitionToState(State.DODGE);
					else if (Input.GetButtonDown("MeleeAttack"))
					{
						_attack.IsMeleeAttack = true;
						TransitionToState(State.ATTACK);
					}
					else if (Input.GetButtonDown("RangeAttack"))
					{
						_attack.IsMeleeAttack = false;
						TransitionToState(State.ATTACK);
					}
					else if (Input.GetButtonDown("Bash") && _ability.HasLearned(0) && _ability.Bash.CheckBashable())
					{
						_ability.AbilityID = 0;
						TransitionToState(State.ABILITY);
					}
				}
				else
				{
					_jump.JumpPressed = false;
					TransitionToState(State.JUMP);
				}
				break;

			case State.JUMP:
				_jump.DoUpdate();
				if (Input.GetButtonDown("Dodge") && _dodge.CanDodge)
					TransitionToState(State.DODGE);
				else if (Input.GetButtonDown("MeleeAttack"))
				{
					_attack.IsMeleeAttack = true;
					TransitionToState(State.ATTACK);
				}
				else if (Input.GetButtonDown("RangeAttack"))
				{
					_attack.IsMeleeAttack = false;
					TransitionToState(State.ATTACK);
				}
				else if (Input.GetButtonDown("Bash") && _ability.HasLearned(0) && _ability.Bash.CheckBashable())
				{
					_ability.AbilityID = 0;
					TransitionToState(State.ABILITY);
				}

				if (_jump.HasLanded)
					TransitionToState(State.LOCOMOTION);
				break;

			case State.DODGE:
				_dodge.DoUpdate();
				if (_dodge.DodgeEnded)
					TransitionToState(State.LOCOMOTION);
				break;

			case State.ATTACK:
				_attack.DoUpdate();
				if (_attack.AttackEnded)
				{
					TransitionToState(State.LOCOMOTION);
				}
				break;


			case State.ABILITY:
				_ability.DoUpdate();
				if (_ability.AbilityEnded)
					TransitionToState(State.LOCOMOTION);
				break;

			case State.HIT:
				_hit.DoUpdate();
				if (_hit.HitEnded)
					TransitionToState(State.LOCOMOTION);
				break;

			case State.DIE:
				_die.DoUpdate();
				break;

			default:
				break;
		}

		if (_controller.CurrentArribute.CurrentHealth <= 0)
			TransitionToState(State.DIE);
		if (_controller.IsHit)
		{
			_controller.IsHit = false;
			TransitionToState(State.HIT);
		}
	}

    protected override void OnStateExit()
    {
		switch (_currentState)
		{
			case State.LOCOMOTION:
				_locomotion.DoExit();
				break;
			case State.JUMP:
				_jump.DoExit();
				break;
			case State.DODGE:
				_dodge.DoExit();
				break;
			case State.ATTACK:
				_attack.DoExit();
				break;
			case State.ABILITY:
				_ability.DoExit();
				break;
			case State.HIT:
				_hit.DoExit();
				break;
			case State.DIE:
				_die.DoExit();
				break;
			default:
				break;
		}
	}

	#endregion

	#region Privates

	private PlayerController _controller;
	private PlayerLocomotion _locomotion;
	private PlayerJump _jump;
	private PlayerDodge _dodge;
	private PlayerAttack _attack;
	private PlayerAbility _ability;
	private PlayerDie _die;
	private PlayerHit _hit;

    #endregion
}
