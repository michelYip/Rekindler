using UnityEngine;

public class PlayerController : AbstractController
{
    #region Exposed

    [SerializeField] private PlayerAttributesScriptable m_defaultAttribute;
    [SerializeField] private PlayerAttributesScriptable m_currentArribute;

	#endregion

	#region Unity API

	protected override void Awake()
	{
		base.Awake();
        m_currentArribute.InitializeAttribute(m_defaultAttribute);
        HasInvincibilityFrame = false;
	}

	private void Update()
	{
        UpdateGroundCheckRays();
        if (m_isDebug)
		{
            Debug.Log(IsGrounded());
		}
    }

	protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

	private void OnDrawGizmos()
	{
        if (!m_drawGizmos) return;
		else
		{
            Gizmos.color = Color.red;
            foreach (Ray2D ray in _groundCheckRays)
			{
                Gizmos.DrawLine(ray.origin, ray.origin + Vector2.down * m_groundCheckRaysDistance);

                //Gizmos.DrawRay(ray.origin, ray.direction);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("EnemyHitBox"))
        {
            if (!HasInvincibilityFrame)
			{
                int damageAmount = m_currentArribute.TakeDamage(collision.GetComponent<HitBoxController>().DamageAmount);

                DamageUI damageDisplay = Instantiate(DamagePrefab, transform.position, DamagePrefab.transform.rotation).GetComponent<DamageUI>();
                damageDisplay.SetDamageNumber(damageAmount);
                damageDisplay.SetColor(Color.red);

                StartCoroutine(ControllerHit());

                IsHit = true;
            }
        }
    }

    public void SetCollisionsLayer(bool ignore)
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerHurtBox"), LayerMask.NameToLayer("EnemyHurtBox"), ignore);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerHurtBox"), LayerMask.NameToLayer("Projectile"), ignore);
    }

    #endregion

    #region Main Methods

    public float GetHorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }
    public PlayerAttributesScriptable CurrentArribute { get => m_currentArribute; set => m_currentArribute = value; }
	public bool IsHit { get => _isHit; set => _isHit = value; }
	public bool HasInvincibilityFrame { get => _hasInvincibilityFrame; set => _hasInvincibilityFrame = value; }


	#endregion

	#region Privates

	private bool _isHit;
    private bool _hasInvincibilityFrame;

    #endregion
}
