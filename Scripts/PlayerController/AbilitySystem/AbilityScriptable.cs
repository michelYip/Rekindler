using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerAttribute/Ability")]
public class AbilityScriptable : ScriptableObject
{
    #region Exposed

    [SerializeField] private int m_abilityID;
    [SerializeField] private string m_abilityScriptName;
    [SerializeField] private List<string> m_abilityName = new List<string>(3);
    [SerializeField] private List<string> m_abilityDescription = new List<string>(3);
    [SerializeField] private string m_flavourText;
    [SerializeField] private Sprite m_abilityIcon;

    #endregion

    #region Unity API
    #endregion

    #region Main Methods
	public int AbilityID { get => m_abilityID; set => m_abilityID = value; }
	public string AbilityScript { get => m_abilityScriptName; set => m_abilityScriptName = value; }
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
	public List<string> AbilityName { get => m_abilityName; set => m_abilityName = value; }
	public List<string> AbilityDescription { get => m_abilityDescription; set => m_abilityDescription = value; }
	public Sprite AbilityIcon { get => m_abilityIcon; set => m_abilityIcon = value; }

	#endregion

	#region Privates

	private int _currentLevel;

	#endregion
}
