// dnSpy decompiler from Assembly-CSharp.dll class: AnimalController
using System;
using UnityEngine;
using UnityEngine.AI;

public class AnimalController : CommonAnimalModel
{
	private void OnEnable()
	{
		EventManager.gameOver += this.GameOver;
		EventManager.gameStarted += this.GameStarted;
		EventManager.gameRetried += this.GameRetried;
		EventManager.bulletFiredAlert += this.Alerted;
	}

	private void OnDisable()
	{
		EventManager.gameStarted -= this.GameStarted;
		EventManager.gameOver -= this.GameOver;
		EventManager.gameRetried -= this.GameRetried;
		EventManager.bulletFiredAlert -= this.Alerted;
	}

	private new void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
		this.startingPos = base.transform.position;
		base.Start();
		this.animationCtrl = base.GetComponent<AnimalAnimationController>();
		this.agent.enabled = true;
		if (this.behaviourId == 2)
		{
			this.PlayWalk();
		}
		else if (this.behaviourId == 3)
		{
			this.PlayWalk();
		}
		else if (this.behaviourId == 4 || this.behaviourId == 1)
		{
			this.PlayWalk();
		}
		this.lastAttackTime = Time.time;
		this.playerPos = this.GetPlayerPos();
		base.InvokeRepeating("GetPlayerPos", 0f, 3f);
		this.randomRange = UnityEngine.Random.Range(-4f, 4f);
		this.positionChangeTime = Time.time;
		this.stayTime = (float)UnityEngine.Random.Range(3, 7);
		this.positionChangeTimeConstant = (float)UnityEngine.Random.Range(6, 9);
	}

	private void PlayWalk()
	{
		this.animalState = CommonAnimalModel.State.walking;
		this.animationCtrl.PlayWalk();
		this.agent.destination = this.DestinationPos;
		this.agent.speed = this.refModelMeta.walkSpeed;
		this.randDestinationPos = this.DestinationPos;
	}

	private void PlayRun()
	{
		this.animalState = CommonAnimalModel.State.running;
		this.animationCtrl.PlayRunning();
		this.agent.destination = this.DestinationPos;
		this.agent.speed = this.refModelMeta.runSpeed;
		this.randDestinationPos = this.DestinationPos;
	}

	public void ApplyDamage(float damageVal, string partName = "body")
	{
		if (this.animalState == CommonAnimalModel.State.dead || this.isGameOver)
		{
			return;
		}
		int bodyId = 0;
		if (partName != null)
		{
			if (!(partName == "body"))
			{
				if (partName == "Head")
				{
					bodyId = 2;
				}
			}
			else
			{
				bodyId = 1;
			}
		}
		MonoBehaviour.print("Got it " + partName);
		this.health -= damageVal;
		if (this.health <= 0f)
		{
			if (partName == "Head" && Singleton<GameController>.Instance.achievementManager != null)
			{
				Singleton<GameController>.Instance.achievementManager.updateAchievement(9);
			}
			this.MakeDead(false, bodyId);
		}
		else
		{
			if (Constants.isSoundOn())
			{
				int num = UnityEngine.Random.Range(0, this.hitSound.Length);
				if (this.hitSound.Length > 0)
				{
					AudioSource.PlayClipAtPoint(this.hitSound[num], base.transform.position);
				}
			}
			base.gameObject.SendMessage("gotHitAtBase", SendMessageOptions.DontRequireReceiver);
		}
		if (this.animalID == 1 || this.typeId == 1 || this.animalID == 3)
		{
			Singleton<GameController>.Instance.gameSceneController.ShowShotBadge(partName);
		}
	}

	public void MakeDead(bool deadByOtherAnimal = false, int bodyId = 0)
	{
		this.health = 0f;
		this.stopNavMeshAgent();
		this.animalState = CommonAnimalModel.State.dead;
		this.animationCtrl.PlayDieAnim();
		base.gameObject.tag = "Untagged";
		base.GetComponent<BoxCollider>().enabled = false;
		if (this.animalID == 3)
		{
			Singleton<GameController>.Instance.gameSceneController.IncrementEnemiesCountWithTypeNew(3, bodyId);
			if (Singleton<GameController>.Instance.achievementManager != null)
			{
				Singleton<GameController>.Instance.achievementManager.updateAchievement(1);
			}
			if (Singleton<GameController>.Instance.taskManager != null)
			{
				Singleton<GameController>.Instance.taskManager.getCurrentDayModel().updateTask(3);
			}
		}
		else if (this.typeId != 1)
		{
			if (deadByOtherAnimal)
			{
				Singleton<GameController>.Instance.gameSceneController.IncrementEnemiesCountWithTypeNew(11, 0);
			}
			else
			{
				Singleton<GameController>.Instance.gameSceneController.IncrementEnemiesCountWithTypeNew(10, 0);
			}
		}
		if (this.isOtherAnimalPointed)
		{
			Singleton<GameController>.Instance.gameSceneController.AnimalInSafe();
		}
		UnityEngine.Object.Destroy(base.gameObject, 8f);
		if (this.pathIndex != -1)
		{
			Singleton<GameController>.Instance.gameSceneController.makePositionFree(this.pathIndex, 3);
		}
		if (Constants.isSoundOn())
		{
			int num = UnityEngine.Random.Range(0, this.deadClips.Length);
			if (this.deadClips.Length > 0)
			{
				AudioSource.PlayClipAtPoint(this.deadClips[num], base.transform.position);
			}
		}
	}

	private void SetIdle()
	{
		this.animalState = CommonAnimalModel.State.idle;
		this.stopNavMeshAgent();
		this.animationCtrl.PlayIdle();
	}

	private void Update()
	{
		if (this.animalState == CommonAnimalModel.State.dead || this.isGameOver || !this.isGameStarted)
		{
			return;
		}
		if (!this.isfollowingPlayer)
		{
			if (this.thisAnimalPointed)
			{
				return;
			}
			if (this.isAlerted)
			{
				Vector3 vector2;
				if (this.animalState != CommonAnimalModel.State.running)
				{
					Vector3 vector;
					if (this.RandomPoint(base.transform.position, 200f, out vector))
					{
						this.agent.enabled = true;
						this.agent.ResetPath();
						this.agent.destination = vector;
						this.agent.speed = this.refModelMeta.runSpeed;
						this.DestinationPos = vector;
						this.animalState = CommonAnimalModel.State.running;
						this.animationCtrl.PlayRunning();
						this.positionChangeTime = Time.time;
					}
				}
				else if ((Vector3.Distance(base.transform.position, this.DestinationPos) <= 5f || Time.time - this.positionChangeTime > this.positionChangeTimeConstant) && this.RandomPoint(base.transform.position, 200f, out vector2))
				{
					this.agent.enabled = true;
					this.agent.ResetPath();
					this.animationCtrl.PlayRunning();
					this.agent.destination = vector2;
					this.agent.speed = this.refModelMeta.runSpeed;
					this.DestinationPos = vector2;
					this.positionChangeTime = Time.time;
					this.animalState = CommonAnimalModel.State.running;
				}
			}
			else if (this.animalState == CommonAnimalModel.State.walking || this.animalState == CommonAnimalModel.State.running)
			{
				if (Vector3.Distance(base.transform.position, this.DestinationPos) <= 5f || Time.time - this.positionChangeTime > this.positionChangeTimeConstant)
				{
					this.SetIdle();
					this.lastStayTime = Time.time;
					if (this.idleSound.Length > 0 && Constants.isSoundOn())
					{
						base.GetComponent<AudioSource>().PlayOneShot(this.idleSound[0]);
					}
				}
			}
			else if (this.animalState == CommonAnimalModel.State.idle && Time.time - this.lastStayTime > this.stayTime)
			{
				this.animalState = CommonAnimalModel.State.walking;
				Vector3 vector3;
				if (this.RandomPoint(base.transform.position, 200f, out vector3))
				{
					this.agent.enabled = true;
					this.agent.ResetPath();
					this.animationCtrl.PlayWalk();
					this.agent.destination = vector3;
					this.agent.speed = this.refModelMeta.walkSpeed;
					this.DestinationPos = vector3;
					this.positionChangeTime = Time.time;
				}
			}
		}
		if (this.canFollowPlayer && !this.canFollowAnimal)
		{
			if (this.CheckForAlert())
			{
				if (Vector3.Distance(base.transform.position, this.playerPos) >= this.refModelMeta.stopAtDistanceToPlayer)
				{
					if (this.animalState != CommonAnimalModel.State.running)
					{
						this.agent.enabled = true;
						this.animalState = CommonAnimalModel.State.running;
						this.animationCtrl.PlayRunning();
						this.agent.speed = this.refModelMeta.runSpeed;
					}
					this.animalState = CommonAnimalModel.State.running;
					this.agent.destination = this.playerPos;
					this.DestinationPos = this.playerPos;
					this.isfollowingPlayer = true;
				}
				else
				{
					if (this.animalState != CommonAnimalModel.State.idle && this.animalState != CommonAnimalModel.State.attacking)
					{
						this.SetAttackingState();
						int num = UnityEngine.Random.Range(0, this.attackSound.Length);
						if (this.attackSound.Length > 0)
						{
							AudioSource.PlayClipAtPoint(this.attackSound[num], base.transform.position);
						}
					}
					if (this.animalState != CommonAnimalModel.State.dead)
					{
						base.transform.LookAt(this.playerPos);
					}
					base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
				}
			}
			else if (this.isAlerted)
			{
			}
			if (this.canFollowPlayer && this.animalState == CommonAnimalModel.State.attacking && Time.time - this.lastAttackTime > 3.5f)
			{
				this.attackOnPlayerDetected();
				this.lastAttackTime = Time.time;
				int num2 = UnityEngine.Random.Range(0, this.attackSound.Length);
				if (this.attackSound.Length > 0)
				{
					AudioSource.PlayClipAtPoint(this.attackSound[num2], base.transform.position);
				}
				base.transform.LookAt(this.playerPos);
				base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
			}
		}
		else if (this.canFollowPlayer && this.canFollowAnimal)
		{
			if (this.CheckForAlert())
			{
				if (Vector3.Distance(base.transform.position, this.playerPos) >= 17f)
				{
					if (this.animalState != CommonAnimalModel.State.running)
					{
						this.agent.enabled = true;
						this.animalState = CommonAnimalModel.State.running;
						this.animationCtrl.PlayRunning();
						this.agent.speed = this.refModelMeta.runSpeed;
					}
					this.animalState = CommonAnimalModel.State.running;
					this.agent.destination = this.playerPos;
					this.DestinationPos = this.playerPos;
					this.isfollowingPlayer = true;
				}
				else
				{
					if (this.animalState != CommonAnimalModel.State.idle && this.animalState != CommonAnimalModel.State.attacking)
					{
						this.SetAttackingState();
						int num3 = UnityEngine.Random.Range(0, this.attackSound.Length);
						if (this.attackSound.Length > 0)
						{
							AudioSource.PlayClipAtPoint(this.attackSound[num3], base.transform.position);
						}
					}
					if (this.animalState != CommonAnimalModel.State.dead)
					{
						base.transform.LookAt(this.playerPos);
					}
					base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
				}
			}
			else if (this.isAlerted)
			{
			}
			if (this.canFollowPlayer && this.animalState == CommonAnimalModel.State.attacking && Time.time - this.lastAttackTime > 3.5f)
			{
				this.attackOnPlayerDetected();
				this.lastAttackTime = Time.time;
				int num4 = UnityEngine.Random.Range(0, this.attackSound.Length);
				if (this.attackSound.Length > 0)
				{
					AudioSource.PlayClipAtPoint(this.attackSound[num4], base.transform.position);
				}
				base.transform.LookAt(this.playerPos);
				base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
			}
		}
		else if (!this.canFollowPlayer && this.canFollowAnimal && Time.time - this.lastAttackTime > 13f)
		{
			if (this.animalState != CommonAnimalModel.State.followingAnimal && this.animalState != CommonAnimalModel.State.attacking)
			{
				this.GetTargetAnimal();
			}
			if (this.isOtherAnimalPointed && this.targetAnimal != null && this.animalState != CommonAnimalModel.State.attacking)
			{
				if (this.animalState != CommonAnimalModel.State.followingAnimal)
				{
					this.agent.enabled = true;
					this.animationCtrl.PlayRunning();
					this.agent.speed = this.refModelMeta.runSpeed + 3f;
					this.isfollowingPlayer = true;
					this.animalState = CommonAnimalModel.State.followingAnimal;
				}
				this.agent.destination = this.targetAnimal.transform.position;
				this.DestinationPos = this.targetAnimal.gameObject.transform.position;
				if (Vector3.Distance(base.transform.position, this.targetAnimal.transform.position) < 9f)
				{
					this.stopNavMeshAgent();
					this.animalState = CommonAnimalModel.State.attacking;
					this.animationCtrl.PlayAttackAnim();
					this.animalDetectTime = Time.time;
					this.targetAnimal.GetComponent<AnimalController>().animalTargetted();
				}
			}
			if (this.isOtherAnimalPointed && this.targetAnimal != null && this.animalState == CommonAnimalModel.State.attacking)
			{
				this.isfollowingPlayer = true;
				if (Vector3.Distance(base.transform.position, this.targetAnimal.gameObject.transform.position) > 10f)
				{
					this.animalState = CommonAnimalModel.State.followingAnimal;
				}
				if (Time.time - this.animalDetectTime > this.refModelMeta.attackTime)
				{
					this.ResumeAfterkilling();
				}
			}
		}
	}

	private void ResumeAfterkilling()
	{
		if (this.targetAnimal == null)
		{
			return;
		}
		this.targetAnimal.GetComponent<AnimalController>().MakeDead(true, 0);
		this.isfollowingPlayer = false;
		this.lastAttackTime = Time.time;
		this.isOtherAnimalPointed = false;
		this.targetAnimal = null;
		this.SetIdle();
	}

	public void animalTargetted()
	{
		this.isAlerted = false;
		this.stopNavMeshAgent();
		this.SetIdle();
		this.thisAnimalPointed = true;
	}

	private void GetTargetAnimal()
	{
		AnimalController[] array = UnityEngine.Object.FindObjectsOfType<AnimalController>();
		if (array.Length > 1)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].animalID != this.animalID && this.typeId == 1 && array[i].typeId != 1 && Vector3.Distance(base.transform.position, array[i].gameObject.transform.position) < 180f)
				{
					this.isOtherAnimalPointed = true;
					this.targetAnimal = array[i].gameObject;
					array[i].Alerted();
					Singleton<GameController>.Instance.gameSceneController.AnimalPointed();
					return;
				}
			}
		}
	}

	private void SetAttackingState()
	{
		this.animalState = CommonAnimalModel.State.attacking;
		this.stopNavMeshAgent();
		this.animationCtrl.PlayAttackAnim();
	}

	private void stopNavMeshAgent()
	{
		if (this.agent.isActiveAndEnabled)
		{
			this.agent.Stop(true);
			this.agent.speed = 0f;
			this.agent.ResetPath();
			this.agent.enabled = false;
		}
	}

	private void attackOnPlayerDetected()
	{
		Singleton<GameController>.Instance.gameSceneController.SetPlayersHealth(22f);
	}

	private Vector3 GetPlayerPos()
	{
		this.playerPos = Singleton<GameController>.Instance.refAllController.getCurrentController().gameObject.transform.position;
		this.playerPos = new Vector3(this.playerPos.x + this.randomRange, this.playerPos.y, this.playerPos.z + this.randomRange);
		return this.playerPos;
	}

	private bool RandomPoint(Vector3 center, float range, out Vector3 result)
	{
		for (int i = 0; i < 10; i++)
		{
			Vector3 sourcePosition = center + UnityEngine.Random.insideUnitSphere * range;
			NavMeshHit navMeshHit;
			if (NavMesh.SamplePosition(sourcePosition, out navMeshHit, range, -1))
			{
				result = navMeshHit.position;
				if (Vector3.Distance(base.transform.position, result) > 30f)
				{
					return true;
				}
			}
		}
		result = Vector3.zero;
		return false;
	}

	private bool RandomPointForPlayer(Vector3 center, float range, out Vector3 result)
	{
		center = this.playerPos;
		Vector3 normalized = (center - base.transform.position).normalized;
		for (int i = 0; i < 20; i++)
		{
			Vector3 sourcePosition = center + normalized * 4f;
			NavMeshHit navMeshHit;
			if (NavMesh.SamplePosition(sourcePosition, out navMeshHit, range, -1))
			{
				result = navMeshHit.position;
				if (Vector3.Distance(center, result) > 4f)
				{
					return true;
				}
			}
		}
		result = Vector3.zero;
		return false;
	}

	public bool CheckForAlert()
	{
		if (this.canFollowPlayer && this.isAlerted)
		{
			return true;
		}
		if (this.canFollowPlayer && Time.time - this.lastAttackTime > 14f && Vector3.Distance(this.playerPos, base.transform.position) < 140f)
		{
			this.isAlerted = true;
			return true;
		}
		return false;
	}

	public void Alerted()
	{
		if (!this.thisAnimalPointed)
		{
			this.isAlerted = true;
			this.positionChangeTime = Time.time - 7f;
		}
		base.CancelInvoke("cancelAlert");
		base.Invoke("cancelAlert", (float)UnityEngine.Random.Range(8, 10));
	}

	private void cancelAlert()
	{
		if (!this.thisAnimalPointed && !this.isOtherAnimalPointed)
		{
			this.isAlerted = false;
		}
	}

	public void GameStarted()
	{
		this.isGameStarted = true;
		this.lastAttackTime = Time.time;
	}

	private void GameOver()
	{
		this.isGameOver = true;
	}

	private void GameRetried()
	{
		this.isGameStarted = true;
		this.isGameOver = false;
		this.lastAttackTime += 5f;
	}

	public AnimalAnimationController animationCtrl;

	private NavMeshAgent agent;

	private bool isfollowingPlayer;

	public bool isAlerted;

	public bool isGameOver;

	public bool isGameStarted;

	private float lastAttackTime;

	private float lastStayTime;

	private float animalDetectTime;

	private float positionChangeTime;

	private float stayTime;

	private float positionChangeTimeConstant;

	private Vector3 playerPos;

	private float randomRange;

	private bool isOtherAnimalPointed;

	private bool thisAnimalPointed;

	private GameObject targetAnimal;
}
