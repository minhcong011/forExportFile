using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200001F RID: 31
public class FPS : MonoBehaviour
{
	// Token: 0x0600007A RID: 122 RVA: 0x00004D7C File Offset: 0x00002F7C
	private void Start()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 900;
		this.fpsRect = new Rect(10f, 10f, 400f, 100f);
		Debug.Log("#" + SceneManager.GetActiveScene().name);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00004DD4 File Offset: 0x00002FD4
	private void OnGUI()
	{
		this.timeLeft -= Time.deltaTime;
		if (this.timeLeft < 0f)
		{
			this.MinFPS = 9999f;
			this.timeLeft = 2f;
			this.MaxFPS = 0f;
		}
		this.guiStyle.fontSize = 30;
		this.guiStyle.normal.textColor = Color.white;
		float num = (float)Mathf.RoundToInt(1f / Time.deltaTime);
		if (this.MinFPS > num)
		{
			this.MinFPS = num;
		}
		if (this.MaxFPS < num)
		{
			this.MaxFPS = num;
		}
		GUI.Label(this.fpsRect, string.Concat(new object[]
		{
			"FPS Min:",
			this.MinFPS,
			" Max:",
			this.MaxFPS
		}), this.guiStyle);
	}

	// Token: 0x040000D2 RID: 210
	private Rect fpsRect;

	// Token: 0x040000D3 RID: 211
	private float MinFPS;

	// Token: 0x040000D4 RID: 212
	private float MaxFPS;

	// Token: 0x040000D5 RID: 213
	private GUIStyle guiStyle = new GUIStyle();

	// Token: 0x040000D6 RID: 214
	private float timeLeft = 2f;
}
