using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

// Token: 0x02000004 RID: 4
[RequireComponent(typeof(Image))]
public class ForcedReset : MonoBehaviour
{
	// Token: 0x06000006 RID: 6 RVA: 0x00002260 File Offset: 0x00000460
	private void Update()
	{
		if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
		{
			SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
		}
	}
}
