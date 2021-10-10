using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsHUD : MonoBehaviour
{
    #region Exposed

    [SerializeField] private PlayerAttributesScriptable m_currentPlayerStats;

    [SerializeField] private Image m_healthBarBG;
    [SerializeField] private Image m_healthBar;
    [SerializeField] private TextMeshProUGUI m_currentHealth;

    [SerializeField] private float m_healthBarSizeModifier = 4f;

    #endregion

    #region Unity API
    private void Start()
    {
        UpdatePlayerStats();
    }

    #endregion

    #region Main Methods

    public void UpdatePlayerStats()
	{
        m_healthBarBG.rectTransform.sizeDelta = new Vector2(m_healthBarSizeModifier * m_currentPlayerStats.TotalHealth, m_healthBarBG.rectTransform.sizeDelta.y);
        m_healthBar.rectTransform.sizeDelta = new Vector2(m_healthBarSizeModifier * m_currentPlayerStats.CurrentHealth, m_healthBarBG.rectTransform.sizeDelta.y);
        //m_healthBar.fillAmount = (float)m_currentPlayerStats.CurrentHealth / m_currentPlayerStats.TotalHealth;
        m_currentHealth.text = m_currentPlayerStats.CurrentHealth.ToString() + "/" + m_currentPlayerStats.TotalHealth.ToString();
        if (m_currentPlayerStats.CurrentHealth < 0f)
            m_currentHealth.text = "0/" + m_currentPlayerStats.TotalHealth.ToString();
    }

    #endregion

    #region Privates
    #endregion
}
