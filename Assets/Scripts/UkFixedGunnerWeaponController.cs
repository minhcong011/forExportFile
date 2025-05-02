// dnSpy decompiler from Assembly-CSharp.dll class: UkFixedGunnerWeaponController
using System;
using System.Collections.Generic;
using UnityEngine;

public class UkFixedGunnerWeaponController : CommonEnemyBehaviour
{
	private void Start()
	{
		this.refGameSceneCont = Singleton<GameController>.Instance.gameSceneController;
		base.Invoke("DoAttack", 3f);
	}

	public void DoAttack()
	{
		int num = UnityEngine.Random.Range(4, 7);
		base.InvokeRepeating("shootViaMachine", this.delayTime, this.attackTime);
		base.InvokeRepeating("shootViaRocket", this.delayTime + 3f, (float)UnityEngine.Random.Range(7, 9));
	}

	public void machineFire()
	{
		this.CurrentWeapon = UnityEngine.Random.Range(0, this.WeaponLists.Count);
		this.LaunchWeapon(this.CurrentWeapon);
	}

	public void tankRocket()
	{
		this.CurrentWeapon = 1;
		this.LaunchWeapon(this.CurrentWeapon);
	}

	private void shootViaMachine()
	{
		if (this.gameOver)
		{
			base.CancelInvoke("shootViaMachine");
			base.CancelInvoke("shootViaRocket");
		}
		this.machineFire();
	}

	private void shootViaRocket()
	{
		this.tankRocket();
	}

	public void LaunchWeapon(int index)
	{
		this.CurrentWeapon = index;
		if (this.CurrentWeapon < this.WeaponLists.Count && this.WeaponLists[index] != null)
		{
			this.WeaponLists[index].gameObject.GetComponent<WeaponLauncher>().Shoot(this.damage);
		}
	}

	private new void Update()
	{
		if (UnityEngine.Input.GetKey(KeyCode.P))
		{
			this.DoAttack();
		}
		if (!this.gameOver && (this.refGameSceneCont.isGameCompleted || this.refGameSceneCont.isGameOver))
		{
			this.gameOver = true;
			base.CancelInvoke("findTarget");
			base.CancelInvoke("shootViaMachine");
			base.CancelInvoke("shootViaRocket");
		}
		if (!this.refGameSceneCont.canAlert || !this.refGameSceneCont.isGameStarted || this.refGameSceneCont.isGameCompleted || this.refGameSceneCont.isGameOver)
		{
			return;
		}
		if (this.refGameSceneCont.canAlert)
		{
		}
		Quaternion b = Quaternion.LookRotation(this.GetCurrentPlayerTransform().position - base.transform.position);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, 0.08f);
		base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
		this.WeaponLists[this.CurrentWeapon].transform.rotation = Quaternion.Slerp(this.WeaponLists[this.CurrentWeapon].transform.rotation, b, 0.08f);
		this.WeaponLists[this.CurrentWeapon].transform.localEulerAngles = new Vector3(0f, 0f, this.WeaponLists[this.CurrentWeapon].transform.localEulerAngles.z);
	}

	public Transform GetCurrentPlayerTransform()
	{
		return Singleton<GameController>.Instance.refAllController.getCurrentController().transform;
	}

	public void DiedAtBase()
	{
		Singleton<GameController>.Instance.gameSceneController.IncrementEnemiesCountWithTypeNew(5, 0);
		if (this.particles.Count > 0)
		{
			this.particles[0].SetActive(true);
		}
	}

	public void gotHitAtBase()
	{
		if (this.health >= 0f)
		{
		}
	}

	public List<WeaponLauncher> WeaponLists;

	private int CurrentWeapon;

	private GameSceneController refGameSceneCont;

	private bool gameOver;

	public List<GameObject> particles;
}
