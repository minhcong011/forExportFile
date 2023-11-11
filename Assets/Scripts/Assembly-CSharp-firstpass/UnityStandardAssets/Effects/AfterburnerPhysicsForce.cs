using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x0200001A RID: 26
	[RequireComponent(typeof(SphereCollider))]
	public class AfterburnerPhysicsForce : MonoBehaviour
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00003EEC File Offset: 0x000020EC
		private void OnEnable()
		{
			this.m_Sphere = (base.GetComponent<Collider>() as SphereCollider);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003F00 File Offset: 0x00002100
		private void FixedUpdate()
		{
			this.m_Cols = Physics.OverlapSphere(base.transform.position + this.m_Sphere.center, this.m_Sphere.radius);
			for (int i = 0; i < this.m_Cols.Length; i++)
			{
				if (this.m_Cols[i].attachedRigidbody != null)
				{
					Vector3 vector = base.transform.InverseTransformPoint(this.m_Cols[i].transform.position);
					vector = Vector3.MoveTowards(vector, new Vector3(0f, 0f, vector.z), this.effectWidth * 0.5f);
					float value = Mathf.Abs(Mathf.Atan2(vector.x, vector.z) * 57.29578f);
					float num = Mathf.InverseLerp(this.effectDistance, 0f, vector.magnitude);
					num *= Mathf.InverseLerp(this.effectAngle, 0f, value);
					Vector3 vector2 = this.m_Cols[i].transform.position - base.transform.position;
					this.m_Cols[i].attachedRigidbody.AddForceAtPosition(vector2.normalized * this.force * num, Vector3.Lerp(this.m_Cols[i].transform.position, base.transform.TransformPoint(0f, 0f, vector.z), 0.1f));
				}
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004084 File Offset: 0x00002284
		private void OnDrawGizmosSelected()
		{
			if (this.m_Sphere == null)
			{
				this.m_Sphere = (base.GetComponent<Collider>() as SphereCollider);
			}
			this.m_Sphere.radius = this.effectDistance * 0.5f;
			this.m_Sphere.center = new Vector3(0f, 0f, this.effectDistance * 0.5f);
			Vector3[] array = new Vector3[]
			{
				Vector3.up,
				-Vector3.up,
				Vector3.right,
				-Vector3.right
			};
			Vector3[] array2 = new Vector3[]
			{
				-Vector3.right,
				Vector3.right,
				Vector3.up,
				-Vector3.up
			};
			Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
			for (int i = 0; i < 4; i++)
			{
				Vector3 vector = base.transform.position + base.transform.rotation * array[i] * this.effectWidth * 0.5f;
				Vector3 a = base.transform.TransformDirection(Quaternion.AngleAxis(this.effectAngle, array2[i]) * Vector3.forward);
				Gizmos.DrawLine(vector, vector + a * this.m_Sphere.radius * 2f);
			}
		}

		// Token: 0x0400007D RID: 125
		public float effectAngle = 15f;

		// Token: 0x0400007E RID: 126
		public float effectWidth = 1f;

		// Token: 0x0400007F RID: 127
		public float effectDistance = 10f;

		// Token: 0x04000080 RID: 128
		public float force = 10f;

		// Token: 0x04000081 RID: 129
		private Collider[] m_Cols;

		// Token: 0x04000082 RID: 130
		private SphereCollider m_Sphere;
	}
}
