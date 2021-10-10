using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractRangeWeapon : AbstractWeapon, IRangeWeapon
{
    #region Exposed
    #endregion

    #region Unity API
    #endregion

    #region Main Methods

    public abstract void RangeAttack();
    public abstract void Shoot();
	public int ProjectileDamage { get => _projectileDamage; set => _projectileDamage = value; }

    #endregion

    #region Privates

    protected int _projectileDamage;

	#endregion
}
