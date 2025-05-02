// dnSpy decompiler from Assembly-CSharp.dll class: AirStrikeController
using System;
using System.Collections;
using UnityEngine;

public class AirStrikeController : MonoBehaviour
{
	private void Start()
	{
		this.airstrikeTime = Time.time - 2f;
	}

	public void ShowAirstrike()
	{
		if (Time.time - this.airstrikeTime < 2f)
		{
			UnityEngine.Debug.Log("AirStrike ::  returned already effected");
			return;
		}
		int num = Singleton<GameController>.Instance.storeManager.GetAirStrikeCount();
		num--;
		if (num <= 0)
		{
		}
		Singleton<GameController>.Instance.storeManager.GiveAirStrike(-1);
		this.airstrikeTime = Time.time;
		WeaponAmmoManagerCS weaponAmmoManagerCS = UnityEngine.Object.FindObjectOfType<WeaponAmmoManagerCS>();
		if (weaponAmmoManagerCS != null)
		{
			weaponAmmoManagerCS.enablePoison();
		}
		base.Invoke("EffectEnemies", 0.5f);
	}

	public void EffectEnemies()
	{
		base.StartCoroutine(this.killEnemies());
	}

	private IEnumerator killEnemies()
	{
		yield return new WaitForSeconds(1f);
		GameObject[] enemyLists = GameObject.FindGameObjectsWithTag("Enemy");
		UnityEngine.Debug.Log("enemyLists .count " + enemyLists.Length);
		foreach (GameObject enmey in enemyLists)
		{
			UnityEngine.Debug.Log("enemyLists .name " + enemyLists.Length);
			yield return new WaitForSeconds(0.05f);
		}
		yield break;
	}

	public GameObject blast;

	public Transform[] targets;

	private float airstrikeTime;
}
