using UnityEngine;

public class NightBorneController : EnemyController
{
    #region Exposed

    [Header("Player Detection")]
    [SerializeField] private float m_visionRadius = 5f;
    [SerializeField] private float m_fallSpeed = 3f;
    [SerializeField] private LayerMask m_playerLayerMask;

    [Header("Environment Detection")]
    [SerializeField] private float m_forwardRayOffset = 0.5f;
    [SerializeField] private float m_forwardRayLength = 3f;

    #endregion

    #region Unity API
    protected override void Awake()
    {
        base.Awake();
        _target = null;
    }

	protected override void OnEnable()
	{
		base.OnEnable();
        Animator.SetTrigger("TeleportTrigger");
	}

	protected override void Update()
    {
        base.Update();
        UpdateGroundCheckRays();

        if (!IsGrounded())
            Velocity += Vector2.up * m_fallSpeed * Physics2D.gravity * Time.deltaTime;
		else
		{
            AdjustHeight();
            Velocity *= Vector2.right;
		}
    }

	private void OnDrawGizmosSelected()
	{
        if (!m_drawGizmos) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_visionRadius);
        foreach (Ray2D ray in _groundCheckRays)
        {
            Gizmos.DrawLine(ray.origin, ray.origin + Vector2.down * m_groundCheckRaysDistance);
            //Gizmos.DrawRay(ray.origin, ray.direction);
        }
    }

    #endregion

    #region Main Methods

    public Collider2D LookForPlayer()
	{
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, m_visionRadius, m_playerLayerMask);
        if (playerCollider != null)
		{
            LayerMask anyLayer = FloorLayerMask | m_playerLayerMask;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, playerCollider.transform.position - transform.position, Mathf.Infinity, anyLayer);
            if (hit != false && hit.collider == playerCollider)
			{
                //Debug.Log("Player found");
                _target = playerCollider.transform;
                return playerCollider;
			}
			else
			{
                //Debug.Log("Can't see Player within radius");
                _target = null;
                return null;
			}
        }
		else
		{
            //Debug.Log("Looking for player");
            _target = null;
            return null;
		}
	}

    public bool CanWalkForward()
    {
        RaycastHit2D hitWall0;
        RaycastHit2D hitWall1;
        bool isHittingWall;
        RaycastHit2D hitGround;
        bool isHittingGround;
        hitWall0 = Physics2D.Raycast(_forwardCheckRay[0].origin, _forwardCheckRay[0].direction, m_forwardRayLength, FloorLayerMask);
        hitWall1 = Physics2D.Raycast(_forwardCheckRay[1].origin, _forwardCheckRay[1].direction, m_forwardRayLength, FloorLayerMask);
        hitGround = Physics2D.Raycast(_forwardCheckRay[2].origin, _forwardCheckRay[2].direction, m_forwardRayLength, FloorLayerMask);

        isHittingWall = hitWall0 || hitWall1;
        isHittingGround = hitGround;

        return !isHittingWall && isHittingGround;
    }

    public void UpdateForwardCheckRay()
    {
        Vector2 origin = (Vector2)transform.position + Vector2.right * transform.localScale.x * m_forwardRayOffset;
        _forwardCheckRay[0] = new Ray2D(origin, Vector2.right * transform.localScale.x + Vector2.up);
        _forwardCheckRay[1] = new Ray2D(origin, Vector2.right * transform.localScale.x);
        _forwardCheckRay[2] = new Ray2D(origin, Vector2.right * transform.localScale.x - Vector2.up);
    }


    public LayerMask PlayerLayerMask { get => m_playerLayerMask; set => m_playerLayerMask = value; }
	public Transform Target { get => _target; set => _target = value; }
	public Ray2D[] ForwardCheckRay { get => _forwardCheckRay; set => _forwardCheckRay = value; }
	public float ForwardRayOffset { get => m_forwardRayOffset; set => m_forwardRayOffset = value; }
	public float ForwardRayLength { get => m_forwardRayLength; set => m_forwardRayLength = value; }

	#endregion

	#region Privates

	private Transform _target;
	private Ray2D[] _forwardCheckRay = new Ray2D[3]; // 0 : Forward up, 1 : Forward, 2 : Forward down

	#endregion
}