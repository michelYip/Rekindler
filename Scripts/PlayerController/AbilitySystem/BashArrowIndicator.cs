using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BashArrowIndicator : MonoBehaviour
{
    #region Exposed
    #endregion

    #region Unity API

    void Update()
    {
        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    #endregion

    #region Main Methods
    #endregion

    #region Privates
    #endregion
}
