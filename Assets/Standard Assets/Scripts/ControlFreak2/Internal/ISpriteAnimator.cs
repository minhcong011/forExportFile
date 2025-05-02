// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.ISpriteAnimator
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	public interface ISpriteAnimator
	{
		void AddUsedSprites(ISpriteOptimizer optimizer);

		void OnSpriteOptimization(ISpriteOptimizer optimizer);

		MonoBehaviour GetComponent();
	}
}
