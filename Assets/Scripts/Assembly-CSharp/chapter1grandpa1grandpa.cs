using UnityEngine;

// Token: 0x02000024 RID: 36
public class chapter1grandpa1grandpa : MonoBehaviour
{
	// Token: 0x0600008C RID: 140 RVA: 0x0000578E File Offset: 0x0000398E
	private void Start()
	{
		if (VariblesGlobal.BadEndNumber == 1)
		{
			chapter1grandpa1grandpa.Action = 0;
			this.Pos = base.transform.position;
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600008D RID: 141 RVA: 0x000057BC File Offset: 0x000039BC
	private void Update()
	{
		Vector3 forward = new Vector3(this.Player.transform.position.x, base.transform.position.y, this.Player.transform.position.z) - base.transform.position;
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.LookRotation(forward), 0.1f);
		if (chapter1grandpa1grandpa.Action == 1)
		{
			this.SetAnimate("walk");
			this.Pos.z = this.Pos.z - 1f * Time.deltaTime;
			this.Timer1 -= Time.deltaTime;
			if (this.Timer1 <= 0f)
			{
				chapter1grandpa1grandpa.Action = 2;
			}
			base.transform.position = this.Pos;
		}
		if (chapter1grandpa1grandpa.Action == 2)
		{
			this.SetAnimate("search");
			this.Timer2 -= Time.deltaTime;
			if (this.Timer2 <= 0f)
			{
				chapter1grandpa1grandpa.Action = 3;
			}
		}
		if (chapter1grandpa1grandpa.Action == 3)
		{
			this.SetAnimate("idle");
		}
	}

	// Token: 0x0600008E RID: 142 RVA: 0x000058F8 File Offset: 0x00003AF8
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

	// Token: 0x040000F3 RID: 243
	public GameObject Player;

	// Token: 0x040000F4 RID: 244
	public static int Action;

	// Token: 0x040000F5 RID: 245
	private string AnimT2;

	// Token: 0x040000F6 RID: 246
	private float Timer1 = 2f;

	// Token: 0x040000F7 RID: 247
	private float Timer2 = 2f;

	// Token: 0x040000F8 RID: 248
	private Vector3 Pos;
}
