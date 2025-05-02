// dnSpy decompiler from Assembly-CSharp.dll class: AI
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
	private void Start()
	{
		this.loadValues();
		this.playerObj = GameObject.FindWithTag("Player");
		Mathf.Clamp01(this.randomSpawnChance);
		if (UnityEngine.Random.value > this.randomSpawnChance)
		{
			UnityEngine.Object.Destroy(this.myTransform.gameObject);
		}
	}

	private void loadValues()
	{
		this.myTransform = base.transform;
		this.upVec = Vector3.up;
		MonoBehaviour.print("start positiion is : " + this.startPosition);
		this.timeout = 0f;
		this.attackedTime = -16f;
		if (this.jokeFx)
		{
			this.jokePlaying = this.jokeFx.length * -2f;
		}
		this.collisionState = false;
		this.footstepsFx = this.myTransform.gameObject.AddComponent<AudioSource>();
		this.footstepsFx.spatialBlend = 1f;
		this.footstepsFx.volume = this.footStepVol;
		this.footstepsFx.pitch = 1f;
		this.footstepsFx.dopplerLevel = 0f;
		this.footstepsFx.bypassEffects = true;
		this.footstepsFx.bypassListenerEffects = true;
		this.footstepsFx.bypassReverbZones = true;
		this.footstepsFx.maxDistance = 10f;
		this.footstepsFx.rolloffMode = AudioRolloffMode.Linear;
		this.footstepsFx.playOnAwake = false;
		this.vocalFx = this.myTransform.gameObject.AddComponent<AudioSource>();
		this.vocalFx.spatialBlend = 1f;
		this.vocalFx.volume = this.vocalVol;
		this.vocalFx.pitch = 1f;
		this.vocalFx.dopplerLevel = 0f;
		this.vocalFx.bypassEffects = true;
		this.vocalFx.bypassListenerEffects = true;
		this.vocalFx.bypassReverbZones = true;
		this.vocalFx.maxDistance = 10f;
		this.vocalFx.rolloffMode = AudioRolloffMode.Linear;
		this.vocalFx.playOnAwake = false;
		this.searchMask = 1059841;
		if (this.objectWithAnims == null)
		{
			this.objectWithAnims = base.transform;
		}
		this.AnimatorComponent = this.objectWithAnims.GetComponent<Animator>();
		this.playerObj = GameObject.FindWithTag("Player");
		this.playerTransform = this.playerObj.transform;
		this.NPCAttackComponent = base.GetComponent<NPCAttack>();
		this.CharacterDamageComponent = base.GetComponent<CharacterDamage>();
		this.agent = base.GetComponent<NavMeshAgent>();
		this.agent.speed = this.runSpeed;
		this.agent.acceleration = 60f;
		this.colliders = base.GetComponentsInChildren<Collider>();
		this.attackRangeAmt = this.attackRange;
		this.AnimatorComponent.SetInteger("AnimState", 0);
		if (!this.spawned)
		{
			this.SpawnNPC();
		}
	}

	public Vector3 RandomNavmeshLocation(float radius)
	{
		Vector3 vector = UnityEngine.Random.insideUnitSphere * radius;
		vector += base.transform.position;
		Vector3 result = Vector3.zero;
		NavMeshHit navMeshHit;
		if (NavMesh.SamplePosition(vector, out navMeshHit, radius, 1))
		{
			result = navMeshHit.position;
		}
		this.lastLocTime = Time.time;
		return result;
	}

	public void SpawnNPC()
	{
		base.StopAllCoroutines();
		if (this.agent.isOnNavMesh)
		{
			this.spawnTime = Time.time;
			base.StartCoroutine(this.PlayFootSteps());
			if (this.objectWithAnims != this.myTransform)
			{
				base.StartCoroutine(this.UpdateModelYaw());
			}
			if (!this.huntPlayer)
			{
				if (!this.standWatch && this.waypointGroup && this.waypointGroup.wayPoints[this.firstWaypoint - 1])
				{
					this.curWayPoint = this.waypointGroup.wayPoints[this.firstWaypoint - 1];
					this.speedAmt = this.runSpeed;
					this.startPosition = this.curWayPoint.position;
					this.TravelToPoint(this.curWayPoint.position);
					base.StartCoroutine(this.Patrol());
				}
				else
				{
					if (Time.time - this.lastLocTime > 8f)
					{
						this.startPosition = this.RandomNavmeshLocation(10f);
						this.TravelToPoint(this.startPosition);
					}
					base.StartCoroutine(this.StandWatch());
				}
			}
			else
			{
				this.factionNum = 2;
				this.target = this.playerTransform;
				this.lastVisibleTargetPosition = this.target.position + this.target.up * this.targetEyeHeight;
				this.attackRange = 2048f;
				base.StartCoroutine(this.AttackTarget());
				this.speedAmt = this.runSpeed;
				this.TravelToPoint(this.playerObj.transform.position);
			}
		}
		else
		{
			UnityEngine.Debug.Log("<color=red>NPC can't find Navmesh:</color> Please bake Navmesh for this scene or reposition NPC closer to navmesh.");
		}
	}

	private void OnDrawGizmos()
	{
		if (this.drawDebugGizmos)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(base.transform.position, 0.2f);
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(base.transform.position + new Vector3(0f, this.eyeHeight, 0f), 0.2f);
			Vector3 vector = base.transform.position + base.transform.up * this.eyeHeight;
			Vector3 vector2 = this.lastVisibleTargetPosition;
			Vector3 normalized = (vector2 - vector).normalized;
			float d = Vector3.Distance(vector, vector2);
			Vector3 vector3 = vector + normalized * d;
			if (Physics.Linecast(vector, vector2))
			{
				Gizmos.color = Color.red;
			}
			else
			{
				Gizmos.color = Color.green;
			}
			Gizmos.DrawLine(vector, vector3);
			Gizmos.DrawSphere(vector3, 0.2f);
			if (this.target)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(vector, this.target.position + base.transform.up * this.targetEyeHeight);
			}
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(base.transform.position + base.transform.forward * 0.6f + base.transform.up * this.eyeHeight, 0.2f);
			Vector3 vector4 = base.transform.forward;
			vector4 = Quaternion.Euler(0f, -90f, 0f) * vector4;
			this.agent = base.GetComponent<NavMeshAgent>();
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(base.transform.position + base.transform.up * this.eyeHeight + vector4 * this.agent.radius, 0.2f);
			Gizmos.DrawSphere(base.transform.position + base.transform.up * this.eyeHeight - vector4 * this.agent.radius, 0.2f);
		}
	}

	private void InitializeAnim()
	{
		if (!this.animInit && !this.animInitialized)
		{
			this.animInit = true;
			this.animInitialized = true;
		}
		else
		{
			this.animInit = false;
		}
	}

	private IEnumerator StandWatch()
	{
		while (!this.huntPlayer)
		{
			if (this.attackedTime + 6f > Time.time)
			{
				this.attackRangeAmt = this.attackRange * 6f;
			}
			else
			{
				this.attackRangeAmt = this.attackRange;
			}
			if (this.playerObj.activeInHierarchy)
			{
				this.collisionState = true;
			}
			this.CanSeeTarget();
			if ((this.target && this.targetVisible) || this.heardPlayer || this.heardTarget)
			{
				yield return base.StartCoroutine(this.AttackTarget());
			}
			else
			{
				yield return base.StartCoroutine(this.AttackTarget());
			}
			if (this.attackTime < Time.time)
			{
				if ((!this.followPlayer || this.orderedMove) && Vector3.Distance(this.startPosition, this.myTransform.position) > this.pickNextDestDist)
				{
					if (!this.orderedMove)
					{
						this.speedAmt = this.walkSpeed;
					}
					else
					{
						this.speedAmt = this.runSpeed;
					}
					this.InitializeAnim();
					if (Time.time - this.lastLocTime > 5f)
					{
						this.startPosition = this.RandomNavmeshLocation(10f);
						this.TravelToPoint(this.startPosition);
					}
				}
				else if (this.followPlayer && !this.orderedMove && Vector3.Distance(this.playerObj.transform.position, this.myTransform.position) > this.pickNextDestDist)
				{
					if (this.followPlayer && Vector3.Distance(this.playerObj.transform.position, this.myTransform.position) > this.pickNextDestDist * 2f)
					{
						this.speedAmt = this.runSpeed;
						this.lastRunTime = Time.time;
					}
					else if (this.lastRunTime + 2f < Time.time)
					{
						this.speedAmt = this.walkSpeed;
					}
					this.InitializeAnim();
					this.TravelToPoint(this.playerObj.transform.position);
				}
				else
				{
					this.speedAmt = 0f;
					this.agent.Stop();
					this.SetSpeed(this.speedAmt);
					if (this.attackFinished && this.attackTime < Time.time)
					{
						this.AnimatorComponent.SetInteger("AnimState", 0);
					}
				}
			}
			if (this.animInit)
			{
				yield return null;
			}
			else
			{
				yield return new WaitForSeconds(0.3f);
			}
		}
		this.SpawnNPC();
		yield break;
		yield break;
	}

	private IEnumerator Patrol()
	{
		while (!this.huntPlayer)
		{
			if (!this.curWayPoint || !this.waypointGroup)
			{
				base.StartCoroutine(this.StandWatch());
				yield break;
			}
			Vector3 waypointPosition = this.curWayPoint.position;
			float waypointDist = Vector3.Distance(waypointPosition, this.myTransform.position);
			int waypointNumber = this.waypointGroup.wayPoints.IndexOf(this.curWayPoint);
			if (this.patrolOnce && waypointNumber == this.waypointGroup.wayPoints.Count - 1)
			{
				if (waypointDist < this.pickNextDestDist)
				{
					this.speedAmt = 0f;
					this.startPosition = waypointPosition;
					base.StartCoroutine(this.StandWatch());
					yield break;
				}
			}
			else if (waypointDist < this.pickNextDestDist)
			{
				if (this.waypointGroup.wayPoints.Count == 1)
				{
					this.speedAmt = 0f;
					this.startPosition = waypointPosition;
					base.StartCoroutine(this.StandWatch());
					yield break;
				}
				this.curWayPoint = this.PickNextWaypoint(this.curWayPoint, waypointNumber);
				if (this.spawned && Vector3.Distance(waypointPosition, this.myTransform.position) < this.pickNextDestDist)
				{
					this.walkOnPatrol = true;
				}
			}
			if (this.attackedTime + 6f > Time.time)
			{
				this.attackRangeAmt = this.attackRange * 6f;
			}
			else
			{
				this.attackRangeAmt = this.attackRange;
			}
			if (this.playerObj.activeInHierarchy)
			{
				this.collisionState = true;
			}
			this.CanSeeTarget();
			if ((this.target && this.targetVisible) || this.heardPlayer || this.heardTarget)
			{
				yield return base.StartCoroutine(this.AttackTarget());
			}
			else if (this.attackTime < Time.time)
			{
				if (this.orderedMove && !this.followPlayer)
				{
					if (Vector3.Distance(this.startPosition, this.myTransform.position) <= this.pickNextDestDist)
					{
						this.speedAmt = 0f;
						this.agent.Stop();
						this.SetSpeed(this.speedAmt);
						if (this.attackFinished && this.attackTime < Time.time)
						{
							this.AnimatorComponent.SetInteger("AnimState", 0);
						}
						base.StartCoroutine(this.StandWatch());
						yield break;
					}
					this.speedAmt = this.runSpeed;
					this.TravelToPoint(this.startPosition);
				}
				else if (!this.orderedMove && this.followPlayer)
				{
					if (Vector3.Distance(this.playerObj.transform.position, this.myTransform.position) > this.pickNextDestDist)
					{
						if (Vector3.Distance(this.playerObj.transform.position, this.myTransform.position) > this.pickNextDestDist * 2f)
						{
							this.speedAmt = this.runSpeed;
							this.lastRunTime = Time.time;
						}
						else if (this.lastRunTime + 2f < Time.time)
						{
							this.speedAmt = this.walkSpeed;
						}
						this.TravelToPoint(this.playerObj.transform.position);
					}
					else
					{
						this.speedAmt = 0f;
						this.agent.Stop();
						this.SetSpeed(this.speedAmt);
						if (this.attackFinished && this.attackTime < Time.time)
						{
							this.AnimatorComponent.SetInteger("AnimState", 0);
						}
					}
				}
				else
				{
					if (this.walkOnPatrol)
					{
						this.speedAmt = this.walkSpeed;
					}
					else
					{
						this.speedAmt = this.runSpeed;
					}
					this.TravelToPoint(waypointPosition);
				}
			}
			yield return new WaitForSeconds(0.3f);
		}
		this.SpawnNPC();
		yield break;
		yield break;
	}

	private void CanSeeTarget()
	{
		if (this.spawnTime + 1f > Time.time)
		{
			return;
		}
		if ((this.TargetAIComponent && !this.TargetAIComponent.enabled) || (this.target && !this.target.gameObject.activeInHierarchy))
		{
			this.target = null;
			this.TargetAIComponent = null;
			this.targetVisible = false;
			this.heardTarget = false;
			return;
		}
		if (this.factionNum != 1 || this.playerAttacked)
		{
			float num = Vector3.Distance(this.myTransform.position + this.upVec * this.eyeHeight, this.playerTransform.position + this.upVec * 1.8f * 0.25f);
			if (this.heardPlayer || this.huntPlayer || num >= this.listenRange || this.target == this.playerTransform || this.target == null)
			{
			}
			if (this.huntPlayer)
			{
				this.targetEyeHeight = 0.45f;
				this.target = this.playerTransform;
			}
			if (num < this.attackRangeAmt)
			{
				this.target = this.playerTransform;
			}
		}
		if (!(this.target == this.playerTransform) && (!this.TargetAIComponent || !this.TargetAIComponent.enabled || !(this.target != null)))
		{
			this.targetVisible = false;
			return;
		}
		Vector3 vector = this.myTransform.position + this.upVec * this.eyeHeight;
		this.targetPos = this.target.position + this.target.up * this.targetEyeHeight;
		this.targetDistance = Vector3.Distance(vector, this.targetPos);
		this.targetDirection = (this.targetPos - vector).normalized;
		Vector3.Cross(this.targetDirection, Vector3.forward).Normalize();
		if (this.targetDistance > this.attackRangeAmt)
		{
			this.sightBlocked = true;
			this.targetVisible = false;
			return;
		}
		this.hits = Physics.RaycastAll(vector, this.targetDirection, this.targetDistance, this.searchMask);
		this.sightBlocked = false;
		if (this.playerIsBehind)
		{
			base.transform.LookAt(this.target);
			this.playerIsBehind = false;
		}
		if (!this.huntPlayer && this.timeout < Time.time && this.attackedTime + 6f < Time.time && this.target == this.playerTransform && !this.heardPlayer)
		{
			Vector3 normalized = (this.targetPos - vector).normalized;
			if (Vector3.Dot(normalized, base.transform.forward) < -0.45f)
			{
				this.sightBlocked = true;
				this.playerIsBehind = true;
				this.targetVisible = false;
				return;
			}
		}
		for (int i = 0; i < this.hits.Length; i++)
		{
			if ((!this.hits[i].transform.IsChildOf(this.target) && !this.hits[i].transform.IsChildOf(this.myTransform)) || (!this.playerAttacked && this.factionNum == 1 && this.target != this.playerObj && this.hits[i].collider == this.playerTransform.GetComponent<CapsuleCollider>()))
			{
				this.sightBlocked = true;
				break;
			}
			if (this.hits[i].transform.IsChildOf(this.target))
			{
				this.attackHit = this.hits[i];
				break;
			}
		}
		if (this.sightBlocked)
		{
			if (this.TargetAIComponent && !this.huntPlayer && this.TargetAIComponent.attackTime > Time.time && Vector3.Distance(this.myTransform.position, this.target.position) < this.listenRange)
			{
				this.timeout = Time.time + 6f;
				this.heardTarget = true;
			}
			this.targetVisible = false;
			return;
		}
		if (this.target != this.playerTransform)
		{
			this.pursueTarget = false;
			this.targetVisible = true;
			return;
		}
		this.pursueTarget = true;
		this.targetVisible = true;
	}

	private IEnumerator Shoot()
	{
		this.attackFinished = false;
		this.speedAmt = 0f;
		this.SetSpeed(this.speedAmt);
		this.agent.Stop();
		this.AnimatorComponent.SetInteger("AnimState", 3);
		this.AnimatorComponent.SetTrigger("Attack");
		yield return new WaitForSeconds(this.delayShootTime);
		this.NPCAttackComponent.Fire();
		if (this.cancelAttackTaunt)
		{
			this.vocalFx.Stop();
		}
		this.attackTime = Time.time + 2f;
		yield return new WaitForSeconds(this.delayShootTime + UnityEngine.Random.Range(this.shotDuration, this.shotDuration + 0.75f));
		this.attackFinished = true;
		this.AnimatorComponent.SetInteger("AnimState", 4);
		yield break;
	}

	private IEnumerator AttackTarget()
	{
		for (;;)
		{
			if (Time.timeSinceLevelLoad < 1f)
			{
				yield return new WaitForSeconds(1f);
			}
			if (this.target == null || (this.TargetAIComponent && !this.TargetAIComponent.enabled && !this.huntPlayer))
			{
				break;
			}
			if (this.lastTauntTime + this.tauntDelay < Time.time && UnityEngine.Random.value < this.tauntChance && (this.alertTaunt || this.alertSnds.Length <= 0) && this.tauntSnds.Length > 0)
			{
				this.vocalFx.volume = this.tauntVol;
				this.vocalFx.pitch = UnityEngine.Random.Range(0.94f, 1f);
				this.vocalFx.spatialBlend = 1f;
				this.vocalFx.clip = this.tauntSnds[UnityEngine.Random.Range(0, this.tauntSnds.Length)];
				this.vocalFx.PlayOneShot(this.vocalFx.clip);
				this.lastTauntTime = Time.time;
			}
			if (!this.alertTaunt && this.alertSnds.Length > 0)
			{
				this.vocalFx.volume = this.alertVol;
				this.vocalFx.pitch = UnityEngine.Random.Range(0.94f, 1f);
				this.vocalFx.spatialBlend = 1f;
				this.vocalFx.clip = this.alertSnds[UnityEngine.Random.Range(0, this.alertSnds.Length)];
				this.vocalFx.PlayOneShot(this.vocalFx.clip);
				this.lastTauntTime = Time.time;
				this.alertTaunt = true;
			}
			float distance = Vector3.Distance(this.myTransform.position, this.target.position);
			if (!this.huntPlayer)
			{
				if (this.heardPlayer && (this.target == this.playerTransform || this.target == this.playerTransform))
				{
					this.InitializeAnim();
					this.speedAmt = this.runSpeed;
					this.SearchTarget(this.lastVisibleTargetPosition);
				}
				if (this.heardTarget)
				{
					this.InitializeAnim();
					this.speedAmt = this.runSpeed;
					this.SearchTarget(this.lastVisibleTargetPosition);
				}
				if (distance > this.attackRangeAmt)
				{
					goto Block_14;
				}
			}
			else
			{
				this.InitializeAnim();
				this.target = this.playerTransform;
				this.speedAmt = this.runSpeed;
				this.TravelToPoint(this.target.position);
			}
			if (this.pursueTarget)
			{
				this.lastVisibleTargetPosition = this.playerTransform.position;
			}
			else
			{
				this.lastVisibleTargetPosition = this.target.position + this.target.up * this.targetEyeHeight;
			}
			this.CanSeeTarget();
			if (this.targetVisible)
			{
				this.timeout = Time.time + 6f;
				if (distance > this.shootRange)
				{
					if (!this.huntPlayer)
					{
						this.SearchTarget(this.lastVisibleTargetPosition);
					}
				}
				else
				{
					if (!this.turning)
					{
						base.StopCoroutine("RotateTowards");
						base.StartCoroutine(this.RotateTowards(this.lastVisibleTargetPosition, 20f, 2f, true));
					}
					this.speedAmt = 0f;
					this.SetSpeed(this.speedAmt);
					this.agent.speed = this.speedAmt;
				}
				this.InitializeAnim();
				this.speedAmt = this.runSpeed;
				Vector3 forward = this.myTransform.TransformDirection(Vector3.forward);
				Vector3 targetDirection = this.lastVisibleTargetPosition - (this.myTransform.position + this.myTransform.up * this.eyeHeight);
				targetDirection.y = 0f;
				float angle = Vector3.Angle(targetDirection, forward);
				if (distance < this.shootRange && angle < this.shootAngle)
				{
					if (this.attackFinished)
					{
						yield return base.StartCoroutine(this.Shoot());
					}
					else
					{
						this.speedAmt = 0f;
						this.SetSpeed(this.speedAmt);
						this.agent.Stop();
					}
				}
			}
			else if (!this.huntPlayer && (this.attackFinished || this.huntPlayer))
			{
				if (this.timeout <= Time.time)
				{
					goto IL_7EA;
				}
				this.InitializeAnim();
				this.speedAmt = this.runSpeed;
				this.SetSpeed(this.speedAmt);
				this.SearchTarget(this.lastVisibleTargetPosition);
			}
			if (this.animInit)
			{
				yield return null;
			}
			else
			{
				yield return new WaitForSeconds(0.3f);
			}
		}
		this.timeout = 0f;
		this.heardPlayer = false;
		this.heardTarget = false;
		this.damaged = false;
		this.TargetAIComponent = null;
		yield break;
		Block_14:
		this.speedAmt = this.walkSpeed;
		this.target = null;
		yield break;
		IL_7EA:
		this.heardPlayer = false;
		this.heardTarget = false;
		this.alertTaunt = false;
		this.speedAmt = 0f;
		this.SetSpeed(this.speedAmt);
		this.agent.Stop();
		this.target = null;
		yield break;
		yield break;
	}

	private void SearchTarget(Vector3 position)
	{
		if (this.attackFinished)
		{
			if (this.target == this.playerTransform || this.target == this.playerTransform || (this.TargetAIComponent && this.TargetAIComponent.enabled))
			{
				if (!this.huntPlayer)
				{
					this.speedAmt = this.runSpeed;
					this.TravelToPoint(this.target.position);
				}
			}
			else
			{
				this.timeout = 0f;
				this.damaged = false;
			}
		}
	}

	public IEnumerator RotateTowards(Vector3 position, float rotationSpeed, float turnTimer, bool attacking = true)
	{
		float turnTime = Time.time;
		this.SetSpeed(0f);
		this.agent.Stop();
		while (turnTime + turnTimer > Time.time && !this.cancelRotate)
		{
			this.turning = true;
			if (this.pursueTarget)
			{
				position = this.playerTransform.position;
			}
			else if (this.target && attacking && (this.target == this.playerTransform || (this.TargetAIComponent && this.TargetAIComponent.enabled)))
			{
				this.lastVisibleTargetPosition = this.target.position + this.target.up * this.targetEyeHeight;
			}
			else
			{
				this.lastVisibleTargetPosition = position;
			}
			Vector3 direction = this.lastVisibleTargetPosition - this.myTransform.position;
			direction.y = 0f;
			if (direction.x == 0f || direction.z == 0f)
			{
				break;
			}
			this.myTransform.rotation = Quaternion.Slerp(this.myTransform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
			this.myTransform.eulerAngles = new Vector3(0f, this.myTransform.eulerAngles.y, 0f);
			yield return null;
		}
		this.cancelRotate = false;
		this.turning = false;
		yield break;
	}

	private IEnumerator UpdateModelYaw()
	{
		for (;;)
		{
			if (this.stepInterval > 0f)
			{
				this.yawAmt = Mathf.MoveTowards(this.yawAmt, this.movingYaw, Time.deltaTime * 180f);
			}
			else
			{
				this.yawAmt = Mathf.MoveTowards(this.yawAmt, this.idleYaw, Time.deltaTime * 180f);
			}
			this.objectWithAnims.transform.localRotation = Quaternion.Euler(0f, this.yawAmt, 0f);
			yield return null;
		}
		yield break;
	}

	private void TravelToPoint(Vector3 position)
	{
		if (this.attackFinished)
		{
			this.agent.SetDestination(position);
			this.agent.Resume();
			this.agent.speed = this.speedAmt;
			this.SetSpeed(this.speedAmt);
		}
	}

	private Transform PickNextWaypoint(Transform currentWaypoint, int curWaypointNumber)
	{
		Transform result;
		if (!this.countBackwards)
		{
			if (curWaypointNumber < this.waypointGroup.wayPoints.Count - 1)
			{
				result = this.waypointGroup.wayPoints[curWaypointNumber + 1];
			}
			else
			{
				result = this.waypointGroup.wayPoints[curWaypointNumber - 1];
				this.countBackwards = true;
			}
		}
		else if (curWaypointNumber != 0)
		{
			result = this.waypointGroup.wayPoints[curWaypointNumber - 1];
		}
		else
		{
			result = this.waypointGroup.wayPoints[curWaypointNumber + 1];
			this.countBackwards = false;
		}
		return result;
	}

	private void SetSpeed(float speed)
	{
		if (speed > this.walkSpeed && this.agent.hasPath)
		{
			this.AnimatorComponent.SetInteger("AnimState", 2);
			this.stepInterval = this.runStepTime;
		}
		else if (speed > 0f && this.agent.hasPath)
		{
			this.AnimatorComponent.SetInteger("AnimState", 1);
			this.stepInterval = this.walkStepTime;
		}
		else
		{
			if (this.attackFinished && this.attackTime < Time.time)
			{
				this.AnimatorComponent.SetInteger("AnimState", 0);
			}
			this.stepInterval = -1f;
		}
	}

	private IEnumerator PlayFootSteps()
	{
		for (;;)
		{
			if (this.footSteps.Length > 0 && this.stepInterval > 0f)
			{
				this.footstepsFx.pitch = 1f;
				this.footstepsFx.volume = this.footStepVol;
				this.footstepsFx.clip = this.footSteps[UnityEngine.Random.Range(0, this.footSteps.Length)];
				this.footstepsFx.PlayOneShot(this.footstepsFx.clip);
			}
			yield return new WaitForSeconds(this.stepInterval);
		}
		yield break;
	}

	public void CommandNPC()
	{
		if (this.factionNum == 1 && this.followOnUse && this.commandedTime + 0.5f < Time.time)
		{
			this.orderedMove = false;
			this.cancelRotate = false;
			this.commandedTime = Time.time;
			if (this.attackFinished && !this.turning)
			{
				base.StopCoroutine("RotateTowards");
				base.StartCoroutine(this.RotateTowards(this.playerTransform.position, 10f, 2f, false));
			}
			if (!this.followPlayer)
			{
				if ((this.followFx1 || this.followFx2) && ((this.jokeFx && this.jokePlaying + this.jokeFx.length < Time.time) || !this.jokeFx))
				{
					if (UnityEngine.Random.value > 0.5f)
					{
						this.vocalFx.clip = this.followFx1;
					}
					else
					{
						this.vocalFx.clip = this.followFx2;
					}
					this.vocalFx.pitch = UnityEngine.Random.Range(0.94f, 1f);
					this.vocalFx.spatialBlend = 1f;
					this.vocalFx.PlayOneShot(this.vocalFx.clip);
				}
				this.followPlayer = true;
			}
			else
			{
				if ((this.stayFx1 || this.stayFx2) && ((this.jokeFx && this.jokePlaying + this.jokeFx.length < Time.time) || !this.jokeFx))
				{
					if (UnityEngine.Random.value > 0.5f)
					{
						this.vocalFx.clip = this.stayFx1;
					}
					else
					{
						this.vocalFx.clip = this.stayFx2;
					}
					this.vocalFx.pitch = UnityEngine.Random.Range(0.94f, 1f);
					this.vocalFx.spatialBlend = 1f;
					this.vocalFx.PlayOneShot(this.vocalFx.clip);
				}
				this.followPlayer = false;
			}
		}
		if (this.jokeFx && this.factionNum == 1 && this.followOnUse)
		{
			if (this.jokeCount == 0)
			{
				this.talkedTime = Time.time;
			}
			if (this.talkedTime + 0.5f > Time.time)
			{
				this.talkedTime = Time.time;
				this.jokeCount++;
				if (this.jokeCount > this.jokeActivate)
				{
					if (!this.jokeFx2)
					{
						this.vocalFx.clip = this.jokeFx;
					}
					else if (UnityEngine.Random.value > 0.5f)
					{
						this.vocalFx.clip = this.jokeFx;
					}
					else
					{
						this.vocalFx.clip = this.jokeFx2;
					}
					this.vocalFx.pitch = UnityEngine.Random.Range(0.94f, 1f);
					this.vocalFx.spatialBlend = 1f;
					this.vocalFx.PlayOneShot(this.vocalFx.clip);
					this.jokePlaying = Time.time;
					this.jokeCount = 0;
				}
			}
			else
			{
				this.jokeCount = 0;
			}
		}
	}

	public void GoToPosition(Vector3 position, bool runToPos)
	{
		if (runToPos)
		{
			this.orderedMove = true;
		}
		else
		{
			this.orderedMove = false;
		}
		this.cancelRotate = true;
		this.startPosition = position;
	}

	public void ChangeFaction(int factionChange)
	{
		this.target = null;
		this.factionNum = factionChange;
	}

	[HideInInspector]
	public bool spawned;

	[HideInInspector]
	public GameObject playerObj;

	[HideInInspector]
	public Transform playerTransform;

	[HideInInspector]
	public GameObject NPCMgrObj;

	[HideInInspector]
	public GameObject weaponObj;

	[HideInInspector]
	public NPCAttack NPCAttackComponent;

	[HideInInspector]
	public CharacterDamage TargetCharacterDamageComponent;

	[HideInInspector]
	public CharacterDamage CharacterDamageComponent;

	[HideInInspector]
	public AI TargetAIComponent;

	[HideInInspector]
	public NavMeshAgent agent;

	[HideInInspector]
	public Collider[] colliders;

	private bool collisionState;

	[HideInInspector]
	public Animation AnimationComponent;

	[HideInInspector]
	public Animator AnimatorComponent;

	private float animSpeed;

	[HideInInspector]
	public bool recycleNpcObjOnDeath;

	[Tooltip("The object with the Animation/Animator component which will be accessed by AI.cs to play NPC animations. If none, this root object will be checked for the Animator/Animations component.")]
	public Transform objectWithAnims;

	[Tooltip("Chance between 0 and 1 that NPC will spawn. Used to randomize NPC locations and surprise the player.")]
	[Range(0f, 1f)]
	public float randomSpawnChance = 1f;

	[Tooltip("Running speed of the NPC.")]
	public float runSpeed = 6f;

	[Tooltip("Walking speed of the NPC.")]
	public float walkSpeed = 1f;

	[Tooltip("Speed of running animation.")]
	public float walkAnimSpeed = 1f;

	[Tooltip("Speed of walking animation.")]
	public float runAnimSpeed = 1f;

	private float speedAmt = 1f;

	private float lastRunTime;

	[Tooltip("NPC yaw angle offset when standing.")]
	public float idleYaw;

	[Tooltip("NPC yaw angle offset when moving.")]
	public float movingYaw;

	private float yawAmt;

	[Tooltip("Sets the alignment of this NPC. 1 = friendly to player and hostile to factions 2 and 3, 2 = hostile to player and factions 1 and 3, 3 = hostile to player and factions 1 and 2.")]
	public int factionNum = 1;

	[Tooltip("If false, NPC will attack any character that attacks it, regardless of faction.")]
	public bool ignoreFriendlyFire;

	[HideInInspector]
	public bool playerAttacked;

	[HideInInspector]
	public float attackedTime;

	public Transform myTransform;

	public Transform newLocalTransform;

	private Vector3 upVec;

	[Tooltip("True if NPC will hunt player accross map without needing to detect player first.")]
	public bool huntPlayer;

	[Tooltip("True if NPC should only follow patrol waypoints once.")]
	public bool patrolOnce;

	[Tooltip("True if NPC should walk on patrol, will run on patrol if false.")]
	public bool walkOnPatrol = true;

	private Transform curWayPoint;

	[Tooltip("Drag the parent waypoint object with the WaypointGroup.cs script attached here. If none, NPC will stand watch instead of patrolling.")]
	public WaypointGroup waypointGroup;

	[Tooltip("The number of the waypoint in the waypoint group which should be followed first.")]
	public int firstWaypoint = 1;

	[Tooltip("True if NPC should stand in one place and not patrol waypoint path.")]
	public bool standWatch;

	[Tooltip("True if NPC is following player.")]
	public bool followPlayer;

	[Tooltip("True if NPC can be activated with the use button and start following the player.")]
	public bool followOnUse;

	[Tooltip("True if this NPC wants player to follow them (wont take move orders from player, only from MoveTrigger.cs).")]
	public bool leadPlayer;

	[HideInInspector]
	public bool orderedMove;

	[HideInInspector]
	public bool playerFollow;

	private bool animInit;

	private bool animInitialized;

	private float commandedTime;

	private float talkedTime;

	private bool countBackwards;

	[Tooltip("Minimum distance to destination waypoint that NPC will consider their destination as reached.")]
	public float pickNextDestDist = 2.5f;

	public Vector3 startPosition;

	private float spawnTime;

	[Tooltip("Volume of NPC's vocal sound effects.")]
	public float vocalVol = 0.7f;

	[Tooltip("Sound to play when player commands NPC to stop following.")]
	public AudioClip stayFx1;

	[Tooltip("Sound to play when player commands NPC to stop following.")]
	public AudioClip stayFx2;

	[Tooltip("Sound to play when player commands NPC to start following.")]
	public AudioClip followFx1;

	[Tooltip("Sound to play when player commands NPC to start following.")]
	public AudioClip followFx2;

	[Tooltip("Sound to play when player commands NPC to move to position.")]
	public AudioClip moveToFx1;

	[Tooltip("Sound to play when player commands NPC to move to position.")]
	public AudioClip moveToFx2;

	[Tooltip("Sound to play when NPC has been activated more than joke activate times.")]
	public AudioClip jokeFx;

	[Tooltip("Sound to play when NPC has been activated more than joke activate times.")]
	public AudioClip jokeFx2;

	[Tooltip("Number of consecutive use button presses that activates joke fx.")]
	public int jokeActivate = 33;

	private float jokePlaying;

	private int jokeCount;

	[Tooltip("Sound effects to play when pursuing player.")]
	public AudioClip[] tauntSnds;

	[Tooltip("True if taunt sound shouldn't be played when attacking.")]
	public bool cancelAttackTaunt;

	private float lastTauntTime;

	[Tooltip("Delay between times to check if taunt sound should be played.")]
	public float tauntDelay = 2f;

	[Tooltip("Chance that a taunt sound will play after taunt delay.")]
	[Range(0f, 1f)]
	public float tauntChance = 0.5f;

	[Tooltip("Volume of taunt sound effects.")]
	public float tauntVol = 0.7f;

	[Tooltip("Sound effects to play when NPC discovers player.")]
	public AudioClip[] alertSnds;

	[Tooltip("Volume of alert sound effects.")]
	public float alertVol = 0.7f;

	private bool alertTaunt;

	[HideInInspector]
	public AudioSource vocalFx;

	private AudioSource footstepsFx;

	[Tooltip("Sound effects to play for NPC footsteps.")]
	public AudioClip[] footSteps;

	[Tooltip("Volume of footstep sound effects.")]
	public float footStepVol = 0.5f;

	[Tooltip("Time between footstep sound effects when walking (sync with anim).")]
	public float walkStepTime = 1f;

	[Tooltip("Time between footstep sound effects when running (sync with anim).")]
	public float runStepTime = 1f;

	private float stepInterval;

	private float stepTime;

	[Tooltip("Minimum range to target to start attack.")]
	public float shootRange = 15f;

	[Tooltip("Range that NPC will start chasing target until they are within shoot range.")]
	public float attackRange = 30f;

	[Tooltip("Range that NPC will hear player attacks.")]
	public float listenRange = 30f;

	[Tooltip("Time between shots (longer for burst weapons).")]
	public float shotDuration;

	[Tooltip("Speed of attack animation.")]
	public float shootAnimSpeed = 1f;

	[HideInInspector]
	public float attackRangeAmt = 30f;

	[Tooltip("Percentage to reduce enemy search range if player is crouching.")]
	public float sneakRangeMod = 0.4f;

	private float shootAngle = 3f;

	[Tooltip("Time before atack starts, to allow weapon to be raised before firing.")]
	public float delayShootTime = 0.35f;

	[Tooltip("Random delay between NPC attacks.")]
	public float randShotDelay = 0.75f;

	[Tooltip("Height of rayCast origin which detects targets (can be raised if NPC origin is at their feet).")]
	public float eyeHeight = 0.4f;

	[Tooltip("Draws spheres in editor for position and eye height.")]
	public bool drawDebugGizmos;

	[HideInInspector]
	public Transform target;

	[HideInInspector]
	public bool targetVisible;

	private float lastVisibleTime;

	private Vector3 targetPos;

	[HideInInspector]
	public float targetRadius;

	[HideInInspector]
	public float attackTime = -16f;

	private bool attackFinished = true;

	private bool turning;

	[HideInInspector]
	public bool cancelRotate;

	[HideInInspector]
	public bool followed;

	private float targetDistance;

	private Vector3 targetDirection;

	private RaycastHit[] hits;

	private bool sightBlocked;

	[HideInInspector]
	public bool playerIsBehind;

	[HideInInspector]
	public float targetEyeHeight;

	private bool pursueTarget;

	[HideInInspector]
	public Vector3 lastVisibleTargetPosition;

	[HideInInspector]
	public float timeout;

	[HideInInspector]
	public bool heardPlayer;

	[HideInInspector]
	public bool heardTarget;

	[HideInInspector]
	public bool damaged;

	private bool damagedState;

	[HideInInspector]
	public float lastDamageTime;

	[HideInInspector]
	public LayerMask searchMask = 0;

	[HideInInspector]
	public RaycastHit attackHit;

	private float lastLocTime;
}
