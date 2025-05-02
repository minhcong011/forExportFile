// dnSpy decompiler from Assembly-CSharp.dll class: EnemyBehavior
using System;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
	private void Start()
	{
		this.originalCollSize = base.GetComponent<BoxCollider>().size;
		this.originalCollCenter = base.GetComponent<BoxCollider>().center;
		this.refGameSceneCont = Singleton<GameController>.Instance.gameSceneController;
		this.allController = UnityEngine.Object.FindObjectOfType<AllGameControllers>();
		this.anim = base.GetComponent<Animator>();
		this.anim.SetBool("canWalk", true);
		this.current_status = EnemyBehavior.EnemyStatus.Petrolling;
		this.player = this.GetCurrentPlayerTransform();
		this.fireTime = Time.time - 8f;
		if (!this.canMove)
		{
			this.anim.SetBool("canWalk", false);
			this.StopHoMove();
			MonoBehaviour.print("in it");
			this.anim.SetBool("canHide", true);
		}
		if (this.runningSoldier)
		{
			this.anim.SetBool("canWalk", true);
			this.anim.SetBool("canRun", false);
			this.anim.SetBool("canFire", false);
			this.anim.SetBool("canStand", true);
			if (this.refGameSceneCont.canAlert)
			{
				this.anim.SetBool("canWalk", false);
				this.anim.SetBool("canRun", true);
			}
		}
		this.timeLastHit = Time.time;
		base.transform.LookAt(this.GetCurrentPlayerTransform().position);
	}

	public void AlertEnemy()
	{
		base.Invoke("AssignHidingPos", 1f);
	}

	public Transform GetCurrentPlayerTransform()
	{
		if (this.allController == null)
		{
			this.allController = UnityEngine.Object.FindObjectOfType<AllGameControllers>();
		}
		return this.allController.getCurrentController().transform;
	}

	public void reachedAtPoint(int point)
	{
		if (!this.isStopAndFire)
		{
			return;
		}
		if (this.refGameSceneCont.isGameOver && !this.gameOver)
		{
			this.gameOver = true;
			this.anim.SetBool("canRun", false);
			this.anim.SetBool("canWalk", false);
			this.PauseHoMove();
			return;
		}
		if (!this.refGameSceneCont.canAlert || this.current_status == EnemyBehavior.EnemyStatus.Die || !this.refGameSceneCont.isGameStarted || this.refGameSceneCont.isGameCompleted || this.refGameSceneCont.isGameOver)
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
		float num2 = Vector3.Distance(base.transform.position, this.GetCurrentPlayerTransform().position);
		if (num2 > this.attackDistance)
		{
			return;
		}
		base.transform.LookAt(this.GetCurrentPlayerTransform().position);
		this.anim.SetBool("canRun", false);
		this.anim.SetBool("canWalk", false);
		this.anim.SetBool("canFire", false);
		this.anim.SetBool("canHide", false);
		this.current_status = EnemyBehavior.EnemyStatus.Shooting;
		if (this.selected_terroristType == EnemyBehavior.TerroristType.type1)
		{
			base.InvokeRepeating("fire", 0.5f, 0.8f);
			int num3 = UnityEngine.Random.Range(5, 10);
			base.Invoke("HideOrRun", (float)num3);
		}
		else if (this.selected_terroristType == EnemyBehavior.TerroristType.type2)
		{
			base.Invoke("fire", 0.7f);
			base.Invoke("HideOrRun", 8f);
		}
	}

	private void checkToFire()
	{
		if (this.isStopAndFire || this.enemyBehaviourCheck == EnemyBehavior.EnemyBehaviorCheck.stopAndFireContinuous)
		{
			return;
		}
		if (this.current_status.Equals(EnemyBehavior.EnemyStatus.Die))
		{
			return;
		}
		base.GetComponent<BoxCollider>().enabled = true;
		base.transform.LookAt(this.GetCurrentPlayerTransform().position);
		this.anim.SetBool("canRun", false);
		this.anim.SetBool("canWalk", false);
		this.anim.SetBool("canFire", false);
		this.anim.SetBool("canHide", false);
		this.PauseHoMove();
		if (!this.canMove)
		{
			base.GetComponent<BoxCollider>().size = this.originalCollSize;
			base.GetComponent<BoxCollider>().center = this.originalCollCenter;
		}
		this.current_status = EnemyBehavior.EnemyStatus.Shooting;
		this.fireTime = Time.time;
		this.PauseHoMove();
		if (this.selected_terroristType == EnemyBehavior.TerroristType.type1)
		{
			base.InvokeRepeating("fire", 0.5f, 0.6f);
			int num = UnityEngine.Random.Range(3, 6);
			base.Invoke("HideOrRun", (float)num);
		}
		else if (this.selected_terroristType == EnemyBehavior.TerroristType.type2)
		{
			base.Invoke("fire", 0.8f);
			base.Invoke("HideOrRun", (float)UnityEngine.Random.Range(5, 10));
		}
	}

	private void fire()
	{
		this.PauseHoMove();
		this.anim.SetBool("canFire", true);
		this.anim.SetBool("canWalk", false);
		base.transform.LookAt(this.GetCurrentPlayerTransform().position);
	}

	private void HideOrRun()
	{
		if (this.current_status.Equals(EnemyBehavior.EnemyStatus.Die))
		{
			return;
		}
		base.CancelInvoke("fire");
		this.current_status = EnemyBehavior.EnemyStatus.Hide;
		this.anim.SetBool("canFire", false);
		this.anim.SetBool("canWalk", false);
		if (this.canMove)
		{
			if (this.runningSoldier)
			{
				this.anim.SetBool("canRun", true);
			}
			else
			{
				this.anim.SetBool("canWalk", true);
				this.anim.SetBool("canFire", false);
			}
			if (this.isWalkAndFireStop)
			{
				this.anim.SetBool("canWalk", false);
				this.anim.SetBool("canRun", false);
				this.anim.SetBool("canFire", false);
				this.anim.SetBool("canStand", false);
				this.PauseHoMove();
			}
			else if (!this.fireAndStop)
			{
				if (this.selected_terroristType == EnemyBehavior.TerroristType.type1 && this.enemyBehaviourCheck == EnemyBehavior.EnemyBehaviorCheck.stopAndFireAndIdleHide)
				{
					this.anim.SetBool("canRun", false);
					this.anim.SetBool("canHide", true);
					this.anim.SetBool("canFire", false);
					this.anim.SetBool("canWalk", false);
					this.canMove = false;
					this.runningSoldier = false;
					this.isStopAndFire = false;
					this.isWalkAndFireStop = false;
					this.StopHoMove();
					if (!this.canMove)
					{
						base.GetComponent<BoxCollider>().size = new Vector3(this.originalCollSize.x, 0.86f, this.originalCollSize.z);
						base.GetComponent<BoxCollider>().center = new Vector3(this.originalCollCenter.x, 0.51f, this.originalCollCenter.z);
					}
					this.fireTime = Time.time - 5f;
				}
				if (this.selected_terroristType == EnemyBehavior.TerroristType.type1 && this.enemyBehaviourCheck == EnemyBehavior.EnemyBehaviorCheck.stopAndFireContinuous)
				{
					this.anim.SetBool("canRun", false);
					this.anim.SetBool("canHide", false);
					this.anim.SetBool("canFire", true);
					this.anim.SetBool("canWalk", false);
					this.canMove = false;
					this.runningSoldier = false;
					this.isStopAndFire = false;
					this.isWalkAndFireStop = false;
					this.StopHoMove();
					this.fireTime = Time.time - 5f;
					base.InvokeRepeating("fire", 0.5f, 0.6f);
				}
				else
				{
					this.StartHoMove();
					int num = UnityEngine.Random.Range(0, 7);
					if (num == 1)
					{
						this.anim.SetTrigger("forwardRole");
					}
				}
			}
			else
			{
				this.StopHoMove();
				this.canMove = false;
				this.runningSoldier = false;
				this.isStopAndFire = false;
				this.isWalkAndFireStop = false;
				this.anim.SetBool("canRun", false);
				this.anim.SetBool("canHide", true);
				this.anim.SetBool("canFire", false);
				this.anim.SetBool("canWalk", false);
				if (!this.canMove)
				{
					base.GetComponent<BoxCollider>().size = new Vector3(this.originalCollSize.x, 0.96f, this.originalCollSize.z);
					base.GetComponent<BoxCollider>().center = new Vector3(this.originalCollCenter.x, 0.51f, this.originalCollCenter.z);
				}
			}
		}
		else
		{
			this.anim.SetBool("canRun", false);
			this.anim.SetBool("canHide", true);
			this.anim.SetBool("canFire", false);
			this.anim.SetBool("canWalk", false);
			if (!this.canMove)
			{
				base.GetComponent<BoxCollider>().size = new Vector3(this.originalCollSize.x, 0.85f, this.originalCollSize.z);
				base.GetComponent<BoxCollider>().center = new Vector3(this.originalCollCenter.x, 0.5f, this.originalCollCenter.z);
			}
		}
	}

	public void PauseHoMove()
	{
		hoMove component = base.GetComponent<hoMove>();
		if (component != null)
		{
			component.Pause();
			component.speed = 0f;
		}
	}

	public void StopHoMove()
	{
		hoMove component = base.GetComponent<hoMove>();
		if (component != null)
		{
			component.Stop();
			component.speed = 0f;
			component.enabled = false;
		}
	}

	public void StartHoMove()
	{
		hoMove component = base.GetComponent<hoMove>();
		if (component != null)
		{
			component.Resume();
			component.speed = (float)UnityEngine.Random.Range(4, 7);
			component.originSpeed = component.speed;
		}
	}

	public void canDie()
	{
		base.CancelInvoke("HideOrRun");
		base.CancelInvoke("fire");
		base.GetComponent<BoxCollider>().enabled = false;
		Singleton<GameController>.Instance.gameSceneController.IncrementEnemiesCountWithTypeNew(0, 0);
		this.current_status = EnemyBehavior.EnemyStatus.Die;
		base.CancelInvoke();
		int num = UnityEngine.Random.Range(1, 5);
		this.StopHoMove();
		this.anim.SetBool("canRun", false);
		this.anim.SetBool("canFire", false);
		this.anim.SetBool("canWalk", false);
		this.anim.SetBool("canHide", false);
		if (num == 1)
		{
			this.anim.SetTrigger("Die1");
		}
		else if (num == 2)
		{
			this.anim.SetTrigger("Die2");
		}
		else if (num == 3)
		{
			this.anim.SetTrigger("Die3");
		}
		else if (num == 4)
		{
			this.anim.SetTrigger("Die4");
		}
		UnityEngine.Object.Destroy(base.gameObject, 4f);
	}

	public void Hit()
	{
		if (Time.time - this.timeLastHit > 1.5f)
		{
			this.anim.SetTrigger("Hit");
			this.timeLastHit = Time.time;
		}
	}

	private void Update()
	{
		if (this.refGameSceneCont.isGameOver && !this.gameOver)
		{
			this.gameOver = true;
			this.anim.SetBool("canRun", false);
			this.anim.SetBool("canWalk", false);
			this.PauseHoMove();
			return;
		}
		if (!this.refGameSceneCont.canAlert || this.current_status == EnemyBehavior.EnemyStatus.Die || !this.refGameSceneCont.isGameStarted || this.refGameSceneCont.isGameCompleted || this.refGameSceneCont.isGameOver)
		{
			return;
		}
		if (Time.time - this.fireTime > 9f)
		{
			float num = Vector3.Distance(base.transform.position, this.GetCurrentPlayerTransform().position);
			if (num < this.attackDistance)
			{
				this.checkToFire();
			}
		}
	}

	public void cancelAllInvoke()
	{
		base.CancelInvoke();
	}

	public void cancelPreviousInvokes()
	{
		base.CancelInvoke();
	}

	public bool canMove = true;

	public bool runningSoldier;

	public bool isWalkAndFireStop;

	public bool isStopAndFire;

	public bool fireAndStop;

	public EnemyBehavior.TerroristType selected_terroristType;

	public EnemyBehavior.EnemyStatus current_status;

	private Animator anim;

	private float fireRate = 3f;

	private float fireTime = 4f;

	private GameSceneController refGameSceneCont;

	public int[] stopPoint = new int[]
	{
		2,
		4
	};

	private AllGameControllers allController;

	public float attackDistance = 70f;

	private Vector3 originalCollSize;

	private Vector3 originalCollCenter;

	private float timeLastHit;

	public GameObject pickUpBonus;

	public EnemyBehavior.EnemyBehaviorCheck enemyBehaviourCheck;

	public Transform player;

	private bool gameOver;

	public enum EnemyStatus
	{
		Idle,
		Petrolling,
		Running,
		Hide,
		Shooting,
		Die
	}

	public enum TerroristType
	{
		type1,
		type2
	}

	public enum EnemyBehaviorCheck
	{
		none,
		stopAndFireAndDelay,
		stopAndFireAndStop,
		stopAndFireAndIdleHide,
		stopAndFireContinuous
	}
}
