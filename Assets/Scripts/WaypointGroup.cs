// dnSpy decompiler from Assembly-CSharp.dll class: WaypointGroup
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaypointGroup : MonoBehaviour
{
	private void Start()
	{
		this.myTransform = base.transform;
		this.wayPoints.Clear();
		for (int i = 0; i < this.myTransform.childCount; i++)
		{
			this.wayPoints.Add(this.myTransform.GetChild(i));
		}
	}

	private void OnDrawGizmos()
	{
		if (this.drawWaypoints)
		{
			this.myTransform = base.transform;
			this.wayPoints.Clear();
			for (int i = 0; i < this.myTransform.childCount; i++)
			{
				this.wayPoints.Add(this.myTransform.GetChild(i));
			}
			if (this.wayPoints != null && this.wayPoints.Count != 0)
			{
				for (int j = 0; j < this.wayPoints.Count; j++)
				{
					if (this.wayPoints[j] != null)
					{
						Gizmos.color = Color.green;
						Gizmos.DrawWireSphere(this.wayPoints[j].position, 0.3f);
						if (j != this.wayPoints.Count - 1 && this.wayPoints[j + 1] != null && this.wayPoints.Count > 1)
						{
							if (Physics.Linecast(this.wayPoints[j].position, this.wayPoints[j + 1].position))
							{
								Gizmos.color = Color.red;
								Gizmos.DrawLine(this.wayPoints[j].position, this.wayPoints[j + 1].position);
							}
							else
							{
								Gizmos.color = Color.green;
								Gizmos.DrawLine(this.wayPoints[j].position, this.wayPoints[j + 1].position);
							}
						}
					}
				}
			}
		}
	}

	[Tooltip("True if waypoints in this group and lines between them should be drawn in editor.")]
	public bool drawWaypoints = true;

	[HideInInspector]
	public List<Transform> wayPoints = new List<Transform>();

	private Transform myTransform;
}
