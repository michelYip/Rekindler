using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHitPostProcessing : MonoBehaviour
{
    #region Exposed

    [Header("Vignette Attributes")]
    [SerializeField] private float m_vignetteDuration = 0.2f;
    [SerializeField] private ClampedFloatParameter m_vignetteIntensity;

    [Header("ScreenShake Attributes")]
    [SerializeField] private float m_screenShakeDuration = 0.15f;
    [SerializeField] private ClampedFloatParameter m_screenShakeIntensity;

    #endregion

    #region Unity API
    private void Awake()
    {
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet<Vignette>(out _vignette);
        _volume.profile.TryGet<LensDistortion>(out _lensDistortion);
        _vignette.active = false;
        _lensDistortion.active = false;
        _effectOn = false;
    }

	private void Update()
	{
        if (!_effectOn) return;
        else
		{
			float alphaVignette = (Time.time - _startVignetteTime) / m_vignetteDuration;
			float alphaScreenShake = (Time.time - _startScreenShakeTime) / m_screenShakeDuration;
			_vignette.intensity.Interp((float)m_vignetteIntensity, 0, alphaVignette);
			_lensDistortion.intensity.Interp((float)m_screenShakeIntensity, 0, alphaScreenShake);

			if (alphaVignette > 1f && alphaScreenShake > 1f)
			{
				_vignette.active = false;
				_lensDistortion.active = false;
				_effectOn = false;
			}
		}
	}

	#endregion

	#region Main Methods

	public void HitEffect()
    {
        _effectOn = true;
        _vignette.active = true;
        _lensDistortion.active = true;
        _vignette.intensity = m_vignetteIntensity;
        _lensDistortion.intensity = m_screenShakeIntensity;
        _startVignetteTime = Time.time;
        _startScreenShakeTime = Time.time;
    }

    #endregion

    #region Privates

    private bool _effectOn;

    private Volume _volume;
    private Vignette _vignette;
    private LensDistortion _lensDistortion;

    private float _startVignetteTime;
    private float _startScreenShakeTime;
    private float _startTimeStopTime;

    #endregion
}
