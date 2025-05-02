// dnSpy decompiler from Assembly-CSharp.dll class: HeliAI
using System;
using UnityEngine;

public class HeliAI : MonoBehaviour
{
	private void Start()
	{
		this.canUpdate = false;
		base.transform.position = this.heli_origin[UnityEngine.Random.Range(0, this.heli_origin.Length)].transform.position;
	}

	public void heli_canStart()
	{
		base.InvokeRepeating("heliFire", 5f, 1f);
		base.Invoke("canChase", 10f);
		base.InvokeRepeating("heliFire", 8f, 0.5f);
	}

	public void canChase()
	{
		this.canUpdate = true;
	}

	private void heliFire()
	{
		base.GetComponent<HeliWeaponsController>().helimachineFiring();
	}

	private void Update()
	{
		if (!this.canUpdate)
		{
			return;
		}
		this.heli_Origin();
	}

	public void heli_Origin()
	{
		base.transform.position = Vector3.Lerp(base.transform.position, this.heli_dest[UnityEngine.Random.Range(0, this.heli_dest.Length)].transform.position, 0.1f * Time.deltaTime);
		base.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
	}

	public void Die_Tankstatus()
	{
	}

	public void destroyTank()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private bool canUpdate;

	public Transform[] heli_dest;

	public Transform[] heli_origin;
}
