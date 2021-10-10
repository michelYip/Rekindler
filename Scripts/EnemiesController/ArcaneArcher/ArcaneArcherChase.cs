using UnityEngine;

public class ArcaneArcherChase : MonoBehaviour, IState
{
	#region Exposed

	[SerializeField] private float m_runSpeed = 5f;

	[SerializeField] private float m_attackRange = 15f;

	#endregion

	#region Unity API
	void Awake()
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
		_controller.UpdateForwardCheckRay();
	}

	public void DoUpdate()
	{
		_targetPosition = _controller.Target.position;
		transform.localScale = (_controller.Velocity.x < 0f) ? new Vector3(-1f, 1f, 1f) : new Vector3(1f, 1f, 1f);
		_controller.UpdateForwardCheckRay();

		if (!CanAttack() && _controller.CanWalkForward() && Mathf.Abs(transform.position.x - _targetPosition.x) > m_attackRange)
		{
			_controller.Animator.SetFloat("MoveX", 1f);
			_controller.Velocity = m_runSpeed * ((transform.localScale.x < 0f) ? -1f : 1f) * Vector2.right + _controller.Velocity.y * Vector2.up;
		}
		else
		{
			_controller.Animator.SetFloat("MoveX", 0f);
			_controller.Velocity = Vector2.zero;
		}
	}

	public bool CanAttack()
	{
		return Physics2D.Raycast(transform.position, ((_targetPosition - transform.position) * Vector2.right).normalized, m_attackRange, _controller.PlayerLayerMask);
	}

	#endregion

	#region Privates

	private ArcaneArcherController _controller;
	private Vector3 _targetPosition;

	#endregion
}
