using System;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	// Token: 0x02000050 RID: 80
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(ThirdPersonCharacter))]
	public class AICharacterControl : MonoBehaviour
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00009A15 File Offset: 0x00007C15
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00009A1D File Offset: 0x00007C1D
		public NavMeshAgent agent { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00009A26 File Offset: 0x00007C26
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00009A2E File Offset: 0x00007C2E
		public ThirdPersonCharacter character { get; private set; }

		// Token: 0x060001B5 RID: 437 RVA: 0x00009A37 File Offset: 0x00007C37
		private void Start()
		{
			this.agent = base.GetComponentInChildren<NavMeshAgent>();
			this.character = base.GetComponent<ThirdPersonCharacter>();
			this.agent.updateRotation = false;
			this.agent.updatePosition = true;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009A6C File Offset: 0x00007C6C
		private void Update()
		{
			if (this.target != null)
			{
				this.agent.SetDestination(this.target.position);
			}
			if (this.agent.remainingDistance > this.agent.stoppingDistance)
			{
				this.character.Move(this.agent.desiredVelocity, false, false);
				return;
			}
			this.character.Move(Vector3.zero, false, false);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009AE1 File Offset: 0x00007CE1
		public void SetTarget(Transform target)
		{
			this.target = target;
		}

		// Token: 0x040001C1 RID: 449
		public Transform target;
	}
}
