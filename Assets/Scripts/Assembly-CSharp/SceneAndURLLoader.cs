using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000007 RID: 7
public class SceneAndURLLoader : MonoBehaviour
{
	// Token: 0x0600000F RID: 15 RVA: 0x000024A6 File Offset: 0x000006A6
	private void Awake()
	{
		this.m_PauseMenu = base.GetComponentInChildren<PauseMenu>();
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000024B4 File Offset: 0x000006B4
	public void SceneLoad(string sceneName)
	{
		this.m_PauseMenu.MenuOff();
		SceneManager.LoadScene(sceneName);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000024C7 File Offset: 0x000006C7
	public void LoadURL(string url)
	{
		Application.OpenURL(url);
	}

	// Token: 0x0400000B RID: 11
	private PauseMenu m_PauseMenu;
}
