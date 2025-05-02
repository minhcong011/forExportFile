// dnSpy decompiler from Assembly-CSharp.dll class: EnemySoldier
using System;
using System.Collections;
using UnityEngine;

public class EnemySoldier : CommonEnemyBehaviour
{
	private void OnEnable()
	{
		EventManager.gameRetried += this.GameRetried;
		EventManager.bulletFiredAlert += this.Alerted;
	}

	private void OnDisable()
	{
		EventManager.gameRetried -= this.GameRetried;
		EventManager.bulletFiredAlert -= this.Alerted;
	}

	private new void Awake()
	{
		base.Awake();
		this.enemyMovement = base.GetComponent<hoMove>();
		this.enemyMovement.speed = 2f;
	}

	private void Start()
	{
		this.collider = base.GetComponent<BoxCollider>();
		this.originalCollSize = this.collider.size;
		this.originalCollCenter = this.collider.center;
		this.refGameSceneCont = Singleton<GameController>.Instance.gameSceneController;
		this.allController = UnityEngine.Object.FindObjectOfType<AllGameControllers>();
		this.enemyAnimator = base.GetComponent<EnemyAnimationController>();
		this.player = this.GetCurrentPlayerTransform();
		if (this.enemyBehaviour == EnemyModel.Behaviour.idle || this.enemyBehaviour == EnemyModel.Behaviour.idleAndContinuousFire)
		{
			this.enemyAnimator.StopWalking();
			this.StopHoMove();
		}
		else
		{
			if (this.refGameSceneCont.canAlert)
			{
				this.enemyAnimator.StopWalking();
				this.StartRunning();
			}
			else
			{
				this.PauseHoMove();
			}
			this.collider.size = this.originalCollSize;
			this.collider.center = this.originalCollCenter;
		}
		base.transform.LookAt(this.GetCurrentPlayerTransform().position);
		base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
		this.fireTime = Time.time - 4f;
		this.startTime = Time.time;
		this.damageFactor = (float)Singleton<GameController>.Instance.SelectedLevel * 0.03f * (float)Singleton<GameController>.Instance.SelectedMission;
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
		this.enemyMovement.speed = base.speed;
		if (pathIndexRef != -1)
		{
			this.onceAlerted = true;
		}
	}

	public void StartWalking()
	{
		if (this.enemyBehaviour == EnemyModel.Behaviour.idle || this.enemyBehaviour == EnemyModel.Behaviour.idleAndContinuousFire)
		{
			return;
		}
		this.enemyMovement.speed = 1.3f;
		this.enemyState = EnemyModel.State.walking;
		this.enemyAnimator.StartWalking();
		this.enemyMovement.StartMove();
		this.StartHoMove();
		this.enemyMovement.ChangeSpeed(1f);
		this.enemyMovement.speed = 1.3f;
		base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
	}

	public void StartRunning()
	{
		if (this.enemyBehaviour == EnemyModel.Behaviour.idle || this.enemyBehaviour == EnemyModel.Behaviour.idleAndContinuousFire)
		{
			return;
		}
		this.enemyState = EnemyModel.State.running;
		this.enemyAnimator.StartRunning();
		this.enemyMovement.StartMove();
		this.StartHoMove();
		this.enemyMovement.ChangeSpeed(base.speed);
		this.enemyMovement.speed = base.speed;
		base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
	}

	public Transform GetCurrentPlayerTransform()
	{
		if (this.allController == null)
		{
			this.allController = UnityEngine.Object.FindObjectOfType<AllGameControllers>();
		}
		return this.allController.getCurrentController().transform;
	}

	public Transform GetCurrentPlayerTargetTransform()
	{
		if (this.allController == null)
		{
			this.allController = UnityEngine.Object.FindObjectOfType<AllGameControllers>();
		}
		if (this.allController.getCurrentController().targetPoint != null)
		{
			return this.allController.getCurrentController().targetPoint;
		}
		return this.allController.getCurrentController().transform;
	}

