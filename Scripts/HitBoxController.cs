using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    #region Exposed
    #endregion

    #region Unity API
    void Awake()
    {
        _damageAmount = 0;
    }
    #endregion

    #region Main Methods
	public int DamageAmount { get => _damageAmount; set => _damageAmount = value; }

    #endregion

    #region Privates
    private int _damageAmount;
	#endregion
}
