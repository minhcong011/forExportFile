using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000089 RID: 137
public class StartUiMain : MonoBehaviour
{
	// Token: 0x0600026F RID: 623 RVA: 0x000103A8 File Offset: 0x0000E5A8
	private void Start()
	{
		this.PanelLanguage = GameObject.Find("PanelLanguage");
		this.PanelLanguage.SetActive(false);
		VariblesGlobal.SelectObject = "";
		VariblesGlobal.SelectObjectOld = "";
		this.ClickStart = false;
		GameObject.Find("TextStart").GetComponent<Text>().text = VariblesGlobalText.Text25;
		GameObject.Find("TextSetting").GetComponent<Text>().text = VariblesGlobalText.Text26;
		GameObject.Find("TextVersion").GetComponent<Text>().text = "ver. " + Application.version;
		this.PanelAchievements = GameObject.Find("PanelAchievements");
		this.PanelAchievements.SetActive(false);
		this.Speed = (float)(Screen.width / 6);
		this.Panel2 = GameObject.Find("Panel2");
		if (VariblesGlobal.MainMenuLoadCols == 0)
		{
			this.Panel2.SetActive(false);
		}
		VariblesGlobal.MainMenuLoadCols = 1;
		this.imgMa = GameObject.Find("ImageBG_granny");
		this.imgPa = GameObject.Find("ImageBG_grandpa");
		this.GermanDog = GameObject.Find("GermanDog");
		this.MaX = this.imgMa.transform.position.x;
		this.PaX = this.imgPa.transform.position.x;
		this.DogY = this.GermanDog.transform.position.y;
		this.imgMa.transform.position = new Vector3(this.imgMa.transform.position.x - (float)(Screen.width / 4), this.imgMa.transform.position.y, this.imgMa.transform.position.z);
		this.imgPa.transform.position = new Vector3(this.imgPa.transform.position.x - (float)(Screen.width / 4), this.imgPa.transform.position.y, this.imgPa.transform.position.z);
		this.GermanDog.transform.position = new Vector3(this.GermanDog.transform.position.x, this.GermanDog.transform.position.y - (float)(Screen.height / 2), this.GermanDog.transform.position.z);
		//Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM, BannerType.Adaptive);

		
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0001062C File Offset: 0x0000E82C
	private void Update()
	{
		//if (this.imgPa.transform.position.x < this.PaX)
		//{
		//	this.imgPa.transform.position = new Vector3(this.imgPa.transform.position.x + this.Speed * Time.deltaTime, this.imgPa.transform.position.y, this.imgPa.transform.position.z);
		//}
		//else if (this.imgMa.transform.position.x < this.MaX)
		//{
		//	this.imgMa.transform.position = new Vector3(this.imgMa.transform.position.x + this.Speed / 2f * Time.deltaTime, this.imgMa.transform.position.y, this.imgMa.transform.position.z);
		//}
		//else if (this.GermanDog.transform.position.y < this.DogY)
		//{
		//	this.GermanDog.transform.position = new Vector3(this.GermanDog.transform.position.x, this.GermanDog.transform.position.y + this.Speed / 2f * Time.deltaTime, this.GermanDog.transform.position.z);
		//}
		//else if (this.State1 == 0)
		//{
		//	this.Timer2 -= Time.deltaTime;
		//	if (this.Timer2 <= 0f)
		//	{
		//		this.State1 = 1;
		//		this.Panel2.SetActive(true);
		//	}
		//}
		//if (this.ClickStart)
		//{
		//	SceneManager.LoadScene("pregame");
		//}
	}

	public void loadscnes1()
	{
		SceneManager.LoadScene("pregame");
	}

	// Token: 0x06000271 RID: 625 RVA: 0x00010818 File Offset: 0x0000EA18
	public void StartGame()
	{
		GameObject gameObject = Object.Instantiate(Resources.Load("UI/PanelLoad")) as GameObject;
		gameObject.name = "PanelLoad";
		gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
		this.ClickStart = true;
	}

	// Token: 0x06000272 RID: 626 RVA: 0x00010865 File Offset: 0x0000EA65
	public void ShowPolicy()
	{
		SceneManager.LoadScene("Policy");
	}

	// Token: 0x06000273 RID: 627 RVA: 0x000040E0 File Offset: 0x000022E0
	public void RateGame()
	{
		Application.OpenURL("");
	}

	// Token: 0x06000274 RID: 628 RVA: 0x00010871 File Offset: 0x0000EA71
	public void ExitApp()
	{
		Application.Quit();
	}

	// Token: 0x06000275 RID: 629 RVA: 0x00010878 File Offset: 0x0000EA78
	

	// Token: 0x06000276 RID: 630 RVA: 0x00010884 File Offset: 0x0000EA84
	public void infoCharacters()
	{
		SceneManager.LoadScene("infocharacter");
	}

	// Token: 0x06000277 RID: 631 RVA: 0x00010890 File Offset: 0x0000EA90
	public void ShowAchivments()
	{
		this.PanelAchievements.SetActive(true);
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0001089E File Offset: 0x0000EA9E
	public void HideAchivements()
	{
		this.PanelAchievements.SetActive(false);
	}

	// Token: 0x06000279 RID: 633 RVA: 0x000108AC File Offset: 0x0000EAAC
	public void ShowLanguagePanel()
	{
		this.PanelLanguage.SetActive(true);
	}

	// Token: 0x0400026A RID: 618
	private GameObject Panel2;

	// Token: 0x0400026B RID: 619
	private GameObject PanelAchievements;

	// Token: 0x0400026C RID: 620
	private GameObject imgMa;

	// Token: 0x0400026D RID: 621
	private GameObject imgPa;

	// Token: 0x0400026E RID: 622
	private GameObject GermanDog;

	// Token: 0x0400026F RID: 623
	private GameObject PanelLanguage;

	// Token: 0x04000270 RID: 624
	private float Timer2 = 3f;

	// Token: 0x04000271 RID: 625
	private float MaX;

	// Token: 0x04000272 RID: 626
	private float PaX;

	// Token: 0x04000273 RID: 627
	private float DogY;

	// Token: 0x04000274 RID: 628
	private float Speed;

	// Token: 0x04000275 RID: 629
	private int State1;

	// Token: 0x04000276 RID: 630
	private bool ClickStart;
}
