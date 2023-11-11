using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000019 RID: 25
	public class WaypointProgressTracker : MonoBehaviour
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003B09 File Offset: 0x00001D09
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003B11 File Offset: 0x00001D11
		public WaypointCircuit.RoutePoint targetPoint { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003B1A File Offset: 0x00001D1A
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003B22 File Offset: 0x00001D22
		public WaypointCircuit.RoutePoint speedPoint { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003B2B File Offset: 0x00001D2B
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003B33 File Offset: 0x00001D33
		public WaypointCircuit.RoutePoint progressPoint { get; private set; }

		// Token: 0x0600005B RID: 91 RVA: 0x00003B3C File Offset: 0x00001D3C
		private void Start()
		{
			if (this.target == null)
			{
				this.target = new GameObject(base.name + " Waypoint Target").transform;
			}
			this.Reset();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003B74 File Offset: 0x00001D74
		public void Reset()
		{
			this.progressDistance = 0f;
			this.progressNum = 0;
			if (this.progressStyle == WaypointProgressTracker.ProgressStyle.PointToPoint)
			{
				this.target.position = this.circuit.Waypoints[this.progressNum].position;
				this.target.rotation = this.circuit.Waypoints[this.progressNum].rotation;
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003BE0 File Offset: 0x00001DE0
		private void Update()
		{
			if (this.progressStyle == WaypointProgressTracker.ProgressStyle.SmoothAlongRoute)
			{
				if (Time.deltaTime > 0f)
				{
					this.speed = Mathf.Lerp(this.speed, (this.lastPosition - base.transform.position).magnitude / Time.deltaTime, Time.deltaTime);
				}
				this.target.position = this.circuit.GetRoutePoint(this.progressDistance + this.lookAheadForTargetOffset + this.lookAheadForTargetFactor * this.speed).position;
				this.target.rotation = Quaternion.LookRotation(this.circuit.GetRoutePoint(this.progressDistance + this.lookAheadForSpeedOffset + this.lookAheadForSpeedFactor * this.speed).direction);
				this.progressPoint = this.circuit.GetRoutePoint(this.progressDistance);
				Vector3 lhs = this.progressPoint.position - base.transform.position;
				if (Vector3.Dot(lhs, this.progressPoint.direction) < 0f)
				{
					this.progressDistance += lhs.magnitude * 0.5f;
				}
				this.lastPosition = base.transform.position;
				return;
			}
			if ((this.target.position - base.transform.position).magnitude < this.pointToPointThreshold)
			{
				this.progressNum = (this.progressNum + 1) % this.circuit.Waypoints.Length;
			}
			this.target.position = this.circuit.Waypoints[this.progressNum].position;
			this.target.rotation = this.circuit.Waypoints[this.progressNum].rotation;
			this.progressPoint = this.circuit.GetRoutePoint(this.progressDistance);
			Vector3 lhs2 = this.progressPoint.position - base.transform.position;
			if (Vector3.Dot(lhs2, this.progressPoint.direction) < 0f)
			{
				this.progressDistance += lhs2.magnitude;
			}
			this.lastPosition = base.transform.position;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003E24 File Offset: 0x00002024
		private void OnDrawGizmos()
		{
			if (Application.isPlaying)
			{
				Gizmos.color = Color.green;
				Gizmos.DrawLine(base.transform.position, this.target.position);
				Gizmos.DrawWireSphere(this.circuit.GetRoutePosition(this.progressDistance), 1f);
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(this.target.position, this.target.position + this.target.forward);
			}
		}

		// Token: 0x0400006E RID: 110
		[SerializeField]
		private WaypointCircuit circuit;

		// Token: 0x0400006F RID: 111
		[SerializeField]
		private float lookAheadForTargetOffset = 5f;

		// Token: 0x04000070 RID: 112
		[SerializeField]
		private float lookAheadForTargetFactor = 0.1f;

		// Token: 0x04000071 RID: 113
		[SerializeField]
		private float lookAheadForSpeedOffset = 10f;

		// Token: 0x04000072 RID: 114
		[SerializeField]
		private float lookAheadForSpeedFactor = 0.2f;

		// Token: 0x04000073 RID: 115
		[SerializeField]
		private WaypointProgressTracker.ProgressStyle progressStyle;

		// Token: 0x04000074 RID: 116
		[SerializeField]
		private float pointToPointThreshold = 4f;

		// Token: 0x04000078 RID: 120
		public Transform target;

		// Token: 0x04000079 RID: 121
		private float progressDistance;

		// Token: 0x0400007A RID: 122
		private int progressNum;

		// Token: 0x0400007B RID: 123
		private Vector3 lastPosition;

		// Token: 0x0400007C RID: 124
		private float speed;

		// Token: 0x02000077 RID: 119
		public enum ProgressStyle
		{
			// Token: 0x040002B9 RID: 697
			SmoothAlongRoute,
			// Token: 0x040002BA RID: 698
			PointToPoint
		}
	}
}
