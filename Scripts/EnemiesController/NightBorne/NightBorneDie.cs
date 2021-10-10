using UnityEngine;

public class NightBorneDie : Behaviour, IState
{
    #region Exposed
    #endregion

    #region Unity API
    void Awake()
    {
        _controller = GetComponent<NightBorneController>();
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

    private NightBorneController _controller;

    #endregion
}