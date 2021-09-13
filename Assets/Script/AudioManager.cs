using System;
using MyCanvasPack;
using UnityEngine;

public enum ClipType
{
	Flag,
	Click,
	Bomb
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
	private AudioSource _mySource;

	[SerializeField] private AudioClip flag;
	[SerializeField] private AudioClip click;
	[SerializeField] private AudioClip bomb;

	private void Awake()
	{
		_mySource = GetComponent<AudioSource>();
	}

	private AudioClip GetClip(ClipType clipType)
	{
		return clipType switch
		{
			ClipType.Flag => flag,
			ClipType.Click => click,
			ClipType.Bomb => bomb,
			_ => throw new ArgumentOutOfRangeException(nameof(clipType), clipType, null)
		};
	}

	public void Play(ClipType clipType, float volume = 1)
	{
		var current = GetClip(clipType);
		Play(current, volume);
	}

	private void Play(AudioClip clip, float volume = 1)
	{
		if (!clip) return;
		_mySource.PlayOneShot(clip, volume);
	}
}