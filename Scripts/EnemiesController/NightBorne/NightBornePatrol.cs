using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightBornePatrol : Behaviour, IState
{
	#region Exposed

	[SerializeField] private float m_changeDirectionDuration = 2f;

	[SerializeField] private float m_walkSpeed = 1.25f;

	#endregion

	#region Unity API
	void Awake()
	{
		_controller = GetComponent<NightBorneController>();
	}

	void Update()
	{
		_controller.UpdateForwardCheckRay();
	}

	private void OnDrawGizmos()
	{
		if (!m_drawGizmos) return;
		Gizmos.color = Color.green;
		foreach(Ray2D ray in _controller.ForwardCheckRay)
		{
			Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * _controller.ForwardRayLength);
		}
	}
	#endregion

	#region Main Methods

	public void DoExit()
	{
	}

	public void DoInit()
	{
		_isChangingDirection = false;
	}

	public void DoUpdate()
	{
		if (_isChangingDirection) //IDLE
		{
			if (_controller.IsGrounded())
				_controller.Velocity = Vector2.zero;
			float alpha = (Time.time - _changeDirectionTimer) / m_changeDirectionDuration;
			_controller.Animator.SetFloat("MoveX", 0f);
			if (alpha >= 1f)
			{
				transform.localScale *= (Vector2.left + Vector2.up);
				_isChangingDirection = false;
			}
		}
		else //WALK
		{
			_controller.Animator.SetFloat("MoveX", 0.5f);
			//_controller.Velocity = Vector2.right * transform.localScale.x * m_walkSpeed + Vector2.up * _controller.Velocity.y;
			_controller.Velocity = new Vector2((transform.localScale.x < 0f? -1f:1f) * m_walkSpeed, _controller.Velocity.y);
			if (!_controller.CanWalkForward())
			{
				_isChangingDirection = true;
				_changeDirectionTimer = Time.time;
			}
		}

		/*if (_controller.Velocity.x < 0f)
			transform.localScale = new Vector2(-1, 1);
		else
			transform.localScale = new Vector2(1, 1);*/
	}

	#endregion

	#region Privates

	private bool _isChangingDirection;
	private float _changeDirectionTimer;

	private NightBorneController _controller;

	#endregion
}

