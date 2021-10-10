using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    #region Exposed

    [SerializeField] private PlayerAttributesScriptable m_playerCombatAttribute;

    [Header("Default Weapon")]
    [SerializeField] private GameObject m_defaultMeleeWeapon;
    [SerializeField] private GameObject m_defaultRangeWeapon;

    #endregion

    #region Unity API
    
    private void Awake()
    {
        _controller = GetComponent<PlayerController>();

        _meleeGameObject = Instantiate(m_defaultMeleeWeapon, transform);
        _rangeGameObject = Instantiate(m_defaultRangeWeapon, transform);

        _meleeWeapon = _meleeGameObject.GetComponent<AbstractMeleeWeapon>();
        _meleeWeapon.Controller = _controller;
        _rangeWeapon = _rangeGameObject.GetComponent<AbstractRangeWeapon>();
        _rangeWeapon.Controller = _controller;

        _enableNextAttack = true;
    }

	private void Start()
	{
        _hitBox = GetComponentInChildren<HitBoxController>();
	}

	#endregion

	#region Main Methods

	public void EquipWeapon(AbstractWeapon weapon)
	{
		switch (weapon.WeaponInfo.WeaponType)
		{
			case WeaponType.MELEE:
                _meleeWeapon = (AbstractMeleeWeapon) weapon;
                _meleeWeapon.Controller = _controller;
                break;
			case WeaponType.RANGE:
                _rangeWeapon = (AbstractRangeWeapon) weapon;
                _rangeWeapon.Controller = _controller;
                break;
			default:
				break;
		}
	}

    public void MeleeAttack()
	{
        _enableNextAttack = false;
        _attackEnded = false;
        _controller.Animator.SetTrigger("MeleeTrigger");
        _controller.Animator.SetInteger("WeaponID", _meleeWeapon.WeaponInfo.WeaponID);
        _meleeWeapon.ComboCounter++;
        _meleeWeapon.MeleeAttack();

        _hitBox.DamageAmount = m_playerCombatAttribute.DealDamage(_meleeWeapon);
	}

    public void RangeAttack()
    {
        _enableNextAttack = false;
        _attackEnded = false;
        _controller.Animator.SetTrigger("RangeTrigger");
        _controller.Animator.SetInteger("WeaponID", _rangeWeapon.WeaponInfo.WeaponID);
        _rangeWeapon.ComboCounter++;
        _rangeWeapon.ProjectileDamage = m_playerCombatAttribute.DealDamage(_rangeWeapon);
        _rangeWeapon.RangeAttack();
    }

    public void ResetComboCounter()
	{
        _meleeWeapon.ComboCounter = 0;
        _rangeWeapon.ComboCounter = 0;
    }

    public bool EnableNextAttack { get => _enableNextAttack; set => _enableNextAttack = value; }
	public bool AttackEnded { get => _attackEnded; set => _attackEnded = value; }

    public void EnablingNextAttack()
	{
        _enableNextAttack = true;
	}
    
    public void EndAttack()
	{
        _attackEnded = true;
	}

    public void Shoot()
	{
        _rangeWeapon.Shoot();
	}

    #endregion

    #region Privates

    private PlayerController _controller;

    private GameObject _meleeGameObject;
    private GameObject _rangeGameObject;
    private AbstractMeleeWeapon _meleeWeapon;
    private AbstractRangeWeapon _rangeWeapon;

    private bool _enableNextAttack;
    private bool _attackEnded;

    private HitBoxController _hitBox;

	#endregion
}
