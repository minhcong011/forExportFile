using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000003 RID: 3
public class EventSystemChecker : MonoBehaviour
{
	// Token: 0x06000004 RID: 4 RVA: 0x00002236 File Offset: 0x00000436
	private void Awake()
	{
		if (!Object.FindObjectOfType<EventSystem>())
		{
			GameObject gameObject = new GameObject("EventSystem");
			gameObject.AddComponent<EventSystem>();
			gameObject.AddComponent<StandaloneInputModule>().forceModuleActive = true;
		}
	}
}
