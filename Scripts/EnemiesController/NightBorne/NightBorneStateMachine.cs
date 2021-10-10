using UnityEngine;

public class NightBorneStateMachine : AbstractStateMachine
{
	#region Exposed
	#endregion


	#region Unity API
	void Awake()
    {
		_controller = GetComponent<NightBorneController>();
		_spawn = GetComponent<NightBorneSpawn>();
		_patrol = GetComponent<NightBornePatrol>();
		_chase = GetComponent<NightBorneChase>();
		_attack = GetComponent<NightBorneAttack>();
		_hit = GetComponent<NightBorneHit>();
		_die = GetComponent<NightBorneDie>();

		_currentState = State.SPAWN;
    }

	private void OnEnable()
	{
		if (_currentState != State.SPAWN)
			TransitionToState(State.SPAWN);
	}

	private void OnGUI()
	{
		if (!m_isDebug) return;
		GUI.Button(new Rect(10,100,150,25), _currentState.ToString());
	}

	#endregion


	#region Main Methods

	protected override void OnStateEnter()
	{
		switch (_currentState)
		{
			case State.SPAWN:
				_spawn.DoInit();
				break;
			case State.PATROL:
				_patrol.DoInit();
				break;
			case State.CHASE:
				_chase.DoInit();
				break;
			case State.ATTACK:
				_attack.DoInit();
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

	protected override void OnStateExit()
	{
		switch (_currentState)
		{
			case State.SPAWN:
				_spawn.DoExit();
				break;
			case State.PATROL:
				_patrol.DoExit();
				break;
			case State.CHASE:
				_chase.DoExit();
				break;
			case State.ATTACK:
				_attack.DoExit();
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

	protected override void OnStateUpdate()
	{
		switch (_currentState)
		{
			case State.SPAWN:
				_spawn.DoUpdate();
				if (_spawn.HasSpawned)
					TransitionToState(State.PATROL);
				break;

			case State.PATROL:
				_patrol.DoUpdate();
				if (_controller.LookForPlayer() != null)
					TransitionToState(State.CHASE);
				if (_controller.IsHit)
				{
					_controller.IsHit = false;
					TransitionToState(State.HIT);
				}
				break;

			case State.CHASE:
				_chase.DoUpdate();
				if (_controller.LookForPlayer() == null)
					TransitionToState(State.PATROL);
				if (_chase.CanAttack())
					TransitionToState(State.ATTACK);
				if (_controller.IsHit)
				{
					_controller.IsHit = false;
					TransitionToState(State.HIT);
				}
				break;

			case State.ATTACK:
				_attack.DoUpdate();
				if (_attack.AttackEnded)
					TransitionToState(State.PATROL);
				break;

			case State.HIT:
				_hit.DoUpdate();
				if (_hit.HitEnded)
					TransitionToState(State.PATROL);
				break;

			case State.DIE:
				_die.DoUpdate();
				break;
			default:
				break;
		}

		if (_controller.CurrentHealth <= 0)
			TransitionToState(State.DIE);
		
	}

	#endregion


	#region Privates

	private NightBorneController _controller;
	private NightBorneSpawn _spawn;
	private NightBornePatrol _patrol;
	private NightBorneChase _chase;
	private NightBorneAttack _attack;
	private NightBorneHit _hit;
	private NightBorneDie _die;

	#endregion
}
