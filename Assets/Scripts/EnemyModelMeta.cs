// dnSpy decompiler from Assembly-CSharp.dll class: EnemyModelMeta
using System;
using UnityEngine;

public class EnemyModelMeta
{
	public EnemyModelMeta()
	{
		this.prefabName = "Dummy";
		this.modelMetaId = "1_2_1";
		this.health = 100;
		this.damage = 1f;
		this.speed = 1f;
		this.attackTime = 10f;
		this.hitFactor = 1f;
		this.fireRate = 2f;
		this.delayTime = 4f;
	}

	public EnemyModelMeta(UKMap enemy)
	{
		if (enemy == null)
		{
			UnityEngine.Debug.Log("Mission :: Wave :: EnemyModelMeta :: constructor : enemy map is null");
			return;
		}
		UnityEngine.Debug.Log("EnemyModelMeta");
		if (enemy.ContainsProperty("prefabName"))
		{
			this.prefabName = enemy.GetProperty("prefabName").ToString();
		}
		if (enemy.ContainsProperty("id"))
		{
			this.modelMetaId = enemy.GetProperty("id").ToString();
		}
		UnityEngine.Debug.Log("id");
		if (enemy.ContainsProperty("damage"))
		{
			this.damage = (float)enemy.GetDoubleValueForKey("damage", 1.0);
		}
		UnityEngine.Debug.Log("damage");
		if (enemy.ContainsProperty("health"))
		{
			this.health = enemy.GetIntValueForKey("health", 10);
		}
		if (enemy.ContainsProperty("speed"))
		{
			this.speed = (float)enemy.GetDoubleValueForKey("speed", 1.0);
			UnityEngine.Debug.Log(":: Float value speed " + this.speed);
		}
		if (enemy.ContainsProperty("attackTime"))
		{
			this.attackTime = (float)enemy.GetDoubleValueForKey("attackTime", 5.0);
		}
		if (enemy.ContainsProperty("hitFactor"))
		{
			this.hitFactor = (float)enemy.GetDoubleValueForKey("hitFactor", 1.0);
		}
		if (enemy.ContainsProperty("fireRate"))
		{
			this.fireRate = (float)enemy.GetDoubleValueForKey("fireRate", 2.0);
			UnityEngine.Debug.Log(":: Float value fireRate " + this.fireRate);
		}
		if (enemy.ContainsProperty("delayTime"))
		{
			this.delayTime = (float)enemy.GetDoubleValueForKey("delayTime", 8.0);
			UnityEngine.Debug.Log(":: Float value delayTime " + this.delayTime);
		}
	}

	public string modelMetaId { get; set; }

	public string prefabName { get; set; }

	public int health { get; set; }

	public float damage { get; set; }

	public float speed { get; set; }

	public float attackTime { get; set; }

	public float hitFactor { get; set; }

	public float fireRate { get; set; }

	public float delayTime { get; set; }

	public void CopyMeta(EnemyModelMeta meta)
	{
		this.modelMetaId = meta.modelMetaId;
		this.prefabName = meta.prefabName;
		this.health = meta.health;
		this.damage = meta.damage;
		this.speed = meta.speed;
		this.attackTime = meta.attackTime;
		this.hitFactor = meta.hitFactor;
		this.fireRate = meta.fireRate;
		this.delayTime = meta.delayTime;
	}
}
