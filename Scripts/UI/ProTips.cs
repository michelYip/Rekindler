using ScriptableObjectArchitecture;
using UnityEngine;
using TMPro;

public class ProTips : MonoBehaviour
{
	#region Exposed

	[SerializeField] private StringCollection m_tips;

    #endregion

    #region Unity API
    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

	private void OnEnable()
	{
		int randomTipsIndex = Random.Range(0, m_tips.List.Count);
		_text.text = m_tips[randomTipsIndex];
	}
	#endregion

	#region Main Methods
	#endregion

	#region Privates

	TextMeshProUGUI _text;

    #endregion
}