	public void reachedAtPoint(int point)
	{
		this.lastPoint = point;
		if (this.refGameSceneCont == null)
		{
			return;
		}
		if (this.enemyBehaviour == EnemyModel.Behaviour.idle || this.enemyBehaviour == EnemyModel.Behaviour.idleAndContinuousFire || this.enemyBehaviour == EnemyModel.Behaviour.stopAndFireOnAlert)
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
		if ((this.refGameSceneCont != null && !this.refGameSceneCont.canAlert) || !this.refGameSceneCont.isGameStarted)
		{
			return;
		}
		this.PauseHoMove();
		if (this.refGameSceneCont != null && this.refGameSceneCont.isGameOver && !this.gameOver)
		{
			this.gameOver = true;
			this.enemyAnimator.StopWalking();
			this.enemyAnimator.StopRunning();
			this.PauseHoMove();
			return;
		}
		if (this.enemyState == EnemyModel.State.dead || this.refGameSceneCont.isGameCompleted || this.refGameSceneCont.isGameOver)
		{
			return;
		}
		base.transform.LookAt(this.GetCurrentPlayerTransform().position);
		this.enemyAnimator.StopWalking();
		this.enemyAnimator.StopRunning();
		this.enemyState = EnemyModel.State.firing;
		if (this.enemyBehaviour == EnemyModel.Behaviour.stopAndFireContinuous)
		{
			this.isFirstReached = true;
		}
		if (this.typeId == 1 || this.typeId == 2 || this.typeId == 3)
		{
			base.InvokeRepeating("fire", 0.5f, base.fireRate);
			float time = UnityEngine.Random.Range(this.attackTime - 2f, this.attackTime);
			base.Invoke("HideOrRun", time);
		}
		else if (this.typeId == 4)
		{
			base.Invoke("fire", base.fireRate);
			base.Invoke("HideOrRun", this.attackTime);
		}
		else if (this.typeId == 5)
		{
			base.Invoke("GernadeFire", 0.2f);
			base.Invoke("HideOrRun", UnityEngine.Random.Range(this.attackTime - 1f, this.attackTime));
			this.collider.size = this.originalCollSize;
			this.collider.center = this.originalCollCenter;
		}
	}

	private void checkToFire()
	{
		if (this.enemyState == EnemyModel.State.dead || (this.enemyBehaviour != EnemyModel.Behaviour.idle && this.enemyBehaviour != EnemyModel.Behaviour.idleAndContinuousFire))
		{
			return;
		}
		this.collider.enabled = true;
		base.transform.LookAt(this.GetCurrentPlayerTransform().position);
		this.enemyAnimator.StopWalking();
		this.enemyAnimator.StopRunning();
		this.enemyAnimator.StopHideAnimation();
		this.PauseHoMove();
		if (this.gameOver)
		{
			return;
		}
		if (this.enemyBehaviour == EnemyModel.Behaviour.idle || this.enemyBehaviour == EnemyModel.Behaviour.idleAndContinuousFire)
		{
			this.collider.size = this.originalCollSize;
			this.collider.center = this.originalCollCenter;
		}
		this.enemyState = EnemyModel.State.firing;
		this.fireTime = Time.time;
		if (this.typeId == 1 || this.typeId == 2 || this.typeId == 3)
		{
			base.InvokeRepeating("fire", 0.5f, base.fireRate);
			float time = UnityEngine.Random.Range(this.attackTime - 2f, this.attackTime);
			base.Invoke("HideOrRun", time);
		}
		else if (this.typeId == 4)
		{
			base.Invoke("fire", 0.8f);
			base.Invoke("HideOrRun", UnityEngine.Random.Range(this.attackTime - 1f, this.attackTime));
		}
		else if (this.typeId == 5)
		{
			this.GernadeFire();
			base.Invoke("HideOrRun", UnityEngine.Random.Range(1.5f, 2f));
		}
	}

	private void GernadeFire()
	{
		this.PauseHoMove();
		this.enemyAnimator.StopWalking();
		if (this.gameOver)
		{
			return;
		}
		base.transform.LookAt(this.GetCurrentPlayerTransform().position);
		base.StartCoroutine(this.enemyGernadeFire());
		this.fireTime = Time.time;
	}

	public IEnumerator enemyGernadeFire()
	{
		this.enemyAnimator.PlayFireAnimation();
		this.playFireSound();
		yield return new WaitForSeconds(0.1f);
		yield break;
	}

