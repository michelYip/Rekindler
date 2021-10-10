using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractHybridWeapon : AbstractWeapon, IMeleeWeapon, IRangeWeapon
{
    #region Exposed
    #endregion

    #region Unity API
    #endregion

    #region Main Methods

    public abstract void MeleeAttack();

    public abstract void RangeAttack();
    public abstract void Shoot();

    #endregion

    #region Privates
    #endregion
}
