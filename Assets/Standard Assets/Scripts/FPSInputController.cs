// dnSpy decompiler from Assembly-UnityScript-firstpass.dll class: FPSInputController
using System;
using UnityEngine;

[AddComponentMenu("Character/FPS Input Controller")]
[RequireComponent(typeof(CharacterMotor))]
[Serializable]
public class FPSInputController : MonoBehaviour
{
	public virtual void Awake()
	{
		this.motor = (CharacterMotor)this.GetComponent(typeof(CharacterMotor));
	}

	public virtual void Update()
	{
		Vector3 vector = new Vector3(UnityEngine.Input.GetAxis("Horizontal"), (float)0, UnityEngine.Input.GetAxis("Vertical"));
		if (vector != Vector3.zero)
		{
			float num = vector.magnitude;
			vector /= num;
			num = Mathf.Min((float)1, num);
			num *= num;
			vector *= num;
		}
		this.motor.inputMoveDirection = this.transform.rotation * vector;
		this.motor.inputJump = Input.GetButton("Jump");
	}

	public virtual void Main()
	{
	}

	private CharacterMotor motor;
}
