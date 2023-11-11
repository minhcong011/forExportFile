using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000005 RID: 5
	public class ActivateTrigger : MonoBehaviour
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000228C File Offset: 0x0000048C
		private void DoActivateTrigger()
		{
			this.triggerCount--;
			if (this.triggerCount == 0 || this.repeatTrigger)
			{
				Object @object = this.target ?? base.gameObject;
				Behaviour behaviour = @object as Behaviour;
				GameObject gameObject = @object as GameObject;
				if (behaviour != null)
				{
					gameObject = behaviour.gameObject;
				}
				switch (this.action)
				{
				case ActivateTrigger.Mode.Trigger:
					if (gameObject != null)
					{
						gameObject.BroadcastMessage("DoActivateTrigger");
						return;
					}
					break;
				case ActivateTrigger.Mode.Replace:
					if (this.source != null && gameObject != null)
					{
						Object.Instantiate<GameObject>(this.source, gameObject.transform.position, gameObject.transform.rotation);
						Object.DestroyObject(gameObject);
						return;
					}
					break;
				case ActivateTrigger.Mode.Activate:
					if (gameObject != null)
					{
						gameObject.SetActive(true);
						return;
					}
					break;
				case ActivateTrigger.Mode.Enable:
					if (behaviour != null)
					{
						behaviour.enabled = true;
						return;
					}
					break;
				case ActivateTrigger.Mode.Animate:
					if (gameObject != null)
					{
						gameObject.GetComponent<Animation>().Play();
						return;
					}
					break;
				case ActivateTrigger.Mode.Deactivate:
					if (gameObject != null)
					{
						gameObject.SetActive(false);
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023AD File Offset: 0x000005AD
		private void OnTriggerEnter(Collider other)
		{
			this.DoActivateTrigger();
		}

		// Token: 0x04000002 RID: 2
		public ActivateTrigger.Mode action = ActivateTrigger.Mode.Activate;

		// Token: 0x04000003 RID: 3
		public Object target;

		// Token: 0x04000004 RID: 4
		public GameObject source;

		// Token: 0x04000005 RID: 5
		public int triggerCount = 1;

		// Token: 0x04000006 RID: 6
		public bool repeatTrigger;

		// Token: 0x02000064 RID: 100
		public enum Mode
		{
			// Token: 0x04000277 RID: 631
			Trigger,
			// Token: 0x04000278 RID: 632
			Replace,
			// Token: 0x04000279 RID: 633
			Activate,
			// Token: 0x0400027A RID: 634
			Enable,
			// Token: 0x0400027B RID: 635
			Animate,
			// Token: 0x0400027C RID: 636
			Deactivate
		}
	}
}
