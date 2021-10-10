using UnityEngine;

public class PlayerLocomotion : MonoBehaviour, IState
{
    #region Exposed

    [SerializeField] private float m_moveSpeed = 5f;

    #endregion

    #region Unity API
    void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }
    #endregion

    #region Main Methods

    public void DoInit()
    {
        _controller.Velocity = new Vector2(_controller.Velocity.x, 0f);
        _controller.AdjustHeight();
    }

    public void DoUpdate()
	{
        Locomotion();

        _controller.AdjustHeight();
    }

    public void DoExit()
	{

	}

	public void Locomotion()
	{
        float horizontalInput = _controller.GetHorizontalInput();

        _controller.Animator.SetFloat("MoveX", Mathf.Abs(horizontalInput * MoveSpeed / MoveSpeed));
        if (horizontalInput != 0)
        {
            if (_controller.Velocity.x < 0f)
                transform.localScale = new Vector2(-1, 1);
            else
                transform.localScale = new Vector2(1, 1);
        }
        _controller.Velocity = new Vector2(horizontalInput * MoveSpeed, _controller.Velocity.y);
    }

	#endregion

	#region Privates

	private PlayerController _controller;
	public float MoveSpeed { get => m_moveSpeed; set => m_moveSpeed = value; }

	#endregion
}
