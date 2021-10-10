using UnityEngine;

public enum State
{
	LOCOMOTION,
	JUMP,
	DODGE,
	ATTACK,
	ABILITY,
	HIT,
	DIE,

	SPAWN,
	PATROL,
	CHASE,
}

public abstract class AbstractStateMachine : Behaviour
{
	#region Exposed
	#endregion

	#region Unity API

	private void Update()
	{
		OnStateUpdate();
	}

	#endregion

	#region Main Methods

	protected abstract void OnStateEnter();
	protected abstract void OnStateUpdate();
	protected abstract void OnStateExit();

	public State CurrentState { get => _currentState; set => _currentState = value; }

	protected void TransitionToState(State newState)
	{
		OnStateExit();
		_currentState = newState;
		OnStateEnter();
	}

	#endregion

	#region Privates

	protected State _currentState;

	#endregion
}
