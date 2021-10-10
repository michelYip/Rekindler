using UnityEngine;

public class EnemyScriptable : ScriptableObject
{
    #region Exposed

    [SerializeField] protected int m_health;
    [SerializeField] protected int m_damage;

    #endregion

    #region Unity API
    #endregion

    #region Main Methods

    public int Health { get => m_health; set => m_health = value; }
    public int Damage { get => m_damage; set => m_damage = value; }

    #endregion

    #region Privates
    #endregion
}