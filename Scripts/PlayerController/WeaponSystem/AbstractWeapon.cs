using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon : Behaviour
{
	#region Exposed

	[SerializeField] protected WeaponInfo weaponInfo;
	[SerializeField] protected AudioClip[] m_clips;

	#endregion

	#region Unity API

	protected virtual void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	#endregion

	#region Main Methods

	protected void PlayAudio()
	{
		int randomClipIndex = Random.Range(0, m_clips.Length);
		_audioSource.clip = m_clips[randomClipIndex];
		_audioSource.Play();
	}

	public WeaponInfo WeaponInfo { get => weaponInfo; set => weaponInfo = value; }
	public int ComboCounter { get => _comboCounter; set => _comboCounter = value; }
	public PlayerController Controller { get => _controller; set => _controller = value; }

	#endregion

	#region Privates

	private int _comboCounter;
	private PlayerController _controller;
	protected AudioSource _audioSource;

    #endregion
}
