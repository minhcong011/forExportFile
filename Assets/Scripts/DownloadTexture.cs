// dnSpy decompiler from Assembly-CSharp.dll class: DownloadTexture
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UITexture))]
public class DownloadTexture : MonoBehaviour
{
	private IEnumerator Start()
	{
		WWW www = new WWW(this.url);
		yield return www;
		this.mTex = www.texture;
		if (this.mTex != null)
		{
			UITexture component = base.GetComponent<UITexture>();
			component.mainTexture = this.mTex;
			component.MakePixelPerfect();
		}
		www.Dispose();
		yield break;
	}

	private void OnDestroy()
	{
		if (this.mTex != null)
		{
			UnityEngine.Object.Destroy(this.mTex);
		}
	}

	public string url = "http://www.yourwebsite.com/logo.png";

	private Texture2D mTex;
}
