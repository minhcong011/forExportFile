using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000063 RID: 99
public class PromoStart : MonoBehaviour
{
	// Token: 0x060001B6 RID: 438 RVA: 0x0000B00C File Offset: 0x0000920C
	private void Start()
	{
		this.navMeshAgent = base.GetComponent<NavMeshAgent>();
		this.toPos = GameObject.Find("benchCushion782q1").transform.position;
		this.navMeshAgent.SetDestination(this.toPos);
		VariblesGlobal.tCanvasBlood = 1;
		this.mCanvasBlack = (Object.Instantiate(Resources.Load("UI/CanvasBlack")) as GameObject);
		this.mCanvasBlack.name = "CanvasBlack";
		this.mCanvasBlack.transform.SetParent(GameObject.Find("Canvas").transform, false);
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000B0A1 File Offset: 0x000092A1
	private void Update()
	{
		if (Vector3.Distance(this.toPos, base.transform.position) <= 2f)
		{
			VariblesGlobal.tCanvasBlood = 0;
			Object.Destroy(this.mCanvasBlack);
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040001C2 RID: 450
	private NavMeshAgent navMeshAgent;

	// Token: 0x040001C3 RID: 451
	private Vector3 toPos;

	// Token: 0x040001C4 RID: 452
	private GameObject mCanvasBlack;
}
