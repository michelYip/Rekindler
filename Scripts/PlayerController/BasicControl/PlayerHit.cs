using UnityEngine;
using ScriptableObjectArchitecture;

public class PlayerHit : Behaviour, IState
{
    #region Exposed
    [SerializeField] private GameEvent m_playerHitGameEvent;
    #endregion

    #region Unity API
    void Awake()
    {
        _controller = GetComponent<PlayerController>();
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
        m_playerHitGameEvent.Raise();

        transform.localScale = Vector3.one;
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

    private PlayerController _controller;

    private bool _hitEnded;

    #endregion
}