using UnityEngine;

public class PlayerInteraction : Behaviour
{
    #region Exposed

    [SerializeField] private float m_interactionRadius = 1f;
	[SerializeField] private LayerMask m_usableLayerMask;

	#endregion

	#region Unity API

	private void Update()
	{
		usable = Physics2D.OverlapCircle(transform.position, m_interactionRadius, m_usableLayerMask);
		if (usable != null && Input.GetButtonDown("Use"))
		{
			usable.GetComponent<IUsable>().Use();
		}
	}

	#endregion

	#region Main Methods
	#endregion

	#region Privates

	Collider2D usable;

	#endregion
}
