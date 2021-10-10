using UnityEngine;

public class PlayerAbility : Behaviour, IState
{
	#region Exposed
	#endregion

	#region Unity API
	void Awake()
    {
		_manager = GetComponent<PlayerAbilityManager>();
		_bash = GetComponent<PlayerBash>();
    }

    void Update()
    {
        
    }
	#endregion

	#region Main Methods

	public void DoExit()
	{
		_abilityID = -1;
	}

	public void DoInit()
	{
		if (_abilityID == -1)
		{
			Debug.Log("Unknown ability ID");
			Debug.Break();
		}

		_abilityEnded = _manager.UseAbility(_abilityID);
	}

	public void DoUpdate()
	{
	}

	public bool HasLearned(int abilityID)
	{
		bool result = _manager.AbilitiesDictionary[abilityID].AbilityInfo.CurrentLevel != 0;
		if (!result)
			Debug.Log(_manager.AbilitiesDictionary[abilityID].AbilityInfo.AbilityName[0] + " is not learned yet");
		return result;
	}

	public int AbilityID { get => _abilityID; set => _abilityID = value; }
	public bool AbilityEnded { get => _abilityEnded; set => _abilityEnded = value; }
	public PlayerBash Bash { get => _bash; set => _bash = value; }
	#endregion

	#region Privates

	private int _abilityID;
	private bool _abilityEnded;

	private PlayerAbilityManager _manager;

	private PlayerBash _bash;

	#endregion
}
