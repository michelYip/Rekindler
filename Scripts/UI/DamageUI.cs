using UnityEngine;
using TMPro;

public class DamageUI : MonoBehaviour
{
    #region Exposed
    #endregion

    #region Unity API
    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
        _instantiateTimer = Time.time;
        _startPosition = transform.position + (Vector3)Random.insideUnitCircle - Vector3.forward;
    }

	private void Update()
	{
        float alpha = (Time.time - _instantiateTimer) / _lifetime;
        _text.alpha = 1 - Mathf.Pow(alpha, 2);
        transform.position = Vector3.Lerp(_startPosition, _startPosition + Vector3.up, Mathf.Pow(alpha, 2));

        if (alpha > 2f)
            Destroy(gameObject);
	}

	#endregion

	#region Main Methods

	public void SetDamageNumber(int damage)
	{
        _text.text = damage.ToString();
	}

    public void SetColor(Color color)
	{
        _text.color = color;
	}

    #endregion

    #region Privates

    private TextMeshPro _text;

    private float _instantiateTimer;
    private float _lifetime = 1f;

    private Vector3 _startPosition;

    #endregion
}
