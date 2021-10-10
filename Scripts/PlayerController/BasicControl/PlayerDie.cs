using UnityEngine;
using ScriptableObjectArchitecture;

public class PlayerDie : Behaviour, IState
{
    #region Exposed

    [SerializeField] private GameEvent m_playerDeath;

    #endregion

    #region Unity API
    void Awake()
    {
        _controller = GetComponent<PlayerController>();
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
        m_playerDeath.Raise();
    }

    public void DoUpdate()
    {
    }

    #endregion

    #region Privates

    private PlayerController _controller;

    #endregion
}
