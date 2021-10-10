using System.Collections;
using UnityEngine;

public abstract class AbstractController : Behaviour
{
	#region Exposed

	[SerializeField] protected float m_horizontalOffset = 0.4f;
	[SerializeField] protected float m_groundCheckRaysDistance = 0.65f;
	[SerializeField] protected float m_characterHeightOffset = 0.6f;

	[SerializeField] private LayerMask m_groundLayerMask;
	[SerializeField] private LayerMask m_platformLayerMask;

	#endregion

	#region Unity API
	protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _velocity = Vector2.zero;
        _animator = GetComponent<Animator>();

		_floorLayerMask = m_groundLayerMask | m_platformLayerMask;

		_renderer = GetComponentInChildren<SpriteRenderer>();
		_shaderGUItext = Shader.Find("GUI/Text Shader");
		_shaderSpritesDefault = Shader.Find("Shader Graphs/SpriteGlowUnlit");

		_damagePrefab = Resources.Load("UI/DamageDisplay") as GameObject;
	}

    protected virtual void FixedUpdate()
    {
        _rigidBody.velocity = _velocity;
    }

	protected virtual void OnEnable()
	{
		NormalSprite();
	}

	#endregion

	#region Main Methods

	public bool IsGrounded()
	{
		foreach (Ray2D ray in _groundCheckRays)
		{
			RaycastHit2D hit;
			hit = Physics2D.Raycast(ray.origin, Vector2.down, m_groundCheckRaysDistance, _floorLayerMask);
			if (hit != false)
				return true;
		}
		return false;
	}

	public bool IsOnPlatform()
	{
		foreach (Ray2D ray in _groundCheckRays)
		{
			RaycastHit2D hit;
			hit = Physics2D.Raycast(ray.origin, Vector2.down, m_groundCheckRaysDistance, m_platformLayerMask);
			if (hit != false)
				return true;
		}
		return false;
	}

	public void AdjustHeight()
	{
		RaycastHit2D hit;

		float floorCheckDistance = m_groundCheckRaysDistance * 1.05f;
		float averageHeight = 0;
		Vector3 floorPos = transform.position;
		int n = 0;
		for (int i = 0; i < _groundCheckRays.Length; i++)
		{
			//if (Physics.Raycast(_groundCheckRays[i].origin, Vector2.down, out hit, floorCheckDistance, m_groundLayerMask))
			hit = Physics2D.Raycast(_groundCheckRays[i].origin, Vector2.down, floorCheckDistance, _floorLayerMask);
			if (hit != false)
			{
				averageHeight += hit.point.y + m_characterHeightOffset;
				n++;
			}
		}
		if (n != 0)
		{
			averageHeight /= n;
			floorPos.y = averageHeight;
		}
		else
			floorPos = Vector3.zero;

		if (floorPos != Vector3.zero)
		{
			//_rigidBody.MovePosition(new Vector3(transform.position.x, floorPos.y, transform.position.z));
			transform.position = new Vector3(transform.position.x, floorPos.y , transform.position.z);
		}
	}

	protected void UpdateGroundCheckRays()
	{
		_groundCheckRays[0] = new Ray2D((Vector2)transform.position, Vector2.down);
		_groundCheckRays[1] = new Ray2D((Vector2)transform.position + m_horizontalOffset * Vector2.right, Vector2.down);
		_groundCheckRays[2] = new Ray2D((Vector2)transform.position - m_horizontalOffset * Vector2.right, Vector2.down);
	}

	private void WhiteSprite()
	{
		_renderer.material.shader = _shaderGUItext;
		_renderer.color = Color.white;
	}

	private void NormalSprite()
	{
		_renderer.material.shader = _shaderSpritesDefault;
		_renderer.color = Color.white;
	}

	protected virtual void DisableEntity()
	{
		gameObject.SetActive(false);
	}

	public Rigidbody2D RigidBody { get => _rigidBody; set => _rigidBody = value; }
    public Vector2 Velocity { get => _velocity; set => _velocity = value; }
    public Animator Animator { get => _animator; set => _animator = value; }
	protected LayerMask FloorLayerMask { get => _floorLayerMask; set => _floorLayerMask = value; }
	protected GameObject DamagePrefab { get => _damagePrefab; set => _damagePrefab = value; }

	// To optimize (Coroutine = bad practice)
	public IEnumerator DisablePlayformLayer()
	{
		_floorLayerMask ^= m_platformLayerMask;
		yield return new WaitForSeconds(0.25f);
		_floorLayerMask |= m_platformLayerMask;
	}

	public IEnumerator ControllerHit()
	{
		WhiteSprite();
		yield return new WaitForSeconds(0.1f);
		NormalSprite();
	}

	#endregion

	#region Privates

	protected Rigidbody2D _rigidBody;
    protected Vector2 _velocity;
	private Animator _animator;

	protected Ray2D[] _groundCheckRays = new Ray2D[3];

	private LayerMask _floorLayerMask;

	protected SpriteRenderer _renderer;
	protected Shader _shaderGUItext;
	protected Shader _shaderSpritesDefault;

	protected GameObject _damagePrefab;

	#endregion
}
