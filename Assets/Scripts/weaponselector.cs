// dnSpy decompiler from Assembly-CSharp.dll class: weaponselector
using System;
using System.Collections;
using System.Collections.Generic;
using ControlFreak2;
using UnityEngine;
using UnityEngine.UI;

public class weaponselector : MonoBehaviour
{
	private void Awake()
	{
		for (int i = 0; i < this.Weapons.Length; i++)
		{
			this.Weapons[i].gameObject.SetActive(false);
		}
		this.currentWeapon = 0;
		this.canswitch = true;
		this.HaveWeapons = new bool[this.Weapons.Length];
		this.HaveWeapons[0] = true;
		if (this.WeaponsAmmo.Length == 0)
		{
			this.WeaponsAmmo = new int[this.Weapons.Length];
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!this.firstFired && (CF2Input.GetButton("Fire1") || (double)CF2Input.GetAxis("Fire1") > 0.1 || CF2Input.GetButton("ThrowGrenade")))
		{
			this.Fired();
		}
		bool flag = false;
		if (this.oldAmmo != this.currentammo)
		{
			this.oldAmmo = this.currentammo;
			flag = true;
		}
		if (this.oldTotalAmmo != this.WeaponsAmmo[this.currentWeapon])
		{
			this.oldTotalAmmo = this.currentammo;
			flag = true;
		}
		if (flag)
		{
			if (this.WeaponsAmmo[this.currentWeapon] == -1)
			{
				this.ammotext.text = string.Empty;
			}
			else
			{
				this.ammotext.text = this.currentammo + " / " + this.WeaponsAmmo[this.currentWeapon];
			}
		}
		this.grenadetext.text = this.grenade.ToString();
		if ((CF2Input.GetAxis("CycleWeapons") > 0f && Time.time > this.nextselect && this.canswitch) || (CF2Input.GetButtonDown("CycleWeapons") && Time.time > this.nextselect && this.canswitch && !this.hideweapons))
		{
			this.nextselect = Time.time + this.selectInterval;
			int num = -1;
			for (int i = this.currentWeapon + 1; i < this.Weapons.Length; i++)
			{
				if (this.HaveWeapons[i])
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				for (int j = 0; j < this.currentWeapon - 1; j++)
				{
					if (this.HaveWeapons[j])
					{
						num = j;
						break;
					}
				}
			}
			if (num == -1)
			{
				num = 0;
			}
			this.previousWeapon = this.currentWeapon;
			this.currentWeapon = num;
			if (this.currentWeapon != this.previousWeapon)
			{
				this.playSwithSound();
				base.StartCoroutine(this.selectWeapon(this.currentWeapon));
			}
		}
		else if (CF2Input.GetAxis("CycleWeapons") < 0f && Time.time > this.nextselect && this.canswitch && !this.hideweapons)
		{
			this.nextselect = Time.time + this.selectInterval;
			int num2 = 0;
			if (this.currentWeapon > 0)
			{
				for (int k = this.currentWeapon - 1; k > -1; k--)
				{
					if (this.HaveWeapons[k])
					{
						num2 = k;
						break;
					}
				}
			}
			else
			{
				for (int l = this.Weapons.Length - 1; l > -1; l--)
				{
					if (this.HaveWeapons[l])
					{
						num2 = l;
						break;
					}
				}
			}
			this.previousWeapon = this.currentWeapon;
			this.currentWeapon = num2;
			if (this.currentWeapon != this.previousWeapon)
			{
				this.playSwithSound();
				base.StartCoroutine(this.selectWeapon(this.currentWeapon));
			}
		}
		if (this.hideweapons != this.oldhideweapons)
		{
			if (this.hideweapons)
			{
				base.StartCoroutine(this.hidecurrentWeapon(this.currentWeapon));
			}
			else
			{
				base.StartCoroutine(this.unhidecurrentWeapon(this.currentWeapon));
			}
		}
	}

	public void playSwithSound()
	{
		this.myaudioSource.PlayOneShot(this.switchsound, 1f);
	}

	public void PickAmmo(int weaponNumber, int amountAmmo)
	{
		this.WeaponsAmmo[weaponNumber] += amountAmmo;
		if (weaponNumber == this.currentWeapon)
		{
			this.Weapons[weaponNumber].gameObject.BroadcastMessage("pickAmmo", this.WeaponsAmmo[weaponNumber]);
		}
	}

	public void SetAmmo(int weaponNumber, int amountAmmo)
	{
		this.WeaponsAmmo[weaponNumber] += amountAmmo;
		if (weaponNumber == this.currentWeapon)
		{
			this.Weapons[weaponNumber].gameObject.BroadcastMessage("pickAmmo", this.WeaponsAmmo[weaponNumber]);
		}
	}

	public void InitCurrentWeaponAmmo(int amountAmmo)
	{
		if (this.WeaponsAmmo.Length == 0)
		{
			this.WeaponsAmmo = new int[this.Weapons.Length];
		}
		this.WeaponsAmmo[this.currentWeapon] += amountAmmo;
	}

