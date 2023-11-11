using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x02000042 RID: 66
	public class SkidTrail : MonoBehaviour
	{
		// Token: 0x06000156 RID: 342 RVA: 0x0000817B File Offset: 0x0000637B
		private IEnumerator Start()
		{
			for (;;)
			{
				yield return null;
				if (base.transform.parent.parent == null)
				{
					Object.Destroy(base.gameObject, this.m_PersistTime);
				}
			}
			yield break;
		}

		// Token: 0x0400014C RID: 332
		[SerializeField]
		private float m_PersistTime;
	}
}
