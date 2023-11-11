using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200006F RID: 111
public class HumanPa : MonoBehaviour
{
	// Token: 0x060001DF RID: 479 RVA: 0x0000BFD8 File Offset: 0x0000A1D8
	private void Start()
	{
		this.sceneName = SceneManager.GetActiveScene();
		this.StepSound = base.gameObject.GetComponent<AudioSource>();
		this.Player = GameObject.Find("FPSController");
		this.CameraGrandPA = GameObject.Find("CameraGrandPA");
		this.navMeshAgent = base.GetComponent<NavMeshAgent>();
		HumanPa.PositionGo = GameObject.Find("PosGrandPA").transform.position;
		this.DefaultPosition = GameObject.Find("PosGrandPA").transform.position;
		this.HumanPAPos = GameObject.Find("PosGrandPA").transform.position;
		this.SetAnimate("idle");
		this.CameraGrandPApos = this.CameraGrandPA.transform.localPosition;
		this.CameraGrandPArot = new Vector3(this.CameraGrandPA.transform.localEulerAngles.x, this.CameraGrandPA.transform.localEulerAngles.y, this.CameraGrandPA.transform.localEulerAngles.z);
		this.CameraGrandPA.SetActive(false);
		Object.Destroy(GameObject.Find("PosGrandPA"));
		int gameMode = VariblesGlobal.GameMode;
		if (gameMode == 2)
		{
			this.AngleSee = 180f;
			this.navMeshAgent.speed = 3.8f;
			return;
		}
		if (gameMode != 3)
		{
			return;
		}
		this.AngleSee = 100f;
		this.navMeshAgent.speed = 2f;
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x0000C148 File Offset: 0x0000A348
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
		if (Physics.Raycast(ray, out raycastHit) && VariblesGlobal.PlayerHide == 0 && this.State != 2 && raycastHit.collider.gameObject.tag == "PlayerHead" && VariblesGlobal.GameMode > 0 && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 50f && (num < this.AngleSee || Vector3.Distance(this.Player.transform.position, base.transform.position) <= 7f))
		{
			VariblesGlobal.PaSee = 1;
			this.TimerSee = 0f;
			this.TimerSearch = 0f;
			if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 2f && this.State != 3 && this.State != 4)
			{
				this.State = 3;
			}
			HumanPa.PositionGo = this.Player.transform.position;
		}
		if (VariblesGlobal.PaSee == 1)
		{
			this.TimerSee += Time.deltaTime;
			if (this.TimerSee > 1f)
			{
				VariblesGlobal.PaSee = 0;
				HumanPa.PositionGo = this.Player.transform.position;
			}
		}
		if (HumanPa.State2 == 1)
		{
			HumanPa.State2 = 0;
			HumanPa.PositionGo = this.Player.transform.position;
		}
		switch (this.State)
		{
		case 0:
			if (Vector3.Distance(this.DefaultPosition, base.transform.position) <= 1f)
			{
				HumanPa.CameraPosition = true;
				this.SetAnimate("idle");
			}
			else
			{
				HumanPa.CameraPosition = false;
			}
			this.GoDefault();
			break;
		case 1:
			this.SetAnimate("search");
			this.TimerSearch -= Time.deltaTime;
			if (this.TimerSearch <= 0f)
			{
				this.State = 0;
				HumanPa.PositionGo = this.DefaultPosition;
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
			VariblesGlobal.BankaEnergyTime = 0f;
			this.TeleporteT(base.transform.position);
			this.CameraGrandPA.SetActive(true);
			this.SetAnimate("attack");
			this.TimerAttack -= Time.deltaTime;
			if (this.TimerAttack <= 0f)
			{
				VariblesGlobal.SelectObject = "";
				VariblesGlobal.ActionDropDown = 1;
				VariblesGlobal.tCanvasBlood = 1;
				this.CameraGrandPA.transform.eulerAngles = new Vector3(this.CameraGrandPA.transform.eulerAngles.x, this.CameraGrandPA.transform.eulerAngles.y, this.CameraGrandPA.transform.eulerAngles.z - 45f);
				this.TimerAttack = 1f;
				this.CameraGrandPA.GetComponent<Rigidbody>().isKinematic = false;
				this.CameraGrandPA.GetComponent<Rigidbody>().AddForce(this.CameraGrandPA.transform.right * VariblesGlobal.SpeedKick);
				this.State = 4;
				this.StateAD = 0;
				Object.Instantiate(Resources.Load("sound/kickSound"));
				Object.Instantiate(Resources.Load("sound/deadman"));
				int num2 = Random.Range(0, 4);
				if (num2 != 0)
				{
					if (num2 == 1)
					{
						Object.Instantiate(Resources.Load("soundHuman/SoundGrannyLaught1"));
					}
				}
				else
				{
					Object.Instantiate(Resources.Load("soundHuman/SoundGrandpaLaught1"));
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

					//Debug.Log(3);
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
				this.CameraGrandPA.GetComponent<Rigidbody>().isKinematic = true;
				this.CameraGrandPA.transform.localEulerAngles = this.CameraGrandPArot;
				this.CameraGrandPA.transform.localPosition = this.CameraGrandPApos;
				this.TimerAttackOfftime = 4f;
				VariblesGlobal.tCanvasBlood = 0;
				this.State = 0;
				this.CameraGrandPA.SetActive(false);
				this.Player.GetComponent<CharacterController>().enabled = false;
				this.Player.transform.position = GameObject.Find("SpawnPoint").transform.position;
				this.Player.GetComponent<CharacterController>().enabled = true;
				this.TeleporteT(this.HumanPAPos);
				if (VariblesGlobal.GameMode == 2)
				{
					VariblesGlobal.BadEndNumber = 1;
					SceneManager.LoadScene("badend");
				}
			}
			break;
		}
		if (Mathf.Abs((HumanPa.PositionOld - base.transform.position).magnitude) < 0.01f && this.State == 0)
		{
			this.SetAnimate("idle");
			this.TimerZastryala += Time.deltaTime;
			if (this.TimerZastryala > 6f)
			{
				this.TimerZastryala = 0f;
				HumanPa.PositionGo = this.DefaultPosition;
			}
		}
		else
		{
			this.TimerZastryala = 0f;
		}
		HumanPa.PositionOld = base.transform.position;
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x0000C878 File Offset: 0x0000AA78
	private void GoDefault()
	{
		if (Vector3.Distance(HumanPa.PositionGo, base.transform.position) <= 2f)
		{
			if (!HumanPa.CameraPosition)
			{
				this.State = 1;
				this.TimerSearch = 5f;
			}
			else
			{
				HumanPa.PositionGo = this.DefaultPosition;
			}
		}
		else
		{
			this.SetAnimate("walk");
		}
		this.navMeshAgent.SetDestination(HumanPa.PositionGo);
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
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

	// Token: 0x060001E3 RID: 483 RVA: 0x0000C99D File Offset: 0x0000AB9D
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "bullet" && this.State != 3 && this.State != 4)
		{
			this.TimerDie = 200f;
			this.State = 2;
		}
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x00003939 File Offset: 0x00001B39
	private void TeleporteT(Vector3 PosTeleport)
	{
		base.GetComponent<NavMeshAgent>().isStopped = true;
		base.GetComponent<NavMeshAgent>().ResetPath();
		base.GetComponent<NavMeshAgent>().Warp(PosTeleport);
		base.GetComponent<NavMeshAgent>().SetDestination(PosTeleport);
		base.transform.position = PosTeleport;
	}

	// Token: 0x040001DC RID: 476
	private GameObject Player;

	// Token: 0x040001DD RID: 477
	private GameObject CameraGrandPA;

	// Token: 0x040001DE RID: 478
	private AudioSource StepSound;

	// Token: 0x040001DF RID: 479
	private Vector3 CameraGrandPApos;

	// Token: 0x040001E0 RID: 480
	private Vector3 CameraGrandPArot;

	// Token: 0x040001E1 RID: 481
	private Vector3 HumanPAPos;

	// Token: 0x040001E2 RID: 482
	private NavMeshAgent navMeshAgent;

	// Token: 0x040001E3 RID: 483
	public static Vector3 PositionGo;

	// Token: 0x040001E4 RID: 484
	public static Vector3 PositionOld;

	// Token: 0x040001E5 RID: 485
	public static bool CameraPosition = true;

	// Token: 0x040001E6 RID: 486
	private Vector3 DefaultPosition;

	// Token: 0x040001E7 RID: 487
	private string AnimT2;

	// Token: 0x040001E8 RID: 488
	private int State;

	// Token: 0x040001E9 RID: 489
	private int StateAD;

	// Token: 0x040001EA RID: 490
	public static int State2 = 0;

	// Token: 0x040001EB RID: 491
	private float TimerSearch;

	// Token: 0x040001EC RID: 492
	private float TimerDie;

	// Token: 0x040001ED RID: 493
	private float TimerZastryala;

	// Token: 0x040001EE RID: 494
	private float TimerAttack = 1f;

	// Token: 0x040001EF RID: 495
	private float TimerAttackOfftime = 4f;

	// Token: 0x040001F0 RID: 496
	private float TimerSee;

	// Token: 0x040001F1 RID: 497
	private Scene sceneName;

	// Token: 0x040001F2 RID: 498
	private float AngleSee = 150f;
}
