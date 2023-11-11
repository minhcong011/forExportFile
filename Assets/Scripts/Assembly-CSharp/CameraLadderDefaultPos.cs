using UnityEngine;

// Token: 0x02000027 RID: 39
public class CameraLadderDefaultPos : MonoBehaviour
{
	// Token: 0x06000099 RID: 153 RVA: 0x00005FE0 File Offset: 0x000041E0
	private void Start()
	{
		this.TP_ladder = GameObject.Find("tp_ladder");
		this.Player = GameObject.Find("FPSController");
		this.Ypos = base.transform.position.y;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00006018 File Offset: 0x00004218
	private void Update()
	{
		Debug.Log(base.transform.position.y);
		if (base.transform.position.y < 6f)
		{
			this.Ypos += 0.9f * Time.deltaTime;
		}
		this.Xpos += 0.003f * Time.deltaTime;
		base.transform.position = new Vector3(base.transform.position.x + this.Xpos, this.Ypos, base.transform.position.z);
		this.TimeLeft -= Time.deltaTime;
		if (this.TimeLeft < 0f)
		{
			this.Player.GetComponent<CharacterController>().enabled = false;
			this.Player.transform.position = this.TP_ladder.transform.position;
			this.Player.GetComponent<CharacterController>().enabled = true;
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000105 RID: 261
	private GameObject Player;

	// Token: 0x04000106 RID: 262
	private GameObject TP_ladder;

	// Token: 0x04000107 RID: 263
	private float Ypos;

	// Token: 0x04000108 RID: 264
	private float Xpos;

	// Token: 0x04000109 RID: 265
	private float TimeLeft = 4f;
}
