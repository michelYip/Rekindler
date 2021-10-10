using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemyController : AbstractController
{
    #region Exposed
    
    [Header("Enemy Base Attributes")]
    [SerializeField] protected EnemyScriptable _defaultAttributes;
    [SerializeField] private ParticleSystem m_hitVFX;

    #endregion

    #region Unity API
    protected override void Awake()
    {
        base.Awake();
        _healthBar = transform.Find("EnemyUI/HealthBar").GetComponent<Image>();
        _collider = GetComponent<Collider2D>();
        _isDisabled = true;
    }

    protected virtual void Start()
	{
        _affectedRoom = transform.parent.parent.GetComponent<RoomManager>();
	}

    protected override void OnEnable()
	{
        base.OnEnable();
        _currentHealth = _defaultAttributes.Health;
        _currentDamage = _defaultAttributes.Damage;

        _collider.enabled = true;
        _healthBar.fillAmount = 1;
        _isHit = false;
        _isDisabled = false;
    }

    protected override void DisableEntity()
	{
        _isDisabled = true;
        _affectedRoom.EnemiesPool.Enqueue(gameObject);
        _affectedRoom.CheckRoomState();
        base.DisableEntity();
	}

    protected virtual void Update()
	{
        _healthBar.transform.localScale = (transform.localScale.x < 0f) ? new Vector2(-1, 1) : new Vector2(1,1);
	}

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("EnemyHurtBox") && collision.CompareTag("PlayerHitBox"))
        {
            int damageAmount = collision.GetComponent<HitBoxController>().DamageAmount;

            CurrentHealth -= damageAmount;
            
            DamageUI damageDisplay = Instantiate(DamagePrefab, transform.position, DamagePrefab.transform.rotation).GetComponent<DamageUI>();
            damageDisplay.SetDamageNumber(damageAmount);
            damageDisplay.SetColor(Color.yellow);

            _healthBar.fillAmount = (float)CurrentHealth / _defaultAttributes.Health;

            StartCoroutine(ControllerHit());

            IsHit = true;

            m_hitVFX.Play();          
        }
    }

    #endregion

    #region Main Methods

    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public int CurrentDamage { get => _currentDamage; set => _currentDamage = value; }
	public bool IsHit { get => _isHit; set => _isHit = value; }
	public bool IsDisabled { get => _isDisabled; set => _isDisabled = value; }

	#endregion

	#region Privates

	protected int _currentHealth;
	protected int _currentDamage;

    protected Image _healthBar;

	private bool _isHit;
    private bool _isDisabled;
    private Collider2D _collider;

    private RoomManager _affectedRoom;

	#endregion
}