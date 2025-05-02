// dnSpy decompiler from Assembly-CSharp.dll class: FileDownloader
using System;
using UnityEngine;

public class FileDownloader : MonoBehaviour
{
	private void Start()
	{
		this.myWWW = new WWW(this.URL);
	}

	private void FixedUpdate()
	{
		if (this.myWWW.isDone && !this.dwnlded)
		{
			this.Info = this.myWWW.text;
			this.dwnlded = true;
		}
	}

	public string URL = "https://www.dropbox.com/home?preview=Missions.html";

	public string Info;

	public bool dwnlded;

	public WWW myWWW;
}
