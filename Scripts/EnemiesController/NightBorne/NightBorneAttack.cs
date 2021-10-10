using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightBorneAttack : Behaviour, IState
{
    #region Exposed
    #endregion

    #region Unity API
    private void Awake()
    {
        _controller = GetComponent<NightBorneController>();
    }

    private void Start()
	{
        _hitbox = GetComponentInChildren<HitBoxController>();
        _hitbox.DamageAmount = _controller.CurrentDamage;
    }

    #endregion

    #region Main Methods

    public void DoExit()
    {
    }

    public void DoInit()
    {
        _controller.Velocity = Vector2.zero;
        _controller.Animator.SetTrigger("AttackTrigger");
        _attackEnded = false;
    }

    public void DoUpdate()
    {
    }

    public void AttackEnd()
	{
        _attackEnded = true;
	}
	public bool AttackEnded { get => _attackEnded; set => _attackEnded = value; }

    #endregion

    #region Privates

    private NightBorneController _controller;

    private bool _attackEnded;

    private HitBoxController _hitbox;

	#endregion
}
