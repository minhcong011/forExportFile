// dnSpy decompiler from Assembly-CSharp.dll class: typewriter
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class typewriter : MonoBehaviour
{
	private void Awake()
	{
		if (Singleton<GameController>.Instance.soundController == null)
		{
			this.playSound = false;
		}
	}

	public void OnEnable()
	{
		this.txt = base.GetComponent<Text>();
		this.txt.enabled = false;
		base.StartCoroutine("PlayText");
	}

	public void OnDisable()
	{
		if (this.shouldStoreLocal)
		{
			this.txt.text = this.story;
		}
	}

	private IEnumerator PlayText()
	{
		yield return new WaitForSeconds(this.delay);
		this.story = this.txt.text;
		this.txt.text = string.Empty;
		this.txt.enabled = true;
		foreach (char c in this.story)
		{
			Text text2 = this.txt;
			text2.text += c;
			if (this.playSound)
			{
				Singleton<GameController>.Instance.soundController.PlayTypeWriterSound();
			}
			yield return new WaitForSeconds(0.05f);
		}
		yield break;
	}

	private Text txt;

	private string story;

	public float delay = 0.3f;

	public bool shouldStoreLocal;

	public bool playSound;
}
