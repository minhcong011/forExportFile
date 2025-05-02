// dnSpy decompiler from Assembly-CSharp.dll class: SoundController
using System;
using UnityEngine;

public class SoundController : MonoBehaviour
{
	private void Awake()
	{
		if (!SoundController.isCreated)
		{
			SoundController.isCreated = true;
			UnityEngine.Object.DontDestroyOnLoad(this);
			Singleton<GameController>.Instance.soundController = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		this.audio = base.GetComponent<AudioSource>();
	}

	public void PlayMainMenuMusic()
	{
		if (Constants.isMusicOn())
		{
			this.audio.volume = 0.9f;
			this.audio.clip = this.mainMenuBg;
			this.audio.Play();
		}
	}

	public void StopMusic()
	{
		this.audio.Stop();
	}

	public void PlayIntroMusic()
	{
		if (Constants.isMusicOn())
		{
			this.audio.clip = this.IntroBg;
			this.audio.Play();
		}
	}

	public void PlayInGameMusic()
	{
		if (Constants.isMusicOn())
		{
			this.audio.clip = this.inGameBgMusic;
			this.audio.Play();
			this.audio.volume = 0.6f;
		}
	}

	public void StopIntroMusic()
	{
		this.audio.clip = this.mainMenuBg;
		if (Constants.isMusicOn())
		{
			this.audio.Play();
		}
	}

	private void Start()
	{
	}

	public void PlayButtonClick()
	{
		if (Constants.isSoundOn())
		{
			this.audio.PlayOneShot(this.buttonClickClip);
		}
	}

	public void PlayGameOver()
	{
		if (Constants.isSoundOn())
		{
			this.audio.PlayOneShot(this.gameOverClip);
		}
	}

	public void PlayGameOverVoice()
	{
		if (Constants.isSoundOn())
		{
			this.audio.PlayOneShot(this.gameOverVoice);
		}
	}

	public void PlayStoreProduct()
	{
		if (Constants.isSoundOn())
		{
			this.audio.PlayOneShot(this.consumeStoreProduct);
		}
	}

	public void PlayClaimSound()
	{
		if (Constants.isSoundOn() && this.claimProduct != null)
		{
			this.audio.PlayOneShot(this.claimProduct);
		}
	}

	public void PlayGiftPopUp()
	{
		if (Constants.isSoundOn() && this.giftPopUp != null)
		{
			this.audio.PlayOneShot(this.giftPopUp);
		}
	}

	public void PlayCollectStar()
	{
		if (Constants.isSoundOn() && this.collectStar != null)
		{
			this.audio.PlayOneShot(this.collectStar);
		}
	}

	public void PlayAnimalAlert()
	{
		if (Constants.isSoundOn() && this.alertAnimalClip != null)
		{
			this.audio.PlayOneShot(this.alertAnimalClip, 0.35f);
		}
	}

	public void playVoice(int id, AudioSource source = null)
	{
		if (id < this.voices.Length && Constants.isSoundOn() && this.voices[id] != null)
		{
			if (source != null)
			{
				source.PlayOneShot(this.voices[id], 1f);
			}
			else
			{
				this.audio.PlayOneShot(this.voices[id], 0.9f);
			}
		}
	}

	public void PlayBlastSound()
	{
		if (Constants.isSoundOn() && this.blastClip != null)
		{
			this.audio.PlayOneShot(this.blastClip, 1f);
		}
	}

	public void PlayTypeWriterSound()
	{
		if (Constants.isSoundOn() && this.typeWriterTick != null)
		{
			this.audio.PlayOneShot(this.typeWriterTick, 1f);
		}
	}

	public void PlayGivenSound(AudioClip clip)
	{
		if (Constants.isSoundOn() && clip != null)
		{
			float volumeScale = UnityEngine.Random.Range(0.8f, 1f);
			this.audio.PlayOneShot(clip, volumeScale);
		}
	}

	public void PlayGunCollectSound()
	{
		if (Constants.isSoundOn() && this.gunCollectSound != null)
		{
			this.audio.PlayOneShot(this.gunCollectSound, 1f);
		}
	}

	private static bool isCreated;

	public AudioClip buttonClickClip;

	public AudioClip mainMenuBg;

	public AudioClip IntroBg;

	public AudioClip inGameBgMusic;

	public AudioClip gameOverClip;

	public AudioClip consumeStoreProduct;

	public AudioClip claimProduct;

	public AudioClip giftPopUp;

	public AudioClip gameOverVoice;

	public AudioClip collectStar;

	public AudioClip alertAnimalClip;

	public AudioClip blastClip;

	public AudioClip typeWriterTick;

	public AudioClip gunCollectSound;

	public AudioClip[] voices;

	private AudioSource audio;
}
