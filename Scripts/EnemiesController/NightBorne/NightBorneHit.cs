using UnityEngine;

public class NightBorneHit : Behaviour, IState
{
    #region Exposed
    #endregion

    #region Unity API
    void Awake()
    {
        _controller = GetComponent<NightBorneController>();
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

    private NightBorneController _controller;

    private bool _hitEnded;

	#endregion
}
