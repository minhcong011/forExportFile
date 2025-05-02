// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.Characters.DualStickShooterCharaMotor
using System;
using UnityEngine;

namespace ControlFreak2.Demos.Characters
{
	public class DualStickShooterCharaMotor : MonoBehaviour
	{
		private void OnEnable()
		{
			this.worldOrientAngle = (this.worldMoveAngle = DualStickShooterCharaMotor.GetAngleFromDir(base.transform.forward, DualStickShooterCharaMotor.WorldPlane.XZ, 0f));
			this.rb = base.GetComponent<Rigidbody>();
			this.MoveOrientation(this.worldOrientAngle);
		}

		private void MoveOrientation(float angle)
		{
			Quaternion quaternion = Quaternion.AngleAxis(angle + this.orientationOffset, Vector3.up);
			if (this.rb != null)
			{
				this.rb.MoveRotation(quaternion);
			}
			else
			{
				base.transform.rotation = quaternion;
			}
		}

		private void MovePosition(Vector3 worldPos)
		{
			if (this.rb != null)
			{
				this.rb.MovePosition(worldPos);
			}
			else
			{
				base.transform.position = worldPos;
			}
		}

		private void Update()
		{
			this.SetCameraRelativeInput(this.cameraTransform, (!string.IsNullOrEmpty(this.moveHorzAxis)) ? CF2Input.GetAxis(this.moveHorzAxis) : 0f, (!string.IsNullOrEmpty(this.moveVertAxis)) ? CF2Input.GetAxis(this.moveVertAxis) : 0f, (!string.IsNullOrEmpty(this.aimHorzAxis)) ? CF2Input.GetAxis(this.aimHorzAxis) : 0f, (!string.IsNullOrEmpty(this.aimVertAxis)) ? CF2Input.GetAxis(this.aimVertAxis) : 0f);
		}

		private void FixedUpdate()
		{
			float num = 0.01f;
			float target = 0f;
			float num2 = 0f;
			if (this.inputAimTilt > num)
			{
				target = this.inputAimAngle;
				num2 = this.inputAimTilt;
			}
			else if (this.inputMoveTilt > num)
			{
				target = this.inputMoveAngle;
				num2 = this.inputMoveTilt;
			}
			if (num2 > 0f)
			{
				this.worldOrientAngle = Mathf.MoveTowardsAngle(this.worldOrientAngle, target, num2 * this.turnSpeed * Time.deltaTime);
			}
			if (this.inputMoveTilt > num)
			{
				this.worldMoveAngle = this.inputMoveAngle;
				this.moveTilt = this.inputMoveTilt;
			}
			else
			{
				this.moveTilt = 0f;
			}
			float num3 = this.worldMoveAngle - this.worldOrientAngle;
			this.localDir = Quaternion.AngleAxis(num3, Vector3.up) * new Vector3(0f, 0f, 1f);
			this.localDir *= this.moveTilt;
			Vector3 point = this.localDir;
			point.x *= this.strafeSpeed;
			point.z *= ((this.localDir.z < 0f) ? this.backwardSpeed : this.forwardSpeed);
			this.worldVel = Quaternion.AngleAxis(-num3 + this.worldMoveAngle, Vector3.up) * point;
			Vector3 vector = base.transform.position;
			vector += this.worldVel * Time.deltaTime;
			vector += this.gravity * Time.deltaTime;
			this.MovePosition(vector);
			this.MoveOrientation(this.worldOrientAngle);
		}

		public float GetOrientAngle()
		{
			return this.worldOrientAngle;
		}

		public Vector3 GetLocalDir()
		{
			return this.localDir;
		}

		public void SetWorldInput(float moveAngle, float moveTilt, float aimAngle, float aimTilt)
		{
			this.inputMoveAngle = moveAngle;
			this.inputMoveTilt = moveTilt;
			this.inputAimAngle = aimAngle;
			this.inputAimTilt = aimTilt;
		}

		public void SetCameraRelativeInput(Transform camTr, float moveX, float moveY, float aimX, float aimY)
		{
			float num = 0f;
			float aimTilt = 0f;
			float joyAngleAndTilt = DualStickShooterCharaMotor.GetJoyAngleAndTilt(camTr, moveX, moveY, DualStickShooterCharaMotor.WorldPlane.XZ, out num);
			float joyAngleAndTilt2 = DualStickShooterCharaMotor.GetJoyAngleAndTilt(camTr, aimX, aimY, DualStickShooterCharaMotor.WorldPlane.XZ, out aimTilt);
			this.SetWorldInput(joyAngleAndTilt, num, joyAngleAndTilt2, aimTilt);
		}

