using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007E RID: 126
public class infoboxText : MonoBehaviour
{
	// Token: 0x06000226 RID: 550 RVA: 0x0000F6A9 File Offset: 0x0000D8A9
	private void Start()
	{
		GameObject.Find("infoboxText").GetComponent<Text>().text = (VariblesGlobal.infoboxText ?? "");
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0000F6CD File Offset: 0x0000D8CD
	private void Update()
	{
		this.TimeLeft -= Time.deltaTime;
		if (this.TimeLeft <= 0f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000252 RID: 594
	private float TimeLeft = 5f;
}
