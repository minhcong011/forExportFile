// dnSpy decompiler from Assembly-CSharp.dll class: store
using System;
using UnityEngine;

public class store : MonoBehaviour
{
	private void Awake()
	{
		if (!this.is_game_play)
		{
			this.is_store_selected = true;
		}
		this.set_player_prefs();
	}

	public void showStore()
	{
		this.is_store_selected = true;
		this.get_count_of_items();
	}

	private void OnEnable()
	{
		OpenIABEventManager.UIEventTrigger += this.PurchasedCB;
		this.get_count_of_items();
	}

	private void OnDisable()
	{
		OpenIABEventManager.UIEventTrigger -= this.PurchasedCB;
		this.get_count_of_items();
	}

	public void PurchasedCB(int id)
	{
		switch (id)
		{
		case 0:
		{
			MonoBehaviour.print("Item CB");
			int num = PlayerPrefs.GetInt("total_coins", 0);
			num += 1000;
			PlayerPrefs.SetInt("total_coins", num);
			break;
		}
		case 1:
		{
			MonoBehaviour.print("Item CB");
			int num = PlayerPrefs.GetInt("total_coins", 0);
			num += 2000;
			PlayerPrefs.SetInt("total_coins", num);
			break;
		}
		case 2:
		{
			MonoBehaviour.print("Item CB");
			int num = PlayerPrefs.GetInt("total_coins", 0);
			num += 4000;
			PlayerPrefs.SetInt("total_coins", num);
			break;
		}
		case 3:
		{
			MonoBehaviour.print("Item CB");
			int num = PlayerPrefs.GetInt("total_coins", 0);
			num += 8000;
			PlayerPrefs.SetInt("total_coins", num);
			break;
		}
		}
		this.get_count_of_items();
	}

	private void Start()
	{
		this.armour_type_euiped = PlayerPrefs.GetInt("armour_selected");
		this.get_count_of_items();
		this.set_weapon_upgrade_prices();
		this.is_weapon_screen = false;
		this.show_not_enough_propmt = false;
		if (GameObject.FindGameObjectWithTag("ads_obj") != null)
		{
			this.ads_obj = GameObject.FindGameObjectWithTag("ads_obj");
		}
	}

	private void Update()
	{
	}

