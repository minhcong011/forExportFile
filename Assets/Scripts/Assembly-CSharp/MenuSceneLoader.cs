using UnityEngine;

// Token: 0x02000005 RID: 5
public class MenuSceneLoader : MonoBehaviour
{
	// Token: 0x06000008 RID: 8 RVA: 0x000023CE File Offset: 0x000005CE
	private void Awake()
	{
		if (this.m_Go == null)
		{
			this.m_Go = Object.Instantiate<GameObject>(this.menuUI);
		}
	}

	// Token: 0x04000005 RID: 5
	public GameObject menuUI;

	// Token: 0x04000006 RID: 6
	private GameObject m_Go;
}
