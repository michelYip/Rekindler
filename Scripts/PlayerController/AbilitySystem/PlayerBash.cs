using UnityEngine;

public class PlayerBash : AbstractAbility
{
    #region Exposed

    [SerializeField] private LayerMask m_bashLayerMask;
    [SerializeField] private float m_bashRadius = 1.5f;
    [SerializeField] private float m_bashSpeed = 6f;

    [SerializeField] private float m_bashDuration = 0.6f;

    [SerializeField] private ParticleSystem m_bashVFX;

    #endregion

    #region Unity API
    protected override void Awake()
    {
        base.Awake();
        GameObject arrow = Resources.Load("UI/BashArrowIndicator") as GameObject;
        _arrowIndicator = Instantiate(arrow);
        _arrowIndicator.SetActive(false);
        _isBashing = false;
    }

    void Update()
    {
        if (_bashStart == true)
		{
            _controller.Velocity = Vector2.zero;
            if (Input.GetButtonUp("Bash"))
			{
                _bashStart = false;
                _isBashing = true;
                _bashDirection = _arrowIndicator.transform.rotation * Vector2.right;
                _controller.Animator.SetBool("IsHoldingBash", false);
                _arrowIndicator.SetActive(false);
                Time.timeScale = 1f;
                _bashDirection = _bashDirection.normalized * m_bashSpeed;
                _bashTimer = Time.time;

                transform.position = _target.transform.position;
                m_bashVFX.Play();

                _controller.HasInvincibilityFrame = true;
                _controller.SetCollisionsLayer(true);
            }
		}
        if (_isBashing)
		{
            float alpha = (Time.time - _bashTimer) / m_bashDuration;
            _controller.Velocity = Vector2.Lerp(_bashDirection, _bashDirection * 0.5f, alpha);
            Vector2 dir = _controller.Velocity.normalized;
            _controller.Animator.SetFloat("MoveX", Mathf.Abs(dir.x));
            _controller.Animator.SetFloat("MoveY", Mathf.Abs(dir.y));

            transform.localScale = new Vector3((_controller.Velocity.x > 0) ? 1 : -1, (_controller.Velocity.y > 0) ? 1 : -1, 1);


            if (_controller.IsHit)
            {
                _isBashing = false;
                _controller.Velocity = Vector2.zero;
            }
        }

    }

	private void OnDrawGizmos()
	{
        if (!m_drawGizmos) return;
        else
		{
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, m_bashRadius);
		}
	}

	#endregion

	#region Main Methods

	public override bool UseAbility()
    {
        if (!CheckBashable())
            return true;
        else
        {
            Debug.Log("Use Bash");
            _bashStart = true;
            _isBashing = false;
            _controller.Animator.SetBool("IsHoldingBash", true);

            _arrowIndicator.SetActive(true);
            _target = GetClosestCollider();
            _arrowIndicator.transform.position = _target.transform.position;

            Time.timeScale = 0f;
            return false;
        }
    }

    public void BashEnded()
	{
        transform.localScale = Vector3.one;
        _bashStart = false;
        _isBashing = false;
        AbilityEnd();

        _controller.HasInvincibilityFrame = false;
        _controller.SetCollisionsLayer(false);
    }

	public bool CheckBashable()
	{
        _bashable = Physics2D.OverlapCircleAll(transform.position, m_bashRadius, m_bashLayerMask);
        return (_bashable.Length != 0);
    }

    private Collider2D GetClosestCollider()
	{
        if (_bashable.Length == 0) return null;
        else
		{
            Collider2D result = null;
            float minDist = Mathf.Infinity;
            foreach(Collider2D collider in _bashable)
			{
                if (minDist > Vector2.Distance(transform.position, collider.transform.position))
				{
                    result = collider;
                    minDist = Vector2.Distance(transform.position, collider.transform.position);
                }
			}
            return result;
		}
	}

	#endregion

	#region Privates

	private Collider2D[] _bashable;
    private Collider2D _target;

    private bool _bashStart;
    private bool _isBashing;

    private GameObject _arrowIndicator;
    private Vector2 _bashDirection;
    private float _bashTimer;

    #endregion
}
