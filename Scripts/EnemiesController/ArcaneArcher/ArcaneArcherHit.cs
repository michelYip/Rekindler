using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneArcherHit : MonoBehaviour, IState
{
    #region Exposed
    #endregion

    #region Unity API
    void Awake()
    {
        _controller = GetComponent<ArcaneArcherController>();
        _hitEnded = false;
    }
    #endregion

    #region Main Methods

    public void DoExit()
    {
    }

    public void DoInit()
    {
        _hitEnded = false;
        _controller.Velocity = Vector2.zero;
        _controller.Animator.SetTrigger("HitTrigger");
    }

    public void DoUpdate()
    {
    }

    public void HitEnd()
    {
        _hitEnded = true;
    }

    public bool HitEnded { get => _hitEnded; set => _hitEnded = value; }

    #endregion

    #region Privates

    private ArcaneArcherController _controller;

    private bool _hitEnded;

    #endregion
}
