using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

// Token: 0x0200000E RID: 14
public class ChInfoPa : MonoBehaviour
{
	// Token: 0x0600002E RID: 46 RVA: 0x00002A60 File Offset: 0x00000C60
	private void Start()
	{
		this.sceneName = SceneManager.GetActiveScene();
		this.StepSound = base.gameObject.GetComponent<AudioSource>();
		this.CameraGrandPA = GameObject.Find("CameraGrandPA");
		this.SetAnimate("walk");
		this.CameraGrandPA.SetActive(false);
		this.StepSound.volume = 0f;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002AC0 File Offset: 0x00000CC0
	private void OnEnable()
	{
		this.AnimT2 = "";
		this.SetAnimate("walk");
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002AD8 File Offset: 0x00000CD8
	private void Update()
	{
		this.Yrot += 40f * Time.deltaTime;
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, this.Yrot, base.transform.eulerAngles.z);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002B34 File Offset: 0x00000D34
	private void SetAnimate(string AnimT)
	{
		if (this.AnimT2 != AnimT)
		{
			this.AnimT2 = AnimT;
			base.GetComponent<Animator>().SetBool("die", false);
			base.GetComponent<Animator>().SetBool("search", false);
			base.GetComponent<Animator>().SetBool("idle", false);
			base.GetComponent<Animator>().SetBool("attack", false);
			base.GetComponent<Animator>().SetBool("walk", false);
			base.GetComponent<Animator>().SetBool(AnimT, true);
		}
	}

	// Token: 0x04000047 RID: 71
	private GameObject Player;

	// Token: 0x04000048 RID: 72
	private GameObject CameraGrandPA;

	// Token: 0x04000049 RID: 73
	private AudioSource StepSound;

	// Token: 0x0400004A RID: 74
	private Vector3 CameraGrandPApos;

	// Token: 0x0400004B RID: 75
	private Vector3 CameraGrandPArot;

	// Token: 0x0400004C RID: 76
	private Vector3 HumanPAPos;

	// Token: 0x0400004D RID: 77
	private NavMeshAgent navMeshAgent;

	// Token: 0x0400004E RID: 78
	public static Vector3 PositionGo;

	// Token: 0x0400004F RID: 79
	public static Vector3 PositionOld;

	// Token: 0x04000050 RID: 80
	public static bool CameraPosition = true;

	// Token: 0x04000051 RID: 81
	private Vector3 DefaultPosition;

	// Token: 0x04000052 RID: 82
	private string AnimT2;

	// Token: 0x04000053 RID: 83
	private int State;

	// Token: 0x04000054 RID: 84
	private int StateAD;

	// Token: 0x04000055 RID: 85
	public static int State2 = 0;

	// Token: 0x04000056 RID: 86
	private float TimerSearch;

	// Token: 0x04000057 RID: 87
	private float TimerDie;

	// Token: 0x04000058 RID: 88
	private float TimerZastryala;

	// Token: 0x04000059 RID: 89
	private float TimerAttack = 1f;

	// Token: 0x0400005A RID: 90
	private float TimerAttackOfftime = 4f;

	// Token: 0x0400005B RID: 91
	private float TimerSee;

	// Token: 0x0400005C RID: 92
	private Scene sceneName;

	// Token: 0x0400005D RID: 93
	private float Yrot;
}
