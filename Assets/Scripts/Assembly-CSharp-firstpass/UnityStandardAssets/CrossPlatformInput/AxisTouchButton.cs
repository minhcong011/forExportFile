using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x0200002E RID: 46
	public class AxisTouchButton : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00005F60 File Offset: 0x00004160
		private void OnEnable()
		{
			if (!CrossPlatformInputManager.AxisExists(this.axisName))
			{
				this.m_Axis = new CrossPlatformInputManager.VirtualAxis(this.axisName);
				CrossPlatformInputManager.RegisterVirtualAxis(this.m_Axis);
			}
			else
			{
				this.m_Axis = CrossPlatformInputManager.VirtualAxisReference(this.axisName);
			}
			this.FindPairedButton();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005FB0 File Offset: 0x000041B0
		private void FindPairedButton()
		{
			AxisTouchButton[] array = Object.FindObjectsOfType(typeof(AxisTouchButton)) as AxisTouchButton[];
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].axisName == this.axisName && array[i] != this)
					{
						this.m_PairedWith = array[i];
					}
				}
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000600C File Offset: 0x0000420C
		private void OnDisable()
		{
			this.m_Axis.Remove();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000601C File Offset: 0x0000421C
		public void OnPointerDown(PointerEventData data)
		{
			if (this.m_PairedWith == null)
			{
				this.FindPairedButton();
			}
			this.m_Axis.Update(Mathf.MoveTowards(this.m_Axis.GetValue, this.axisValue, this.responseSpeed * Time.deltaTime));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000606A File Offset: 0x0000426A
		public void OnPointerUp(PointerEventData data)
		{
			this.m_Axis.Update(Mathf.MoveTowards(this.m_Axis.GetValue, 0f, this.responseSpeed * Time.deltaTime));
		}

		// Token: 0x040000C5 RID: 197
		public string axisName = "Horizontal";

		// Token: 0x040000C6 RID: 198
		public float axisValue = 1f;

		// Token: 0x040000C7 RID: 199
		public float responseSpeed = 3f;

		// Token: 0x040000C8 RID: 200
		public float returnToCentreSpeed = 3f;

		// Token: 0x040000C9 RID: 201
		private AxisTouchButton m_PairedWith;

		// Token: 0x040000CA RID: 202
		private CrossPlatformInputManager.VirtualAxis m_Axis;
	}
}
