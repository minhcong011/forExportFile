// dnSpy decompiler from Assembly-CSharp.dll class: MoveTrigger
using System;
using UnityEngine;

public class MoveTrigger : MonoBehaviour
{
	private void Start()
	{
		if (!this.isStartingTrigger)
		{
			base.gameObject.SetActive(false);
		}
		if (this.npcToMove)
		{
			this.npcToMove.leadPlayer = true;
			this.npcToMove.followOnUse = false;
		}
	}

	private void OnTriggerStay(Collider col)
	{
		if (this.npcToMove && this.npcToMove.enabled && col.gameObject.tag == "Player" && !this.moved && !this.npcToMove.followed)
		{
			if (!this.runToGoal)
			{
				this.npcToMove.GoToPosition(this.movePosition.position, false);
			}
			else
			{
				this.npcToMove.GoToPosition(this.movePosition.position, true);
			}
			if (this.followSnds.Length > 0)
			{
				this.npcToMove.vocalFx.volume = this.followVol;
				this.npcToMove.vocalFx.pitch = UnityEngine.Random.Range(0.94f, 1f);
				this.npcToMove.vocalFx.spatialBlend = 1f;
				this.npcToMove.vocalFx.clip = this.followSnds[UnityEngine.Random.Range(0, this.followSnds.Length)];
				this.npcToMove.vocalFx.PlayOneShot(this.npcToMove.vocalFx.clip);
			}
			if (this.nextMoveTrigger)
			{
				this.nextMoveTrigger.gameObject.SetActive(true);
				this.nextMoveTrigger.moved = false;
				this.nextMoveTrigger.rotated = false;
			}
			this.npcToMove.followed = true;
			this.moved = true;
		}
	}

	private void Update()
	{
		if (this.moved && !this.rotated && Vector3.Distance(this.movePosition.position, this.npcToMove.myTransform.position) < this.npcToMove.pickNextDestDist)
		{
			this.npcToMove.cancelRotate = false;
			this.npcToMove.StartCoroutine(this.npcToMove.RotateTowards(this.npcToMove.playerTransform.position, 10f, 2f, false));
			this.npcToMove.followed = false;
			base.gameObject.SetActive(false);
			this.rotated = true;
		}
	}

	[Tooltip("The NPC to move when the Player enters this trigger.")]
	public AI npcToMove;

	[Tooltip("The position to move the NPC to.")]
	public Transform movePosition;

	[Tooltip("True if NPC should run to the move position, false if they should walk.")]
	public bool runToGoal;

	private bool moved;

	private bool rotated;

	[Tooltip("The next MoveTrigger.cs component to activate after this one.")]
	public MoveTrigger nextMoveTrigger;

	[Tooltip("True if this trigger should be active at scene start, instead of waiting to be activated by other MoveTrigger.cs components when NPC reaches goal.")]
	public bool isStartingTrigger;

	[Tooltip("Sound effects to play when NPC starts traveling to move position (following vocals).")]
	public AudioClip[] followSnds;

	[Tooltip("Volume of follow sound effects.")]
	public float followVol = 0.7f;
}
