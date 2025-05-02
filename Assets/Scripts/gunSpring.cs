// dnSpy decompiler from Assembly-CSharp.dll class: gunSpring
using System;
using UnityEngine;

public class gunSpring : MonoBehaviour
{
	private void Start()
	{
		this.startRotation = base.transform.localEulerAngles;
	}

	private void Update()
	{
		this.curY = this.yrotator.transform.localEulerAngles.y;
		this.velocityY = Mathf.DeltaAngle(this.curY, this.oldY);
		this.y -= this.velocityY;
		this.y -= this.yposition * 1.1f;
		this.y *= 0.8f;
		this.yposition += this.y * Time.deltaTime * this.speed;
		this.yposition = Mathf.Clamp(this.yposition, -0.3f, 0.3f);
		this.curX = this.xrotator.transform.localEulerAngles.x;
		this.velocityX = Mathf.DeltaAngle(this.curX, this.oldX);
		this.x -= this.velocityX;
		this.x -= this.xposition * 1.1f;
		this.x *= 0.8f;
		this.xposition += this.x * Time.deltaTime * this.speed;
		this.xposition = Mathf.Clamp(this.xposition, -0.3f, 0.3f);
		if (Mathf.Abs(this.y) < this.VelocityThreshold && Mathf.Abs(this.yposition) < this.PositionThreshold)
		{
			this.y = 0f;
			this.yposition = 0f;
		}
		if (Mathf.Abs(this.x) < this.VelocityThreshold && Mathf.Abs(this.xposition) < this.PositionThreshold)
		{
			this.x = 0f;
			this.xposition = 0f;
		}
		this.wantedRotation = new Vector3(this.xposition * this.rotateamount, this.yposition * this.rotateamount, 0f);
		base.transform.localEulerAngles = this.startRotation + this.wantedRotation;
		this.oldY = this.curY;
		this.oldX = this.curX;
	}

	public Transform yrotator;

	public Transform xrotator;

	public float rotateamount = 10f;

	private Vector3 wantedRotation;

	private Vector3 startRotation;

	public float speed = 1f;

	private float curY;

	private float curX;

	private float yposition;

	private float xposition;

	private float oldY;

	private float oldX;

	private float VelocityThreshold = 0.01f;

	private float PositionThreshold = 0.01f;

	private float y;

	private float x;

	private float velocityY;

	private float velocityX;
}
