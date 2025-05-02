// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.CFUtils
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ControlFreak2
{
	public static class CFUtils
	{
		public static float realDeltaTime
		{
			get
			{
				return (Time.captureFramerate > 0) ? (1f / (float)Time.captureFramerate) : Time.unscaledDeltaTime;
			}
		}

		public static float realDeltaTimeClamped
		{
			get
			{
				return Mathf.Min(CFUtils.realDeltaTime, 0.5f);
			}
		}

		public static bool editorStopped
		{
			get
			{
				return false;
			}
		}

		public static bool forcedMobileModeEnabled
		{
			get
			{
				return true;
			}
		}

		public static string LogPrefixFull()
		{
			return "[" + Time.frameCount + "] ";
		}

		public static string LogPrefix()
		{
			return "[" + Time.frameCount + "] ";
		}

		public static float ApplyDeltaInput(float accum, float delta)
		{
			if (accum >= 0f && delta >= 0f)
			{
				return Mathf.Max(accum, delta);
			}
			if (accum < 0f && delta < 0f)
			{
				return Mathf.Min(accum, delta);
			}
			return accum + delta;
		}

		public static int ApplyDeltaInputInt(int accum, int delta)
		{
			if (accum >= 0 && delta >= 0)
			{
				return Mathf.Max(accum, delta);
			}
			if (accum < 0 && delta < 0)
			{
				return Mathf.Min(accum, delta);
			}
			return accum + delta;
		}

		public static float ApplyPositveDeltaInput(float positiveAccum, float delta)
		{
			return (delta <= 0f || delta <= positiveAccum) ? positiveAccum : delta;
		}

		public static float ApplyNegativeDeltaInput(float negativeAccum, float delta)
		{
			return (delta >= 0f || delta >= negativeAccum) ? negativeAccum : delta;
		}

		public static void ApplySignedDeltaInput(float v, ref float plusAccum, ref float minusAccum)
		{
			if (v >= 0f)
			{
				if (v > plusAccum)
				{
					plusAccum = v;
				}
			}
			else if (v < minusAccum)
			{
				minusAccum = v;
			}
		}

		public static void ApplySignedDeltaInputInt(int v, ref int plusAccum, ref int minusAccum)
		{
			if (v >= 0)
			{
				if (v > plusAccum)
				{
					plusAccum = v;
				}
			}
			else if (v < minusAccum)
			{
				minusAccum = v;
			}
		}

		public static int GetScrollValue(float drag, int prevScroll, float thresh, float magnet)
		{
			bool flag = drag < 0f;
			if (flag)
			{
				drag = -drag;
				prevScroll = -prevScroll;
			}
			float f = drag / thresh;
			int num = Mathf.FloorToInt(f);
			return (!flag) ? num : (-num);
		}

		public static float MoveTowards(float a, float b, float secondsPerUnit, float deltaTime, float epsilon)
		{
			if (Mathf.Abs(b - a) <= epsilon)
			{
				return b;
			}
			return (secondsPerUnit >= 0.001f && secondsPerUnit > deltaTime) ? Mathf.MoveTowards(a, b, deltaTime / secondsPerUnit) : b;
		}

		private static float GetLerpFactor(float smoothingTime, float deltaTime, float maxLerpFactor)
		{
			return Mathf.Min(maxLerpFactor, (smoothingTime > deltaTime) ? (deltaTime / smoothingTime) : 1f);
		}

		public static float SmoothTowards(float a, float b, float smoothingTime, float deltaTime, float epsilon, float maxLeftFactor = 0.75f)
		{
			if (Mathf.Abs(b - a) <= epsilon)
			{
				return b;
			}
			return (smoothingTime >= 0.001f) ? Mathf.Lerp(a, b, CFUtils.GetLerpFactor(smoothingTime, deltaTime, maxLeftFactor)) : b;
		}

		public static float SmoothTowardsAngle(float a, float b, float smoothingTime, float deltaTime, float epsilon, float maxLeftFactor = 0.75f)
		{
			if (Mathf.Abs(Mathf.DeltaAngle(b, a)) <= epsilon)
			{
				return b;
			}
			return (smoothingTime >= 0.001f) ? Mathf.MoveTowardsAngle(a, b, Mathf.Abs(b - a) * CFUtils.GetLerpFactor(smoothingTime, deltaTime, maxLeftFactor)) : b;
		}

		public static Vector2 SmoothTowardsVec2(Vector2 a, Vector2 b, float smoothingTime, float deltaTime, float sqrEpsilon, float maxLeftFactor = 0.75f)
		{
			if (sqrEpsilon != 0f && (b - a).sqrMagnitude <= sqrEpsilon)
			{
				return b;
			}
			return (smoothingTime >= 0.001f) ? Vector2.Lerp(a, b, CFUtils.GetLerpFactor(smoothingTime, deltaTime, maxLeftFactor)) : b;
		}

		public static Vector3 SmoothTowardsVec3(Vector3 a, Vector3 b, float smoothingTime, float deltaTime, float sqrEpsilon, float maxLeftFactor = 0.75f)
		{
			if (sqrEpsilon != 0f && (b - a).sqrMagnitude <= sqrEpsilon)
			{
				return b;
			}
			return (smoothingTime >= 0.001f) ? Vector3.Lerp(a, b, CFUtils.GetLerpFactor(smoothingTime, deltaTime, maxLeftFactor)) : b;
		}

		public static Color SmoothTowardsColor(Color a, Color b, float smoothingTime, float deltaTime, float maxLeftFactor = 0.75f)
		{
			return (smoothingTime >= 0.001f) ? Color.Lerp(a, b, CFUtils.GetLerpFactor(smoothingTime, deltaTime, maxLeftFactor)) : b;
		}

		public static Quaternion SmoothTowardsQuat(Quaternion a, Quaternion b, float smoothingTime, float deltaTime, float maxLeftFactor = 0.75f)
		{
			return (smoothingTime >= 0.001f) ? Quaternion.Slerp(a, b, CFUtils.GetLerpFactor(smoothingTime, deltaTime, maxLeftFactor)) : b;
		}

		public static float SmoothDamp(float valFrom, float valTo, ref float vel, float smoothingTime, float deltaTime, float epsilon)
		{
			if (smoothingTime < 0.001f || Mathf.Abs(valTo - valFrom) <= epsilon)
			{
				vel = 0f;
				return valTo;
			}
			return Mathf.SmoothDamp(valFrom, valTo, ref vel, smoothingTime, 1E+07f, deltaTime);
		}

		public static Color ScaleColorAlpha(Color color, float alphaScale)
		{
			color.a *= alphaScale;
			return color;
		}

		public static Component GetComponentHereOrInParent(Component comp, Type compType)
		{
			if (comp == null)
			{
				return null;
			}
			Component component;
			return (!((component = comp.GetComponent(compType)) != null)) ? comp.GetComponentInParent(compType) : component;
		}

		public static bool IsStretchyRectTransform(Transform tr)
		{
			RectTransform rectTransform = tr as RectTransform;
			return rectTransform != null && rectTransform.anchorMax != rectTransform.anchorMin;
		}

		public static Rect TransformRect(Rect r, Matrix4x4 tr, bool round)
		{
			Bounds bounds = CFUtils.TransformRectAsBounds(r, tr, round);
			Vector3 min = bounds.min;
			Vector3 size = bounds.size;
			return new Rect(min.x, min.y, size.x, size.y);
		}

		public static Bounds TransformRectAsBounds(Rect r, Matrix4x4 tr, bool round)
		{
			Vector3 a = tr.MultiplyPoint3x4(r.center);
			Vector3 vector = tr.MultiplyVector(new Vector3(1f, 0f, 0f));
			Vector3 vector2 = tr.MultiplyVector(new Vector3(0f, 1f, 0f));
			float d = r.width * 0.5f;
			float d2 = r.height * 0.5f;
			vector *= d;
			vector2 *= d2;
			Vector3 vector4;
			Vector3 vector5;
			if (round)
			{
				Vector3 vector3 = vector2;
				vector4 = Vector3.Min(vector3, -vector3);
				vector5 = Vector3.Max(vector3, -vector3);
				vector3 = vector * 0.77f + vector2 * 0.77f;
				vector4 = Vector3.Min(vector4, Vector3.Min(vector3, -vector3));
				vector5 = Vector3.Max(vector5, Vector3.Max(vector3, -vector3));
				vector3 = vector;
				vector4 = Vector3.Min(vector4, Vector3.Min(vector3, -vector3));
				vector5 = Vector3.Max(vector5, Vector3.Max(vector3, -vector3));
				vector3 = vector * -0.77f + vector2 * 0.77f;
				vector4 = Vector3.Min(vector4, Vector3.Min(vector3, -vector3));
				vector5 = Vector3.Max(vector5, Vector3.Max(vector3, -vector3));
			}
			else
			{
				Vector3 vector3 = vector + vector2;
				vector4 = Vector3.Min(vector3, -vector3);
				vector5 = Vector3.Max(vector3, -vector3);
				vector3 = vector - vector2;
				vector4 = Vector3.Min(vector4, Vector3.Min(vector3, -vector3));
				vector5 = Vector3.Max(vector5, Vector3.Max(vector3, -vector3));
			}
			return new Bounds(a + (vector4 + vector5) * 0.5f, vector5 - vector4);
		}

		public static Matrix4x4 ChangeMatrixTranl(Matrix4x4 m, Vector3 newTransl)
		{
			m.SetColumn(3, new Vector4(newTransl.x, newTransl.y, newTransl.z, m.m33));
			return m;
		}

		public static Bounds GetWorldAABB(Matrix4x4 tf, Vector3 center, Vector3 size)
		{
			Vector3 vector = center - size * 0.5f;
			Vector3 vector2 = center + size * 0.5f;
			Vector3 vector3 = tf.MultiplyPoint3x4(new Vector3(vector.x, vector.y, vector.z));
			Vector3 vector5;
			Vector3 vector4 = vector5 = vector3;
			vector3 = tf.MultiplyPoint3x4(new Vector3(vector.x, vector.y, vector2.z));
			vector5 = Vector3.Min(vector5, vector3);
			vector4 = Vector3.Max(vector4, vector3);
			vector3 = tf.MultiplyPoint3x4(new Vector3(vector.x, vector2.y, vector.z));
			vector5 = Vector3.Min(vector5, vector3);
			vector4 = Vector3.Max(vector4, vector3);
			vector3 = tf.MultiplyPoint3x4(new Vector3(vector.x, vector2.y, vector2.z));
			vector5 = Vector3.Min(vector5, vector3);
			vector4 = Vector3.Max(vector4, vector3);
			vector3 = tf.MultiplyPoint3x4(new Vector3(vector2.x, vector.y, vector.z));
			vector5 = Vector3.Min(vector5, vector3);
			vector4 = Vector3.Max(vector4, vector3);
			vector3 = tf.MultiplyPoint3x4(new Vector3(vector2.x, vector2.y, vector.z));
			vector5 = Vector3.Min(vector5, vector3);
			vector4 = Vector3.Max(vector4, vector3);
			vector3 = tf.MultiplyPoint3x4(new Vector3(vector2.x, vector.y, vector2.z));
			vector5 = Vector3.Min(vector5, vector3);
			vector4 = Vector3.Max(vector4, vector3);
			vector3 = tf.MultiplyPoint3x4(new Vector3(vector2.x, vector2.y, vector2.z));
			vector5 = Vector3.Min(vector5, vector3);
			vector4 = Vector3.Max(vector4, vector3);
			return new Bounds((vector5 + vector4) * 0.5f, vector4 - vector5);
		}

		public static Bounds GetWorldAABB(Matrix4x4 tf, Bounds localBounds)
		{
			return CFUtils.GetWorldAABB(tf, localBounds.center, localBounds.size);
		}

		public static Rect GetWorldRect(Matrix4x4 tf, Bounds localBounds)
		{
			Bounds worldAABB = CFUtils.GetWorldAABB(tf, localBounds);
			Vector3 min = worldAABB.min;
			Vector3 max = worldAABB.max;
			return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
		}

		public static Vector2 ClampRectInside(Rect rect, bool rectIsRound, Rect limiterRect, bool limiterIsRound)
		{
			if (!limiterIsRound)
			{
				return CFUtils.ClampRectInsideRect(rect, limiterRect);
			}
			if (rectIsRound)
			{
				return CFUtils.ClampEllipseInsideEllipse(rect, limiterRect);
			}
			return CFUtils.ClampRectInsideEllipse(rect, limiterRect);
		}

		public static Vector2 ClampRectInsideEllipse(Rect rect, Rect limiterRect)
		{
			return CFUtils.ClampEllipseInsideEllipse(rect, limiterRect);
		}

		public static Vector2 ClampEllipseInsideEllipse(Rect rect, Rect limiterRect)
		{
			Vector2 b = rect.size * 0.5f;
			Vector2 a = limiterRect.size * 0.5f;
			Vector2 center = rect.center;
			Vector2 center2 = limiterRect.center;
			if (b.x >= a.x || b.y >= a.y)
			{
				Vector2 result;
				if (b.x >= a.x)
				{
					result.x = center2.x - center.x;
				}
				else
				{
					result.x = ((center.x < center2.x) ? Mathf.Max(0f, center2.x - a.x - (center.x - b.x)) : Mathf.Min(0f, center2.x + a.x - (center.x + b.x)));
				}
				if (b.y >= a.y)
				{
					result.y = center2.y - center.y;
				}
				else
				{
					result.y = ((center.y < center2.y) ? Mathf.Max(0f, center2.y - a.y - (center.y - b.y)) : Mathf.Min(0f, center2.y + a.y - (center.y + b.y)));
				}
				return result;
			}
			Vector2 vector = a - b;
			Vector2 vector2 = center - center2;
			Vector2 a2 = vector2;
			a2.x /= vector.x;
			a2.y /= vector.y;
			if (a2.sqrMagnitude < 1f)
			{
				return Vector2.zero;
			}
			a2.Normalize();
			a2.x *= vector.x;
			a2.y *= vector.y;
			return a2 - vector2;
		}

		public static Vector2 ClampRectInsideRect(Rect rect, Rect limiterRect)
		{
			Vector2 center = rect.center;
			Vector2 center2 = limiterRect.center;
			Vector2 b = rect.size * 0.5f;
			Vector2 b2 = limiterRect.size * 0.5f;
			Vector2 vector = center - b;
			Vector2 vector2 = center + b;
			Vector2 vector3 = center2 - b2;
			Vector2 vector4 = center2 + b2;
			Vector2 zero = Vector2.zero;
			if (b.x >= b2.x)
			{
				zero.x = center2.x - center.x;
			}
			else if (vector2.x > vector4.x)
			{
				zero.x = vector4.x - vector2.x;
			}
			else if (vector.x < vector3.x)
			{
				zero.x = vector3.x - vector.x;
			}
			if (b.y >= b2.y)
			{
				zero.y = center2.y - center.y;
			}
			else if (vector2.y > vector4.y)
			{
				zero.y = vector4.y - vector2.y;
			}
			else if (vector.y < vector3.y)
			{
				zero.y = vector3.y - vector.y;
			}
			return zero;
		}

		public static Vector2 ClampInsideUnitCircle(Vector2 np)
		{
			return (np.sqrMagnitude >= 1f) ? np.normalized : np;
		}

		public static Vector2 ClampInsideUnitSquare(Vector2 np)
		{
			float num = 1f;
			if (np.x > 1f || np.x < -1f)
			{
				num = Mathf.Abs(1f / np.x);
			}
			if (np.y > 1f || np.y < -1f)
			{
				num = Mathf.Min(num, Mathf.Abs(1f / np.y));
			}
			return (num >= 1f) ? np : (np * num);
		}

		public static Vector2 ClampPerAxisInsideUnitSquare(Vector2 np)
		{
			np.x = Mathf.Clamp(np.x, -1f, 1f);
			np.y = Mathf.Clamp(np.y, -1f, 1f);
			return np;
		}

		public static Vector2 ClampInsideRect(Vector2 v, Rect r)
		{
			if (r.Contains(v))
			{
				return v;
			}
			Vector2 center = r.center;
			Vector2 vector = r.size * 0.5f;
			v -= center;
			float num = 1f;
			if (v.x > vector.x || v.x < -vector.x)
			{
				num = Mathf.Abs(vector.x / v.x);
			}
			if (v.y > vector.y || v.y < -vector.y)
			{
				num = Mathf.Min(num, Mathf.Abs(vector.y / v.y));
			}
			return center + v * num;
		}

		public static bool IsDirDiagonal(Dir dir)
		{
			return (dir - Dir.U & 1) == 1;
		}

		public static float VecToAngle(Vector2 vec)
		{
			return CFUtils.NormalizeAnglePositive(Mathf.Atan2(vec.x, vec.y) * 57.29578f);
		}

		public static float VecToAngle(Vector2 vec, float defaultAngle, float deadZoneSq)
		{
			float sqrMagnitude = vec.sqrMagnitude;
			if (sqrMagnitude < deadZoneSq)
			{
				return defaultAngle;
			}
			if (Mathf.Abs(sqrMagnitude - 1f) > 0.0001f)
			{
				vec.Normalize();
			}
			return CFUtils.NormalizeAnglePositive(Mathf.Atan2(vec.x, vec.y) * 57.29578f);
		}

		public static float DirToAngle(Dir d)
		{
			if (d < Dir.U || d > Dir.UL)
			{
				return 0f;
			}
			return (float)(d - Dir.U) * 45f;
		}

		public static Dir DirFromAngle(float ang, bool as8way)
		{
			ang += ((!as8way) ? 45f : 22.5f);
			ang = CFUtils.NormalizeAnglePositive(ang);
			Dir result;
			if (as8way)
			{
				if (ang < 45f)
				{
					result = Dir.U;
				}
				else if (ang < 90f)
				{
					result = Dir.UR;
				}
				else if (ang < 135f)
				{
					result = Dir.R;
				}
				else if (ang < 180f)
				{
					result = Dir.DR;
				}
				else if (ang < 225f)
				{
					result = Dir.D;
				}
				else if (ang < 270f)
				{
					result = Dir.DL;
				}
				else if (ang < 315f)
				{
					result = Dir.L;
				}
				else
				{
					result = Dir.UL;
				}
			}
			else if (ang < 90f)
			{
				result = Dir.U;
			}
			else if (ang < 180f)
			{
				result = Dir.R;
			}
			else if (ang < 270f)
			{
				result = Dir.D;
			}
			else
			{
				result = Dir.L;
			}
			return result;
		}

		public static Dir DirFromAngleEx(float ang, bool as8way, Dir lastDir, float magnetPow)
		{
			if (lastDir != Dir.N && magnetPow > 0.001f && Mathf.Abs(Mathf.DeltaAngle(ang, CFUtils.DirToAngle(lastDir))) < (1f + Mathf.Clamp01(magnetPow) * 0.5f) * ((!as8way) ? 45f : 22.5f))
			{
				return lastDir;
			}
			return CFUtils.DirFromAngle(ang, as8way);
		}

		public static int DirDeltaAngle(Dir dirFrom, Dir dirTo)
		{
			if (dirFrom == Dir.N || dirTo == Dir.N)
			{
				return 0;
			}
			return Mathf.RoundToInt(Mathf.DeltaAngle(CFUtils.DirToAngle(dirFrom), CFUtils.DirToAngle(dirTo)));
		}

		public static Vector2 DirToNormal(Dir dir)
		{
			switch (dir)
			{
			case Dir.U:
				return new Vector2(0f, 1f);
			case Dir.UR:
				return new Vector2(0.707106769f, 0.707106769f);
			case Dir.R:
				return new Vector2(1f, 0f);
			case Dir.DR:
				return new Vector2(0.707106769f, -0.707106769f);
			case Dir.D:
				return new Vector2(0f, -1f);
			case Dir.DL:
				return new Vector2(-0.707106769f, -0.707106769f);
			case Dir.L:
				return new Vector2(-1f, 0f);
			case Dir.UL:
				return new Vector2(-0.707106769f, 0.707106769f);
			default:
				return Vector2.zero;
			}
		}

		public static Vector2 DirToTangent(Dir dir)
		{
			switch (dir)
			{
			case Dir.U:
				return new Vector2(1f, 0f);
			case Dir.UR:
				return new Vector2(0.707106769f, -0.707106769f);
			case Dir.R:
				return new Vector2(0f, -1f);
			case Dir.DR:
				return new Vector2(-0.707106769f, -0.707106769f);
			case Dir.D:
				return new Vector2(-1f, 0f);
			case Dir.DL:
				return new Vector2(-0.707106769f, 0.707106769f);
			case Dir.L:
				return new Vector2(0f, 1f);
			case Dir.UL:
				return new Vector2(0.707106769f, 0.707106769f);
			default:
				return Vector2.zero;
			}
		}

		public static Dir GetOppositeDir(Dir dir)
		{
			switch (dir)
			{
			case Dir.U:
				return Dir.D;
			case Dir.UR:
				return Dir.DL;
			case Dir.R:
				return Dir.L;
			case Dir.DR:
				return Dir.UL;
			case Dir.D:
				return Dir.U;
			case Dir.DL:
				return Dir.UR;
			case Dir.L:
				return Dir.R;
			case Dir.UL:
				return Dir.DR;
			default:
				return Dir.N;
			}
		}

		public static Vector2 DirToVector(Dir dir, bool circular)
		{
			switch (dir)
			{
			case Dir.U:
				return new Vector2(0f, 1f);
			case Dir.UR:
			{
				Vector2 result;
				if (circular)
				{
					Vector2 vector = new Vector2(0.707106769f, 0.707106769f);
					result = vector.normalized;
				}
				else
				{
					result = new Vector2(1f, 1f);
				}
				return result;
			}
			case Dir.R:
				return new Vector2(1f, 0f);
			case Dir.DR:
			{
				Vector2 result2;
				if (circular)
				{
					Vector2 vector2 = new Vector2(0.707106769f, -0.707106769f);
					result2 = vector2.normalized;
				}
				else
				{
					result2 = new Vector2(1f, -1f);
				}
				return result2;
			}
			case Dir.D:
				return new Vector2(0f, -1f);
			case Dir.DL:
			{
				Vector2 result3;
				if (circular)
				{
					Vector2 vector3 = new Vector2(-0.707106769f, -0.707106769f);
					result3 = vector3.normalized;
				}
				else
				{
					result3 = new Vector2(-1f, -1f);
				}
				return result3;
			}
			case Dir.L:
				return new Vector2(-1f, 0f);
			case Dir.UL:
			{
				Vector2 result4;
				if (circular)
				{
					Vector2 vector4 = new Vector2(-0.707106769f, 0.707106769f);
					result4 = vector4.normalized;
				}
				else
				{
					result4 = new Vector2(-1f, 1f);
				}
				return result4;
			}
			default:
				return Vector2.zero;
			}
		}

		public static Dir VecToDir(Vector2 vec, bool as8way)
		{
			return CFUtils.DirFromAngle(CFUtils.VecToAngle(vec), as8way);
		}

		public static Dir VecToDir(Vector2 vec, Dir defaultDir, float deadZoneSq, bool as8way)
		{
			float sqrMagnitude = vec.sqrMagnitude;
			if (sqrMagnitude <= deadZoneSq)
			{
				return defaultDir;
			}
			if (Mathf.Abs(sqrMagnitude - 1f) > 1E-05f)
			{
				vec.Normalize();
			}
			return CFUtils.DirFromAngle(CFUtils.VecToAngle(vec), as8way);
		}

		public static float NormalizeAnglePositive(float a)
		{
			if (a >= 360f)
			{
				return Mathf.Repeat(a, 360f);
			}
			if (a >= 0f)
			{
				return a;
			}
			if (a <= -360f)
			{
				a = Mathf.Repeat(a, 360f);
			}
			return 360f + a;
		}

		public static float SmartDeltaAngle(float startAngle, float curAngle, float lastDelta)
		{
			float num = Mathf.DeltaAngle(startAngle + lastDelta, curAngle);
			return lastDelta + num;
		}

		public static Dir DigitalToDir(bool digiU, bool digiR, bool digiD, bool digiL)
		{
			if (digiU && digiD)
			{
				digiD = (digiU = false);
			}
			if (digiR && digiL)
			{
				digiL = (digiR = false);
			}
			if (digiU)
			{
				return (!digiR) ? ((!digiL) ? Dir.U : Dir.UL) : Dir.UR;
			}
			if (digiD)
			{
				return (!digiR) ? ((!digiL) ? Dir.D : Dir.DL) : Dir.DR;
			}
			return (!digiR) ? ((!digiL) ? Dir.N : Dir.L) : Dir.R;
		}

		public static Vector2 CircularToSquareJoystickVec(Vector2 circularVec)
		{
			if (circularVec.sqrMagnitude < 1E-05f)
			{
				return Vector2.zero;
			}
			Vector2 vector = circularVec;
			Vector2 normalized = circularVec.normalized;
			vector *= Mathf.Abs(normalized.x) + Mathf.Abs(normalized.y);
			vector.x = Mathf.Clamp(vector.x, -1f, 1f);
			vector.y = Mathf.Clamp(vector.y, -1f, 1f);
			return vector;
		}

		public static Vector2 SquareToCircularJoystickVec(Vector2 squareVec)
		{
			if (squareVec.sqrMagnitude < 1E-05f)
			{
				return Vector2.zero;
			}
			Vector2 vector = squareVec;
			Vector2 normalized = squareVec.normalized;
			vector /= Mathf.Abs(normalized.x) + Mathf.Abs(normalized.y);
			vector.x = Mathf.Clamp(vector.x, -1f, 1f);
			vector.y = Mathf.Clamp(vector.y, -1f, 1f);
			return vector;
		}

		public static int GetLineNumber(string str, int index)
		{
			int num = 1;
			int startIndex = 0;
			int num2;
			while ((num2 = str.IndexOf('\n', startIndex)) >= 0 && num2 < index)
			{
				num++;
				startIndex = num2 + 1;
			}
			return num;
		}

		public static int GetEnumMaxValue(Type enumType)
		{
			int num = 0;
			IEnumerator enumerator = Enum.GetValues(enumType).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					int num2 = (int)obj;
					num = ((num != 0) ? Mathf.Max(num, num2) : num2);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return num;
		}

		public static int CycleInt(int curId, int dir, int maxId)
		{
			curId += dir;
			if (curId < 0)
			{
				curId = maxId;
			}
			else if (curId > maxId)
			{
				curId = 0;
			}
			return curId;
		}

		public static void SetEventSystemSelectedObject(GameObject o)
		{
			EventSystem current = EventSystem.current;
			if (current != null)
			{
				current.firstSelectedGameObject = o;
				if (current.currentInputModule is GamepadInputModule)
				{
					current.SetSelectedGameObject(null, null);
					current.SetSelectedGameObject(o, null);
				}
			}
		}

		public const float SqrtOf2 = 1.41421354f;

		public const float SqrtOf3 = 1.73205078f;

		public const float OneOverSqrtOf2 = 0.707106769f;

		public const float OneOverSqrtOf3 = 0.577350259f;

		public const float TanLenFor45Tri = 0.765366852f;

		public const float TanLenFor90Tri = 1.41421354f;

		public const float MAX_DELTA_TIME = 0.5f;

		private const float MAX_LEFT_FACTOR = 0.75f;
	}
}