	private void OnGUI()
	{
		if (this.is_store_selected)
		{
			float x = (float)Screen.width / this.native_width;
			float y = (float)Screen.height / this.native_height;
			GUI.matrix = Matrix4x4.TRS(new Vector3(0f, 0f, 0f), Quaternion.identity, new Vector3(x, y, 1f));
			if (!this.called)
			{
				Time.timeScale = 0f;
				this.called = true;
			}
			GUI.DrawTexture(new Rect(0f, 0f, 1280f, 800f), this.bg);
			GUI.DrawTexture(this.bg2R, this.bg2);
			GUI.DrawTexture(this.store_barR, this.store_bar);
			GUI.DrawTexture(this.coinsBgR, this.coinsBg);
			GUI.Label(this.total_coins_labelR, this.total_coins.ToString(), this.skin.customStyles[8]);
			GUI.Label(new Rect(530f, 10f, 200f, 50f), "STORE", this.skin.customStyles[19]);
			if (!this.is_medi_pack_screen && !this.is_missile_pack_screen && !this.is_armour_pack_screen && !this.is_air_strike_pack_screen && !this.show_not_enough_propmt && !this.show_money_deals && !this.isWatched)
			{
				if (GUI.Button(this.back_btnR, string.Empty, this.back_btn))
				{
					if (this.is_store_selected && this.joystic != null)
					{
						this.storepanel.SetActive(false);
					}
					this.is_store_selected = false;
					if (!this.is_game_play)
					{
						Time.timeScale = 1f;
						UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
					}
					else
					{
						UIController uicontroller = UnityEngine.Object.FindObjectOfType<UIController>();
						if (uicontroller != null)
						{
							Time.timeScale = 1f;
							uicontroller.hideStore();
						}
					}
				}
				if (this.is_weapon_screen)
				{
					if (GUI.Button(this.mini_gun_btnR, string.Empty, this.mini_gun_btn))
					{
						this.buy_weapon_upgrade(0);
					}
					Rect position = this.mini_gun_btnR;
					position.y -= 300f;
					GUI.Label(position, this.mini_gun_upgrade_price.ToString(), this.skin.customStyles[14]);
					GUI.Label(this.mini_gun_btnR, "12", this.skin.customStyles[15]);
					if (GUI.Button(this.rocket_launcher_btnR, string.Empty, this.rocket_launcher_btn))
					{
						this.buy_weapon_upgrade(1);
					}
					position = this.rocket_launcher_btnR;
					position.y -= 300f;
					GUI.Label(position, this.rocket_launcher_upgrade_price.ToString(), this.skin.customStyles[14]);
					GUI.Label(this.rocket_launcher_btnR, "15", this.skin.customStyles[15]);
				}
				else
				{
					if (GUI.Button(this.medi_pack_btnR, string.Empty, this.medi_pack_btn))
					{
						this.is_medi_pack_screen = true;
					}
					if (GUI.Button(this.missile_btnR, string.Empty, this.missile_btn))
					{
						this.is_missile_pack_screen = true;
					}
					if (GUI.Button(this.armour_btnR, string.Empty, this.armour_btn))
					{
						this.is_armour_pack_screen = true;
					}
					if (GUI.Button(this.air_strike_btnR, string.Empty, this.air_strike_btn))
					{
						this.is_air_strike_pack_screen = true;
					}
				}
			}
			if (this.isWatched)
			{
				GUI.DrawTexture(this.congrats_barR, this.congrats_bar);
				GUI.Label(new Rect(700f, 405.5f, 0f, 0f), "Awarded with 50 coins.", this.skin.customStyles[17]);
				if (GUI.Button(this.congrats_bar_popUp_closeR, string.Empty, this.congrats_bar_popUp_close))
				{
					this.isWatched = false;
				}
			}
			if (this.show_not_enough_propmt)
			{
				GUI.DrawTexture(this.no_money_bgR, this.no_money_bg);
				if (this.isVideoAdAvailable)
				{
					if (GUI.Button(this.yes_btnR, string.Empty, this.yes_btn))
					{
						this.show_not_enough_propmt = false;
						this.watchVideo_from_Store();
					}
					if (GUI.Button(this.no_btnR, string.Empty, this.no_btn))
					{
						this.show_not_enough_propmt = false;
					}
				}
				else if (!this.isVideoAdAvailable)
				{
					if (GUI.Button(this.no_btnR, string.Empty, this.no_btn))
					{
						this.show_not_enough_propmt = false;
					}
					GUI.DrawTexture(this.yes_btnR, this.hover_watchBtn);
				}
			}
			if (this.show_money_deals)
			{
				GUI.DrawTexture(this.money_deal_bgR, this.money_deals_bg);
				for (int i = 0; i < this.deals_btnR.Length; i++)
				{
					if (GUI.Button(this.deals_btnR[i], string.Empty, this.deals_btn[i]))
					{
						if (GameObject.FindGameObjectWithTag("ads_obj") != null)
						{
						}
						OpenIABPurchaseHandler openIABPurchaseHandler = UnityEngine.Object.FindObjectOfType<OpenIABPurchaseHandler>();
						if (openIABPurchaseHandler != null)
						{
							openIABPurchaseHandler.PurchasePressed(i);
						}
						int @int = PlayerPrefs.GetInt("total_coins", 0);
						this.get_count_of_items();
					}
					Rect position2 = this.deals_btnR[i];
					position2.y -= 50f;
					GUI.Label(position2, "+" + store.deals_prices[i].ToString(), this.skin.customStyles[12]);
					position2.y += 200f;
					GUI.Label(position2, "+" + ((float)this.deals_pricesCost[i] + 0.99f).ToString(), this.skin.customStyles[12]);
				}
				if (GUI.Button(this.back_btnR, string.Empty, this.back_btn))
				{
					this.show_money_deals = false;
				}
			}
			if (this.is_medi_pack_screen)
			{
				if (this.medi_pack_screen_btnR.Length > 0)
				{
					for (int j = 0; j < this.medi_pack_screen_btnR.Length; j++)
					{
						if (GUI.Button(this.medi_pack_screen_btnR[j], string.Empty, this.medi_pack_screen_btn[j]))
						{
							this.buy_medic_pack(this.medi_pack_deal[j], this.medi_prices[j]);
						}
						Rect position3 = this.medi_pack_screen_btnR[j];
						position3.y -= 260f;
						position3.x += 30f;
						GUI.Label(position3, this.medi_prices[j].ToString(), this.skin.customStyles[11]);
					}
				}
				if (GUI.Button(this.back_btnR, string.Empty, this.back_btn))
				{
					this.is_medi_pack_screen = false;
				}
				GUI.DrawTexture(this.item_count_texR, this.medic_count_tex);
				GUI.Label(this.medi_pack_labelR, "X " + this.total_medi_pack.ToString(), this.skin.customStyles[10]);
			}
			if (this.is_missile_pack_screen)
			{
				if (this.missile_screen_btnR.Length > 0)
				{
					for (int k = 0; k < this.missile_screen_btnR.Length; k++)
					{
						if (GUI.Button(this.missile_screen_btnR[k], string.Empty, this.missile_screen_btn[k]))
						{
							this.buy_misisle_pack(this.missile_pack_deal[k], this.missile_prices[k]);
						}
						Rect position4 = this.missile_screen_btnR[k];
						position4.y -= 260f;
						position4.x += 30f;
						GUI.Label(position4, this.missile_prices[k].ToString(), this.skin.customStyles[11]);
					}
				}
				if (GUI.Button(this.back_btnR, string.Empty, this.back_btn))
				{
					this.is_missile_pack_screen = false;
				}
				GUI.DrawTexture(this.item_count_texR, this.missile_count_tex);
				GUI.Label(this.missile_labelR, "X " + this.total_missile_pack.ToString(), this.skin.customStyles[10]);
			}
			if (this.is_armour_pack_screen)
			{
				if (this.armoue_screen_btnR.Length > 0)
				{
					for (int l = 0; l < this.armoue_screen_btnR.Length; l++)
					{
						if (this.shield_count_array[l] <= 0)
						{
							if (GUI.Button(this.armoue_screen_btnR[l], string.Empty, this.armour_screen_btn[l]))
							{
								this.buy_armour_pack(l, this.armour_prices[l]);
							}
							Rect position5 = this.armoue_screen_btnR[l];
							position5.y -= 260f;
							position5.x += 30f;
							GUI.Label(position5, this.armour_prices[l].ToString(), this.skin.customStyles[11]);
						}
						else
						{
							if (GUI.Button(this.armoue_screen_btnR[l], string.Empty, this.armour_screen_btn[l]))
							{
								this.armour_type_euiped = l;
								PlayerPrefs.SetInt("armour_selected", l);
							}
							if (l == this.armour_type_euiped)
							{
								this.current_equipe_tex = this.equiped_tex;
							}
							else
							{
								this.current_equipe_tex = this.equipe_tex;
							}
							GUI.DrawTexture(new Rect(this.armoue_screen_btnR[l].x, this.armoue_screen_btnR[l].y + 265f, 250f, 70f), this.current_equipe_tex);
						}
					}
				}
				if (GUI.Button(this.back_btnR, string.Empty, this.back_btn))
				{
					this.is_armour_pack_screen = false;
				}
				if (!this.is_game_play)
				{
				}
			}
			if (this.is_air_strike_pack_screen)
			{
				if (this.air_strike_screen_btnR.Length > 0)
				{
					for (int m = 0; m < this.air_strike_screen_btnR.Length; m++)
					{
						if (GUI.Button(this.air_strike_screen_btnR[m], string.Empty, this.air_strike_screen_btn[m]))
						{
							this.buy_air_strike_pack(this.air_strike_pack_deal[m], this.air_strike_prices[m]);
						}
						Rect position6 = this.air_strike_screen_btnR[m];
						position6.y -= 260f;
						position6.x += 30f;
						GUI.Label(position6, this.air_strike_prices[m].ToString(), this.skin.customStyles[11]);
					}
				}
				if (GUI.Button(this.back_btnR, string.Empty, this.back_btn))
				{
					this.is_air_strike_pack_screen = false;
				}
				GUI.DrawTexture(this.item_count_texR, this.air_strike_count_tex);
				GUI.Label(this.air_strike_labelR, "X " + this.total_air_strike_pack.ToString(), this.skin.customStyles[10]);
			}
		}
		else
		{
			this.called = false;
		}
	}