	private void ThrowGernade()
	{
		if (this.gameOver)
		{
			return;
		}
		Vector3 position = this.GetCurrentPlayerTargetTransform().position;
		Vector3 vector = new Vector3(UnityEngine.Random.Range(-0.6f, 0.6f), UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f)) / 80f;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.bullet);
		gameObject.transform.localPosition = this.fireOrigin.position;
		gameObject.transform.forward = base.transform.forward;
		Physics.IgnoreCollision(this.collider, gameObject.GetComponent<CapsuleCollider>());
		GernadeAI component = gameObject.GetComponent<GernadeAI>();
		if (component != null)
		{
			component.setTarget(position);
			component.damage = this.damage;
		}
	}

	private void fire()
	{
		if (this.gameOver)
		{
			return;
		}
		this.PauseHoMove();
		this.enemyAnimator.StopWalking();
		base.transform.LookAt(this.GetCurrentPlayerTransform().position);
		base.transform.LookAt(this.GetCurrentPlayerTransform().position);
		RaycastHit raycastHit;
		if (Physics.Linecast(this.fireOrigin.position, this.GetCurrentPlayerTargetTransform().position, out raycastHit) && raycastHit.transform.gameObject.tag != "Player")
		{
			return;
		}
		base.StartCoroutine(this.enemyFire());
	}

	public IEnumerator enemyFire()
	{
		this.enemyAnimator.PlayFireAnimation();
		Vector3 playerPos = this.GetCurrentPlayerTargetTransform().position;
		Vector3 spread = new Vector3(UnityEngine.Random.Range(-0.02f, 0.02f), UnityEngine.Random.Range(-0.1f, 0.25f), 0f) / 10f;
		for (int i = 0; i < this.bulletBurst; i++)
		{
			GameObject fire;
			if (this.typeId != 4)
			{
				fire = ObjectPool.instance.GetObjectForType("Enemy_bullet_Pool", false);
			}
			else
			{
				fire = UnityEngine.Object.Instantiate<GameObject>(this.bullet, this.fireOrigin.position, this.fireOrigin.rotation);
			}
			fire.transform.position = this.fireOrigin.position;
			fire.transform.rotation = this.fireOrigin.rotation;
			Vector3 direction = base.transform.forward + spread;
			Vector3 dire = (playerPos - fire.transform.position).normalized;
			dire += spread;
			fire.transform.forward = dire;
			this.playFireSound();
			Damage_Tps bulletDamage = fire.GetComponent<Damage_Tps>();
			if (bulletDamage != null)
			{
				bulletDamage.Damage = this.damage;
			}
			if (this.muzzleFlash != null)
			{
				this.muzzleFlash.SetActive(true);
			}
			fire.SetActive(true);
			yield return new WaitForSeconds(0.06f);
			if (this.muzzleFlash != null)
			{
				this.muzzleFlash.SetActive(false);
			}
			float r = UnityEngine.Random.Range(0.1f, 0.15f);
			yield return new WaitForSeconds(r);
		}
		yield return new WaitForSeconds(0.2f);
		yield break;
	}

	private void HideOrRun()
	{
		if (this.enemyState == EnemyModel.State.dead)
		{
			return;
		}
		base.CancelInvoke("fire");
		this.enemyState = EnemyModel.State.hiding;
		this.enemyAnimator.StopWalking();
		if (this.enemyBehaviour != EnemyModel.Behaviour.idle && this.enemyBehaviour != EnemyModel.Behaviour.idleAndContinuousFire)
		{
			if (this.enemyBehaviour == EnemyModel.Behaviour.moveAndHide)
			{
				this.enemyAnimator.StopWalking();
				this.enemyBehaviour = EnemyModel.Behaviour.idle;
				this.behaviourId = 1;
				this.StopHoMove();
				this.enemyState = EnemyModel.State.hiding;
				this.enemyAnimator.PlayHideAnimation();
				this.PauseHoMove();
			}
			else if (this.enemyBehaviour == EnemyModel.Behaviour.stopAndFireContinuous)
			{
				this.StopHoMove();
				this.enemyBehaviour = EnemyModel.Behaviour.idleAndContinuousFire;
				this.enemyState = EnemyModel.State.idle;
			}
			else if (this.enemyBehaviour == EnemyModel.Behaviour.moveFireMove)
			{
				this.enemyAnimator.StartRunning();
				this.StartHoMove();
				this.enemyMovement.ChangeSpeed(base.speed);
				this.enemyState = EnemyModel.State.running;
				base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
				int num = UnityEngine.Random.Range(0, 8);
			}
		}
		else
		{
			this.enemyAnimator.StopWalking();
			this.enemyAnimator.StopRunning();
			if (this.enemyBehaviour == EnemyModel.Behaviour.idleAndContinuousFire)
			{
				this.collider.size = this.originalCollSize;
				this.collider.center = this.originalCollCenter;
				int num2 = UnityEngine.Random.Range(1, 4);
				if (num2 == 3)
				{
					this.enemyAnimator.PlayReloadAnimation();
				}
			}
			else if (this.enemyBehaviour == EnemyModel.Behaviour.idle)
			{
				this.enemyAnimator.PlayHideAnimation();
				if (this.enemyBehaviour == EnemyModel.Behaviour.idle)
				{
				}
			}
		}
	}

	public void playFireSound()
	{
		int num = UnityEngine.Random.Range(0, 3);
		if (num == 1 || num == 2)
		{
			Singleton<GameController>.Instance.soundController.PlayGivenSound(this.fireSound);
		}
		else if (this.fireSound != null && Constants.isSoundOn())
		{
			AudioSource.PlayClipAtPoint(this.fireSound, base.transform.position, 1f);
		}
	}

	public void gotHitAtBase()
	{
		this.enemyAnimator.PlayHit();
	}

	public void DiedAtBase()
	{
		this.canDie();
	}

	public void DiedWithCollider()
	{
		base.GetComponent<Animator>().enabled = false;
		UKRagdollController component = base.GetComponent<UKRagdollController>();
		if (component != null)
		{
			if (base.GetComponent<Rigidbody>() == null)
			{
				base.gameObject.AddComponent<Rigidbody>();
			}
			Rigidbody component2 = base.GetComponent<Rigidbody>();
			component2.velocity = base.transform.forward;
			component2.useGravity = true;
			if (this.canUseForce)
			{
				int num = UnityEngine.Random.Range(3000, 6000);
				Vector3 vector = -component2.velocity.normalized * (float)num;
				component2.AddForce(vector, ForceMode.Force);
				component2.AddTorque(vector, ForceMode.Force);
			}
			component.enabled = true;
			component.EnableRagdoll();
		}
	}

	private void PlayVoiceAfterTime()
	{
		int num = UnityEngine.Random.Range(0, 3);
		if (num != 1 || Constants.isSoundOn())
		{
		}
	}

	public void canDie()
	{
		base.CancelInvoke("HideOrRun");
		base.CancelInvoke("fire");
		if (this.pathIndex != -1)
		{
			Singleton<GameController>.Instance.gameSceneController.makePositionFree(this.pathIndex, 0);
		}
		if (this.isheadShot)
		{
		}
		this.enemyState = EnemyModel.State.dead;
		base.CancelInvoke();
		this.enemyAnimator.StopRunning();
		this.enemyAnimator.StopWalking();
		this.enemyAnimator.StopHideAnimation();
		this.StopHoMove();
		this.DiedWithCollider();
		base.Invoke("decrementWithDelay", this.delayDestroy);
		UnityEngine.Object.Destroy(base.gameObject, 4f);
		int num = UnityEngine.Random.Range(0, 2);
		if (num == 1 || num == 0)
		{
			Singleton<GameController>.Instance.soundController.PlayGivenSound(this.deadSound);
		}
		else if (this.deadSound != null && Constants.isSoundOn())
		{
			AudioSource.PlayClipAtPoint(this.deadSound, base.transform.position, 1f);
		}
		this.BreakParts();
	}

	private void decrementWithDelay()
	{
		int bodyId = 0;
		if (this.isheadShot)
		{
			bodyId = 1;
		}
		Singleton<GameController>.Instance.gameSceneController.IncrementEnemiesCountWithTypeNew(0, bodyId);
	}

	public void PauseHoMove()
	{
		if (this.enemyMovement != null && this.enemyMovement.enabled)
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
		if (this.enemyMovement != null && this.enemyMovement.enabled)
		{
			this.enemyMovement.Resume();
		}
	}

	private void ReduceHideCollider()
	{
		this.collider.size = new Vector3(this.originalCollSize.x, this.originalCollSize.y / 1.2f, this.originalCollSize.z);
		this.collider.center = new Vector3(this.originalCollCenter.x, this.originalCollCenter.y / 1.8f, this.originalCollCenter.z);
	}

	private new void Update()
	{
		base.Update();
		if (this.refGameSceneCont.isGameOver && !this.gameOver)
		{
			this.gameOver = true;
			return;
		}
		if (!this.refGameSceneCont.canAlert || this.enemyState == EnemyModel.State.dead || !this.refGameSceneCont.isGameStarted || this.refGameSceneCont.isGameCompleted || this.refGameSceneCont.isGameOver)
		{
			return;
		}
		if (!this.onceAlerted)
		{
			if (Time.time - this.alertTime > this.randAlertTime)
			{
				this.changeWalkTorunning();
				this.onceAlerted = true;
			}
			return;
		}
		if (Time.time - this.fireTime > this.delayTime)
		{
			this.checkToFire();
		}
		if (Time.time - this.startTime > 11f)
		{
			this.damage += this.damage * this.damageFactor;
			this.startTime = Time.time;
		}
	}

	private void GameRetried()
	{
		this.gameOver = false;
		if (this.enemyBehaviour == EnemyModel.Behaviour.stopAndFireContinuous && !this.isFirstReached)
		{
			this.StartHoMove();
			this.reachedAtPoint(this.lastPoint);
		}
		else
		{
			this.HideOrRun();
		}
	}

	private void Alerted()
	{
		float num = (float)UnityEngine.Random.Range(0, 3);
		this.alertTime = Time.time;
		this.changeWalkTorunning();
	}

	private void changeWalkTorunning()
	{
		if (this.enemyBehaviour == EnemyModel.Behaviour.moveAndHide || this.enemyBehaviour == EnemyModel.Behaviour.moveFireMove || this.enemyBehaviour == EnemyModel.Behaviour.stopAndFireContinuous)
		{
			this.enemyState = EnemyModel.State.running;
			this.enemyAnimator.StopWalking();
			this.enemyAnimator.StartRunning();
			this.StartHoMove();
			this.enemyMovement.ChangeSpeed(base.speed);
			this.enemyMovement.speed = base.speed;
			base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
		}
		else if (this.enemyBehaviour == EnemyModel.Behaviour.stopAndFireOnAlert)
		{
			this.enemyBehaviour = EnemyModel.Behaviour.idleAndContinuousFire;
			this.enemyState = EnemyModel.State.idle;
			this.checkToFire();
			this.onceAlerted = true;
		}
	}

	private void BreakParts()
	{
		for (int i = 0; i < this.breakingParts.Length; i++)
		{
			this.breakingParts[i].AddComponent<Rigidbody>();
			Rigidbody component = this.breakingParts[i].GetComponent<Rigidbody>();
			float d = (float)UnityEngine.Random.Range(3000, 7000);
			Vector3 vector = -component.velocity.normalized * d;
			component.AddRelativeForce(vector, ForceMode.Force);
			component.AddTorque(vector, ForceMode.Force);
		}
	}

	public int pathIndex = -1;

	public int[] stopPoint = new int[]
	{
		2,
		4
	};

	private GameSceneController refGameSceneCont;

	private AllGameControllers allController;

	private Vector3 originalCollSize;

	private Vector3 originalCollCenter;

	private float fireTime;

	private hoMove enemyMovement;

	private Transform player;

	private bool gameOver;

	private EnemyAnimationController enemyAnimator;

	private BoxCollider collider;

	private float startTime;

	private bool isFirstReached;

	private float damageFactor = 1f;

	public GameObject[] breakingParts;

	private int lastPoint;

	private float alertTime;

	private float randAlertTime;

	private bool onceAlerted;
}
