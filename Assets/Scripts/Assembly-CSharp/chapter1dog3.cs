using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

// Token: 0x02000022 RID: 34
public class chapter1dog3 : MonoBehaviour
{
	// Token: 0x06000085 RID: 133 RVA: 0x000051E2 File Offset: 0x000033E2
	private void Start()
	{
		this.navMeshAgent = base.GetComponent<NavMeshAgent>();
		this.navMeshAgent.speed = 1f;
		this.SetAnimate("isWalking2");
	}

	// Token: 0x06000086 RID: 134 RVA: 0x0000520C File Offset: 0x0000340C
	private void Update()
	{
		if (VariblesGlobal.BadEndDog1 == 1 && this.Action == 0)
		{
			this.Action = 1;
		}
		if (this.Action == 1)
		{
			this.Action = 2;
			base.GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("GrandPa1").transform.position);
		}
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00005260 File Offset: 0x00003460
	private void SetAnimate(string AnimT)
	{
		if (this.AnimT2 != AnimT)
		{
			this.AnimT2 = AnimT;
			base.GetComponent<Animator>().SetBool("isIdle", false);
			base.GetComponent<Animator>().SetBool("isWalking", false);
			base.GetComponent<Animator>().SetBool("isWalking2", false);
			base.GetComponent<Animator>().SetBool("isAttacking", false);
			base.GetComponent<Animator>().SetBool("isEat", false);
			base.GetComponent<Animator>().SetBool(AnimT, true);
		}
	}

	// Token: 0x040000E2 RID: 226
	private NavMeshAgent navMeshAgent;

	// Token: 0x040000E3 RID: 227
	private string AnimT2;

	// Token: 0x040000E4 RID: 228
	private Scene sceneName;

	// Token: 0x040000E5 RID: 229
	private int Action;

	// Token: 0x040000E6 RID: 230
	private float Timer1 = 4f;
}
