using UnityEngine;

[CreateAssetMenu(menuName ="PlayerAttribute/Attibute")]
public class PlayerAttributesScriptable : ScriptableObject
{
    #region Exposed

    [SerializeField] private int m_baseHealth;
	[SerializeField] private int m_baseHealthFlatModifier;
	[SerializeField] private float m_baseDamageModifier;
    [SerializeField] private float m_meleeDamageModifier;
    [SerializeField] private float m_rangeDamageModifier;
    [SerializeField] private float m_damageReductionModifier;

	//   [SerializeField] private float m_movementSpeedModifier;

	#endregion

	#region Unity API
	#endregion

	#region Main Methods

	public void InitializeAttribute(PlayerAttributesScriptable other)
	{
		BaseHealth = other.BaseHealth;
		BaseHealthFlatModifier = other.BaseHealthFlatModifier;
		TotalHealth = other.TotalHealth;
		BaseDamageModifier = other.BaseDamageModifier;
		MeleeDamageModifier = other.MeleeDamageModifier;
		RangeDamageModifier = other.RangeDamageModifier;
		DamageReductionModifier = other.DamageReductionModifier;

		TotalHealth = BaseHealth + BaseHealthFlatModifier;
		CurrentHealth = TotalHealth;
	}

	public int TakeDamage(int damageReceived)
	{
		int totalDamage = Mathf.RoundToInt(damageReceived * (2 - DamageReductionModifier));
		CurrentHealth -= totalDamage;
		return totalDamage;
	}

	public int DealDamage(AbstractWeapon weapon)
	{
		if (weapon.WeaponInfo.WeaponType == WeaponType.MELEE)
			return Mathf.RoundToInt(BaseDamageModifier * MeleeDamageModifier * (weapon.WeaponInfo.MeleeWeaponDamage[weapon.ComboCounter - 1]));
		else if (weapon.WeaponInfo.WeaponType == WeaponType.RANGE)
			return Mathf.RoundToInt(BaseDamageModifier * RangeDamageModifier * (weapon.WeaponInfo.RangeWeaponDamage[weapon.ComboCounter - 1]));
		else
		{
			Debug.Log("Cannot calculate damage, weapon type undefined...");
			Debug.Break();
			return 0;
		}
	}

	// GETTER & SETTER

	public int BaseHealth { get => m_baseHealth; set => m_baseHealth = value; }

	public int BaseHealthFlatModifier { get => m_baseHealthFlatModifier; set => m_baseHealthFlatModifier = value; }

	public float BaseDamageModifier { get => m_baseDamageModifier; set => m_baseDamageModifier = value; }
	public float MeleeDamageModifier { get => m_meleeDamageModifier; set => m_meleeDamageModifier = value; }
	public float RangeDamageModifier { get => m_rangeDamageModifier; set => m_rangeDamageModifier = value; }
	public float DamageReductionModifier { get => m_damageReductionModifier; set => m_damageReductionModifier = value; }



	public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
	public int TotalHealth { get => _totalHealth; set => _totalHealth = value; }

	#endregion

	#region Privates

	private int _currentHealth;
	private int _totalHealth;

	#endregion
}
