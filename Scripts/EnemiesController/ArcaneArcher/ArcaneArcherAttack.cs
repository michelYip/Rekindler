using UnityEngine;

public class ArcaneArcherAttack : MonoBehaviour, IState
{
    #region Exposed

    [SerializeField] private GameObject m_arrowPrefab;
    [SerializeField] private float m_arrowSpeed = 8f;
    [SerializeField] private float m_arrowLifeTime = 3f;

    [SerializeField] private float m_attackCooldown = 1f;

    #endregion

    #region Unity API
    private void Awake()
    {
        _controller = GetComponent<ArcaneArcherController>();
    }

    #endregion

    #region Main Methods

    public void DoExit()
    {
    }

    public void DoInit()
    {
        if (Time.time > _attackTimer + m_attackCooldown)
		{
            //return Physics2D.Raycast(transform.position, ((_targetPosition - transform.position) * Vector2.right).normalized, m_attackRange, _controller.PlayerLayerMask);

            //LocalScale
            if (_controller.Target != null)
                transform.localScale = ((_controller.Target.position.x - transform.position.x) < 0f) ? new Vector3(-1, 1, 1) : Vector3.one;

            _controller.Velocity = Vector2.zero;
            _controller.Animator.SetTrigger("AttackTrigger");
            _attackEnded = false;
        }
		else
		{
            AttackEnd();
		}
    }

    public void DoUpdate()
    {
    }

    public void Shoot()
	{
        Vector2 dir = Vector2.right * _controller.transform.localScale.x;
        ArrowController arrow = Instantiate(m_arrowPrefab, transform.position, m_arrowPrefab.transform.rotation).GetComponent<ArrowController>();
        arrow.transform.localScale = _controller.transform.localScale;
        arrow.Shoot(m_arrowSpeed, m_arrowLifeTime, _controller.CurrentDamage);
        _attackTimer = Time.time;
    }

    public void AttackEnd()
    {
        _attackEnded = true;
    }

    public bool AttackOnCooldown()
	{
        return Time.time > _attackTimer + m_attackCooldown;
	}

    public bool AttackEnded { get => _attackEnded; set => _attackEnded = value; }

    #endregion

    #region Privates

    private ArcaneArcherController _controller;

    private bool _attackEnded;

    private float _attackTimer;

    #endregion
}
