// dnSpy decompiler from Assembly-CSharp.dll class: WeaponAmmoManagerCS
using System;
using UnityEngine;

public class WeaponAmmoManagerCS : MonoBehaviour
{
	private void Awake()
	{
		int[] array = new int[]
		{
			0,
			6
		};
	}

	private void Start()
	{
	}

	private void setInitialAmmos()
	{
	}

	public void setAmmos(int gunId, int clipSize, int totalBullets)
	{
		int[] value = new int[]
		{
			gunId,
			clipSize,
			totalBullets
		};
		base.gameObject.SendMessage("setAmmosJS", value, SendMessageOptions.DontRequireReceiver);
	}

	public void AddAmmos(int count)
	{
		base.gameObject.SendMessage("addAmmosJS", count, SendMessageOptions.DontRequireReceiver);
	}

	public void setDefaultGuns(int[] gunsIndex)
	{
		base.gameObject.SendMessage("setDefaultGunsJS", gunsIndex, SendMessageOptions.DontRequireReceiver);
		this.gunCount = gunsIndex.Length;
	}

	public void setUnlockedGuns(WeaponManagerStore wManager)
	{
		int num = 0;
		for (int i = 0; i < wManager.gunModels.Count; i++)
		{
			if (wManager.GetIsGunUnlocked(i))
			{
				num++;
			}
		}
		for (int j = 0; j < wManager.gunModels.Count; j++)
		{
		}
	}

	public int getIndexAccordingToNewController(int ind)
	{
		int result;
		switch (ind)
		{
		case 0:
			result = 2;
			break;
		case 1:
			result = 2;
			break;
		case 2:
			result = 3;
			break;
		case 3:
			result = 4;
			break;
		default:
			result = 1;
			break;
		}
		return result;
	}

	public void setGunSpecs(int gunId, GunModel model)
	{
		int[] array = new int[6];
		for (int i = 0; i < array.Length - 1; i++)
		{
			array[i] = (int)Convert.ToInt16(model.GetUpgradeItemById(i).GetCurrentItemValues());
		}
		array[5] = gunId;
		base.gameObject.SendMessage("setGunSpecsJS", array, SendMessageOptions.DontRequireReceiver);
	}

	public void enablePoison()
	{
		this.poison.SetActive(true);
		base.CancelInvoke("disablePoison");
		base.Invoke("disablePoison", 5f);
	}

	private void disablePoison()
	{
		this.poison.SetActive(false);
	}

	private int ammos = 5;

	private int clipSize = 20;

	public GameObject poison;

	public int gunCount;
}
