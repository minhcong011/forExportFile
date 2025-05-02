// dnSpy decompiler from Assembly-CSharp.dll class: door1
using System;
using ControlFreak2;
using UnityEngine;

public class door1 : MonoBehaviour
{
	private void Start()
	{
		this.startposition = this.door.transform.localPosition;
		this.currentposition = this.startposition;
		this.oldposition = this.currentposition;
	}

	private void Update()
	{
		this.currentposition = this.door.transform.localPosition;
		this.doorvelocity = (this.currentposition - this.oldposition).magnitude;
		if (this.opendoor)
		{
			this.door.transform.localPosition = Vector3.MoveTowards(this.door.transform.localPosition, this.startposition + this.moveposition, this.speed * Time.deltaTime);
		}
		else
		{
			this.door.transform.localPosition = Vector3.MoveTowards(this.door.transform.localPosition, this.startposition, this.speed * Time.deltaTime);
		}
		if (this.doorvelocity == 0f)
		{
			this.myAudioSource.Stop();
		}
		else if (!this.myAudioSource.isPlaying)
		{
			this.myAudioSource.loop = true;
			this.myAudioSource.clip = this.moveSound;
			this.myAudioSource.Play();
		}
		this.oldposition = this.currentposition;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player" && this.canOpen && CF2Input.GetButtonDown("Action"))
		{
			this.myAudioSource2.PlayOneShot(this.actionsound);
			this.opendoor = !this.opendoor;
		}
	}

	public Transform door;

	public AudioSource myAudioSource;

	public AudioSource myAudioSource2;

	public AudioClip moveSound;

	public AudioClip actionsound;

	public bool canOpen = true;

	public float speed = 2f;

	public Vector3 moveposition = Vector3.zero;

	private Vector3 oldposition;

	private Vector3 currentposition;

	private Vector3 startposition;

	private float doorvelocity;

	private bool opendoor;
}
