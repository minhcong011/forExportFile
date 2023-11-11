using System;
using UnityEngine;

namespace UnityStandardAssets.Water
{
	// Token: 0x02000027 RID: 39
	public class MeshContainer
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00004855 File Offset: 0x00002A55
		public MeshContainer(Mesh m)
		{
			this.mesh = m;
			this.vertices = m.vertices;
			this.normals = m.normals;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000487C File Offset: 0x00002A7C
		public void Update()
		{
			this.mesh.vertices = this.vertices;
			this.mesh.normals = this.normals;
		}

		// Token: 0x040000A0 RID: 160
		public Mesh mesh;

		// Token: 0x040000A1 RID: 161
		public Vector3[] vertices;

		// Token: 0x040000A2 RID: 162
		public Vector3[] normals;
	}
}
