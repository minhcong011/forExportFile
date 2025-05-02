// dnSpy decompiler from Assembly-CSharp.dll class: DestructionVehicle
using System;
using UnityEngine;

public class DestructionVehicle : CommonEnemyBehaviour
{
	private new void Awake()
	{
		base.Awake();
		this.enemyMovement = base.GetComponent<hoMove>();
	}

	private void OnEnable()
	{
		EventManager.gameRetried += this.GameRetried;
	}

	private void OnDisable()
	{
		EventManager.gameRetried -= this.GameRetried;
	}

	private void Start()
	{
		this.refGameSceneCont = Singleton<GameController>.Instance.gameSceneController;
		this.canFind = false;
		if (this.behaviourId == 3 || this.behaviourId == 5)
		{
			this.enemyMovement.StartMove();
			this.StartHoMove();
			if (!this.refGameSceneCont.canAlert)
			{
				this.PauseHoMove();
			}
		}
		else
		{
			this.PauseHoMove();
			this.lookPlayer = true;
		}
		this.leftEffect.SetActive(false);
		this.rightEffect.SetActive(false);
		this.centerEffect.SetActive(false);
	}

	public void setInitials(EnemyMeta meta, int pathIndexRef = -1)
	{
		base.modelId = meta.modelId;
		base.speed = meta.speed;
		this.attackTime = meta.attackTime;
		this.stopPoint = meta.stopPoint;
		this.pathIndex = pathIndexRef;
		this.damage = meta.damage;
		base.fireRate = meta.fireRate;
		this.health = (float)meta.health;
		this.delayTime = meta.delayTime;
		string[] array = base.modelId.Split(new char[]
		{
			'_'
		});
		this.enemyId = (int)Convert.ToInt16(array[0]);
		this.typeId = (int)Convert.ToInt16(array[1]);
		this.behaviourId = (int)Convert.ToInt16(array[2]);
		this.bulletBurst = meta.bulletBurst;
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
		default:
			this.enemyBehaviour = EnemyModel.Behaviour.idle;
			break;
		}
		this.enemyMovement.speed = base.speed;
	}

	public void StartMoving()
	{
		if (this.enemyBehaviour == EnemyModel.Behaviour.idle || this.enemyBehaviour == EnemyModel.Behaviour.idleAndContinuousFire)
		{
			return;
		}
		this.enemyState = EnemyModel.State.walking;
		this.enemyMovement.StartMove();
		this.StartHoMove();
		base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
		if (Constants.isSoundOn())
		{
			base.GetComponent<AudioSource>().Play();
		}
	}

	public void reachedAtPoint(int point)
	{
		if (this.refGameSceneCont == null)
		{
			return;
		}
		if (this.enemyBehaviour == EnemyModel.Behaviour.idle || this.enemyBehaviour == EnemyModel.Behaviour.idleAndContinuousFire)
		{
			return;
		}
		if (this.refGameSceneCont != null && this.refGameSceneCont.isGameOver && !this.gameOver)
		{
			this.gameOver = true;
			return;
		}
		if (this.enemyBehaviour == EnemyModel.Behaviour.moving)
		{
			return;
		}
		if (this.enemyState == EnemyModel.State.dead || !this.refGameSceneCont.isGameStarted || this.refGameSceneCont.isGameCompleted || this.refGameSceneCont.isGameOver)
		{
			return;
		}
		bool flag = false;
		foreach (int num in this.stopPoint)
		{
			if (num == point)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			return;
		}
		this.PauseHoMove();
		this.lookPlayer = true;
		this.enemyState = EnemyModel.State.idle;
		if (this.enemyBehaviour == EnemyModel.Behaviour.moveAndHide)
		{
			this.behaviourId = 1;
			this.enemyBehaviour = EnemyModel.Behaviour.idle;
		}
		else if (this.enemyBehaviour == EnemyModel.Behaviour.moveFireMove)
		{
			this.behaviourId = 1;
			this.enemyBehaviour = EnemyModel.Behaviour.idle;
		}
	}

	public void setDealyPoints()
	{
		hoMove component = base.GetComponent<hoMove>();
		if (component != null)
		{
		}
	}

	public void DoAttack()
	{
		int num = UnityEngine.Random.Range(4, 7);
		base.InvokeRepeating("shootViaMachine", this.delayTime, 0.2f);
	}

	private void shootViaMachine()
	{
		if (Vector3.Distance(this.GetCurrentPlayerTransform().position, base.transform.position) > 270f)
		{
			return;
		}
		if (this.gameOver)
		{
			base.CancelInvoke("findTarget");
			base.CancelInvoke("shootViaMachine");
			base.CancelInvoke("shootViaRocket");
		}
		base.GetComponent<TankWeaponController>().machineFire();
	}

	private void shootViaRocket()
	{
		base.GetComponent<TankWeaponController>().tankRocket();
	}

	private void findTarget()
	{
		if (base.gameObject)
		{
			this.canFind = true;
			this.nozzle.transform.LookAt(this.GetCurrentPlayerTransform());
			if (this.selectedType == DestructionVehicle.enemyType.tank)
			{
				this.nozzle.transform.eulerAngles = new Vector3(-90f, this.nozzle.transform.eulerAngles.y + 2f, 0f);
			}
			else
			{
				this.nozzle.transform.eulerAngles = new Vector3(this.nozzle.transform.eulerAngles.x, this.nozzle.transform.eulerAngles.y + 2f, 0f);
			}
		}
	}

	private new void Update()
	{
		if (this.lookPlayer)
		{
			Quaternion b = Quaternion.LookRotation(this.GetCurrentPlayerTransform().position - base.transform.position);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, 0.04f);
		}
		if (UnityEngine.Input.GetKey(KeyCode.P))
		{
			this.DoAttack();
		}
		if (!this.gameOver && (this.refGameSceneCont.isGameCompleted || this.refGameSceneCont.isGameOver))
		{
			this.gameOver = true;
			base.CancelInvoke("findTarget");
			base.CancelInvoke("shootViaMachine");
			base.CancelInvoke("shootViaRocket");
		}
		if (!this.refGameSceneCont.canAlert || !this.refGameSceneCont.isGameStarted || this.refGameSceneCont.isGameCompleted || this.refGameSceneCont.isGameOver)
		{
			this.canAttack = false;
			return;
		}
		if (this.refGameSceneCont.canAlert && !this.canFind)
		{
			this.canFind = true;
			this.DoAttack();
		}
		if (this.canFind)
		{
			this.nozzle.transform.LookAt(this.GetCurrentPlayerTransform());
			if (this.selectedType == DestructionVehicle.enemyType.tank)
			{
				this.nozzle.transform.eulerAngles = new Vector3(-90f, this.nozzle.transform.eulerAngles.y + 2f, 0f);
			}
			else
			{
				this.nozzle.transform.eulerAngles = new Vector3(this.nozzle.transform.eulerAngles.x, this.nozzle.transform.eulerAngles.y + 2f, 0f);
			}
		}
	}

	public void PauseHoMove()
	{
		if (this.enemyMovement != null)
		{
			this.enemyMovement.Pause();
		}
	}

	public void StopHoMove()
	{
		if (this.enemyMovement != null)
		{
			this.enemyMovement.Stop();
			this.enemyMovement.speed = 0f;
			this.enemyMovement.enabled = false;
		}
	}

	public void StartHoMove()
	{
		if (this.enemyMovement != null)
		{
			this.enemyMovement.Resume();
		}
	}

	public Transform GetCurrentPlayerTransform()
	{
		return Singleton<GameController>.Instance.refAllController.getCurrentController().transform;
	}

	public void CancelInvokesScript()
	{
		base.CancelInvoke("findTarget");
		base.CancelInvoke("shootViaMachine");
		base.CancelInvoke("shootViaRocket");
	}

	public void DiedAtBase()
	{
		base.gameObject.GetComponent<hoMove>().Stop();
		base.gameObject.GetComponent<Rigidbody>().useGravity = true;
		base.gameObject.GetComponent<Rigidbody>().isKinematic = false;
		base.gameObject.GetComponent<Rigidbody>().AddRelativeForce(2000f, 3000f, 2000f, ForceMode.Force);
		base.gameObject.GetComponent<Rigidbody>().AddRelativeTorque(1000f, 4000f, 8890f);
		UnityEngine.Object.Destroy(base.gameObject, 5.3f);
		this.CancelInvokesScript();
		Singleton<GameController>.Instance.gameSceneController.IncrementEnemiesCountWithTypeNew(2, 0);
		if (this.pathIndex != -1)
		{
			Singleton<GameController>.Instance.gameSceneController.makePositionFree(this.pathIndex, this.enemyId);
		}
	}

	public void gotHitAtBase()
	{
		if (this.health >= 0f)
		{
			if (this.health >= 220f && this.health < 300f && (this.selectedType.Equals(DestructionVehicle.enemyType.tank) || this.selectedType.Equals(DestructionVehicle.enemyType.heli)))
			{
				this.centerEffect.SetActive(true);
			}
			if (this.health <= 200f && (this.selectedType.Equals(DestructionVehicle.enemyType.tank) || this.selectedType.Equals(DestructionVehicle.enemyType.heli)))
			{
				this.leftEffect.SetActive(true);
				this.rightEffect.SetActive(true);
			}
		}
		this.healthSlider.transform.LookAt(this.GetCurrentPlayerTransform().position);
	}

	private void GameRetried()
	{
		this.gameOver = false;
		this.canFind = false;
	}

	public int[] stopPoint = new int[]
	{
		2,
		4
	};

	public GameObject nozzle;

	private bool canFind;

	private bool canAttack;

	private bool gameOver;

	public bool isMoveable;

	public GameObject leftEffect;

	public GameObject rightEffect;

	public GameObject centerEffect;

	public GameObject fakeMode;

	public GameObject centrePos;

	public int pathIndex = -1;

	private hoMove enemyMovement;

	private Vector3 desiredPos;

	private bool lookPlayer;

	private GameSceneController refGameSceneCont;

	public DestructionVehicle.enemyType selectedType = DestructionVehicle.enemyType.heli;

	public enum enemyType
	{
		tank,
		heli,
		humvee
	}
}
