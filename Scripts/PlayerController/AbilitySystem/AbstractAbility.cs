using UnityEngine;

public abstract class AbstractAbility : Behaviour
{
	#region Exposed

	[SerializeField] private AbilityScriptable m_abilityInfo;


    #endregion

    #region Unity API
    protected virtual void Awake()
    {
        _abilityState = GetComponent<PlayerAbility>();
        _controller = GetComponent<PlayerController>();
    }
    #endregion

    #region Main Methods

    public virtual bool UseAbility() 
    {
        return false;
    }

    protected void AbilityEnd()
	{
        _abilityState.AbilityEnded = true;
	}

	public AbilityScriptable AbilityInfo { get => m_abilityInfo; set => m_abilityInfo = value; }

    #endregion

    #region Privates

    protected PlayerAbility _abilityState;
    protected PlayerController _controller;

    #endregion
}
