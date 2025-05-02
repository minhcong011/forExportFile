// dnSpy decompiler from Assembly-CSharp.dll class: EnemyMeta
using System;
using UnityEngine;

public class EnemyMeta : MonoBehaviour
{
	private void Awake()
	{
		this.enemyTransform = base.transform;
	}

	public void CopyCOnstructor(EnemyMeta meta)
	{
		this.path = meta.path;
		this.enemyTransform = meta.enemyTransform;
		this.speed = meta.speed;
		this.prefab = meta.prefab;
		this.stopPoint = meta.stopPoint;
		this.attackTime = meta.attackTime;
		this.damageToPlayer = meta.damageToPlayer;
		this.modelId = meta.modelId;
		this.health = meta.health;
		this.bulletBurst = meta.bulletBurst;
		this.damage = meta.damage;
		this.hitFactor = meta.hitFactor;
		this.fireRate = meta.fireRate;
		this.delayTime = meta.delayTime;
	}

	public PathManager path;

	public Transform enemyTransform;

	public float speed = 2f;

	public GameObject prefab;

	public int[] stopPoint = new int[]
	{
		2,
		4
	};

	public float attackTime = 2f;

	public float damageToPlayer = 2f;

	public string modelId;

	public int health;

	public int bulletBurst = 3;

	public float damage = 1f;

	public float hitFactor = 1f;

	public float fireRate = 2f;

	public float delayTime = 8f;
}
