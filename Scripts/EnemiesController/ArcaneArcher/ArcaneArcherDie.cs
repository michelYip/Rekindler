using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneArcherDie : MonoBehaviour, IState
{
    #region Exposed
    #endregion

    #region Unity API
    void Awake()
    {
        _controller = GetComponent<ArcaneArcherController>();
    }
    #endregion

    #region Main Methods

    public void DoExit()
    {
    }

    public void DoInit()
    {
        _controller.Animator.SetTrigger("DieTrigger");
        _controller.Velocity = Vector2.zero;
    }

    public void DoUpdate()
    {
    }

    #endregion

    #region Privates

    private ArcaneArcherController _controller;

	#endregion
}
