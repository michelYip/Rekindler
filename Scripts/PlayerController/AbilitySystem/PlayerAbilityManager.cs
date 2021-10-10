using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : Behaviour
{
    #region Exposed

    #endregion

    #region Unity API
    void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _abilitiesDictionary = new Dictionary<int, AbstractAbility>();

        AbstractAbility[] _abilityList = GetComponents<AbstractAbility>();
        
        foreach(AbstractAbility ability in _abilityList)
		{
            ability.AbilityInfo.CurrentLevel = 0;
            AddAbilities(ability);
		}
    }

    void Update()
    {
        
    }

	private void OnGUI()
	{
        if (!m_isDebug) return;
		if (GUI.Button(new Rect(10,10,150,25), "List abilities"))
		{
            foreach(int key in _abilitiesDictionary.Keys)
			{
                Debug.Log(_abilitiesDictionary[key]);
			}
		}

        foreach(AbstractAbility ability in _abilitiesDictionary.Values)
		{
            if (GUI.Button(new Rect(10, 35 + (ability.AbilityInfo.AbilityID + 1)*25, 150, 25), "Upgrade " + ability.AbilityInfo.AbilityName[0]))
            {
                ability.AbilityInfo.CurrentLevel++;
                if (ability.AbilityInfo.CurrentLevel >= 3) ability.AbilityInfo.CurrentLevel = 3;
            }
        }
    }

	#endregion

	#region Main Methods

    public bool UseAbility(int id)
	{
        if (_abilitiesDictionary[id].AbilityInfo.CurrentLevel == 0)
        {
            if (m_isDebug)
                Debug.Log(_abilitiesDictionary[id].AbilityInfo.AbilityName[0] + " is not learned yet");
            return true;
        }
        else
        {
            return _abilitiesDictionary[id].UseAbility();
        }
	}

	private void AddAbilities(AbstractAbility ability)
	{
        int id = ability.AbilityInfo.AbilityID;
        _abilitiesDictionary.Add(id, gameObject.GetComponent(ability.AbilityInfo.AbilityScript) as AbstractAbility);
    }
	public Dictionary<int, AbstractAbility> AbilitiesDictionary { get => _abilitiesDictionary; set => _abilitiesDictionary = value; }

    #endregion

    #region Privates

    private PlayerController _controller;
    private Dictionary<int, AbstractAbility> _abilitiesDictionary;


	#endregion
}
