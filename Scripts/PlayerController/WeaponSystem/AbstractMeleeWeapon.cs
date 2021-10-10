using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMeleeWeapon : AbstractWeapon, IMeleeWeapon
{
	#region Exposed
	#endregion

	#region Unity API
	#endregion

	#region Main Methods

	public abstract void MeleeAttack();

    #endregion

    #region Privates
    #endregion
}
