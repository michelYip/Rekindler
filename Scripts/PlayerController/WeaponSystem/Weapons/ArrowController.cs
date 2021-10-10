using UnityEngine;

public class ArrowController : MonoBehaviour
{
    #region Exposed
    #endregion

    #region Unity API
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _hitbox = GetComponent<HitBoxController>();
    }

    private void Update()
    {
        float alpha = (Time.time - _shootTime) / _lifeTime;
        _rigidbody.velocity = Vector2.Lerp(_velocity, _finalVelocity, alpha * alpha * alpha);

        if (alpha >= 1f)
		{
            Destroy(gameObject);
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (gameObject.CompareTag("PlayerHitBox") && collision.CompareTag("EnemyHurtBox"))
		{
            Destroy(gameObject);
		}
        else if (gameObject.CompareTag("EnemyHitBox") && collision.CompareTag("PlayerHurtBox"))
        {
            Destroy(gameObject);
        }
    }

	#endregion

	#region Main Methods

	public void Shoot(float speed, float lifeTime, int damage)
	{
        _velocity = Vector2.right * transform.localScale.x * speed;
        _finalVelocity = _velocity / 2;
        _shootTime = Time.time;
        _lifeTime = lifeTime;
        _hitbox.DamageAmount = damage;
	}

    #endregion

    #region Privates

    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    private Vector2 _finalVelocity;

    private float _shootTime;
    private float _lifeTime;

    private HitBoxController _hitbox;

    #endregion
}
