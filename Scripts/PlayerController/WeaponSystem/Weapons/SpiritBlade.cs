using UnityEngine;

public class SpiritBlade : AbstractMeleeWeapon
{
	#region Exposed

	[SerializeField] private float m_thrustSpeed = 6f;

	[SerializeField] private BoxCollider2D m_collider0;
	[SerializeField] private BoxCollider2D m_collider1;
	[SerializeField] private BoxCollider2D m_collider2;


	#endregion

	#region Unity API

	protected override void Awake()
	{
		base.Awake();
		_hitbox = null;
		if (transform.parent != null)
			_hitbox = transform.parent.Find("Hitbox");

		if (_hitbox != null)
			_hitboxCollider = _hitbox.GetComponent<BoxCollider2D>();
		else
		{
			Debug.Log("Hitbox not instantiated");
		}
	}

	private void Update()
	{
		if (Controller != null)
		{
			if (ComboCounter == 3)
			{
				float alpha = (Time.time - _thrustStart) / 0.5f;
				Controller.Velocity = Vector2.Lerp(_thrustStartVelocity, Vector2.zero, alpha);
			}
		}
	}

	#endregion

	#region Main Methods

	public override void MeleeAttack()
	{
        if (m_isDebug)
        {
            Debug.Log("Spirit Blade Melee Attack");
            if (transform.parent != null)
                Debug.Log(transform.parent.name);
        }

		switch (ComboCounter)
		{
			case 1:
				SetHitbox(m_collider0);
				break;
			case 2:
				SetHitbox(m_collider1);
				break;
			case 3:
				SetHitbox(m_collider2);
				_thrustStart = Time.time;
				_thrustStartVelocity = Vector2.right * transform.parent.localScale.x * m_thrustSpeed;
				break;
			default:
				break;
		}

		PlayAudio();
	}

	private void SetHitbox(BoxCollider2D _collider)
	{
		if (_hitboxCollider == null) return;

		_hitboxCollider.offset = _collider.offset;
		_hitboxCollider.size = _collider.size;
	}

	#endregion

	#region Privates

	private float _thrustStart;
	private Vector2 _thrustStartVelocity;

	private Transform _hitbox;
	private BoxCollider2D _hitboxCollider;

    #endregion
}
