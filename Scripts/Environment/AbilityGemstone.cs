using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityGemstone : MonoBehaviour, IUsable
{
    #region Exposed

    [SerializeField] private AbilityScriptable _ability;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _canvas;

    #endregion

    #region Unity API
    private void Start()
    {
        if (_ability == null)
		{
            Debug.Log("No ability is assigned to this Ability Gemstone");
            Debug.Break();
		}

        _name.text = _ability.AbilityName[_ability.CurrentLevel];
        _description.text = _ability.AbilityDescription[_ability.CurrentLevel];
        _icon.sprite = _ability.AbilityIcon;

        _canvas.SetActive(false);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("PlayerHurtBox"))
            _canvas.SetActive(true);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.CompareTag("PlayerHurtBox"))
            _canvas.SetActive(false);
    }

	#endregion

	#region Main Methods

	public void Use()
	{
        UpgradeAbility();
        Destroy(gameObject);
	}

    private void UpgradeAbility()
	{
        _ability.CurrentLevel++;
        if (_ability.CurrentLevel >= 3) _ability.CurrentLevel = 3;
	}

    #endregion

    #region Privates

    #endregion
}
