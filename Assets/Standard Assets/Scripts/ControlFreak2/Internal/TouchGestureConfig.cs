// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.TouchGestureConfig
using System;

namespace ControlFreak2.Internal
{
	[Serializable]
	public class TouchGestureConfig
	{
		public TouchGestureConfig()
		{
			this.maxTapCount = 1;
			this.cleanTapsOnly = true;
			this.swipeOriginalDirResetMode = DirectionState.OriginalDirResetMode.On180;
		}

		public int maxTapCount;

		public bool cleanTapsOnly;

		public bool detectLongPress;

		public bool detectLongTap;

		public bool endLongPressWhenMoved;

		public bool endLongPressWhenSwiped;

		public TouchGestureConfig.DirMode dirMode;

		public DirectionState.OriginalDirResetMode swipeOriginalDirResetMode;

		public TouchGestureConfig.DirConstraint swipeConstraint;

		public TouchGestureConfig.DirConstraint swipeDirConstraint;

		public TouchGestureConfig.DirConstraint scrollConstraint;

		public enum DirMode
		{
			FourWay,
			EightWay
		}

		public enum DirConstraint
		{
			None,
			Horizontal,
			Vertical,
			Auto
		}
	}
}
