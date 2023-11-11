using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200006E RID: 110
public class HumanMa : MonoBehaviour
{
	// Token: 0x060001D7 RID: 471 RVA: 0x0000B490 File Offset: 0x00009690
	public Button btnTest;
	
	private void Start()
	{
		Debug.Log("Duong3");
		



		this.sceneName = SceneManager.GetActiveScene();
		this.StepSound = base.gameObject.GetComponent<AudioSource>();
		this.Player = GameObject.Find("FPSController");
		this.CameraGrandMA = GameObject.Find("CameraGrandMA");
		this.navMeshAgent = base.GetComponent<NavMeshAgent>();
		string name = this.sceneName.name;
		if (!(name == "game"))
		{
			if (name == "game2")
			{
				HumanMa.PositionGo = GameObject.Find("Stairs758").transform.position;
			}
		}
		else
		{
			HumanMa.PositionGo = GameObject.Find("table45").transform.position;
		}
		this.HumanMaPos = base.transform.position;
		this.SetAnimate("walk");
		this.CameraGrandMApos = this.CameraGrandMA.transform.localPosition;
		this.CameraGrandMArot = new Vector3(this.CameraGrandMA.transform.localEulerAngles.x, this.CameraGrandMA.transform.localEulerAngles.y, this.CameraGrandMA.transform.localEulerAngles.z);
		this.CameraGrandMA.SetActive(false);
		int gameMode = VariblesGlobal.GameMode;
		if (gameMode == 2)
		{
			this.AngleSee = 160f;
			this.navMeshAgent.speed = 3.5f;
			return;
		}
		if (gameMode != 3)
		{
			return;
		}
		this.AngleSee = 90f;
		this.navMeshAgent.speed = 1.8f;
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x0000B610 File Offset: 0x00009810
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
		if (Physics.Raycast(ray, out raycastHit) && VariblesGlobal.PlayerHide == 0 
			&& this.State != 2 && raycastHit.collider.gameObject.tag == "PlayerHead" && VariblesGlobal.GameMode > 0 &&
			Vector3.Distance(this.Player.transform.position, base.transform.position) <= 50f &&
			(num < this.AngleSee || Vector3.Distance(this.Player.transform.position, base.transform.position) <= 7f))
		{
			VariblesGlobal.MaSee = 1;
			this.TimerSee = 0f;
			this.TimerSearch = 0f;
			if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 2f && this.State != 3 && this.State != 4)
			{
				this.State = 3;
			}
			HumanMa.PositionGo = this.Player.transform.position;
		}
		if (VariblesGlobal.MaSee == 1)
		{
			this.TimerSee += Time.deltaTime;
			if (this.TimerSee > 1f)
			{
				VariblesGlobal.MaSee = 0;
				HumanMa.PositionGo = this.Player.transform.position;
			}
		}
		switch (this.State)
		{
		case 0:
			this.SetAnimate("walk");
			this.GoDefault();
			break;
		case 1:
			this.SetAnimate("search");
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
				Debug.Log("die");
			this.TimerDie -= Time.deltaTime;
			if (this.TimerDie <= 0f)
			{
				this.State = 0;
			}
			break;
		case 3:
			VariblesGlobal.PlayerHide = 1;
			VariblesGlobal.BankaEnergyTime = 0f;
			this.TeleporteT(base.transform.position);
			this.CameraGrandMA.SetActive(true);
			this.SetAnimate("attack");
			this.TimerAttack -= Time.deltaTime;
			if (this.TimerAttack <= 0f)
			{
				VariblesGlobal.SelectObject = "";
				VariblesGlobal.ActionDropDown = 1;
				VariblesGlobal.tCanvasBlood = 1;
				this.CameraGrandMA.transform.eulerAngles = new Vector3(this.CameraGrandMA.transform.eulerAngles.x, this.CameraGrandMA.transform.eulerAngles.y, this.CameraGrandMA.transform.eulerAngles.z - 45f);
				this.TimerAttack = 1f;
				this.CameraGrandMA.GetComponent<Rigidbody>().isKinematic = false;
				this.CameraGrandMA.GetComponent<Rigidbody>().AddForce(this.CameraGrandMA.transform.right * VariblesGlobal.SpeedKick);
				this.State = 4;
				this.StateAD = 0;
				Object.Instantiate(Resources.Load("sound/kickSound"));
				Object.Instantiate(Resources.Load("sound/deadman"));
				if (Random.Range(0, 3) == 0)
				{
					Object.Instantiate(Resources.Load("soundHuman/SoundGrannyLaught2"));
				}
			}
			break;
		case 4:
			this.TimerAttackOfftime -= Time.deltaTime;
			if (this.TimerAttackOfftime <= 1f & this.StateAD == 0)
			{
				VariblesGlobal.ADcounter = 1;
				this.StateAD = 1;
				VariblesGlobal.ShowADinter = true;

				//Debug.Log(2);
				adcontroller.Instance.RequestInterstitial();
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
				this.State = 0;
				this.CameraGrandMA.SetActive(false);
				this.Player.GetComponent<CharacterController>().enabled = false;
				this.Player.transform.position = GameObject.Find("SpawnPoint").transform.position;
				this.Player.GetComponent<CharacterController>().enabled = true;
				this.TeleporteT(this.HumanMaPos);
				if (VariblesGlobal.GameMode == 2)
				{
					VariblesGlobal.BadEndNumber = 0;
					SceneManager.LoadScene("badend");
				}
			}
			break;
		}
		if (Mathf.Abs((HumanMa.PositionOld - base.transform.position).magnitude) < 0.01f && this.State == 0)
		{
			this.SetAnimate("idle");
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
		HumanMa.PositionOld = base.transform.position;
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0000BCC8 File Offset: 0x00009EC8
	private void GoDefault()
	{
		if (Vector3.Distance(HumanMa.PositionGo, base.transform.position) <= 2f)
		{
			this.State = 1;
			this.TimerSearch = 5f;
			return;
		}
		this.navMeshAgent.SetDestination(HumanMa.PositionGo);
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0000BD18 File Offset: 0x00009F18
	private void GeneratePosition()
	{
		this.SetAnimate("walk");
		string name = this.sceneName.name;
		if (!(name == "game"))
		{
			if (!(name == "game2"))
			{
				return;
			}
			switch (Random.Range(0, 4))
			{
			case 0:
				HumanMa.PositionGo = GameObject.Find("campfireStones").transform.position;
				return;
			case 1:
				HumanMa.PositionGo = GameObject.Find("treeStump1").transform.position;
				return;
			case 2:
				HumanMa.PositionGo = GameObject.Find("Stairs758").transform.position;
				return;
			case 3:
				HumanMa.PositionGo = GameObject.Find("treeStump2").transform.position;
				return;
			default:
				HumanMa.PositionGo = GameObject.Find("GM_CAR").transform.position;
				return;
			}
		}
		else
		{
			switch (Random.Range(0, 5))
			{
			case 0:
				HumanMa.PositionGo = GameObject.Find("table45").transform.position;
				return;
			case 1:
				HumanMa.PositionGo = GameObject.Find("ChairMainRoom").transform.position;
				return;
			case 2:
				HumanMa.PositionGo = GameObject.Find("pumpkin371").transform.position;
				return;
			case 3:
				HumanMa.PositionGo = GameObject.Find("kitchenFridge").transform.position;
				return;
			case 4:
				HumanMa.PositionGo = GameObject.Find("sideTable_4").transform.position;
				return;
			default:
				HumanMa.PositionGo = GameObject.Find("GM_CAR").transform.position;
				return;
			}
		}
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
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
			if (AnimT == "walk")
			{
				this.StepSound.volume = 1f;
				return;
			}
			this.StepSound.volume = 0f;
		}
	}

	// Token: 0x060001DC RID: 476 RVA: 0x0000BF75 File Offset: 0x0000A175
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "bullet" && this.State != 3 && this.State != 4)
		{
			this.TimerDie = 120f;
			this.State = 2;
		}
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00003939 File Offset: 0x00001B39
	private void TeleporteT(Vector3 PosTeleport)
	{
		base.GetComponent<NavMeshAgent>().isStopped = true;
		base.GetComponent<NavMeshAgent>().ResetPath();
		base.GetComponent<NavMeshAgent>().Warp(PosTeleport);
		base.GetComponent<NavMeshAgent>().SetDestination(PosTeleport);
		base.transform.position = PosTeleport;
	}

