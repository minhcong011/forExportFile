using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200000D RID: 13
public class ChInfoMain : MonoBehaviour
{
	// Token: 0x06000026 RID: 38 RVA: 0x00002994 File Offset: 0x00000B94
	private void Start()
	{
		this.Ma = GameObject.Find("GrandMA");
		this.Pa = GameObject.Find("GrandPA");
		this.Dog = GameObject.Find("DogGrandPa");
		this.Pa.SetActive(false);
		this.Dog.SetActive(false);
	}

	// Token: 0x06000027 RID: 39 RVA: 0x000029E9 File Offset: 0x00000BE9
	public void SelectMa()
	{
		this.HideAll();
		this.Ma.SetActive(true);
	}

	// Token: 0x06000028 RID: 40 RVA: 0x000029FD File Offset: 0x00000BFD
	public void SelectPa()
	{
		this.HideAll();
		this.Pa.SetActive(true);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002A11 File Offset: 0x00000C11
	public void SelectDog()
	{
		this.HideAll();
		this.Dog.SetActive(true);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002A25 File Offset: 0x00000C25
	public void SelectSon()
	{
		this.HideAll();
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002A2D File Offset: 0x00000C2D
	private void HideAll()
	{
		this.Ma.SetActive(false);
		this.Pa.SetActive(false);
		this.Dog.SetActive(false);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002A53 File Offset: 0x00000C53
	public void BackMenu()
	{
		SceneManager.LoadScene("main");
	}

	// Token: 0x04000043 RID: 67
	private GameObject Ma;

	// Token: 0x04000044 RID: 68
	private GameObject Pa;

	// Token: 0x04000045 RID: 69
	private GameObject Dog;

	// Token: 0x04000046 RID: 70
	private GameObject Son;
}