	private void set_player_prefs()
	{
		if (!PlayerPrefs.HasKey("total_coins"))
		{
			PlayerPrefs.SetInt("total_coins", 0);
		}
		if (!PlayerPrefs.HasKey("air_strike_count"))
		{
			PlayerPrefs.SetInt("air_strike_count", 1);
		}
		if (!PlayerPrefs.HasKey("medic_pack_count"))
		{
			PlayerPrefs.SetInt("medic_pack_count", 1);
		}
		this.shield_count_array = new int[this.max_type_of_shields];
		if (!PlayerPrefs.HasKey("shield_0"))
		{
			PlayerPrefs.SetInt("shield_0", 1);
		}
		for (int i = 0; i < this.max_type_of_shields; i++)
		{
			if (!PlayerPrefs.HasKey("shield_" + i.ToString()))
			{
				PlayerPrefs.SetInt("shield_" + i.ToString(), 0);
			}
			this.shield_count_array[i] = PlayerPrefs.GetInt("shield_" + i.ToString());
		}
		if (!PlayerPrefs.HasKey("armour_selected"))
		{
			PlayerPrefs.SetInt("armour_selected", 0);
		}
		if (!PlayerPrefs.HasKey("missilesCount"))
		{
			PlayerPrefs.SetInt("missilesCount", 1);
		}
		if (!PlayerPrefs.HasKey("mini_gun_unlocked"))
		{
			PlayerPrefs.SetInt("mini_gun_unlocked", 0);
		}
		if (!PlayerPrefs.HasKey("rocket_launcher_unlocked"))
		{
			PlayerPrefs.SetInt("rocket_launcher_unlocked", 0);
		}
		if (!PlayerPrefs.HasKey("armour_equipped"))
		{
			PlayerPrefs.SetInt("armour_equipped", 0);
		}
	}

