// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.JoystickState
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	public class JoystickState
	{
		public JoystickState(JoystickConfig config)
		{
			this.config = config;
			this.dirState = new DirectionState();
			this.dirState4 = new DirectionState();
			this.dirState8 = new DirectionState();
			this.Reset();
		}

		public Vector2 GetVector()
		{
			return this.pos;
		}

		public Vector2 GetVectorRaw()
		{
			return this.posRaw;
		}

		public Vector2 GetVectorEx(bool squareMode)
		{
			if (this.config.clampMode == JoystickConfig.ClampMode.Square == squareMode)
			{
				return this.GetVector();
			}
			return (!squareMode) ? CFUtils.SquareToCircularJoystickVec(this.GetVector()) : CFUtils.CircularToSquareJoystickVec(this.GetVector());
		}

		public Vector2 GetVectorRawEx(bool squareMode)
		{
			if (this.config.clampMode == JoystickConfig.ClampMode.Square == squareMode)
			{
				return this.GetVectorRaw();
			}
			return (!squareMode) ? CFUtils.SquareToCircularJoystickVec(this.GetVectorRaw()) : CFUtils.CircularToSquareJoystickVec(this.GetVectorRaw());
		}

		public float GetAngle()
		{
			return this.angle;
		}

		public float GetTilt()
		{
			return this.tilt;
		}

		public DirectionState GetDirState()
		{
			return this.dirState;
		}

		public DirectionState GetDirState4()
		{
			return this.dirState4;
		}

		public DirectionState GetDirState8()
		{
			return this.dirState8;
		}

		public Dir GetDir()
		{
			return this.dirState.GetCur();
		}

		public Dir GetDir4()
		{
			return this.dirState4.GetCur();
		}

		public Dir GetDir8()
		{
			return this.dirState8.GetCur();
		}

		public bool JustPressedDir(Dir dir)
		{
			return this.dirState.JustPressed(dir);
		}

		public bool JustPressedDir4(Dir dir)
		{
			return this.dirState4.JustPressed(dir);
		}

		public bool JustPressedDir8(Dir dir)
		{
			return this.dirState8.JustPressed(dir);
		}

		public bool JustReleasedDir(Dir dir)
		{
			return this.dirState.JustReleased(dir);
		}

		public bool JustReleasedDir4(Dir dir)
		{
			return this.dirState4.JustReleased(dir);
		}

		public bool JustReleasedDir8(Dir dir)
		{
			return this.dirState8.JustReleased(dir);
		}

		public void SetConfig(JoystickConfig config)
		{
			this.config = config;
		}

		public void Reset()
		{
			this.normDir = Vector2.up;
			this.safeAngle = 0f;
			this.dirState.Reset();
			this.dirState4.Reset();
			this.dirState8.Reset();
			this.nextFrameDigiD = (this.nextFrameDigiL = (this.nextFrameDigiR = (this.nextFrameDigiU = false)));
			this.nextFramePos = Vector2.zero;
			this.digiInputAnalogVecCur = Vector2.zero;
			this.angle = 0f;
			this.dirLastNonNeutral4 = Dir.N;
			this.dirLastNonNeutral8 = Dir.N;
			this.normDir = new Vector2(0f, 1f);
			this.pos = (this.posRaw = Vector2.zero);
		}

		public void ApplyUnclampedVec(Vector2 v)
		{
			if (this.config.clampMode == JoystickConfig.ClampMode.Circle)
			{
				v = CFUtils.ClampInsideUnitCircle(v);
			}
			else
			{
				v = CFUtils.ClampInsideUnitSquare(v);
			}
			this.nextFramePos.x = CFUtils.ApplyDeltaInput(this.nextFramePos.x, v.x);
			this.nextFramePos.y = CFUtils.ApplyDeltaInput(this.nextFramePos.y, v.y);
		}

		public void ApplyClampedVec(Vector2 v, JoystickConfig.ClampMode clampMode)
		{
			if (clampMode != this.config.clampMode)
			{
				if (this.config.clampMode == JoystickConfig.ClampMode.Circle)
				{
					v = CFUtils.SquareToCircularJoystickVec(v);
				}
				else
				{
					v = CFUtils.CircularToSquareJoystickVec(v);
				}
			}
			this.nextFramePos.x = CFUtils.ApplyDeltaInput(this.nextFramePos.x, v.x);
			this.nextFramePos.y = CFUtils.ApplyDeltaInput(this.nextFramePos.y, v.y);
		}

		public void ApplyDir(Dir dir)
		{
			this.ApplyDigital(dir == Dir.U || dir == Dir.UL || dir == Dir.UR, dir == Dir.R || dir == Dir.UR || dir == Dir.DR, dir == Dir.D || dir == Dir.DL || dir == Dir.DR, dir == Dir.L || dir == Dir.UL || dir == Dir.DL);
		}

		public void ApplyDigital(bool digiU, bool digiR, bool digiD, bool digiL)
		{
			if (digiU)
			{
				this.nextFrameDigiU = true;
			}
			if (digiR)
			{
				this.nextFrameDigiR = true;
			}
			if (digiD)
			{
				this.nextFrameDigiD = true;
			}
			if (digiL)
			{
				this.nextFrameDigiL = true;
			}
		}

		public void ApplyState(JoystickState state)
		{
			if (this.config.stickMode == JoystickConfig.StickMode.Analog)
			{
				this.ApplyClampedVec(state.GetVectorRaw(), state.config.clampMode);
			}
			else if (this.config.stickMode == JoystickConfig.StickMode.Digital4)
			{
				this.ApplyDir(state.GetDir4());
			}
			else
			{
				this.ApplyDir(state.GetDir8());
			}
		}

		public void Update()
		{
			Dir targetDir = CFUtils.DigitalToDir(this.nextFrameDigiU, this.nextFrameDigiR, this.nextFrameDigiD, this.nextFrameDigiL);
			this.digiInputAnalogVecCur = this.config.AnimateDirToAnalogVec(this.digiInputAnalogVecCur, targetDir);
			this.posRaw.x = CFUtils.ApplyDeltaInput(this.nextFramePos.x, this.digiInputAnalogVecCur.x);
			this.posRaw.y = CFUtils.ApplyDeltaInput(this.nextFramePos.y, this.digiInputAnalogVecCur.y);
			this.nextFramePos = Vector2.zero;
			this.nextFrameDigiU = false;
			this.nextFrameDigiR = false;
			this.nextFrameDigiD = false;
			this.nextFrameDigiL = false;
			if (this.config.blockX)
			{
				this.posRaw.x = 0f;
			}
			if (this.config.blockY)
			{
				this.posRaw.y = 0f;
			}
			Vector2 vector = this.posRaw;
			this.posRaw = this.config.ClampNormalizedPos(this.posRaw);
			float num = this.posRaw.magnitude;
			float num2 = this.safeAngle;
			if (this.posRaw.sqrMagnitude < 0.0001f)
			{
				num = 0f;
				this.tilt = 0f;
			}
			else
			{
				this.normDir = this.posRaw.normalized;
				this.tilt = num;
				num2 = Mathf.Atan2(this.normDir.x, this.normDir.y) * 57.29578f;
				num2 = CFUtils.NormalizeAnglePositive(num2);
			}
			Dir dir = this.GetDir4();
			Dir dir2 = this.GetDir8();
			float num3 = num;
			float num4 = num;
			if (this.config.digitalDetectionMode == JoystickConfig.DigitalDetectionMode.Touch)
			{
				num3 = Mathf.Max(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
				num4 = num3;
				num4 = Mathf.Max(num4, Mathf.Abs(0.707106769f * vector.x + 0.707106769f * vector.y));
				num4 = Mathf.Max(num4, Mathf.Abs(0.707106769f * vector.x - 0.707106769f * vector.y));
			}
			if (num < 0.001f)
			{
				this.dirLastNonNeutral4 = (this.dirLastNonNeutral8 = Dir.N);
			}
			float num5 = (this.dirLastNonNeutral4 == Dir.N) ? (0.5f * (this.config.digitalEnterThresh + this.config.digitalLeaveThresh)) : this.config.digitalEnterThresh;
			dir = ((num3 <= num5) ? ((num3 <= this.config.digitalLeaveThresh) ? Dir.N : dir) : CFUtils.DirFromAngleEx(num2, false, this.dirLastNonNeutral4, this.config.angularMagnet));
			float num6 = (this.dirLastNonNeutral8 == Dir.N) ? (0.5f * (this.config.digitalEnterThresh + this.config.digitalLeaveThresh)) : this.config.digitalEnterThresh;
			dir2 = ((num4 <= num6) ? ((num4 <= this.config.digitalLeaveThresh) ? Dir.N : dir2) : CFUtils.DirFromAngleEx(num2, true, this.dirLastNonNeutral8, this.config.angularMagnet));
			if (dir != Dir.N)
			{
				this.dirLastNonNeutral4 = dir;
			}
			if (dir2 != Dir.N)
			{
				this.dirLastNonNeutral8 = dir2;
			}
			this.dirState.BeginFrame();
			this.dirState4.BeginFrame();
			this.dirState8.BeginFrame();
			this.dirState4.SetDir(dir, this.config.originalDirResetMode);
			this.dirState8.SetDir(dir2, this.config.originalDirResetMode);
			this.dirState.SetDir((this.config.stickMode != JoystickConfig.StickMode.Digital4) ? this.GetDir8() : this.GetDir4(), this.config.originalDirResetMode);
			JoystickConfig.StickMode stickMode = this.config.stickMode;
			if (stickMode != JoystickConfig.StickMode.Analog)
			{
				if (stickMode == JoystickConfig.StickMode.Digital4 || stickMode == JoystickConfig.StickMode.Digital8)
				{
					this.pos = CFUtils.DirToVector(this.GetDir(), this.config.clampMode == JoystickConfig.ClampMode.Circle);
					this.angle = CFUtils.DirToAngle(this.GetDir());
					this.tilt = (float)((this.GetDir() != Dir.N) ? 1 : 0);
					if (this.GetDir() != Dir.N)
					{
						this.normDir = this.pos.normalized;
					}
				}
			}
			else
			{
				this.angle = num2;
				if (this.config.perAxisDeadzones)
				{
					this.pos.x = this.config.GetAnalogVal(this.posRaw.x);
					this.pos.y = this.config.GetAnalogVal(this.posRaw.y);
					this.tilt = this.pos.magnitude;
				}
				else
				{
					this.tilt = this.config.GetAnalogVal(num);
					this.pos = this.normDir * this.tilt;
					if (this.config.clampMode == JoystickConfig.ClampMode.Square)
					{
						this.pos = CFUtils.CircularToSquareJoystickVec(this.pos);
					}
				}
			}
			this.safeAngle = this.angle;
		}

		public JoystickConfig config;

		private float tilt;

		private float angle;

		private float safeAngle;

		private Vector2 pos;

		private Vector2 posRaw;

		private Vector2 normDir;

		private DirectionState dirState;

		private DirectionState dirState4;

		private DirectionState dirState8;

		private Dir dirLastNonNeutral4;

		private Dir dirLastNonNeutral8;

		private Vector2 nextFramePos;

		private bool nextFrameDigiU;

		private bool nextFrameDigiR;

		private bool nextFrameDigiD;

		private bool nextFrameDigiL;

		private Vector2 digiInputAnalogVecCur;

		private const float MIN_SAFE_TILT = 0.01f;

		private const float MIN_SAFE_TILT_SQ = 0.0001f;
	}
}
