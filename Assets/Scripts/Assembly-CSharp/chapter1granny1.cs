using UnityEngine;

// Token: 0x02000025 RID: 37
public class chapter1granny1 : MonoBehaviour
{
	// Token: 0x06000091 RID: 145 RVA: 0x0000599C File Offset: 0x00003B9C
	private void Start()
	{
		if (VariblesGlobal.BadEndNumber == 0)
		{
			this.CamPos = base.transform.position;
			this.LookCamera1 = GameObject.Find("LookCamera1");
			this.LookPos = this.LookCamera1.transform.position;
			this.Drag = base.GetComponent<AudioSource>();
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00005A00 File Offset: 0x00003C00
	private void Update()
	{
		this.TimerMove -= Time.deltaTime;
		if (this.TimerMove > 0f)
		{
			this.CamPos.x = this.CamPos.x + 2f * Time.deltaTime;
		}
		base.transform.LookAt(this.LookCamera1.transform);
		if (this.StateLook <= 12)
		{
			this.TimerLook[this.StateLook] -= Time.deltaTime;
			if (this.TimerLook[this.StateLook] < 0f)
			{
				this.StateLook++;
			}
		}
		switch (this.StateLook)
		{
		case 0:
			this.LookPos.x = this.LookPos.x + 2f * Time.deltaTime;
			this.LookCamera1.transform.position = this.LookPos;
			break;
		case 1:
			this.LookPos.x = this.LookPos.x + 3f * Time.deltaTime;
			this.LookCamera1.transform.position = this.LookPos;
			break;
		case 2:
			this.LookPos.x = this.LookPos.x + 2f * Time.deltaTime;
			this.LookPos.z = this.LookPos.z - 0.15f * Time.deltaTime;
			this.LookCamera1.transform.position = this.LookPos;
			break;
		case 3:
			this.LookPos.x = this.LookPos.x + 1.6f * Time.deltaTime;
			this.LookPos.z = this.LookPos.z + 0.3f * Time.deltaTime;
			this.LookCamera1.transform.position = this.LookPos;
			break;
		case 4:
			this.LookPos.x = this.LookPos.x + 2f * Time.deltaTime;
			this.LookPos.z = this.LookPos.z - 0.15f * Time.deltaTime;
			this.LookCamera1.transform.position = this.LookPos;
			break;
		case 6:
			this.Drag.volume = 0f;
			this.LookPos.z = this.LookPos.z - 5f * Time.deltaTime;
			this.LookPos.y = this.LookPos.y - 1.2f * Time.deltaTime;
			this.LookCamera1.transform.position = this.LookPos;
			break;
		case 7:
			this.LookPos.x = this.LookPos.x + 7f * Time.deltaTime;
			this.LookPos.z = this.LookPos.z + 6.2f * Time.deltaTime;
			this.LookPos.y = this.LookPos.y + 0.003f * Time.deltaTime;
			this.LookCamera1.transform.position = this.LookPos;
			this.CamPos.y = this.CamPos.y + 0.8f * Time.deltaTime;
			break;
		case 9:
			chapter1granny1granny.Action = 1;
			break;
		case 10:
			if (this.StateAddForce == 0)
			{
				Object.Instantiate(Resources.Load("sound/kickSound"));
				Object.Instantiate(Resources.Load("soundHuman/SoundGrannyLaught2"));
				this.StateAddForce = 1;
				base.gameObject.AddComponent<Rigidbody>();
				base.gameObject.GetComponent<Rigidbody>().AddForce(base.transform.right * 100f);
			}
			break;
		case 12:
			if (this.StateDeath == 0)
			{
				this.StateDeath = 1;
				Object.Instantiate(Resources.Load("sound/deadman"));
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/PanelDeath")) as GameObject;
				gameObject.name = "mPanelDead";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
			break;
		}
		if (this.StateLook < 10)
		{
			base.transform.position = this.CamPos;
		}
	}

	// Token: 0x040000F9 RID: 249
	private Vector3 CamPos;

	// Token: 0x040000FA RID: 250
	private Vector3 LookPos;

	// Token: 0x040000FB RID: 251
	private GameObject LookCamera1;

	// Token: 0x040000FC RID: 252
	private AudioSource Drag;

	// Token: 0x040000FD RID: 253
	private float[] TimerLook = new float[]
	{
		3f,
		3f,
		3f,
		3f,
		4f,
		1f,
		1f,
		1.1f,
		0.04f,
		1f,
		1f,
		1.2f,
		1f
	};

	// Token: 0x040000FE RID: 254
	private float TimerMove = 17.5f;

	// Token: 0x040000FF RID: 255
	private int StateLook;

	// Token: 0x04000100 RID: 256
	private int StateAddForce;

	// Token: 0x04000101 RID: 257
	private int StateDeath;
}
