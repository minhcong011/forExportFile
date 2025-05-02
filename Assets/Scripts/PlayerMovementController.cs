// dnSpy decompiler from Assembly-CSharp.dll class: PlayerMovementController
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementController : MonoBehaviour
{
	private void Start()
	{
		this.originalCollheight = base.GetComponent<CapsuleCollider>().height;
		this.originalCollCenter = base.GetComponent<CapsuleCollider>().center;
		this.lastMovementTime = Time.time - 4f;
		this.lastCrouchTime = Time.time - 2f;
		this.originalFPSPos = this.fpsRoot.transform.localPosition;
		this.downTex.transform.localScale = Vector3.one;
	}

	public void leftPressed()
	{
		if (this.playerPositions.Count == 0)
		{
			return;
		}
		if (Time.time - this.lastMovementTime < this.delay)
		{
			return;
		}
		if (this.currentIndex == 0)
		{
			return;
		}
		if (this.playerPositions[this.currentIndex].position == this.playerPositions[this.currentIndex - 1].position)
		{
			return;
		}
		this.currentIndex--;
		this.moveTex.SetActive(false);
		if (this.currentIndex < 0)
		{
			this.currentIndex = 0;
		}
		this.lastMovementTime = Time.time;
		this.positionToMove = this.playerPositions[this.currentIndex].position;
		this.updatePosition = true;
		base.GetComponent<NavMeshAgent>().enabled = true;
	}

	public void RightPressed()
	{
		if (this.playerPositions.Count == 0)
		{
			return;
		}
		if (Time.time - this.lastMovementTime < this.delay)
		{
			return;
		}
		if (this.currentIndex == this.playerPositions.Count - 1)
		{
			return;
		}
		if (this.playerPositions[this.currentIndex].position == this.playerPositions[this.currentIndex + 1].position)
		{
			return;
		}
		this.moveTex.SetActive(false);
		this.currentIndex++;
		if (this.currentIndex >= this.playerPositions.Count)
		{
			this.currentIndex = this.playerPositions.Count - 1;
		}
		this.lastMovementTime = Time.time;
		this.positionToMove = this.playerPositions[this.currentIndex].position;
		this.updatePosition = true;
		base.GetComponent<NavMeshAgent>().enabled = true;
		this.swayObj.SendMessage("setChangePosition", true, SendMessageOptions.DontRequireReceiver);
		base.GetComponent<NavMeshAgent>().enabled = true;
	}

	public void rightDown()
	{
		this.positionToMove = this.playerPositions[2].position;
		this.updatePosition = true;
	}

	public void rightUp()
	{
		this.updatePosition = false;
		base.GetComponent<NavMeshAgent>().enabled = true;
		base.Invoke("setNavmeshOff", 0.6f);
	}

	public void LeftDown()
	{
		this.positionToMove = this.playerPositions[0].position;
		this.updatePosition = true;
	}

	public void leftUp()
	{
		this.updatePosition = false;
		base.GetComponent<NavMeshAgent>().enabled = true;
		base.Invoke("setNavmeshOff", 0.6f);
	}

	public void crouchPressed()
	{
		if (Time.time - this.lastCrouchTime < 1f)
		{
			return;
		}
		if (this.isStanding)
		{
			this.downTex.transform.localScale = new Vector3(1f, -1f, 1f);
			this.crouchPosToMove = new Vector3(0f, -2.6f, 0f);
			base.GetComponent<CapsuleCollider>().height = 1f;
			base.GetComponent<CapsuleCollider>().center = new Vector3(this.originalCollCenter.x, -1.2f, this.originalCollCenter.z);
		}
		else
		{
			this.downTex.transform.localScale = Vector3.one;
			this.crouchPosToMove = this.originalFPSPos;
			base.GetComponent<CapsuleCollider>().height = this.originalCollheight;
			base.GetComponent<CapsuleCollider>().center = new Vector3(this.originalCollCenter.x, this.originalCollCenter.y, this.originalCollCenter.z);
		}
		this.lastCrouchTime = Time.time;
		this.updatecrouchPosition = true;
		this.isStanding = !this.isStanding;
	}

	public void setPlayerPositions(List<Transform> positions)
	{
		this.playerPositions.Clear();
		this.playerPositions = positions;
	}

	public void setPlayerAtIndex(Transform t, int id = 1)
	{
		base.GetComponent<NavMeshAgent>().enabled = false;
		this.currentIndex = id;
		t.localPosition = this.playerPositions[this.currentIndex].localPosition;
		base.Invoke("setNavmeshOff", 2f);
		base.GetComponent<NavMeshAgent>().enabled = true;
	}

	private void setNavmeshOff()
	{
		base.GetComponent<NavMeshAgent>().enabled = false;
	}

	private void Update()
	{
		if (this.updatePosition)
		{
			this.weight = Time.deltaTime * this.speed;
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.positionToMove, this.weight);
		}
		if (this.updatecrouchPosition)
		{
			this.fpsRoot.transform.localPosition = Vector3.Lerp(this.fpsRoot.transform.localPosition, this.crouchPosToMove, 0.17f);
			if (Time.time - this.lastCrouchTime > 1.5f)
			{
				this.updatecrouchPosition = false;
			}
		}
	}

	public List<Transform> playerPositions;

	public int currentIndex = 1;

	public float delay = 4f;

	private float lastMovementTime;

	private bool updatePosition;

	private Vector3 positionToMove;

	public GameObject swayObj;

	public GameObject moveTex;

	public GameObject downTex;

	public bool isStanding = true;

	public GameObject fpsRoot;

	private float lastCrouchTime;

	private bool updatecrouchPosition;

	private Vector3 crouchPosToMove;

	private Vector3 originalFPSPos;

	private float originalCollheight;

	private Vector3 originalCollCenter;

	public float speed = 1f;

	private float weight;
}
