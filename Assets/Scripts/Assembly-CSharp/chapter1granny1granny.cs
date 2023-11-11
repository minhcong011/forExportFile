using UnityEngine;

// Token: 0x02000026 RID: 38
public class chapter1granny1granny : MonoBehaviour
{
	// Token: 0x06000094 RID: 148 RVA: 0x00005EA7 File Offset: 0x000040A7
	private void Start()
	{
		if (VariblesGlobal.BadEndNumber == 0)
		{
			chapter1granny1granny.Action = 0;
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00005EC4 File Offset: 0x000040C4
	private void Update()
	{
		Vector3 forward = new Vector3(this.Player.transform.position.x, base.transform.position.y, this.Player.transform.position.z) - base.transform.position;
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.LookRotation(forward), 0.1f);
		if (chapter1granny1granny.Action == 1)
		{
			this.SetAnimate("attack");
		}
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00005F5C File Offset: 0x0000415C
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

	// Token: 0x04000102 RID: 258
	public GameObject Player;

	// Token: 0x04000103 RID: 259
	public static int Action;

	// Token: 0x04000104 RID: 260
	private string AnimT2;
}
