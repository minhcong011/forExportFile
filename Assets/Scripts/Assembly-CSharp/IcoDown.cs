using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000079 RID: 121
public class IcoDown : MonoBehaviour
{
	// Token: 0x06000215 RID: 533 RVA: 0x0000F264 File Offset: 0x0000D464
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		this.FPSimpulse = GameObject.Find("FPSimpulse");
		VariblesGlobal.PositionUP = this.PositionUP;
		this.icoUp = GameObject.Find("IcoUp");
		this.icoDown = GameObject.Find("IcoDown");
		this.icoUp.SetActive(false);
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
	public void HumanDown()
	{
		if (this.PositionUP)
		{
			this.icoUp.SetActive(true);
			this.icoDown.SetActive(false);
			this.PositionUP = false;
			this.Player.GetComponent<CharacterController>().height = 0.2f;
			this.FPSimpulse.transform.position = new Vector3(this.FPSimpulse.transform.position.x, this.FPSimpulse.transform.position.y + 0.7f, this.FPSimpulse.transform.position.z);
		}
		VariblesGlobal.PositionUP = this.PositionUP;
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000F37C File Offset: 0x0000D57C
	public void HumanUp()
	{
		if (!this.PositionUP)
		{
			this.icoUp.SetActive(false);
			this.icoDown.SetActive(true);
			this.PositionUP = true;
			this.FPSimpulse.transform.position = new Vector3(this.FPSimpulse.transform.position.x, this.FPSimpulse.transform.position.y - 0.7f, this.FPSimpulse.transform.position.z);
			this.Player.GetComponent<CharacterController>().height = 1.8f;
			this.Player.GetComponent<CharacterController>().enabled = false;
			this.Player.transform.position = new Vector3(this.Player.transform.position.x, this.Player.transform.position.y + 1f, this.Player.transform.position.z);
			this.Player.GetComponent<CharacterController>().enabled = true;
		}
		VariblesGlobal.PositionUP = this.PositionUP;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x00003939 File Offset: 0x00001B39
	private void TeleporteT(Vector3 PosTeleport)
	{
		base.GetComponent<NavMeshAgent>().isStopped = true;
		base.GetComponent<NavMeshAgent>().ResetPath();
		base.GetComponent<NavMeshAgent>().Warp(PosTeleport);
		base.GetComponent<NavMeshAgent>().SetDestination(PosTeleport);
		base.transform.position = PosTeleport;
	}

	// Token: 0x0400024B RID: 587
	private GameObject Player;

	// Token: 0x0400024C RID: 588
	private GameObject FPSimpulse;

	// Token: 0x0400024D RID: 589
	private GameObject icoUp;

	// Token: 0x0400024E RID: 590
	private GameObject icoDown;

	// Token: 0x0400024F RID: 591
	private bool PositionUP = true;
}
