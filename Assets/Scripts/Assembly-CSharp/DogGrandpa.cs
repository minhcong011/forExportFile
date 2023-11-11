using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000011 RID: 17
public class DogGrandpa : MonoBehaviour
{
	// Token: 0x06000039 RID: 57 RVA: 0x00002C10 File Offset: 0x00000E10
	private void Start()
	{
		Debug.Log("DogGrandpa");

		this.TimerEat = 0f;
		this.DogPos0 = GameObject.Find("DogPos0").transform.position;
		this.DogPos1 = GameObject.Find("DogPos1").transform.position;
		this.DogPos2 = GameObject.Find("DogPos2").transform.position;
		this.DogPos3 = GameObject.Find("DogPos3").transform.position;
		this.doorKeyDark = GameObject.Find("doorKeyDark");
		this.GrandPA = GameObject.Find("GrandPA");
		this.sceneName = SceneManager.GetActiveScene();
		this.StepSound = base.gameObject.GetComponent<AudioSource>();
		this.Player = GameObject.Find("FPSController");
		this.CameraGrandMA = GameObject.Find("CameraDogPA");
		this.navMeshAgent = base.GetComponent<NavMeshAgent>();
		this.navMeshAgent.speed = 3.6f;
		DogGrandpa.PositionGo = this.GrandPA.transform.position;
		this.HumanMaPos = base.transform.position;
		this.SetAnimate("walk");
		this.CameraGrandMApos = this.CameraGrandMA.transform.localPosition;
		this.CameraGrandMArot = new Vector3(this.CameraGrandMA.transform.localEulerAngles.x, this.CameraGrandMA.transform.localEulerAngles.y, this.CameraGrandMA.transform.localEulerAngles.z);
		this.CameraGrandMA.SetActive(false);
		Object.Destroy(GameObject.Find("DogPos0"));
		Object.Destroy(GameObject.Find("DogPos1"));
		Object.Destroy(GameObject.Find("DogPos2"));
		Object.Destroy(GameObject.Find("DogPos3"));
		this.gm_meat = GameObject.Find("gm_meat");
		int gameMode = VariblesGlobal.GameMode;
		if (gameMode == 2)
		{
			this.AngleSee = 240f;
			return;
		}
		if (gameMode != 3)
		{
			return;
		}
		this.AngleSee = 140f;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00002E18 File Offset: 0x00001018
	private void Update()
	{
		if (VariblesGlobal.PauseGame)
		{
			this.TeleporteT(base.transform.position);
		}
		Vector3 vector = new Vector3(base.transform.position.x, base.transform.position.y + 1.7f, base.transform.position.z);
		Vector3 normalized = (new Vector3(this.Player.transform.position.x, this.Player.transform.position.y + 0.8f, this.Player.transform.position.z) - vector).normalized;
		Ray ray = new Ray(vector, normalized);
		float num = Vector3.Angle(new Vector3(this.Player.transform.position.x, base.transform.position.y, this.Player.transform.position.z) - base.transform.position, base.transform.forward);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit) && VariblesGlobal.PlayerHide == 0 && this.State != 2 && this.State != 5 && raycastHit.collider.gameObject.tag == "PlayerHead" && VariblesGlobal.GameMode > 0 && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 50f && (num < this.AngleSee || Vector3.Distance(this.Player.transform.position, base.transform.position) <= 12f))
		{
			this.navMeshAgent.speed = 9f;
			VariblesGlobal.DogSee = 1;
			this.TimerSee = 0f;
			this.TimerSearch = 0f;
			if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 2f && this.State != 3 && this.State != 4)
			{
				this.State = 3;
				Object.Instantiate(Resources.Load("sound/soundDogAttack"));
			}
			DogGrandpa.PositionGo = this.Player.transform.position;
		}
		if (VariblesGlobal.DogSee == 1)
		{
			this.TimerSee += Time.deltaTime;
			if (this.TimerSee > 4f)
			{
				VariblesGlobal.DogSee = 0;
				DogGrandpa.PositionGo = this.Player.transform.position;
			}
		}
		switch (this.State)
		{
		case 0:
			this.SetAnimate("isWalking");
			this.GoDefault();
			break;
		case 1:
			this.SetAnimate("isIdle");
			this.TimerSearch -= Time.deltaTime;
			if (this.TimerSearch <= 0f)
			{
				this.GeneratePosition();
				this.State = 0;
			}
			break;
		case 2:
			this.TeleporteT(base.transform.position);
			this.SetAnimate("die");
			this.TimerDie -= Time.deltaTime;
			if (this.TimerDie <= 0f)
			{
				this.State = 0;
			}
			break;
		case 3:
			VariblesGlobal.PlayerHide = 1;
			VariblesGlobal.tCanvasBlood = 1;
			VariblesGlobal.BankaEnergyTime = 0f;
			this.TeleporteT(base.transform.position);
			this.CameraGrandMA.SetActive(true);
			this.TimerAttack -= Time.deltaTime;
			if (this.TimerAttack <= 0f)
			{
				VariblesGlobal.SelectObject = "";
				VariblesGlobal.ActionDropDown = 1;
				this.CameraGrandMA.transform.eulerAngles = new Vector3(this.CameraGrandMA.transform.eulerAngles.x, this.CameraGrandMA.transform.eulerAngles.y, this.CameraGrandMA.transform.eulerAngles.z - 45f);
				this.TimerAttack = 1.5f;
				this.CameraGrandMA.GetComponent<Rigidbody>().isKinematic = false;
				this.CameraGrandMA.GetComponent<Rigidbody>().AddForce(this.CameraGrandMA.transform.right * 40f);
				this.State = 4;
				this.StateAD = 0;
				Object.Instantiate(Resources.Load("sound/deadman"));
				this.SetAnimate("isIdle");
			}
			else
			{
				this.SetAnimate("isAttacking");
			}
			break;
		case 4:
			this.TimerAttackOfftime -= Time.deltaTime;
			if (this.TimerAttackOfftime <= 1f & this.StateAD == 0 & VariblesGlobal.ADcounter == 0)
			{
				VariblesGlobal.ADcounter = 1;
				this.StateAD = 1;
				VariblesGlobal.ShowADinter = true;

			    AdsController.instance.ShowInterstitialAd();
		    }
			if (this.TimerAttackOfftime <= 1f & this.StateAD == 0 & VariblesGlobal.ADcounter == 1)
			{
				VariblesGlobal.ADcounter = 0;
				this.StateAD = 1;
			}
			if (this.TimerAttackOfftime <= 0f)
			{
				VariblesGlobal.tCanvasUpDown = 0;
				VariblesGlobal.PlayerHide = 0;
				VariblesGlobal.Death++;
				GameObject.Find("IcoSkullText").GetComponent<Text>().text = string.Concat(VariblesGlobal.Death);
				this.CameraGrandMA.GetComponent<Rigidbody>().isKinematic = true;
				this.CameraGrandMA.transform.localEulerAngles = this.CameraGrandMArot;
				this.CameraGrandMA.transform.localPosition = this.CameraGrandMApos;
				this.TimerAttackOfftime = 4f;
				VariblesGlobal.tCanvasBlood = 0;
				this.State = 1;
				this.CameraGrandMA.SetActive(false);
				if (this.doorKeyDark.GetComponent<Door>().Lock)
				{
					GameObject gameObject = Object.Instantiate(Resources.Load("UI/PanelLoad")) as GameObject;
					gameObject.name = "PanelLoad";
					gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
					SceneManager.LoadScene("game2");
				}
				else
				{
					this.Player.GetComponent<CharacterController>().enabled = false;
					this.Player.transform.position = GameObject.Find("SpawnPoint").transform.position;
					this.Player.GetComponent<CharacterController>().enabled = true;
					this.TeleporteT(this.HumanMaPos);
				}
			}
			break;
		case 5:
			this.TimerEat -= Time.deltaTime;
			if (this.TimerEat <= 0f)
			{
				this.navMeshAgent.speed = 9f;
				this.gm_meat.transform.position = VariblesGlobal.game2_meatCoor;
				this.State = 0;
			}
			else
			{
				this.navMeshAgent.SetDestination(this.gm_meat.transform.position);
				if (Vector3.Distance(this.gm_meat.transform.position, base.transform.position) <= 2f)
				{
					this.SetAnimate("isEat");
				}
			}
			break;
		}
		if (Mathf.Abs((DogGrandpa.PositionOld - base.transform.position).magnitude) < 0.01f && this.State == 0)
		{
			this.SetAnimate("isIdle");
			this.TimerZastryala += Time.deltaTime;
			if (this.TimerZastryala > 6f)
			{
				this.TimerZastryala = 0f;
				this.GeneratePosition();
			}
		}
		else
		{
			this.TimerZastryala = 0f;
		}
		DogGrandpa.PositionOld = base.transform.position;
		if (Vector3.Distance(this.gm_meat.transform.position, base.transform.position) <= 7f)
		{
			if (this.State != 5 && this.State != 3 && this.State != 4)
			{
				this.navMeshAgent.speed = 3f;
				this.TimerEat = 60f;
				this.State = 5;
				if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 10f && GameObject.Find("infobox") == null)
				{
					VariblesGlobal.infoboxText = VariblesGlobalText.Text51;
					GameObject gameObject2 = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
					gameObject2.name = "infobox";
					gameObject2.transform.SetParent(GameObject.Find("Canvas").transform, false);
					return;
				}
			}
		}
		else
		{
			this.TimerEat = 0f;
		}
	}

	// Token: 0x0600003B RID: 59 RVA: 0x000036C8 File Offset: 0x000018C8
	private void GoDefault()
	{
		if (Vector3.Distance(DogGrandpa.PositionGo, base.transform.position) > 2.5f)
		{
			this.navMeshAgent.SetDestination(DogGrandpa.PositionGo);
			return;
		}
		this.State = 1;
		if (VariblesGlobal.game2_PosGrandPA == 0)
		{
			this.TimerSearch = 0f;
			return;
		}
		this.TimerSearch = 5f;
		this.navMeshAgent.speed = 9f;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00003738 File Offset: 0x00001938
	private void GeneratePosition()
	{
		this.SetAnimate("isWalking");
		if (VariblesGlobal.game2_PosGrandPA == 0)
		{
			DogGrandpa.PositionGo = this.GrandPA.transform.position;
			return;
		}
		int num;
		if (this.doorKeyDark.GetComponent<Door>().Lock)
		{
			num = Random.Range(0, 2);
		}
		else
		{
			num = Random.Range(0, 4);
		}
		switch (num)
		{
		case 0:
			DogGrandpa.PositionGo = this.DogPos0;
			break;
		case 1:
			DogGrandpa.PositionGo = this.DogPos1;
			break;
		case 2:
			DogGrandpa.PositionGo = this.DogPos2;
			break;
		case 3:
			DogGrandpa.PositionGo = this.DogPos3;
			break;
		default:
			DogGrandpa.PositionGo = this.GrandPA.transform.position;
			break;
		}
		if (VariblesGlobal.SelectObject == "canoePaddle")
		{
			DogGrandpa.PositionGo = this.DogPos2;
		}
		if (VariblesGlobal.SelectObject == "Wood")
		{
			DogGrandpa.PositionGo = this.DogPos3;
		}
	}

	// Token: 0x0600003D RID: 61 RVA: 0x0000382C File Offset: 0x00001A2C
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
			if (AnimT == "isWalking")
			{
				this.StepSound.volume = 1f;
				return;
			}
			if (AnimT == "isWalking2")
			{
				this.StepSound.volume = 1f;
				return;
			}
			this.StepSound.volume = 0f;
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00003901 File Offset: 0x00001B01
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "bullet" && this.State != 3 && this.State != 4)
		{
			this.TimerDie = 5f;
			this.State = 2;
		}
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00003939 File Offset: 0x00001B39
	private void TeleporteT(Vector3 PosTeleport)
	{
		base.GetComponent<NavMeshAgent>().isStopped = true;
		base.GetComponent<NavMeshAgent>().ResetPath();
		base.GetComponent<NavMeshAgent>().Warp(PosTeleport);
		base.GetComponent<NavMeshAgent>().SetDestination(PosTeleport);
		base.transform.position = PosTeleport;
	}

	// Token: 0x0400005F RID: 95
	private GameObject Player;

	// Token: 0x04000060 RID: 96
	private GameObject CameraGrandMA;

	// Token: 0x04000061 RID: 97
	private GameObject GrandPA;

	// Token: 0x04000062 RID: 98
	private GameObject gm_meat;

	// Token: 0x04000063 RID: 99
	private GameObject doorKeyDark;

	// Token: 0x04000064 RID: 100
	private Vector3 DogPos1;

	// Token: 0x04000065 RID: 101
	private Vector3 DogPos2;

	// Token: 0x04000066 RID: 102
	private Vector3 DogPos3;

	// Token: 0x04000067 RID: 103
	private Vector3 DogPos0;

	// Token: 0x04000068 RID: 104
	private AudioSource StepSound;

	// Token: 0x04000069 RID: 105
	private Vector3 CameraGrandMApos;

	// Token: 0x0400006A RID: 106
	private Vector3 CameraGrandMArot;

	// Token: 0x0400006B RID: 107
	private Vector3 HumanMaPos;

	// Token: 0x0400006C RID: 108
	public static Vector3 PositionGo;

	// Token: 0x0400006D RID: 109
	public static Vector3 PositionOld;

	// Token: 0x0400006E RID: 110
	private NavMeshAgent navMeshAgent;

	// Token: 0x0400006F RID: 111
	private string AnimT2;

	// Token: 0x04000070 RID: 112
	private int State;

	// Token: 0x04000071 RID: 113
	private int StateAD;

	// Token: 0x04000072 RID: 114
	private float TimerSearch;

	// Token: 0x04000073 RID: 115
	private float TimerDie;

	// Token: 0x04000074 RID: 116
	private float TimerZastryala;

	// Token: 0x04000075 RID: 117
	private float TimerAttack = 1.5f;

	// Token: 0x04000076 RID: 118
	private float TimerAttackOfftime = 4f;

	// Token: 0x04000077 RID: 119
	private float TimerSee;

	// Token: 0x04000078 RID: 120
	private float TimerEat;

	// Token: 0x04000079 RID: 121
	private Scene sceneName;

	// Token: 0x0400007A RID: 122
	private float AngleSee = 210f;
}
