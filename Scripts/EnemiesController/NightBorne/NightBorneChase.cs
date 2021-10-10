using UnityEngine;

public class NightBorneChase : Behaviour, IState
{
	#region Exposed

	[SerializeField] private float m_runSpeed = 7f;
	[SerializeField] private float m_teleportOffset = 1.5f;

	[SerializeField] private float m_attackRange = 1.5f;

	#endregion

	#region Unity API
	void Awake()
	{
		_controller = GetComponent<NightBorneController>();
		_localScale = transform.localScale;
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
		transform.localScale = (_targetPosition.x - transform.position.x < 0f) ? new Vector2(-_localScale.x,_localScale.y) : (Vector2)_localScale;
		_controller.UpdateForwardCheckRay();

		if (!CanAttack() && _controller.CanWalkForward() && Mathf.Abs(transform.position.x - _targetPosition.x) > m_attackRange)
		{
			_controller.Animator.SetFloat("MoveX", 1f);
			_controller.Velocity =  m_runSpeed * ((transform.localScale.x < 0f) ? -1f : 1f) * Vector2.right + _controller.Velocity.y * Vector2.up;
		}
		else
		{
			//Teleportation
		}
	}

	public bool CanAttack()
	{
		return Vector2.Distance((Vector2)transform.position, (Vector2)_targetPosition) < m_attackRange;
	}

	#endregion

	#region Privates

	private NightBorneController _controller;
	private Vector3 _targetPosition;

	private Vector3 _localScale;

	#endregion
}
