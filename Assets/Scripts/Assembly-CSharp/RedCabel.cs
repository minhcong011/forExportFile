using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000052 RID: 82
public class RedCabel : MonoBehaviour
{
	// Token: 0x0600016C RID: 364 RVA: 0x00009C58 File Offset: 0x00007E58
	private void Start()
	{
		this.sceneName = SceneManager.GetActiveScene();
		this.Player = GameObject.Find("FPSController");
		string name = this.sceneName.name;
		if (!(name == "game") && name == "game2")
		{
			this.CodeDoorGreen = GameObject.Find("CodeDoorGreen");
			this.CodeDoorGreen.SetActive(false);
		}
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00009CC4 File Offset: 0x00007EC4
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f && VariblesGlobal.SelectObject == "Kusachki")
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00009D14 File Offset: 0x00007F14
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00009D64 File Offset: 0x00007F64
	private void TakeObject()
	{
		string name = this.sceneName.name;
		if (!(name == "game"))
		{
			if (name == "game2")
			{
				VariblesGlobal.game2_OpenDoor = 1;
				Object.Instantiate(Resources.Load("sound/SoundElectricalReset"));
				this.CodeDoorGreen.SetActive(true);
			}
		}
		else
		{
			VariblesGlobal.Camera01 = 1;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000192 RID: 402
	private GameObject Player;

	// Token: 0x04000193 RID: 403
	private GameObject CodeDoorGreen;

	// Token: 0x04000194 RID: 404
	private float clickTime;

	// Token: 0x04000195 RID: 405
	private Scene sceneName;
}