	// Token: 0x040001C8 RID: 456
	//private GameObject Player;
	public GameObject Player;
	// Token: 0x040001C9 RID: 457
	private GameObject CameraGrandMA;

	// Token: 0x040001CA RID: 458
	private AudioSource StepSound;

	// Token: 0x040001CB RID: 459
	private Vector3 CameraGrandMApos;

	// Token: 0x040001CC RID: 460
	private Vector3 CameraGrandMArot;

	// Token: 0x040001CD RID: 461
	private Vector3 HumanMaPos;

	// Token: 0x040001CE RID: 462
	public static Vector3 PositionGo;

	// Token: 0x040001CF RID: 463
	public static Vector3 PositionOld;

	// Token: 0x040001D0 RID: 464
	private NavMeshAgent navMeshAgent;

	// Token: 0x040001D1 RID: 465
	private string AnimT2;

	// Token: 0x040001D2 RID: 466
	private int State;

	// Token: 0x040001D3 RID: 467
	private int StateAD;

	// Token: 0x040001D4 RID: 468
	private float TimerSearch;

	// Token: 0x040001D5 RID: 469
	private float TimerDie;

	// Token: 0x040001D6 RID: 470
	private float TimerZastryala;

	// Token: 0x040001D7 RID: 471
	private float TimerAttack = 1f;

	// Token: 0x040001D8 RID: 472
	private float TimerAttackOfftime = 4f;

	// Token: 0x040001D9 RID: 473
	private float TimerSee;

	// Token: 0x040001DA RID: 474
	private Scene sceneName;

	// Token: 0x040001DB RID: 475
	private float AngleSee = 130f;
}