	public void set_weapon_upgrade_prices()
	{
		int @int = PlayerPrefs.GetInt("mini_gun_unlocked");
		this.mini_gun_upgrade_price = 1000 + @int * 1500;
		int int2 = PlayerPrefs.GetInt("rocket_launcher_unlocked");
		this.rocket_launcher_upgrade_price = 1200 + int2 * 1300;
	}

	public void buy_weapon_upgrade(int id)
	{
		if (id != 0)
		{
			if (id == 1)
			{
				if (this.total_coins >= this.rocket_launcher_upgrade_price)
				{
					this.total_coins -= this.rocket_launcher_upgrade_price;
					PlayerPrefs.SetInt("total_coins", this.total_coins);
					this.get_count_of_items();
					int num = PlayerPrefs.GetInt("rocket_launcher_unlocked");
					num++;
					PlayerPrefs.SetInt("rocket_launcher_unlocked", num);
					this.set_weapon_upgrade_prices();
				}
				else
				{
					this.show_not_enough_propmt = true;
					MonoBehaviour.print("total_coins>=rocket_launcher_upgrade_price");
				}
			}
		}
		else if (this.total_coins >= this.mini_gun_upgrade_price)
		{
			this.total_coins -= this.mini_gun_upgrade_price;
			PlayerPrefs.SetInt("total_coins", this.total_coins);
			this.get_count_of_items();
			int num2 = PlayerPrefs.GetInt("mini_gun_unlocked");
			num2++;
			PlayerPrefs.SetInt("mini_gun_unlocked", num2);
			this.set_weapon_upgrade_prices();
		}
		else
		{
			this.show_not_enough_propmt = true;
			MonoBehaviour.print("total_coins>=mini_gun_upgrade_price");
		}
	}

	public void get_count_of_items()
	{
		this.total_coins = PlayerPrefs.GetInt("total_coins");
		this.total_medi_pack = PlayerPrefs.GetInt("medic_pack_count");
		this.total_missile_pack = PlayerPrefs.GetInt("missilesCount");
		this.total_air_strike_pack = PlayerPrefs.GetInt("air_strike_count");
		for (int i = 0; i < this.max_type_of_shields; i++)
		{
			this.shield_count_array[i] = PlayerPrefs.GetInt("shield_" + i.ToString());
		}
		if (this.is_game_play)
		{
			base.gameObject.SendMessage("refresh_values", SendMessageOptions.DontRequireReceiver);
		}
	}

