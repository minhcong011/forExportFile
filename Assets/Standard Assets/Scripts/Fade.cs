// dnSpy decompiler from Assembly-UnityScript-firstpass.dll class: Fade
using System;
using UnityEngine;

[Serializable]
public class Fade : MonoBehaviour
{
	public Fade()
	{
		this.drawDepth = -1000;
		this.alpha = 1f;
		this.fadeDir = -1;
	}

	public virtual void OnGUI()
	{
		if (this.isFade)
		{
			this.alpha += (float)this.fadeDir * this.fadeSpeed * Time.deltaTime;
			this.alpha = Mathf.Clamp01(this.alpha);
			float a = this.alpha;
			Color color = GUI.color;
			float num = color.a = a;
			Color color2 = GUI.color = color;
			GUI.depth = this.drawDepth;
			GUI.DrawTexture(new Rect((float)0, (float)0, (float)Screen.width, (float)Screen.height), this.fadeOutTexture);
		}
	}

	public virtual void FadeIn()
	{
		this.fadeDir = -1;
	}

	public virtual void FadeOut()
	{
		this.fadeDir = 1;
	}

	public virtual void Start()
	{
	}

	public virtual void fadeVale()
	{
		this.SendMessage("enablePlayer");
		this.fadeSpeed = 0.07f;
		this.isFade = true;
		this.alpha = (float)1;
		this.FadeIn();
	}

	public virtual void FadeIN()
	{
		this.Invoke("fadeVale", 1f);
		MonoBehaviour.print("I AM THERE SHAN");
	}

	public virtual void Main()
	{
	}

	public Texture2D fadeOutTexture;

	public float fadeSpeed;

	public int drawDepth;

	private float alpha;

	private int fadeDir;

	private bool isFade;
}
