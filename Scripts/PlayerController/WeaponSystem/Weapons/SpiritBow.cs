using UnityEngine;

public class SpiritBow : AbstractRangeWeapon
{
    #region Exposed

    [SerializeField] private GameObject m_arrowPrefab;
    [SerializeField] private float m_arrowSpeed = 8f;
    [SerializeField] private float m_arrowLifeTime = 3f;

    [SerializeField] private float m_instantiateRadius = 0.3f;

    [SerializeField] private AudioClip m_arrowShoot;

	#endregion

	#region Unity API

	private void OnDrawGizmos()
	{
        if (!m_drawGizmos) return;
		else
		{
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, m_instantiateRadius);
		}
	}

	#endregion

	#region Main Methods

	public override void RangeAttack()
	{
        if (m_isDebug)
        {
            Debug.Log("Spirit Bow Range Attack");
        }
        PlayAudio();
    }

    public override void Shoot()
	{
        if (Controller != null)
        {
            Vector2 dir = Vector2.right * Controller.transform.localScale.x;
            ArrowController arrow = Instantiate(m_arrowPrefab, transform.position, m_arrowPrefab.transform.rotation).GetComponent<ArrowController>();
            arrow.transform.localScale = Controller.transform.localScale;
            arrow.Shoot(m_arrowSpeed, m_arrowLifeTime, _projectileDamage);
        }

        _audioSource.clip = m_arrowShoot;
        _audioSource.Play();
    }

	#endregion

	#region Privates
	#endregion
}
