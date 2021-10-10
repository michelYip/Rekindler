using UnityEngine;

public class PlayerAttack : Behaviour, IState
{
	#region Exposed
	#endregion

	#region Unity API
	void Awake()
    {
		_controller = GetComponent<PlayerController>();
		_combatManager = GetComponent<PlayerCombatManager>();
    }
	#endregion

	#region Main Methods

	public void DoInit()
	{
		_attackEnded = false;
		_attackBuffered = false;

		_controller.Velocity = Vector2.zero;

		_controller.Animator.SetTrigger("AttackTrigger");
		if (IsMeleeAttack)
			_combatManager.MeleeAttack();
		else
			_combatManager.RangeAttack();
	}

	public void DoUpdate()
	{
		if (IsMeleeAttack)
		{
			if (Input.GetButtonDown("MeleeAttack")) _attackBuffered = true;
			if (_attackBuffered && _combatManager.EnableNextAttack)
			{
				_combatManager.MeleeAttack();
				_attackBuffered = false;
			}
		}
		else
		{
			if (Input.GetButtonDown("RangeAttack")) _attackBuffered = true;
			if (_attackBuffered && _combatManager.EnableNextAttack)
			{
				_combatManager.RangeAttack();
				_attackBuffered = false;
			}
		}
		_attackEnded = _combatManager.AttackEnded;
	}

	public void DoExit()
	{
		_controller.Animator.ResetTrigger("AttackTrigger");
		_controller.Animator.ResetTrigger("MeleeTrigger");
		_controller.Animator.ResetTrigger("RangeTrigger");

		_combatManager.ResetComboCounter();

		_controller.Velocity = Vector2.zero;
	}

	public bool IsMeleeAttack { get => _isMeleeAttack; set => _isMeleeAttack = value; }
	public bool AttackEnded { get => _attackEnded; set => _attackEnded = value; }

	#endregion

	#region Privates

	private bool _isMeleeAttack;
	private bool _attackEnded;

	private bool _attackBuffered;

	private PlayerController _controller;
	private PlayerCombatManager _combatManager;

	#endregion
}
