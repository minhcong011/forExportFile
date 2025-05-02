// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.DirectionState
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	public class DirectionState
	{
		public DirectionState()
		{
			this.Reset();
		}

		public Dir GetCur()
		{
			return this.dirCur;
		}

		public Dir GetPrev()
		{
			return this.dirPrev;
		}

		public Dir GetOriginal()
		{
			return this.dirOriginalCur;
		}

		public Dir GetPrevOriginal()
		{
			return this.dirOriginalPrev;
		}

		public bool JustPressed(Dir dir)
		{
			return this.dirPrev != dir && this.dirCur == dir;
		}

		public bool JustReleased(Dir dir)
		{
			return this.dirPrev == dir && this.dirCur != dir;
		}

		public void Reset()
		{
			this.dirCur = Dir.N;
			this.dirPrev = Dir.N;
			this.dirOriginalCur = Dir.N;
			this.dirOriginalPrev = Dir.N;
		}

		public void BeginFrame()
		{
			this.dirPrev = this.dirCur;
			this.dirOriginalPrev = this.dirOriginalCur;
		}

		public void SetDir(Dir dir, DirectionState.OriginalDirResetMode resetMode)
		{
			this.dirCur = dir;
			if (this.dirCur != this.dirPrev)
			{
				if (this.dirCur == Dir.N)
				{
					this.dirOriginalCur = Dir.N;
				}
				else if (this.dirPrev == Dir.N)
				{
					this.dirOriginalCur = this.dirCur;
				}
				else if (resetMode != DirectionState.OriginalDirResetMode.OnNeutral && Mathf.Abs(CFUtils.DirDeltaAngle(this.dirOriginalPrev, this.dirCur)) >= ((resetMode != DirectionState.OriginalDirResetMode.On90) ? ((resetMode != DirectionState.OriginalDirResetMode.On135) ? 180 : 135) : 90))
				{
					this.dirOriginalCur = this.dirCur;
				}
			}
		}

		private Dir dirCur;

		private Dir dirPrev;

		private Dir dirOriginalCur;

		private Dir dirOriginalPrev;

		public enum OriginalDirResetMode
		{
			OnNeutral,
			On180,
			On135,
			On90
		}
	}
}
