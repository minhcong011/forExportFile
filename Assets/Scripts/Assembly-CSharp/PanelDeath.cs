using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000080 RID: 128
public class PanelDeath : MonoBehaviour
{
	// Token: 0x0600022B RID: 555 RVA: 0x0000F7D5 File Offset: 0x0000D9D5
	private void Start()
	{
		GameObject.Find("TextMainMenu").GetComponent<Text>().text = VariblesGlobalText.Text31;
		this.bMainMenu = GameObject.Find("MainMenu");
		this.bMainMenu.SetActive(false);
        //Advertisements.Instance.HideBanner();
    }

	// Token: 0x0600022C RID: 556 RVA: 0x0000F80C File Offset: 0x0000DA0C
	private void Update()
	{
		this.TimerLeft -= Time.deltaTime;
		if (this.TimerLeft <= 0f)
		{
			this.bMainMenu.SetActive(true);
		}
	}

	// Token: 0x0600022D RID: 557 RVA: 0x00002A53 File Offset: 0x00000C53
	public void MainMenu()
	{
		SceneManager.LoadScene("main");
	}

	// Token: 0x04000253 RID: 595
	private float TimerLeft = 5f;

	// Token: 0x04000254 RID: 596
	private GameObject bMainMenu;
}