	public void UpdateCurrentWeaponAmmo(int amountAmmo)
	{
		this.WeaponsAmmo[this.currentWeapon] = amountAmmo;
	}

	private IEnumerator hidecurrentWeapon(int index)
	{
		this.Weapons[index].gameObject.BroadcastMessage("doRetract", SendMessageOptions.DontRequireReceiver);
		yield return new WaitForSeconds(0.15f);
		this.Weapons[index].gameObject.SetActive(false);
		this.oldhideweapons = this.hideweapons;
		yield break;
	}

	private IEnumerator unhidecurrentWeapon(int index)
	{
		yield return new WaitForSeconds(0.15f);
		this.Weapons[index].gameObject.SetActive(true);
		this.Weapons[index].gameObject.BroadcastMessage("doNormal", SendMessageOptions.DontRequireReceiver);
		this.oldhideweapons = this.hideweapons;
		yield break;
	}

	private IEnumerator selectWeapon(int index)
	{
		this.Weapons[this.previousWeapon].gameObject.BroadcastMessage("doRetract", SendMessageOptions.DontRequireReceiver);
		yield return new WaitForSeconds(0.5f);
		this.Weapons[this.previousWeapon].gameObject.SetActive(false);
		this.Weapons[index].gameObject.SetActive(true);
		this.Weapons[index].gameObject.BroadcastMessage("doNormal", SendMessageOptions.DontRequireReceiver);
		this.isZoomed = false;
		if (this.SniperAim != null)
		{
			this.SniperAim.SetActive(false);
		}
		if (this.zoomSlider != null)
		{
			this.zoomSlider.value = 0f;
		}
		yield break;
	}

	public void showAIM(bool show)
	{
		if (show)
		{
			this.AIM.SetActive(true);
		}
		else
		{
			this.AIM.SetActive(false);
		}
	}

	public void ZoomPressed()
	{
		this.isZoomed = !this.isZoomed;
		this.Weapons[this.currentWeapon].gameObject.BroadcastMessage("ZoomPressed", SendMessageOptions.DontRequireReceiver);
		SniperZoomController component = this.Weapons[this.currentWeapon].gameObject.GetComponent<SniperZoomController>();
		if (component != null)
		{
			if (this.isZoomed)
			{
				component.ShowAim(true);
				this.showAIM(false);
				base.Invoke("ShowSniperAim", 0.13f);
			}
			else
			{
				this.SniperAim.SetActive(false);
				component.ShowAim(false);
				this.showAIM(true);
			}
		}
	}

	private void ShowSniperAim()
	{
		if (this.SniperAim != null)
		{
			this.SniperAim.SetActive(true);
		}
	}

	public void OnValueChanged()
	{
		if (this.isZoomed)
		{
			SniperZoomController component = this.Weapons[this.currentWeapon].gameObject.GetComponent<SniperZoomController>();
			if (component != null)
			{
				component.SetZoomVal(this.zoomSlider.value * 20f);
			}
		}
	}

	public void setUnlockedGuns()
	{
		WeaponManagerStore weaponManager = Singleton<GameController>.Instance.weaponManager;
		this.unlockedGunsIds.Clear();
		List<GunModel> gunModels = weaponManager.gunModels;
		for (int i = gunModels.Count - 1; i >= 0; i--)
		{
			if (weaponManager.GetIsGunUnlocked(i))
			{
				int indexAccordingToNewController = this.getIndexAccordingToNewController(i);
				this.unlockedGunsIds.Add(indexAccordingToNewController);
				FPSCommonWeaponSpecs fpscommonWeaponSpecs = this.Weapons[indexAccordingToNewController].GetComponent(typeof(FPSCommonWeaponSpecs)) as FPSCommonWeaponSpecs;
				if (fpscommonWeaponSpecs != null)
				{
					fpscommonWeaponSpecs.clipSize = (int)Convert.ToInt16(gunModels[i].GetUpgradeItemById(0).GetCurrentItemValues());
					fpscommonWeaponSpecs.currentammo = fpscommonWeaponSpecs.clipSize;
					fpscommonWeaponSpecs.fireAnimSpeed = gunModels[i].GetUpgradeItemById(4).GetCurrentItemValues();
					fpscommonWeaponSpecs.damage = gunModels[i].GetUpgradeItemById(1).GetCurrentItemValues();
					fpscommonWeaponSpecs.aimFOV = gunModels[i].GetUpgradeItemById(3).GetCurrentItemValues();
					fpscommonWeaponSpecs.ammo = (int)Convert.ToInt16(gunModels[i].GetUpgradeItemById(5).GetCurrentItemValues());
				}
			}
		}
		if (this.unlockedGunsIds.Count > 0)
		{
			this.setHaveWeapons();
			this.currentWeapon = this.unlockedGunsIds[0];
			this.previousWeapon = this.currentWeapon;
			base.StartCoroutine(this.selectWeapon(this.currentWeapon));
		}
	}

