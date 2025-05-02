// dnSpy decompiler from Assembly-CSharp.dll class: UkLightMapLoader
using System;
using System.Collections;
using UnityEngine;

public class UkLightMapLoader : MonoBehaviour
{
	private void Start()
	{
		Application.LoadLevelAdditive("NavyGamePlay");
		base.gameObject.SetActive(false);
	}

	public IEnumerator LoadLightMap(int index)
	{
		yield return new WaitForSeconds(0.1f);
		int totalMaps = 8;
		if (index == 1)
		{
			totalMaps = 8;
		}
		LightmapData[] lightmapData = new LightmapData[totalMaps];
		UnityEngine.Debug.Log("Coming");
		for (int i = 0; i < totalMaps; i++)
		{
			lightmapData[i] = new LightmapData();
			lightmapData[i].lightmapColor = (Resources.Load(string.Concat(new object[]
			{
				"LightMaps/Level",
				index,
				"/Lightmap-",
				i,
				"_comp_light"
			}), typeof(Texture2D)) as Texture2D);
		}
		LightmapSettings.lightmaps = lightmapData;
		yield break;
	}

	private void Update()
	{
	}

	public LightmapData[] data;
}
