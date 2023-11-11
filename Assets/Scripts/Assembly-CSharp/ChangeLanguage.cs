using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000076 RID: 118
public class ChangeLanguage : MonoBehaviour
{
	// Token: 0x06000208 RID: 520 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
	public void aEnglish()
	{
		VariblesGlobal.SelectLanguage = "English";
		this.BackButon();
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000F0D6 File Offset: 0x0000D2D6
	public void aRussian()
	{
		VariblesGlobal.SelectLanguage = "Russian";
		this.BackButon();
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000F0E8 File Offset: 0x0000D2E8
	public void aGerman()
	{
		VariblesGlobal.SelectLanguage = "German";
		this.BackButon();
	}

	// Token: 0x0600020B RID: 523 RVA: 0x0000F0FA File Offset: 0x0000D2FA
	public void aTurkish()
	{
		VariblesGlobal.SelectLanguage = "Turkish";
		this.BackButon();
	}

	// Token: 0x0600020C RID: 524 RVA: 0x0000F10C File Offset: 0x0000D30C
	public void aSpanish()
	{
		VariblesGlobal.SelectLanguage = "Spanish";
		this.BackButon();
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0000F11E File Offset: 0x0000D31E
	public void aJapanese()
	{
		VariblesGlobal.SelectLanguage = "Japanese";
		this.BackButon();
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00002A53 File Offset: 0x00000C53
	public void BackButon()
	{
		SceneManager.LoadScene("main");
	}
}
