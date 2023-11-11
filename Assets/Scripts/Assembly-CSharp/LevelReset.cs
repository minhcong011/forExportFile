using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Token: 0x02000009 RID: 9
public class LevelReset : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	// Token: 0x06000016 RID: 22 RVA: 0x00002564 File Offset: 0x00000764
	public void OnPointerClick(PointerEventData data)
	{
		SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002584 File Offset: 0x00000784
	private void Update()
	{
	}
}
