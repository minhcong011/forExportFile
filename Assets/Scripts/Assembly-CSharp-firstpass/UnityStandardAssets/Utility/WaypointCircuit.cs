using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000018 RID: 24
	public class WaypointCircuit : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000035F9 File Offset: 0x000017F9
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00003601 File Offset: 0x00001801
		public float Length { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000360A File Offset: 0x0000180A
		public Transform[] Waypoints
		{
			get
			{
				return this.waypointList.items;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003617 File Offset: 0x00001817
		private void Awake()
		{
			if (this.Waypoints.Length > 1)
			{
				this.CachePositionsAndDistances();
			}
			this.numPoints = this.Waypoints.Length;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003638 File Offset: 0x00001838
		public WaypointCircuit.RoutePoint GetRoutePoint(float dist)
		{
			Vector3 routePosition = this.GetRoutePosition(dist);
			return new WaypointCircuit.RoutePoint(routePosition, (this.GetRoutePosition(dist + 0.1f) - routePosition).normalized);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003670 File Offset: 0x00001870
		public Vector3 GetRoutePosition(float dist)
		{
			int num = 0;
			if (this.Length == 0f)
			{
				this.Length = this.distances[this.distances.Length - 1];
			}
			dist = Mathf.Repeat(dist, this.Length);
			while (this.distances[num] < dist)
			{
				num++;
			}
			this.p1n = (num - 1 + this.numPoints) % this.numPoints;
			this.p2n = num;
			this.i = Mathf.InverseLerp(this.distances[this.p1n], this.distances[this.p2n], dist);
			if (this.smoothRoute)
			{
				this.p0n = (num - 2 + this.numPoints) % this.numPoints;
				this.p3n = (num + 1) % this.numPoints;
				this.p2n %= this.numPoints;
				this.P0 = this.points[this.p0n];
				this.P1 = this.points[this.p1n];
				this.P2 = this.points[this.p2n];
				this.P3 = this.points[this.p3n];
				return this.CatmullRom(this.P0, this.P1, this.P2, this.P3, this.i);
			}
			this.p1n = (num - 1 + this.numPoints) % this.numPoints;
			this.p2n = num;
			return Vector3.Lerp(this.points[this.p1n], this.points[this.p2n], this.i);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003818 File Offset: 0x00001A18
		private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float i)
		{
			return 0.5f * (2f * p1 + (-p0 + p2) * i + (2f * p0 - 5f * p1 + 4f * p2 - p3) * i * i + (-p0 + 3f * p1 - 3f * p2 + p3) * i * i * i);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000038E0 File Offset: 0x00001AE0
		private void CachePositionsAndDistances()
		{
			this.points = new Vector3[this.Waypoints.Length + 1];
			this.distances = new float[this.Waypoints.Length + 1];
			float num = 0f;
			for (int i = 0; i < this.points.Length; i++)
			{
				Transform transform = this.Waypoints[i % this.Waypoints.Length];
				Transform transform2 = this.Waypoints[(i + 1) % this.Waypoints.Length];
				if (transform != null && transform2 != null)
				{
					Vector3 position = transform.position;
					Vector3 position2 = transform2.position;
					this.points[i] = this.Waypoints[i % this.Waypoints.Length].position;
					this.distances[i] = num;
					num += (position - position2).magnitude;
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000039BE File Offset: 0x00001BBE
		private void OnDrawGizmos()
		{
			this.DrawGizmos(false);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000039C7 File Offset: 0x00001BC7
		private void OnDrawGizmosSelected()
		{
			this.DrawGizmos(true);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000039D0 File Offset: 0x00001BD0
		private void DrawGizmos(bool selected)
		{
			this.waypointList.circuit = this;
			if (this.Waypoints.Length > 1)
			{
				this.numPoints = this.Waypoints.Length;
				this.CachePositionsAndDistances();
				this.Length = this.distances[this.distances.Length - 1];
				Gizmos.color = (selected ? Color.yellow : new Color(1f, 1f, 0f, 0.5f));
				Vector3 from = this.Waypoints[0].position;
				if (this.smoothRoute)
				{
					for (float num = 0f; num < this.Length; num += this.Length / this.editorVisualisationSubsteps)
					{
						Vector3 routePosition = this.GetRoutePosition(num + 1f);
						Gizmos.DrawLine(from, routePosition);
						from = routePosition;
					}
					Gizmos.DrawLine(from, this.Waypoints[0].position);
					return;
				}
				for (int i = 0; i < this.Waypoints.Length; i++)
				{
					Vector3 position = this.Waypoints[(i + 1) % this.Waypoints.Length].position;
					Gizmos.DrawLine(from, position);
					from = position;
				}
			}
		}

		// Token: 0x0400005E RID: 94
		public WaypointCircuit.WaypointList waypointList = new WaypointCircuit.WaypointList();

		// Token: 0x0400005F RID: 95
		[SerializeField]
		private bool smoothRoute = true;

		// Token: 0x04000060 RID: 96
		private int numPoints;

		// Token: 0x04000061 RID: 97
		private Vector3[] points;

		// Token: 0x04000062 RID: 98
		private float[] distances;

		// Token: 0x04000063 RID: 99
		public float editorVisualisationSubsteps = 100f;

		// Token: 0x04000065 RID: 101
		private int p0n;

		// Token: 0x04000066 RID: 102
		private int p1n;

		// Token: 0x04000067 RID: 103
		private int p2n;

		// Token: 0x04000068 RID: 104
		private int p3n;

		// Token: 0x04000069 RID: 105
		private float i;

		// Token: 0x0400006A RID: 106
		private Vector3 P0;

		// Token: 0x0400006B RID: 107
		private Vector3 P1;

		// Token: 0x0400006C RID: 108
		private Vector3 P2;

		// Token: 0x0400006D RID: 109
		private Vector3 P3;

		// Token: 0x02000075 RID: 117
		[Serializable]
		public class WaypointList
		{
			// Token: 0x040002B4 RID: 692
			public WaypointCircuit circuit;

			// Token: 0x040002B5 RID: 693
			public Transform[] items = new Transform[0];
		}

		// Token: 0x02000076 RID: 118
		public struct RoutePoint
		{
			// Token: 0x0600025D RID: 605 RVA: 0x0000CFAE File Offset: 0x0000B1AE
			public RoutePoint(Vector3 position, Vector3 direction)
			{
				this.position = position;
				this.direction = direction;
			}

			// Token: 0x040002B6 RID: 694
			public Vector3 position;

			// Token: 0x040002B7 RID: 695
			public Vector3 direction;
		}
	}
}
