using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000019 RID: 25
public class game2WalkFinish : MonoBehaviour
{
	// Token: 0x06000059 RID: 89 RVA: 0x00004180 File Offset: 0x00002380
	private void Start()
	{
		if (VariblesGlobal.game2_Final == 1)
		{
			this.navMeshAgent = base.GetComponent<NavMeshAgent>();
			this.navMeshAgent.SetDestination(GameObject.Find("SphereWalk").transform.position);
			this.CameraLookWalk = GameObject.Find("CameraLookWalk");
			this.CameraWalk = GameObject.Find("CameraWalk");
			this.CopCar = GameObject.Find("CopCar");
			this.CamX1 = this.CameraLookWalk.transform.position.x;
			this.CamY1 = this.CameraLookWalk.transform.position.y;
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00004238 File Offset: 0x00002438
	private void Update()
	{
		if (this.State1 == 0)
		{
			this.Timer1 -= Time.deltaTime;
			if (this.Timer1 > 0f)
			{
				this.CamX1 -= 30f * Time.deltaTime;
				this.CamY1 += 1f * Time.deltaTime;
				this.CameraLookWalk.transform.position = new Vector3(this.CamX1, this.CamY1, this.CameraLookWalk.transform.position.z);
			}
			else
			{
				this.State2 = 0;
				this.State1 = 1;
			}
		}
		if (this.State2 == 0)
		{
			this.CameraLookWalk.transform.position = this.CopCar.transform.position;
		}
		int state = this.State3;
		this.CameraWalk.transform.LookAt(this.CameraLookWalk.transform.position);
	}

	// Token: 0x04000096 RID: 150
	private NavMeshAgent navMeshAgent;

	// Token: 0x04000097 RID: 151
	private GameObject CameraLookWalk;

	// Token: 0x04000098 RID: 152
	private GameObject CameraWalk;

	// Token: 0x04000099 RID: 153
	private GameObject CopCar;

	// Token: 0x0400009A RID: 154
	private float Timer1 = 2f;

	// Token: 0x0400009B RID: 155
	private float Timer2 = 3f;

	// Token: 0x0400009C RID: 156
	private float Timer3 = 3f;

	// Token: 0x0400009D RID: 157
	private float CamX1;

	// Token: 0x0400009E RID: 158
	private float CamY1;

	// Token: 0x0400009F RID: 159
	private int State1;

	// Token: 0x040000A0 RID: 160
	private int State2 = 1;

	// Token: 0x040000A1 RID: 161
	private int State3 = 1;
}
