using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000085 RID: 133
public class UI_policy : MonoBehaviour
{
	// Token: 0x0600025B RID: 603 RVA: 0x00010195 File Offset: 0x0000E395
	private void Start()
	{
		SaveLoadG.LoadData();
		this.Panel1.SetActive(true);
		this.Panel2.SetActive(false);
		this.Panel3.SetActive(false);
	}

	// Token: 0x0600025C RID: 604 RVA: 0x000101C0 File Offset: 0x0000E3C0
	public void btnShowParntners()
	{
		this.Panel1.SetActive(false);
		this.Panel2.SetActive(true);
		this.Panel3.SetActive(false);
	}

	// Token: 0x0600025D RID: 605 RVA: 0x000101E6 File Offset: 0x0000E3E6
	public void btnYes()
	{
		VariblesGlobal.AdmobNPA = 0;
        //Advertisements.Instance.SetUserConsent(true);
        SaveLoadG.SaveData();
		SceneManager.LoadScene("main");
    }

	// Token: 0x0600025E RID: 606 RVA: 0x000101FD File Offset: 0x0000E3FD
	public void btnPanel2Back()
	{
		this.Panel1.SetActive(true);
		this.Panel2.SetActive(false);
		this.Panel3.SetActive(false);
	}

	// Token: 0x0600025F RID: 607 RVA: 0x00010223 File Offset: 0x0000E423
	public void btnNo()
	{
		VariblesGlobal.AdmobNPA = 1;
        //Advertisements.Instance.SetUserConsent(false);
        SaveLoadG.SaveData();
		this.Panel3.SetActive(true);
		this.Panel2.SetActive(false);
		this.Panel1.SetActive(false);
	}

	// Token: 0x06000260 RID: 608 RVA: 0x00002A53 File Offset: 0x00000C53
	public void btnPanel3Agree()
	{
		SceneManager.LoadScene("main");
	}

	// Token: 0x06000261 RID: 609 RVA: 0x000101FD File Offset: 0x0000E3FD
	public void btnPanel3Back()
	{
		this.Panel1.SetActive(true);
		this.Panel2.SetActive(false);
		this.Panel3.SetActive(false);
	}

	// Token: 0x06000262 RID: 610 RVA: 0x00010254 File Offset: 0x0000E454
	public void btnPolicyWeb()
	{
		Application.OpenURL("");
	}

	// Token: 0x04000264 RID: 612
	public GameObject Panel1;

	// Token: 0x04000265 RID: 613
	public GameObject Panel2;

	// Token: 0x04000266 RID: 614
	public GameObject Panel3;
}
