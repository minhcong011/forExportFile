using System;
using ProBuilder2.Common;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class HighlightNearestFace : MonoBehaviour
{
	// Token: 0x06000004 RID: 4 RVA: 0x000020DC File Offset: 0x000002DC
	private void Start()
	{
		this.target = pb_ShapeGenerator.PlaneGenerator(this.travel, this.travel, 25, 25, Axis.Up, false);
		this.target.SetFaceMaterial(this.target.faces, pb_Constant.DefaultMaterial);
		this.target.transform.position = new Vector3(this.travel * 0.5f, 0f, this.travel * 0.5f);
		this.target.ToMesh();
		this.target.Refresh(RefreshMask.All);
		Camera main = Camera.main;
		main.transform.position = new Vector3(25f, 40f, 0f);
		main.transform.localRotation = Quaternion.Euler(new Vector3(65f, 0f, 0f));
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000021B8 File Offset: 0x000003B8
	private void Update()
	{
		float num = Time.time * this.speed;
		Vector3 position = new Vector3(Mathf.PerlinNoise(num, num) * this.travel, 2f, Mathf.PerlinNoise(num + 1f, num + 1f) * this.travel);
		base.transform.position = position;
		if (this.target == null)
		{
			Debug.LogWarning("Missing the ProBuilder Mesh target!");
			return;
		}
		Vector3 a = this.target.transform.InverseTransformPoint(base.transform.position);
		if (this.nearest != null)
		{
			this.target.SetFaceColor(this.nearest, Color.white);
		}
		int num2 = this.target.faces.Length;
		float num3 = float.PositiveInfinity;
		this.nearest = this.target.faces[0];
		for (int i = 0; i < num2; i++)
		{
			float num4 = Vector3.Distance(a, this.FaceCenter(this.target, this.target.faces[i]));
			if (num4 < num3)
			{
				num3 = num4;
				this.nearest = this.target.faces[i];
			}
		}
		this.target.SetFaceColor(this.nearest, Color.blue);
		this.target.RefreshColors();
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002300 File Offset: 0x00000500
	private Vector3 FaceCenter(pb_Object pb, pb_Face face)
	{
		Vector3[] vertices = pb.vertices;
		Vector3 zero = Vector3.zero;
		foreach (int num in face.distinctIndices)
		{
			zero.x += vertices[num].x;
			zero.y += vertices[num].y;
			zero.z += vertices[num].z;
		}
		float num2 = (float)face.distinctIndices.Length;
		zero.x /= num2;
		zero.y /= num2;
		zero.z /= num2;
		return zero;
	}

	// Token: 0x04000001 RID: 1
	public float travel = 50f;

	// Token: 0x04000002 RID: 2
	public float speed = 0.2f;

	// Token: 0x04000003 RID: 3
	private pb_Object target;

	// Token: 0x04000004 RID: 4
	private pb_Face nearest;
}
