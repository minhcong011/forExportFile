using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000015 RID: 21
	public class SmoothFollow : MonoBehaviour
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000033A1 File Offset: 0x000015A1
		private void Start()
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000033A4 File Offset: 0x000015A4
		private void LateUpdate()
		{
			if (!this.target)
			{
				return;
			}
			float y = this.target.eulerAngles.y;
			float b = this.target.position.y + this.height;
			float num = base.transform.eulerAngles.y;
			float num2 = base.transform.position.y;
			num = Mathf.LerpAngle(num, y, this.rotationDamping * Time.deltaTime);
			num2 = Mathf.Lerp(num2, b, this.heightDamping * Time.deltaTime);
			Quaternion rotation = Quaternion.Euler(0f, num, 0f);
			base.transform.position = this.target.position;
			base.transform.position -= rotation * Vector3.forward * this.distance;
			base.transform.position = new Vector3(base.transform.position.x, num2, base.transform.position.z);
			base.transform.LookAt(this.target);
		}

		// Token: 0x04000056 RID: 86
		[SerializeField]
		private Transform target;

		// Token: 0x04000057 RID: 87
		[SerializeField]
		private float distance = 10f;

		// Token: 0x04000058 RID: 88
		[SerializeField]
		private float height = 5f;

		// Token: 0x04000059 RID: 89
		[SerializeField]
		private float rotationDamping;

		// Token: 0x0400005A RID: 90
		[SerializeField]
		private float heightDamping;
	}
}