	public void set_count_ingame(int medic_kit_value, int missile_value, int air_strike_value)
	{
		this.total_medi_pack = medic_kit_value;
		this.total_air_strike_pack = air_strike_value;
		if (missile_value <= this.total_missile_pack)
		{
			this.total_missile_pack = missile_value;
		}
	}

	public void buy_medic_pack(int medic_count, int current_price)
	{
		if (this.total_coins >= current_price)
		{
			this.total_coins -= current_price;
			PlayerPrefs.SetInt("total_coins", this.total_coins);
			this.total_medi_pack += medic_count;
			PlayerPrefs.SetInt("medic_pack_count", this.total_medi_pack);
			this.get_count_of_items();
		}
		else
		{
			this.is_medi_pack_screen = false;
			this.show_not_enough_propmt = true;
			MonoBehaviour.print("total_coins>=current_price");
		}
	}

	public void buy_misisle_pack(int missile_count, int current_price)
	{
		if (this.total_coins >= current_price)
		{
			this.total_coins -= current_price;
			PlayerPrefs.SetInt("total_coins", this.total_coins);
			this.total_missile_pack += missile_count;
			PlayerPrefs.SetInt("missilesCount", this.total_missile_pack);
			this.get_count_of_items();
		}
		else
		{
			this.is_missile_pack_screen = false;
			this.show_not_enough_propmt = true;
			MonoBehaviour.print("total_coins>=current_price");
		}
	}

	public void buy_air_strike_pack(int air_strike_count, int current_price)
	{
		if (this.total_coins >= current_price)
		{
			this.total_coins -= current_price;
			PlayerPrefs.SetInt("total_coins", this.total_coins);
			this.total_air_strike_pack += air_strike_count;
			PlayerPrefs.SetInt("air_strike_count", this.total_air_strike_pack);
			this.get_count_of_items();
		}
		else
		{
			this.is_air_strike_pack_screen = false;
			this.show_not_enough_propmt = true;
		}
	}

	public void buy_armour_pack(int armour_no, int current_price)
	{
		if (this.total_coins >= current_price)
		{
			this.total_coins -= current_price;
			PlayerPrefs.SetInt("total_coins", this.total_coins);
			this.shield_count_array[armour_no] = this.shield_count_array[armour_no] + 1;
			PlayerPrefs.SetInt("shield_" + armour_no.ToString(), this.shield_count_array[armour_no]);
			if (PlayerPrefs.GetInt("armour_equipped") == 0)
			{
				this.armour_type_euiped = armour_no;
				PlayerPrefs.SetInt("armour_selected", armour_no);
				PlayerPrefs.SetInt("armour_equipped", 1);
			}
			this.get_count_of_items();
		}
		else
		{
			this.is_armour_pack_screen = false;
			this.show_not_enough_propmt = true;
		}
	}

	public void make_game_play_false()
	{
		this.is_game_play = false;
	}

	public void coins_increased()
	{
		this.get_count_of_items();
	}

	public void watchVideo_from_Store()
	{
		UnityEngine.Debug.Log("Showing Video");
	}

	public void show_ads()
	{
		if (this.ads_obj != null)
		{
			this.ads_obj.SendMessage("ShowInterstitial", SendMessageOptions.DontRequireReceiver);
		}
	}

	private float native_width = 1280f;

	private float native_height = 800f;

	private bool called;

	private int total_coins;

	private bool is_weapon_screen;

	private bool is_medi_pack_screen;

	private bool is_armour_pack_screen;

	private bool is_missile_pack_screen;

	private bool is_air_strike_pack_screen;

	public bool show_not_enough_propmt;

	public bool show_money_deals;

	private int total_medi_pack;

	private int total_armour_pack;

	private int total_missile_pack;

	private int total_air_strike_pack;

	private int max_type_of_shields = 5;

	private int[] shield_count_array;

	private int shield_selected;

	private int armour_type_euiped;

	public bool is_game_play;

	public GUISkin skin;