		public static float GetAngleFromDir(Vector3 dir, DualStickShooterCharaMotor.WorldPlane plane, float fallbackAngle)
		{
			Vector2 vector = (plane != DualStickShooterCharaMotor.WorldPlane.XY) ? ((plane != DualStickShooterCharaMotor.WorldPlane.XZ) ? new Vector2(dir.z, dir.y) : new Vector2(dir.x, dir.z)) : new Vector2(dir.x, dir.y);
			if (vector.sqrMagnitude < 1E-06f)
			{
				return fallbackAngle;
			}
			vector.Normalize();
			return 57.29578f * Mathf.Atan2(vector.x, vector.y);
		}

		public static Vector3 GetWorldVecFromCamera(Transform camTr, float x, float y, DualStickShooterCharaMotor.WorldPlane plane)
		{
			Vector3 a;
			Vector3 a2;
			switch (plane)
			{
			case DualStickShooterCharaMotor.WorldPlane.XZ:
				a = camTr.right;
				a2 = camTr.forward;
				a.y = a.z;
				a2.y = a2.z;
				goto IL_A0;
			case DualStickShooterCharaMotor.WorldPlane.XY:
				a = camTr.right;
				a2 = camTr.up;
				a.z = 0f;
				a2.z = 0f;
				goto IL_A0;
			}
			a = camTr.forward;
			a2 = camTr.up;
			a.x = a.z;
			a2.x = a2.z;
			IL_A0:
			a.z = 0f;
			a2.z = 0f;
			a.Normalize();
			a2.Normalize();
			return a * x + a2 * y;
		}

		public static float GetJoyAngleAndTilt(Transform camTr, float x, float y, DualStickShooterCharaMotor.WorldPlane plane, out float tilt)
		{
			Vector2 vector = new Vector2(x, y);
			if (vector.sqrMagnitude < 1E-06f)
			{
				tilt = 0f;
				return 0f;
			}
			Vector2 a = DualStickShooterCharaMotor.GetWorldVecFromCamera(camTr, x, y, plane);
			float magnitude = a.magnitude;
			if (magnitude < 0.0001f)
			{
				tilt = 0f;
				return 0f;
			}
			a /= magnitude;
			tilt = Mathf.Min(magnitude, 1f);
			return 57.29578f * Mathf.Atan2(a.x, a.y);
		}

		public static Vector3 AngleToWorldVec(float angle, DualStickShooterCharaMotor.WorldPlane plane)
		{
			angle *= 0.0174532924f;
			float num = Mathf.Sin(angle);
			float num2 = Mathf.Cos(angle);
			switch (plane)
			{
			case DualStickShooterCharaMotor.WorldPlane.XZ:
				return new Vector3(num, 0f, num2);
			case DualStickShooterCharaMotor.WorldPlane.XY:
				return new Vector3(num, num2, 0f);
			}
			return new Vector3(0f, num2, num);
		}

		public static Vector3 RotateVecOnPlane(Vector3 v, float angle, DualStickShooterCharaMotor.WorldPlane plane)
		{
			angle *= 0.0174532924f;
			float num = Mathf.Sin(angle);
			float num2 = Mathf.Cos(angle);
			switch (plane)
			{
			case DualStickShooterCharaMotor.WorldPlane.XZ:
				return new Vector3(v.x * num2 - v.z * num, v.y, v.x * num + v.z * num2);
			case DualStickShooterCharaMotor.WorldPlane.XY:
				return new Vector3(v.x * num2 - v.y * num, v.x * num + v.y * num2, v.z);
			}
			return new Vector3(v.x, v.z * num + v.y * num2, v.z * num2 - v.y * num);
		}

		public Transform cameraTransform;

		public string moveHorzAxis = "Horizontal";

		public string moveVertAxis = "Vertical";

		public string aimHorzAxis = "Horizontal2";

		public string aimVertAxis = "Vertical2";

		private Rigidbody rb;

		public float forwardSpeed = 10f;

		public float strafeSpeed = 6f;

		public float backwardSpeed = 4f;

		public float turnSpeed = 720f;

		public float orientationOffset;

		public Vector3 gravity = new Vector3(0f, -2f, 0f);

		private Vector3 worldVel;

		private Vector3 localDir;

		private float moveTilt;

		private float worldMoveAngle;

		private float worldOrientAngle;

		private float inputMoveAngle;

		private float inputMoveTilt;

		private float inputAimAngle;

		private float inputAimTilt;

		public enum WorldPlane
		{
			XZ,
			XY,
			ZY
		}
	}
}
