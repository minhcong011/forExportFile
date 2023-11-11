using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

// Token: 0x02000020 RID: 32
public class chapter1dog1 : MonoBehaviour
{
	// Token: 0x0600007D RID: 125 RVA: 0x00004EDB File Offset: 0x000030DB
	private void Start()
	{
		this.navMeshAgent = base.GetComponent<NavMeshAgent>();
		this.navMeshAgent.speed = 1f;
		this.SetAnimate("isWalking2");
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00004F04 File Offset: 0x00003104
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

	// Token: 0x0600007F RID: 127 RVA: 0x00004F58 File Offset: 0x00003158
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

	// Token: 0x040000D7 RID: 215
	private NavMeshAgent navMeshAgent;

	// Token: 0x040000D8 RID: 216
	private string AnimT2;

	// Token: 0x040000D9 RID: 217
	private Scene sceneName;

	// Token: 0x040000DA RID: 218
	private int Action;

	// Token: 0x040000DB RID: 219
	private float Timer1 = 4f;
}