	public void setHaveWeapons()
	{
		for (int i = 0; i < this.Weapons.Length; i++)
		{
			this.HaveWeapons[i] = false;
		}
		for (int j = 0; j < this.Weapons.Length; j++)
		{
			if (this.unlockedGunsIds.Contains(j))
			{
				this.HaveWeapons[j] = true;
			}
		}
	}

	public void AddWeapon(int id)
	{
		int indexAccordingToNewController = this.getIndexAccordingToNewController(id);
		if (!this.unlockedGunsIds.Contains(indexAccordingToNewController))
		{
			this.unlockedGunsIds.Add(indexAccordingToNewController);
			this.HaveWeapons[indexAccordingToNewController] = true;
		}
		this.currentWeapon = id;
		base.StartCoroutine(this.selectWeapon(this.currentWeapon));
	}

	public void AddWeaponWithAmmos(int id, int ammos, int clipSize)
	{
		int indexAccordingToNewController = this.getIndexAccordingToNewController(id);
		FPSCommonWeaponSpecs fpscommonWeaponSpecs = this.Weapons[indexAccordingToNewController].GetComponent(typeof(FPSCommonWeaponSpecs)) as FPSCommonWeaponSpecs;
		if (!this.unlockedGunsIds.Contains(indexAccordingToNewController))
		{
			this.unlockedGunsIds.Add(indexAccordingToNewController);
			this.HaveWeapons[indexAccordingToNewController] = true;
			if (fpscommonWeaponSpecs != null)
			{
				fpscommonWeaponSpecs.clipSize = clipSize;
				fpscommonWeaponSpecs.currentammo = fpscommonWeaponSpecs.clipSize;
				fpscommonWeaponSpecs.ammo = ammos;
				this.SetAmmo(id, ammos);
			}
			this.previousWeapon = this.currentWeapon;
			this.currentWeapon = indexAccordingToNewController;
			base.StartCoroutine(this.selectWeapon(this.currentWeapon));
		}
		else
		{
			this.HaveWeapons[indexAccordingToNewController] = true;
			if (fpscommonWeaponSpecs != null)
			{
				if (fpscommonWeaponSpecs.clipSize < clipSize)
				{
					fpscommonWeaponSpecs.clipSize = clipSize;
					fpscommonWeaponSpecs.currentammo = fpscommonWeaponSpecs.clipSize;
				}
				fpscommonWeaponSpecs.ammo += ammos;
				this.PickAmmo(id, ammos);
			}
			this.previousWeapon = this.currentWeapon;
			this.currentWeapon = indexAccordingToNewController;
			base.StartCoroutine(this.selectWeapon(this.currentWeapon));
		}
	}

	public void AddWeaponAmmos(int mag)
	{
		this.PickAmmo(this.currentWeapon, mag);
	}

	public void SetDefaultGuns(List<GunItem> gunItems)
	{
		this.unlockedGunsIds.Clear();
		for (int i = 0; i < gunItems.Count; i++)
		{
			int indexAccordingToNewController = this.getIndexAccordingToNewController(gunItems[i].gunId);
			this.unlockedGunsIds.Add(indexAccordingToNewController);
			FPSCommonWeaponSpecs fpscommonWeaponSpecs = this.Weapons[indexAccordingToNewController].GetComponent(typeof(FPSCommonWeaponSpecs)) as FPSCommonWeaponSpecs;
			if (fpscommonWeaponSpecs != null)
			{
				fpscommonWeaponSpecs.clipSize = gunItems[i].clipSize;
				fpscommonWeaponSpecs.currentammo = fpscommonWeaponSpecs.clipSize;
				fpscommonWeaponSpecs.ammo = gunItems[i].totalAmmos;
			}
		}
		if (this.unlockedGunsIds.Count > 0)
		{
			this.setHaveWeapons();
			this.currentWeapon = this.unlockedGunsIds[0];
			this.previousWeapon = this.currentWeapon;
			base.StartCoroutine(this.selectWeapon(this.currentWeapon));
		}
	}

	public int getIndexAccordingToNewController(int ind)
	{
		return ind;
	}

	public void Fired()
	{
		if (!this.firstFired)
		{
			if (Singleton<GameController>.Instance.gameSceneController != null && !Singleton<GameController>.Instance.gameSceneController.canAlert)
			{
				Singleton<GameController>.Instance.gameSceneController.AlertEnemies();
			}
			this.firstFired = true;
		}
	}

	public int currentWeapon;

	public Transform[] Weapons;

	public bool[] HaveWeapons;

	public int[] WeaponsAmmo;

	public int grenade;

	public float lastGrenade;

	public float selectInterval = 2f;

	private float nextselect = 2f;

	private int previousWeapon;

	public AudioClip switchsound;

	public AudioSource myaudioSource;

	public bool canswitch;

	public bool hideweapons;

	private bool oldhideweapons;

	public Text ammotext;

	public Text grenadetext;

	public int currentammo = 10;

	public GameObject AIM;

	public GameObject SniperAim;

	private int oldAmmo = -1;

	private int oldTotalAmmo = -1;

	public Slider zoomSlider;

	public bool isZoomed;

	public List<int> unlockedGunsIds;

	private bool firstFired;
}
