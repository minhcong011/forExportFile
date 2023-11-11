using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

// Token: 0x0200000B RID: 11
public class ChInfoDog : MonoBehaviour
{
	// Token: 0x0600001C RID: 28 RVA: 0x000026B4 File Offset: 0x000008B4
	private void Start()
	{
		Debug.Log("111");
		this.StepSound = base.gameObject.GetComponent<AudioSource>();
		this.CameraGrandMA = GameObject.Find("CameraDogPA");
		this.SetAnimate("isWalking2");
		this.CameraGrandMA.SetActive(false);
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002703 File Offset: 0x00000903
	private void OnEnable()
	{
		this.AnimT2 = "";
		this.SetAnimate("isWalking2");
	}

	// Token: 0x0600001E RID: 30 RVA: 0x0000271C File Offset: 0x0000091C
	private void Update()
	{
		this.Yrot += 40f * Time.deltaTime;
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, this.Yrot, base.transform.eulerAngles.z);
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002778 File Offset: 0x00000978
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

	// Token: 0x04000013 RID: 19
	private GameObject Player;

	// Token: 0x04000014 RID: 20
	private GameObject CameraGrandMA;

	// Token: 0x04000015 RID: 21
	private GameObject GrandPA;

	// Token: 0x04000016 RID: 22
	private GameObject gm_meat;

	// Token: 0x04000017 RID: 23
	private GameObject doorKeyDark;

	// Token: 0x04000018 RID: 24
	private Vector3 DogPos1;

	// Token: 0x04000019 RID: 25
	private Vector3 DogPos2;

	// Token: 0x0400001A RID: 26
	private Vector3 DogPos3;

	// Token: 0x0400001B RID: 27
	private Vector3 DogPos0;

	// Token: 0x0400001C RID: 28
	private AudioSource StepSound;

	// Token: 0x0400001D RID: 29
	private Vector3 CameraGrandMApos;

	// Token: 0x0400001E RID: 30
	private Vector3 CameraGrandMArot;

	// Token: 0x0400001F RID: 31
	private Vector3 HumanMaPos;

	// Token: 0x04000020 RID: 32
	public static Vector3 PositionGo;

	// Token: 0x04000021 RID: 33
	public static Vector3 PositionOld;

	// Token: 0x04000022 RID: 34
	private NavMeshAgent navMeshAgent;

	// Token: 0x04000023 RID: 35
	private string AnimT2;

	// Token: 0x04000024 RID: 36
	private int State;

	// Token: 0x04000025 RID: 37
	private int StateAD;

	// Token: 0x04000026 RID: 38
	private float TimerSearch;

	// Token: 0x04000027 RID: 39
	private float TimerDie;

	// Token: 0x04000028 RID: 40
	private float TimerZastryala;

	// Token: 0x04000029 RID: 41
	private float TimerAttack = 1.5f;

	// Token: 0x0400002A RID: 42
	private float TimerAttackOfftime = 4f;

	// Token: 0x0400002B RID: 43
	private float TimerSee;

	// Token: 0x0400002C RID: 44
	private float TimerEat;

	// Token: 0x0400002D RID: 45
	private Scene sceneName;

	// Token: 0x0400002E RID: 46
	private float Yrot;
}
