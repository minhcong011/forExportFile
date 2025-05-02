// dnSpy decompiler from Assembly-CSharp.dll class: AnimalModelMeta
using System;
using UnityEngine;

[Serializable]
public class AnimalModelMeta
{
	public AnimalModelMeta()
	{
		this.prefabName = "Dummy";
		this.modelMetaId = "1_2_1";
		this.health = 100;
		this.damage = 1f;
		this.speed = 1f;
		this.attackTime = 6f;
		this.hitFactor = 1f;
		this.walkSpeed = 14f;
		this.runSpeed = 22f;
	}

	public AnimalModelMeta(UKMap enemy)
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
	}

	public void CopyMeta(AnimalModelMeta meta)
	{
		this.modelMetaId = meta.modelMetaId;
		this.prefabName = meta.prefabName;
		this.health = meta.health;
		this.damage = meta.damage;
		this.speed = meta.speed;
		this.attackTime = meta.attackTime;
		this.hitFactor = meta.hitFactor;
		this.walkSpeed = meta.walkSpeed;
		this.runSpeed = meta.runSpeed;
	}

	public string modelMetaId;

	public string prefabName;

	public int health = 100;

	public float damage;

	public float speed = 1f;

	public float walkSpeed = 14f;

	public float runSpeed = 22f;

	public float attackTime;

	public float hitFactor;

	public float stopAtDistanceToPlayer = 23f;
}
