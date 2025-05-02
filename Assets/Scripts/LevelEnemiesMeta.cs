// dnSpy decompiler from Assembly-CSharp.dll class: LevelEnemiesMeta
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemiesMeta
{
	public LevelEnemiesMeta(UKMap meta)
	{
		this.initialMeta = new List<EnemyModelMeta>();
		this.wavesMeta = new List<EnemyModelMeta>();
		this.enemiesInWaveArray = new List<int>();
		this.waveGenerationCount = new List<int>();
		if (meta == null)
		{
			UnityEngine.Debug.Log(" Uk :: enemy Details null");
		}
		if (meta.ContainsProperty("enemiesCount"))
		{
			this.enemiesCount = meta.GetIntValueForKey("enemiesCount", 1);
		}
		if (meta.ContainsProperty("initialCount"))
		{
			this.initialCount = meta.GetIntValueForKey("initialCount", 1);
		}
		if (meta.ContainsProperty("totalWaves"))
		{
			this.totalWaves = meta.GetIntValueForKey("totalWaves", 1);
		}
		if (meta.ContainsProperty("initialMeta"))
		{
			IList list = meta.GetProperty("initialMeta") as IList;
			IEnumerator enumerator = list.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					UKMap enemy = (UKMap)obj;
					EnemyModelMeta item = new EnemyModelMeta(enemy);
					this.initialMeta.Add(item);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
		if (meta.ContainsProperty("wavesMeta"))
		{
			IList list2 = meta.GetProperty("wavesMeta") as IList;
			IEnumerator enumerator2 = list2.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					object obj2 = enumerator2.Current;
					UKMap enemy2 = (UKMap)obj2;
					EnemyModelMeta item2 = new EnemyModelMeta(enemy2);
					this.wavesMeta.Add(item2);
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = (enumerator2 as IDisposable)) != null)
				{
					disposable2.Dispose();
				}
			}
		}
		if (meta.ContainsProperty("waveGenerationCount"))
		{
			IList list3 = meta.GetProperty("waveGenerationCount") as IList;
			UnityEngine.Debug.Log("waveGenerationCountIList " + list3.Count);
			IEnumerator enumerator3 = list3.GetEnumerator();
			try
			{
				while (enumerator3.MoveNext())
				{
					object obj3 = enumerator3.Current;
					UKMap ukmap = (UKMap)obj3;
					if (ukmap.ContainsProperty("count"))
					{
						this.waveGenerationCount.Add(ukmap.GetIntValueForKey("count", 0));
					}
				}
			}
			finally
			{
				IDisposable disposable3;
				if ((disposable3 = (enumerator3 as IDisposable)) != null)
				{
					disposable3.Dispose();
				}
			}
		}
		if (meta.ContainsProperty("enemiesInWave"))
		{
			IList list4 = meta.GetProperty("enemiesInWave") as IList;
			IEnumerator enumerator4 = list4.GetEnumerator();
			try
			{
				while (enumerator4.MoveNext())
				{
					object obj4 = enumerator4.Current;
					UKMap ukmap2 = (UKMap)obj4;
					if (ukmap2.ContainsProperty("count"))
					{
						this.enemiesInWaveArray.Add(ukmap2.GetIntValueForKey("count", 0));
					}
				}
			}
			finally
			{
				IDisposable disposable4;
				if ((disposable4 = (enumerator4 as IDisposable)) != null)
				{
					disposable4.Dispose();
				}
			}
		}
	}

	public int enemiesCount;

	public int totalWaves;

	public int initialCount;

	public List<EnemyModelMeta> initialMeta;

	public List<EnemyModelMeta> wavesMeta;

	public List<int> waveGenerationCount;

	public List<int> enemiesInWaveArray;
}
