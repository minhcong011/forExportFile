// dnSpy decompiler from Assembly-CSharp.dll class: SplashVideo
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SplashVideo : MonoBehaviour
{
	private void Awake()
	{
		this.videoPlayer = base.gameObject.AddComponent<VideoPlayer>();
		this.audioSource = base.gameObject.AddComponent<AudioSource>();
		this.videoPlayer.playOnAwake = false;
		this.audioSource.playOnAwake = false;
		this.audioSource.Pause();
		this.videoPlayer.source = VideoSource.VideoClip;
		this.videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
		this.videoPlayer.EnableAudioTrack(0, true);
		this.videoPlayer.SetTargetAudioSource(0, this.audioSource);
		this.videoPlayer.clip = this.videoToPlay;
		this.videoPlayer.Prepare();
	}

	private void Start()
	{
		this._CG = base.GetComponent<CanvasGroup>();
		base.StartCoroutine(this.playVideo());
	}

	private IEnumerator playVideo()
	{
		while (!this.videoPlayer.isPrepared)
		{
			yield return null;
		}
		this.image.texture = this.videoPlayer.texture;
		this.videoPlayer.Play();
		this.audioSource.Play();
		yield return new WaitForSeconds(1f);
		this.bgImage.enabled = false;
		UnityEngine.Debug.Log("Done Playing Video");
		yield return new WaitForSeconds(this.SplashTime - 1f);
		while (this._CG.alpha > 0f)
		{
			this._CG.alpha -= 0.04f;
			yield return new WaitForSeconds(0.05f);
		}
		if (this._CBObject != null)
		{
			this._CBObject.SendMessage("StartSplash", SendMessageOptions.DontRequireReceiver);
		}
		yield return new WaitForSeconds(0.1f);
		base.gameObject.SetActive(false);
		yield break;
	}

	public RawImage image;

	public VideoClip videoToPlay;

	public Image bgImage;

	public float SplashTime = 6f;

	public GameObject _CBObject;

	private VideoPlayer videoPlayer;

	private VideoSource videoSource;

	private AudioSource audioSource;

	private CanvasGroup _CG;
}