	public bool is_store_selected;

	public Texture2D bg;

	public Texture2D bg2;

	public Rect bg2R;

	public Texture2D store_bar;

	public Rect store_barR;

	public Texture2D coinsBg;

	public Rect coinsBgR;

	public GUIStyle kits_upgrade_btn;

	public Rect kits_upgrade_btnR;

	public GUIStyle weapon_upgrade_btn;

	public Rect weapon_upgrade_btnR;

	public GUIStyle back_btn;

	public Rect back_btnR;

	public Rect item_count_texR;

	public GUIStyle medi_pack_btn;

	public Rect medi_pack_btnR;

	public GUIStyle[] medi_pack_screen_btn;

	public Rect[] medi_pack_screen_btnR;

	public Texture2D medic_count_tex;

	public Rect medi_pack_labelR;

	private int[] medi_prices = new int[]
	{
		400,
		700,
		1000
	};

	private int[] medi_pack_deal = new int[]
	{
		1,
		3,
		5
	};

	public GUIStyle missile_btn;

	public Rect missile_btnR;

	public GUIStyle[] missile_screen_btn;

	public Rect[] missile_screen_btnR;

	public Rect missile_labelR;

	public Texture2D missile_count_tex;

	private int[] missile_prices = new int[]
	{
		200,
		500,
		700
	};

	private int[] missile_pack_deal = new int[]
	{
		1,
		3,
		5
	};

	public GUIStyle armour_btn;

	public Rect armour_btnR;

	public GUIStyle[] armour_screen_btn;

	public Rect[] armoue_screen_btnR;

	public Rect armour_labelR;

	private int[] armour_prices = new int[]
	{
		700,
		800,
		900,
		1000,
		1100
	};

	public GUIStyle air_strike_btn;

	public Rect air_strike_btnR;

	public GUIStyle[] air_strike_screen_btn;

	public Rect[] air_strike_screen_btnR;

	public Rect air_strike_labelR;

	public Texture2D air_strike_count_tex;

	private int[] air_strike_prices = new int[]
	{
		400,
		800,
		1200
	};

	private int[] air_strike_pack_deal = new int[]
	{
		1,
		3,
		5
	};

	public Rect total_coins_labelR;

	public Texture2D total_coins_BgR;

	public Rect total_coins_Rect;

	public GUIStyle equip_btn;

	public Rect equip_btnR;

	public GUIStyle equiped_btn;

	public Rect equiped_btnR;

	public GUIStyle buy_btn;

	public GameObject joystic;

	public GameObject storepanel;

	public GameObject joystic2;

	private Texture2D current_equipe_tex;

	public Texture2D equipe_tex;

	public Texture2D equiped_tex;

	public GUIStyle play_btn;

	public Rect play_btnR;

	public Texture2D hover_watchBtn;

	public Rect hover_watchBtn_bgR;

	public Texture2D no_money_bg;

	public Rect no_money_bgR;

	public GUIStyle yes_btn;

	public Rect yes_btnR;

	public GUIStyle watch_btn;

	public Rect watch_btnR;

	public GUIStyle no_btn;

	public Rect no_btnR;

	public Texture2D money_deals_bg;

	public Rect money_deal_bgR;

	public GUIStyle[] deals_btn;

	public Rect[] deals_btnR;

	public static int[] deals_prices = new int[]
	{
		1000,
		2000,
		4000,
		8000
	};

	public int[] deals_pricesCost = new int[]
	{
		1,
		2,
		4,
		7
	};

	private bool isVideoAdAvailable;

	private bool isWatched;

	public GUIStyle mini_gun_btn;

	public Rect mini_gun_btnR;

	private int mini_gun_upgrade_price;

	public GUIStyle rocket_launcher_btn;

	public Rect rocket_launcher_btnR;

	private int rocket_launcher_upgrade_price;

	public GUIStyle next_btn;

	public Rect next_btnR;

	public GUIStyle prev_btn;

	public Rect prev_btnR;

	private int armour_x_threshold;

	private GameObject ads_obj;

	public Texture2D congrats_bar;

	public Rect congrats_barR;

	public GUIStyle congrats_bar_popUp_close;

	public Rect congrats_bar_popUp_closeR;
}
