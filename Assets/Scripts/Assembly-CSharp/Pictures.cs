using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class Pictures : MonoBehaviour
{
	// Token: 0x060001B1 RID: 433 RVA: 0x0000ABC4 File Offset: 0x00008DC4
	private void Start()
	{
		switch (this.Number)
		{
		case 1:
			base.GetComponent<TextMesh>().text = VariblesGlobalText.NO_translate1;
			return;
		case 2:
			base.GetComponent<TextMesh>().text = VariblesGlobalText.NO_translate2;
			return;
		case 3:
			base.GetComponent<TextMesh>().text = VariblesGlobalText.Text19;
			return;
		case 4:
			base.GetComponent<TextMesh>().text = VariblesGlobalText.Text36;
			return;
		case 5:
			base.GetComponent<TextMesh>().text = VariblesGlobalText.Text47;
			return;
		case 6:
			base.GetComponent<TextMesh>().text = VariblesGlobalText.Text48;
			return;
		case 7:
			base.GetComponent<TextMesh>().text = VariblesGlobalText.Text52;
			return;
		default:
			return;
		}
	}

	// Token: 0x040001B3 RID: 435
	public int Number;
}
