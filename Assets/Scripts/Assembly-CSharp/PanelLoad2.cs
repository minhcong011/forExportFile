using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000087 RID: 135
public class PanelLoad2 : MonoBehaviour
{
	// Token: 0x06000267 RID: 615 RVA: 0x000102B0 File Offset: 0x0000E4B0
	public void btnContinue()
	{
		SceneManager.LoadScene("game");
	}
}
