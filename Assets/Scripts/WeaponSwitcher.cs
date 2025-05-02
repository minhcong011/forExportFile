// dnSpy decompiler from Assembly-CSharp.dll class: WeaponSwitcher
using System;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
	public void setPlayer_asCharacter()
	{
		this.currentPlayer = WeaponSwitcher.playerType.Character;
	}

	public void setPlayer_asTank()
	{
		this.currentPlayer = WeaponSwitcher.playerType.Tank;
	}

	public void Skip_btnListener()
	{
		this.skip_btn.SetActive(false);
	}

	public void selectSotre()
	{
		Time.timeScale = 0f;
		this.storePanel.SetActive(true);
		if (this.joystic.activeInHierarchy)
		{
			this.joystic.SetActive(false);
			this.is_Joystic1 = true;
		}
		else if (this.joystic2.activeInHierarchy)
		{
			this.joystic2.SetActive(false);
			this.is_Joystic1 = false;
		}
	}

	private void check_Fire_pos()
	{
		if (this.currentPlayer == WeaponSwitcher.playerType.Character)
		{
			this.Player.GetComponent<WeaponController>().CurrentWeapon = 1;
			this.Player.GetComponent<WeaponController>().LaunchWeapon();
		}
		else if (this.currentPlayer == WeaponSwitcher.playerType.Tank)
		{
			this.Tank.GetComponent<WeaponController>().CurrentWeapon = 1;
			this.Tank.GetComponent<WeaponController>().LaunchWeapon();
			this.Tank.GetComponent<WeaponController>().CurrentWeapon = 2;
			this.Tank.GetComponent<WeaponController>().LaunchWeapon();
		}
	}

	private void check_Missile_pos()
	{
		if (this.currentPlayer == WeaponSwitcher.playerType.Character)
		{
			this.Player.GetComponent<WeaponController>().CurrentWeapon = 0;
			this.Player.GetComponent<WeaponController>().LaunchWeapon();
		}
		else if (this.currentPlayer == WeaponSwitcher.playerType.Tank)
		{
			this.Tank.GetComponent<WeaponController>().CurrentWeapon = 0;
			this.Tank.GetComponent<WeaponController>().LaunchWeapon();
		}
	}

	private void Awake()
	{
		this.can_rocketFire = true;
		this.is_Joystic1 = true;
		this.rocketsLimits = new int[]
		{
			0,
			12,
			12,
			16,
			16,
			16,
			16,
			16,
			18,
			18,
			18,
			18,
			18,
			20,
			20,
			20,
			20,
			20,
			20,
			20,
			20,
			20,
			22,
			23,
			24,
			25,
			25,
			25,
			25,
			25,
			35
		};
		this.airStrikeLimit = new int[]
		{
			0,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			1,
			1,
			1,
			1,
			1
		};
		int[] array = new int[31];
		array[5] = 1;
		this.healthLimit = array;
	}

	private void Start()
	{
		this.total_medi_pack = PlayerPrefs.GetInt("medic_pack_count");
		this.total_missile_pack = PlayerPrefs.GetInt("saved_missiles");
		this.total_air_strike_pack = PlayerPrefs.GetInt("air_strike_count");
		MonoBehaviour.print("TOTAL AIR IS" + this.total_air_strike_pack);
		this.rocket_limit = 0;
		MonoBehaviour.print("Total AIRR ISSS" + this.air_strike_limit);
		this.airstrike_Left.text = this.air_strike_limit.ToString();
		this.health_left.text = this.health_refil_limit.ToString();
		this.label.text = "Machine";
		this.ammosLeft.text = this.rocket_limit.ToString();
		this.health_.SetActive(false);
		this.machine_gun.SetActive(false);
		this.rockets.SetActive(true);
		this.air_strike_pack.SetActive(false);
	}

	public void disableSorePopUp()
	{
		this.ammo_finish.SetActive(false);
	}

	private void Update()
	{
		if (this.canFire && Time.time - this.fireTimer > 0.2f)
		{
			this.Fire();
			this.fireTimer = Time.time;
		}
	}

	public void airstrike_fire()
	{
		if (this.air_strike_limit <= 0)
		{
			this.selectSotre();
			return;
		}
		this.Tank.GetComponent<WeaponController>().AirStrikeWeapon();
		this.air_strike_limit--;
		int num = PlayerPrefs.GetInt("air_strike_count");
		num--;
		if (num <= 0)
		{
			num = 0;
		}
		MonoBehaviour.print("R is " + num);
		PlayerPrefs.SetInt("air_strike_count", num);
		this.airstrike_Left.text = this.air_strike_limit.ToString();
	}

	public void health_consume()
	{
		if (this.health_refil_limit <= 0)
		{
			this.selectSotre();
			return;
		}
		this.health_refil_limit--;
		int num = PlayerPrefs.GetInt("medic_pack_count");
		num--;
		if (num <= 0)
		{
			num = 0;
		}
		MonoBehaviour.print("H is " + num);
		PlayerPrefs.SetInt("medic_pack_count", num);
		this.health_left.text = this.health_refil_limit.ToString();
	}

	public void disableStorePOP()
	{
		this.ammo_finish.SetActive(false);
	}

	public void OnPressedState()
	{
		this.canFire = true;
		this.fireTimer = Time.time;
	}

	public void enable_rocket()
	{
		this.can_rocketFire = true;
	}

	public void OnReleaseState()
	{
		this.canFire = false;
	}

	public void Fire()
	{
		this.check_Fire_pos();
	}

	public void rightClick()
	{
		if (!this.machine_gun.activeInHierarchy)
		{
			if (this.rockets.activeInHierarchy)
			{
				if (!this.can_rocketFire)
				{
					this.can_rocketFire = false;
					base.Invoke("enable_rocket", 1f);
					return;
				}
				if (this.rocket_limit <= 0)
				{
					this.selectSotre();
					return;
				}
				this.check_Missile_pos();
				this.rocket_limit--;
				int num = PlayerPrefs.GetInt("saved_missiles");
				num--;
				if (num <= 0)
				{
					num = 0;
				}
				MonoBehaviour.print("Misisle is " + num);
				PlayerPrefs.SetInt("saved_missiles", num);
				this.ammosLeft.text = this.rocket_limit.ToString();
			}
			else if (this.air_strike_pack.activeInHierarchy)
			{
				this.label.text = "Health";
				this.air_strike_pack.SetActive(false);
				this.health_.SetActive(true);
				this.right.SetActive(false);
				this.ammosLeft.text = this.health_refil_limit.ToString();
			}
		}
	}

	public void leftClick()
	{
		if (this.machine_gun.activeInHierarchy)
		{
			this.label.text = "Rockets";
			this.machine_gun.SetActive(false);
			this.rockets.SetActive(true);
			this.left.SetActive(false);
			this.ammosLeft.text = this.rocket_limit.ToString();
		}
		else if (this.air_strike_pack.activeInHierarchy)
		{
			this.label.text = "Machine";
			this.air_strike_pack.SetActive(false);
			this.machine_gun.SetActive(true);
			this.ammosLeft.text = string.Empty;
			this.right.SetActive(true);
		}
		else if (this.health_.activeInHierarchy)
		{
			this.label.text = "Air strike";
			this.health_.SetActive(false);
			this.air_strike_pack.SetActive(true);
			this.right.SetActive(true);
			this.ammosLeft.text = this.air_strike_limit.ToString();
		}
	}

	public GameObject skip_btn;

	public WeaponSwitcher.playerType currentPlayer;

	public bool can_rocketFire;

	public bool is_Joystic1;

	public GameObject machine_gun;

	public GameObject rockets;

	public GameObject air_strike_pack;

	public GameObject left;

	public GameObject right;

	public GameObject Tank;

	public GameObject Player;

	public GameObject health_;

	public GameObject ammo_finish;

	public UILabel label;

	public UILabel ammosLeft;

	public UILabel airstrike_Left;

	public UILabel health_left;

	private bool canFire;

	private float fireTimer;

	public GameObject joystic;

	public GameObject storePanel;

	public GameObject joystic2;

	public int rocket_limit;

	public int air_strike_limit;

	public int health_refil_limit;

	private int total_medi_pack;

	private int total_missile_pack;

	private int total_air_strike_pack;

	public store storre;

	public int[] rocketsLimits;

	public int[] airStrikeLimit;

	public int[] healthLimit;

	public enum playerType
	{
		Character,
		Tank
	}
}
