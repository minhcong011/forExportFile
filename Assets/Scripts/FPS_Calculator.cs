// dnSpy decompiler from Assembly-CSharp.dll class: FPS_Calculator
using System;
using UnityEngine;

[Serializable]
public class FPS_Calculator
{
	public void FPS_Start()
	{
		this.timeleft = this.updateInterval;
	}

	public void FPS_Update()
	{
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
		if ((double)this.timeleft <= 0.0)
		{
			this.fps = this.accum / (float)this.frames;
			this.output = string.Concat(new string[]
			{
				"FPS: <color=",
				this.SelectColor(),
				">",
				this.fps.ToString("f1"),
				"</color>"
			});
			this.timeleft = this.updateInterval;
			this.accum = 0f;
			this.frames = 0;
		}
	}

	private string SelectColor()
	{
		if (this.fps < 30f)
		{
			return "red";
		}
		if (this.fps >= 30f && this.fps < 60f)
		{
			return "yellow";
		}
		if (this.fps >= 60f)
		{
			return "lime";
		}
		return "wight";
	}

	public float updateInterval = 0.5f;

	public string output;

	private float accum;

	private int frames;

	private float timeleft;

	private float fps;
}
