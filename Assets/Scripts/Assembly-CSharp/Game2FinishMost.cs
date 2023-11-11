using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000016 RID: 22
public class Game2FinishMost : MonoBehaviour
{
	// Token: 0x0600004D RID: 77 RVA: 0x00003E4D File Offset: 0x0000204D
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		base.GetComponent<MeshRenderer>().enabled = false;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00003E6C File Offset: 0x0000206C
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			VariblesGlobal.Achievement2 = 1;
			if (VariblesGlobal.Level <= 2)
			{
				VariblesGlobal.Level = 2;
			}
			SaveLoadG.SaveData();
			VariblesGlobal.game2_Final = 1;
			GameObject gameObject = Object.Instantiate(Resources.Load("UI/PanelLoad")) as GameObject;
			gameObject.name = "PanelLoad";
			gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			this.SoundComplete();
			SceneManager.LoadScene("final2");
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00003EF3 File Offset: 0x000020F3
	private void SoundComplete()
	{
		Object.Instantiate(Resources.Load("Sound/SoundComplete"));
	}

	// Token: 0x0400008E RID: 142
	private GameObject Player;
}
