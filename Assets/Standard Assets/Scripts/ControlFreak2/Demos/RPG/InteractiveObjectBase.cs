// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.RPG.InteractiveObjectBase
using System;
using UnityEngine;

namespace ControlFreak2.Demos.RPG
{
	public abstract class InteractiveObjectBase : MonoBehaviour
	{
		public abstract void OnCharacterAction(CharacterAction chara);

		public virtual bool IsNear(CharacterAction chara)
		{
			return (chara.transform.position - base.transform.position).sqrMagnitude < this.radius * this.radius;
		}

		private void OnDrawGizmos()
		{
			this.DrawDefaultGizmo();
		}

		protected void DrawDefaultGizmo()
		{
			Gizmos.matrix = Matrix4x4.identity;
			Gizmos.DrawWireSphere(base.transform.position, this.radius);
		}

		public float radius = 2f;
	}
}
