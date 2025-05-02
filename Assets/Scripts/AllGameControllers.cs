// dnSpy decompiler from Assembly-CSharp.dll class: AllGameControllers
using System;
using UnityEngine;

public class AllGameControllers : MonoBehaviour
{
	private void Awake()
	{
		Singleton<GameController>.Instance.refAllController = this;
	}

	private void Start()
	{
	}

	public WeaponController getCurrentController()
	{
		return this.allControllers[this.currentcontrollerId];
	}

	public int getCurrentControllerId()
	{
		return this.currentcontrollerId;
	}

	public void setCurrentControllerId(int id)
	{
		if (id >= this.allControllersCameras.Length)
		{
			id = 0;
		}
		this.allControllersCameras[this.currentcontrollerId].SetActive(false);
		this.allControllers[this.currentcontrollerId].gameObject.SetActive(false);
		this.currentcontrollerId = id;
		this.allControllersCameras[this.currentcontrollerId].SetActive(true);
		this.allControllers[this.currentcontrollerId].gameObject.SetActive(true);
		this.enableControllerWRTId();
	}

	private void enableControllerWRTId()
	{
		int num = this.currentcontrollerId;
		if (num == 0)
		{
			this.mainThirdPerson.SetActive(true);
		}
	}

	public void fire(int id)
	{
		this.allControllers[this.currentcontrollerId].LaunchWeapon(id);
	}

	public void setControllerPosition(Transform t, int id)
	{
		this.allControllers[id].transform.position = t.position;
		this.allControllers[id].transform.eulerAngles = t.eulerAngles;
		this.allControllers[id].transform.GetChild(0).localEulerAngles = t.eulerAngles;
	}

	public void setInitialMissiles(int[] val)
	{
		for (int i = 0; i < this.allControllers.Length; i++)
		{
			this.allControllers[i].initialMissileCount = val[i];
		}
	}

	public void IncrementController()
	{
		this.allControllersCameras[this.currentcontrollerId].SetActive(false);
		this.allControllers[this.currentcontrollerId].gameObject.SetActive(false);
		this.currentcontrollerId++;
		if (this.currentcontrollerId >= this.allControllersCameras.Length)
		{
			this.currentcontrollerId = 0;
		}
		this.setCurrentControllerId(this.currentcontrollerId);
	}

	public GameObject mainThirdPerson;

	public WeaponController[] allControllers;

	public GameObject[] allControllersCameras;

	public int currentcontrollerId;
}
