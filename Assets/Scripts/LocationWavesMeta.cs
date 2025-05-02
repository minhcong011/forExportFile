// dnSpy decompiler from Assembly-CSharp.dll class: LocationWavesMeta
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LocationWavesMeta
{
	public SingleLocationWaveMeta getCurrentLocationMeta()
	{
		if (this.currentLocationId <= this.locationsMeta.Count)
		{
			return this.locationsMeta[this.currentLocationId - 1];
		}
		UnityEngine.Debug.Log("Location Waves Meta " + this.currentLocationId + " meta is null");
		return null;
	}

	public int totalLevelEnemies;

	public int totalLocations;

	public List<SingleLocationWaveMeta> locationsMeta;

	public int currentLocationId = 1;
}
