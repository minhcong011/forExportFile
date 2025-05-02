// dnSpy decompiler from Assembly-CSharp.dll class: EnemyModel
using System;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
	public string modelId { get; set; }

	public string prefabName { get; set; }

	public Vector3 pos { get; set; }

	public float speed { get; set; }

	public float hitFactor { get; set; }

	public float fireRate { get; set; }

	public void Awake()
	{
		this.refModelMeta = new EnemyModelMeta();
	}

	public void setModelFromParams(EnemyModelMeta meta)
	{
		if (meta == null)
		{
			MonoBehaviour.print("UK :: EnemyModel is null : Returning");
			return;
		}
		this.refModelMeta.CopyMeta(meta);
		string[] array = this.refModelMeta.modelMetaId.Split(new char[]
		{
			'_'
		});
		this.enemyId = (int)Convert.ToInt16(array[0]);
		this.typeId = (int)Convert.ToInt16(array[1]);
		this.behaviourId = (int)Convert.ToInt16(array[2]);
		this.modelId = this.refModelMeta.modelMetaId;
		this.prefabName = this.refModelMeta.prefabName;
		this.health = (float)this.refModelMeta.health;
		this.targetHealth = this.health;
		this.startingHealth = this.health;
		this.damage = this.refModelMeta.damage;
		this.speed = this.refModelMeta.speed;
		this.attackTime = this.refModelMeta.attackTime;
		this.hitFactor = this.refModelMeta.hitFactor;
		this.delayTime = this.refModelMeta.delayTime;
		switch (this.behaviourId)
		{
		case 1:
			this.enemyBehaviour = EnemyModel.Behaviour.idle;
			break;
		case 2:
			this.enemyBehaviour = EnemyModel.Behaviour.idleAndContinuousFire;
			break;
		case 3:
			this.enemyBehaviour = EnemyModel.Behaviour.moveAndHide;
			break;
		case 4:
			this.enemyBehaviour = EnemyModel.Behaviour.moveFireMove;
			break;
		case 5:
			this.enemyBehaviour = EnemyModel.Behaviour.stopAndFireContinuous;
			break;
		case 6:
			this.enemyBehaviour = EnemyModel.Behaviour.moving;
			break;
		case 7:
			this.enemyBehaviour = EnemyModel.Behaviour.fireAndHideLeft;
			break;
		case 8:
			this.enemyBehaviour = EnemyModel.Behaviour.fireAndHideRight;
			break;
		case 9:
			this.enemyBehaviour = EnemyModel.Behaviour.stopAndFireOnAlert;
			break;
		default:
			this.enemyBehaviour = EnemyModel.Behaviour.idle;
			break;
		}
	}

	[HideInInspector]
	private EnemyModelMeta refModelMeta;

	public int enemyId;

	public int typeId;

	public int behaviourId;

	public float health;

	public float targetHealth = 100f;

	public float startingHealth;

	public int bulletBurst = 3;

	public float damage;

	public float attackTime;

	public float delayTime;

	public EnemyModel.Behaviour enemyBehaviour;

	public EnemyModel.State enemyState;

	public enum Behaviour
	{
		idle,
		idleAndContinuousFire,
		moving,
		moveAndHide,
		moveFireMove,
		stopAndFireContinuous,
		fireAndHideLeft,
		fireAndHideRight,
		stopAndFireOnAlert
	}

	public enum State
	{
		idle,
		walking,
		running,
		firing,
		hiding,
		dead
	}
}
