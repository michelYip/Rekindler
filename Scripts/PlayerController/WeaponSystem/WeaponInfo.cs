using UnityEngine;

public enum WeaponType
{
	MELEE,
	RANGE,
	//HYBRID,
}

[CreateAssetMenu(menuName = "PlayerAttribute/Weapon")]
public class WeaponInfo : ScriptableObject
{
	#region Exposed

	[SerializeField] private int _weaponID;
	[SerializeField] private string _weaponName;
	[SerializeField] private WeaponType   _weaponType;
	[SerializeField] private float[] _meleeWeaponDamage;
	[SerializeField] private float[] _rangeWeaponDamage;
	[SerializeField] private string _flavorText;

	#endregion

	#region Unity API
	#endregion

	#region Main Methods

	public void SetWeaponInfo(WeaponInfo b)
	{
		WeaponID = b.WeaponID;
		WeaponName = b.WeaponName;
		WeaponType = b.WeaponType;
		MeleeWeaponDamage = b.MeleeWeaponDamage;
		RangeWeaponDamage = b.RangeWeaponDamage;
		FlavorText = b.FlavorText;
	}

	public int WeaponID { get => _weaponID; set => _weaponID = value; }
	public string WeaponName { get => _weaponName; set => _weaponName = value; }
	public WeaponType WeaponType { get => _weaponType; set => _weaponType = value; }
	public float[] MeleeWeaponDamage { get => _meleeWeaponDamage; set => _meleeWeaponDamage = value; }
	public float[] RangeWeaponDamage { get => _rangeWeaponDamage; set => _rangeWeaponDamage = value; }
	public string FlavorText { get => _flavorText; set => _flavorText = value; }

	#endregion

	#region Privates
	#endregion
}
