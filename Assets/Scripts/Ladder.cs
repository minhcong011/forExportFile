// dnSpy decompiler from Assembly-CSharp.dll class: Ladder
using System;
using UnityEngine;

public class Ladder : MonoBehaviour
{
	private void Start()
	{
		this.myrotation = base.transform.rotation;
		this.direction = this.ladderTop.transform.position - this.ladderBottom.transform.position;
		this.direction = this.direction.normalized;
	}

	private void Update()
	{
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			playercontroller component = other.GetComponent<playercontroller>();
			this.ControllerY = other.transform.position.y;
			this.test = true;
			this.delta = this.ladderTop.position - this.ladderBottom.position;
			this.lengthDiagonal = Mathf.Pow(this.delta.x * this.delta.x + this.delta.z * this.delta.z, 0.5f);
			if (this.lengthDiagonal == 0f)
			{
				this.wantedZ = this.ladderBottom.position.z;
				this.wantedX = this.ladderBottom.position.x;
			}
			else
			{
				this.lengthB = this.lengthDiagonal * ((this.ControllerY - this.ladderBottom.position.y) / (this.ladderTop.position.y - this.ladderBottom.position.y));
				this.wantedZ = this.ladderBottom.position.z + (this.ladderTop.position.z - this.ladderBottom.position.z) * (this.lengthB / this.lengthDiagonal);
				this.wantedX = this.ladderBottom.position.x + (this.ladderTop.position.x - this.ladderBottom.position.x) * (this.lengthB / this.lengthDiagonal);
			}
			this.wantedLadderposition = new Vector3(this.wantedX, this.ControllerY, this.wantedZ);
			component.climbdirection = this.direction;
			component.climbladder = true;
			component.ladderposition = this.wantedLadderposition;
			component.ladderforward = -base.transform.forward;
			component.ladderRotation = this.myrotation;
		}
		else
		{
			this.test = false;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			playercontroller component = other.GetComponent<playercontroller>();
			component.climbladder = false;
			this.test = false;
		}
		this.test = false;
	}

	public bool test;

	private Quaternion myrotation;

	private Vector3 direction;

	public Transform ladderTop;

	public Transform ladderBottom;

	private Vector3 wantedLadderposition;

	private float ControllerY;

	private float lengthDiagonal;

	private Vector3 delta;

	private float lengthB;

	private float wantedZ;

	private float wantedX;
}
