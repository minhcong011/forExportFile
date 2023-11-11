using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

// Token: 0x0200000C RID: 12
public class ChInfoMa : MonoBehaviour
{
	// Token: 0x06000021 RID: 33 RVA: 0x0000281C File Offset: 0x00000A1C
	private void Start()
	{
		this.sceneName = SceneManager.GetActiveScene();
		this.StepSound = base.gameObject.GetComponent<AudioSource>();
		this.CameraGrandMA = GameObject.Find("CameraGrandMA");
		this.SetAnimate("walk");
		this.CameraGrandMA.SetActive(false);
		this.StepSound.volume = 0f;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x0000287C File Offset: 0x00000A7C
	private void OnEnable()
	{
		this.AnimT2 = "";
		this.SetAnimate("walk");
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002894 File Offset: 0x00000A94
	private void Update()
	{
		this.Yrot += 40f * Time.deltaTime;
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, this.Yrot, base.transform.eulerAngles.z);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000028F0 File Offset: 0x00000AF0
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

	// Token: 0x0400002F RID: 47
	private GameObject Player;

	// Token: 0x04000030 RID: 48
	private GameObject CameraGrandMA;

	// Token: 0x04000031 RID: 49
	private AudioSource StepSound;

	// Token: 0x04000032 RID: 50
	private Vector3 CameraGrandMApos;

	// Token: 0x04000033 RID: 51
	private Vector3 CameraGrandMArot;

	// Token: 0x04000034 RID: 52
	private Vector3 HumanMaPos;

	// Token: 0x04000035 RID: 53
	public static Vector3 PositionGo;

	// Token: 0x04000036 RID: 54
	public static Vector3 PositionOld;

	// Token: 0x04000037 RID: 55
	private NavMeshAgent navMeshAgent;

	// Token: 0x04000038 RID: 56
	private string AnimT2;

	// Token: 0x04000039 RID: 57
	private int State;

	// Token: 0x0400003A RID: 58
	private int StateAD;

	// Token: 0x0400003B RID: 59
	private float TimerSearch;

	// Token: 0x0400003C RID: 60
	private float TimerDie;

	// Token: 0x0400003D RID: 61
	private float TimerZastryala;

	// Token: 0x0400003E RID: 62
	private float TimerAttack = 1f;

	// Token: 0x0400003F RID: 63
	private float TimerAttackOfftime = 4f;

	// Token: 0x04000040 RID: 64
	private float TimerSee;

	// Token: 0x04000041 RID: 65
	private Scene sceneName;

	// Token: 0x04000042 RID: 66
	private float Yrot;
}
