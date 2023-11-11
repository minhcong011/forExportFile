using UnityEngine;

// Token: 0x02000023 RID: 35
public class chapter1grandpa1 : MonoBehaviour
{
	// Token: 0x06000089 RID: 137 RVA: 0x000052F8 File Offset: 0x000034F8
	private void Start()
	{
		chapter1grandpa1grandpa.Action = 0;
		VariblesGlobal.BadEndDog1 = 0;
		if (VariblesGlobal.BadEndNumber == 1)
		{
			this.CamPos = base.transform.position;
			this.LookCamera1 = GameObject.Find("LookCamera2");
			this.LookPos = this.LookCamera1.transform.position;
			this.Drag = base.GetComponent<AudioSource>();
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00005368 File Offset: 0x00003568
	private void Update()
	{
		this.TimerMove -= Time.deltaTime;
		if (this.TimerMove > 0f)
		{
			this.CamPos.z = this.CamPos.z - 0.8f * Time.deltaTime;
		}
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
			this.LookPos.z = this.LookPos.z - 0.6f * Time.deltaTime;
			this.LookCamera1.transform.position = this.LookPos;
			break;
		case 1:
			this.LookPos.z = this.LookPos.z - 0.2f * Time.deltaTime;
			this.LookCamera1.transform.position = this.LookPos;
			break;
		case 2:
			this.LookPos.z = this.LookPos.z - 0.4f * Time.deltaTime;
			this.LookPos.x = this.LookPos.x - 0.2f * Time.deltaTime;
			this.LookCamera1.transform.position = this.LookPos;
			break;
		case 3:
			this.LookPos.z = this.LookPos.z - 1f * Time.deltaTime;
			this.LookPos.x = this.LookPos.x + 0.5f * Time.deltaTime;
			this.LookCamera1.transform.position = this.LookPos;
			break;
		case 4:
			this.LookPos.z = this.LookPos.z - 1f * Time.deltaTime;
			this.LookPos.x = this.LookPos.x - 0.2f * Time.deltaTime;
			this.LookCamera1.transform.position = this.LookPos;
			break;
		case 6:
			this.Drag.volume = 0f;
			if (this.StateAddForce == 0)
			{
				Object.Instantiate(Resources.Load("soundHuman/SoundGrannyLaught1"));
				Object.Instantiate(Resources.Load("sound/SoundDropMe"));
				this.StateAddForce = 1;
				chapter1grandpa1grandpa.Action = 1;
				base.gameObject.AddComponent<Rigidbody>();
				base.gameObject.GetComponent<Rigidbody>().AddForce(-base.transform.forward * 600f);
			}
			break;
		case 7:
			if (this.StateDestroy == 0)
			{
				this.StateDestroy = 1;
				Object.Destroy(base.gameObject.GetComponent<Rigidbody>());
			}
			break;
		case 8:
			if (this.StateSvist == 0)
			{
				this.StateSvist = 1;
				Object.Instantiate(Resources.Load("soundHuman/SoundSvist"));
				Object.Instantiate(Resources.Load("soundHuman/SoundDogs"));
				VariblesGlobal.BadEndDog1 = 1;
			}
			this.LookPos.z = this.LookPos.z - 22f * Time.deltaTime;
			this.LookPos.y = this.LookPos.y - 1f * Time.deltaTime;
			this.LookCamera1.transform.position = this.LookPos;
			break;
		case 12:
			if (this.StateDeath == 0)
			{
				this.StateDeath = 1;
				Object.Instantiate(Resources.Load("sound/deadman"));
			}
			break;
		}
		if (this.StateLook <= 5)
		{
			base.transform.position = this.CamPos;
		}
		base.transform.LookAt(this.LookCamera1.transform);
	}

	// Token: 0x040000E7 RID: 231
	private Vector3 CamPos;

	// Token: 0x040000E8 RID: 232
	private Vector3 LookPos;

	// Token: 0x040000E9 RID: 233
	private GameObject LookCamera1;

	// Token: 0x040000EA RID: 234
	private AudioSource Drag;

	// Token: 0x040000EB RID: 235
	private float[] TimerLook = new float[]
	{
		1f,
		1f,
		1f,
		3f,
		2f,
		4f,
		1f,
		2f,
		2f,
		1f,
		1f,
		1.2f,
		1f
	};

	// Token: 0x040000EC RID: 236
	private float TimerMove = 17.5f;

	// Token: 0x040000ED RID: 237
	private int StateLook;

	// Token: 0x040000EE RID: 238
	private int StateDestroy;

	// Token: 0x040000EF RID: 239
	private int StateAnimGrandPa;

	// Token: 0x040000F0 RID: 240
	private int StateAddForce;

	// Token: 0x040000F1 RID: 241
	private int StateDeath;

	// Token: 0x040000F2 RID: 242
	private int StateSvist;
}
