// ILSpyBased#2
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MeHuman : MonoBehaviour
{
    private GameObject Player;

    private GameObject MyHand;

    private GameObject FPSimpulse;

    private GameObject FPS_keyDark;

    private GameObject FPS_keyGreen;

    private GameObject FPS_keyRed;

    private GameObject FPS_saw;

    private GameObject FPS_pistol;

    private GameObject FPS_crowbar;

    private GameObject FPS_axe;

    private GameObject FPS_screwdriver;

    private GameObject FPS_knife;

    private GameObject FPS_wrench01;

    private GameObject FPS_kusachki;

    private GameObject FPS_wood;

    private GameObject FPS_tabcode;

    private GameObject FPS_molotok;

    private GameObject FPS_wheel;

    private GameObject FPS_jerrycan;

    private GameObject FPS_keycar;

    private GameObject FPS_canoe_paddle;

    private GameObject FPS_map;

    private GameObject FPS_meat;

    private GameObject gm_KeyDark;

    private GameObject gm_KeyGreen;

    private GameObject gm_KeyRed;

    private GameObject gm_Saw;

    private GameObject gm_Pistol;

    private GameObject gm_crowbar;

    private GameObject gm_axe;

    private GameObject gm_screwdriver;

    private GameObject gm_knife;

    private GameObject gm_wrench01;

    private GameObject gm_kusachki;

    private GameObject gm_wood;

    private GameObject gm_tabcode;

    private GameObject gm_molotok;

    private GameObject gm_wheel1;

    private GameObject gm_wheel2;

    private GameObject gm_jerrycan;

    private GameObject gm_keycar;

    private GameObject gm_canoe_paddle;

    private GameObject gm_map;

    private GameObject gm_meat;

    private string AnimT2;

    private string TMP_selectObject;

    private GameObject IcoShoot;

    private GameObject IcoDropDown;

    private GameObject IcoBullet;

    private GameObject CanvasBlood;

    private GameObject icoDownUp;

    private void Start()
    {
        this.FPSimpulse = GameObject.Find("FPSimpulse");
        this.Player = GameObject.Find("FPSController");
        this.MyHand = GameObject.Find("FPSHand2");
        this.FPS_keyDark = GameObject.Find("FPS_keyDark");
        this.FPS_keyGreen = GameObject.Find("FPS_keyGreen");
        this.FPS_keyRed = GameObject.Find("FPS_keyRed");
        this.FPS_saw = GameObject.Find("FPS_saw");
        this.FPS_pistol = GameObject.Find("FPS_pistol");
        this.FPS_crowbar = GameObject.Find("FPS_crowbar");
        this.FPS_axe = GameObject.Find("FPS_axe");
        this.FPS_screwdriver = GameObject.Find("FPS_screwdriver");
        this.FPS_knife = GameObject.Find("FPS_knife");
        this.FPS_wrench01 = GameObject.Find("FPS_wrench01");
        this.FPS_kusachki = GameObject.Find("FPS_kusachki");
        this.FPS_wood = GameObject.Find("FPS_wood");
        this.FPS_tabcode = GameObject.Find("FPS_tabcode");
        this.FPS_molotok = GameObject.Find("FPS_molotok");
        this.FPS_wheel = GameObject.Find("FPS_wheel");
        this.FPS_jerrycan = GameObject.Find("FPS_jerrycan");
        this.FPS_keycar = GameObject.Find("FPS_keycar");
        this.FPS_canoe_paddle = GameObject.Find("FPS_canoe_paddle");
        this.FPS_map = GameObject.Find("FPS_map");
        this.FPS_meat = GameObject.Find("FPS_meat");
        this.MyHand.SetActive(false);
        this.FPS_keyDark.SetActive(false);
        this.FPS_keyGreen.SetActive(false);
        this.FPS_keyRed.SetActive(false);
        this.FPS_saw.SetActive(false);
        this.FPS_pistol.SetActive(false);
        this.FPS_crowbar.SetActive(false);
        this.FPS_axe.SetActive(false);
        this.FPS_screwdriver.SetActive(false);
        this.FPS_knife.SetActive(false);
        this.FPS_wrench01.SetActive(false);
        this.FPS_kusachki.SetActive(false);
        this.FPS_wood.SetActive(false);
        this.FPS_tabcode.SetActive(false);
        this.FPS_molotok.SetActive(false);
        this.FPS_wheel.SetActive(false);
        this.FPS_jerrycan.SetActive(false);
        this.FPS_keycar.SetActive(false);
        this.FPS_canoe_paddle.SetActive(false);
        this.FPS_map.SetActive(false);
        this.FPS_meat.SetActive(false);
        this.gm_KeyDark = GameObject.Find("gm_KeyDark");
        this.gm_KeyGreen = GameObject.Find("gm_KeyGreen");
        this.gm_KeyRed = GameObject.Find("gm_KeyRed");
        this.gm_Saw = GameObject.Find("gm_Saw");
        this.gm_Pistol = GameObject.Find("gm_Pistol");
        this.gm_crowbar = GameObject.Find("gm_crowbar");
        this.gm_axe = GameObject.Find("gm_axe");
        this.gm_screwdriver = GameObject.Find("gm_screwdriver");
        this.gm_knife = GameObject.Find("gm_knife");
        this.gm_wrench01 = GameObject.Find("gm_wrench01");
        this.gm_kusachki = GameObject.Find("gm_kusachki");
        this.gm_wood = GameObject.Find("gm_wood");
        this.gm_tabcode = GameObject.Find("gm_tabcode");
        this.gm_molotok = GameObject.Find("gm_molotok");
        this.gm_wheel1 = GameObject.Find("gm_wheel1");
        this.gm_wheel2 = GameObject.Find("gm_wheel2");
        this.gm_jerrycan = GameObject.Find("gm_jerrycan");
        this.gm_keycar = GameObject.Find("gm_keycar");
        this.gm_canoe_paddle = GameObject.Find("gm_canoe_paddle");
        this.gm_map = GameObject.Find("gm_map");
        this.gm_meat = GameObject.Find("gm_meat");
        this.SetAnimate("pistol");
        this.IcoShoot = GameObject.Find("IcoShoot");
        this.IcoShoot.SetActive(false);
        this.IcoDropDown = GameObject.Find("IcoDropDown");
        this.IcoDropDown.SetActive(false);
        this.IcoBullet = GameObject.Find("IcoBullet");
        this.IcoBullet.SetActive(false);
        this.CanvasBlood = GameObject.Find("CanvasBlood");
        this.CanvasBlood.SetActive(false);
        this.icoDownUp = GameObject.Find("icoDownUp");
    }

    private void Update()
    {
        if (VariblesGlobal.tCanvasBlood == 1)
        {
            this.CanvasBlood.SetActive(true);
        }
        else
        {
            this.CanvasBlood.SetActive(false);
        }
        if (VariblesGlobal.tCanvasUpDown == 0)
        {
            this.icoDownUp.SetActive(true);
        }
        else
        {
            this.icoDownUp.SetActive(false);
        }
        if (VariblesGlobal.BankaEnergyTime > 0f)
        {
            VariblesGlobal.BankaEnergyTime -= Time.deltaTime;
            VariblesGlobal.EnergySpeed = 2f;
            if (VariblesGlobal.BankaEnergyTime <= 2f)
            {
                VariblesGlobal.EnergySpeed = 0f;
            }
        }
        if (VariblesGlobal.SelectObjectOld == VariblesGlobal.SelectObject)
        {
            this.TMP_selectObject = VariblesGlobal.SelectObject;
            VariblesGlobal.SelectObjectOld = VariblesGlobal.SelectObject;
        }
        else
        {
            if (VariblesGlobal.SelectObjectOld != "")
            {
                UnityEngine.Object.Instantiate(Resources.Load("Sound/SoundPut"));
            }
            VariblesGlobal.ActionDropDown = 2;
            this.TMP_selectObject = VariblesGlobal.SelectObjectOld;
            VariblesGlobal.SelectObjectOld = VariblesGlobal.SelectObject;
        }
        switch (this.TMP_selectObject)
        {
            case "KeyDark":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_keyDark.SetActive(true);
                this.SetAnimate("keypos");
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_keyDark.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_KeyDark.SetActive(true);
                    this.gm_KeyDark.transform.position = this.FPSimpulse.transform.position;
                    this.AnimT2 = "";
                }
                break;
            case "KeyGreen":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_keyGreen.SetActive(true);
                this.SetAnimate("keypos");
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_keyGreen.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_KeyGreen.SetActive(true);
                    this.gm_KeyGreen.transform.position = this.FPSimpulse.transform.position;
                    this.AnimT2 = "";
                }
                break;
            case "KeyRed":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_keyRed.SetActive(true);
                this.SetAnimate("keypos");
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_keyRed.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_KeyRed.SetActive(true);
                    this.gm_KeyRed.transform.position = this.FPSimpulse.transform.position;
                    this.AnimT2 = "";
                }
                break;
            case "Saw":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_saw.SetActive(true);
                this.SetAnimate("sawpos");
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_saw.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_Saw.SetActive(true);
                    this.gm_Saw.transform.position = new Vector3(this.FPSimpulse.transform.position.x, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z - 0.3f);
                    this.AnimT2 = "";
                }
                break;
            case "Pistol":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_pistol.SetActive(true);
                this.SetAnimate("pistol");
                this.IcoShoot.SetActive(true);
                this.IcoBullet.SetActive(true);
                GameObject.Find("IcoBulletText").GetComponent<Text>().text = string.Concat(VariblesGlobal.Ammo);
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoBullet.SetActive(false);
                    this.IcoDropDown.SetActive(false);
                    this.IcoShoot.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_pistol.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_Pistol.SetActive(true);
                    this.gm_Pistol.transform.position = this.FPSimpulse.transform.position;
                    this.AnimT2 = "";
                }
                break;
            case "Crowbar":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_crowbar.SetActive(true);
                this.SetAnimate("sawpos");
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_crowbar.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_crowbar.SetActive(true);
                    this.gm_crowbar.transform.position = new Vector3(this.FPSimpulse.transform.position.x + 0.6f, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z + 0.3f);
                    this.AnimT2 = "";
                }
                break;
            case "Axe":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_axe.SetActive(true);
                this.SetAnimate("sawpos");
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_axe.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_axe.SetActive(true);
                    this.gm_axe.transform.position = new Vector3(this.FPSimpulse.transform.position.x + 0.6f, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z);
                    this.AnimT2 = "";
                }
                break;
            case "Screwdriver":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_screwdriver.SetActive(true);
                this.SetAnimate("sawpos");
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_screwdriver.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_screwdriver.SetActive(true);
                    this.gm_screwdriver.transform.position = new Vector3(this.FPSimpulse.transform.position.x, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z - 0.1f);
                    this.AnimT2 = "";
                }
                break;
            case "Knife":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_knife.SetActive(true);
                this.SetAnimate("sawpos");
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_knife.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_knife.SetActive(true);
                    this.gm_knife.transform.position = new Vector3(this.FPSimpulse.transform.position.x + 0.1f, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z);
                    this.AnimT2 = "";
                }
                break;
            case "wrench01":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_wrench01.SetActive(true);
                this.SetAnimate("sawpos");
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_wrench01.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_wrench01.SetActive(true);
                    this.gm_wrench01.transform.position = new Vector3(this.FPSimpulse.transform.position.x + 0.1f, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z);
                    this.AnimT2 = "";
                }
                break;
            case "Kusachki":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_kusachki.SetActive(true);
                this.SetAnimate("sawpos");
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_kusachki.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_kusachki.SetActive(true);
                    this.gm_kusachki.transform.position = new Vector3(this.FPSimpulse.transform.position.x + 0.3f, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z);
                    this.AnimT2 = "";
                }
                break;
            case "Wood":
                this.IcoDropDown.SetActive(true);
                if ((Object)this.gm_wood == (Object)null)
                {
                    this.gm_wood = GameObject.Find("gm_wood");
                }
                this.gm_wood.SetActive(false);
                this.MyHand.SetActive(true);
                this.FPS_wood.SetActive(true);
                this.SetAnimate("wood");
                if ((Object)GameObject.Find("set_wood") == (Object)null)
                {
                    if (VariblesGlobal.ActionDropDown > 0)
                    {
                        this.IcoDropDown.SetActive(false);
                        VariblesGlobal.ActionDropDown = 0;
                        this.MyHand.SetActive(false);
                        this.FPS_wood.SetActive(false);
                        if (VariblesGlobal.ActionDropDown == 1)
                        {
                            VariblesGlobal.SelectObject = "";
                        }
                        this.gm_wood.SetActive(true);
                        this.gm_wood.transform.position = new Vector3(this.FPSimpulse.transform.position.x + 0.2f, this.FPSimpulse.transform.position.y + 0.24f, this.FPSimpulse.transform.position.z - 1.5f);
                        this.AnimT2 = "";
                    }
                }
                else
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_wood.SetActive(false);
                    VariblesGlobal.SelectObject = "";
                    this.AnimT2 = "";
                }
                break;
            case "Code":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_tabcode.SetActive(true);
                GameObject.Find("FPS_textcode").GetComponent<TextMesh>().text = VariblesGlobal.CODEnumberGenerate;
                this.SetAnimate("keypos");
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_tabcode.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_tabcode.SetActive(true);
                    this.gm_tabcode.transform.position = new Vector3(this.FPSimpulse.transform.position.x, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z);
                    this.AnimT2 = "";
                }
                break;
            case "Molotok":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_molotok.SetActive(true);
                this.SetAnimate("sawpos");
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_molotok.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_molotok.SetActive(true);
                    this.gm_molotok.transform.position = new Vector3(this.FPSimpulse.transform.position.x, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z);
                    this.AnimT2 = "";
                }
                break;
            case "wheel1":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_wheel.SetActive(true);
                this.SetAnimate("wood");
                if ((Object)GameObject.Find("NormalCar1_FrontLeftWheel") == (Object)null)
                {
                    if (VariblesGlobal.ActionDropDown > 0)
                    {
                        this.IcoDropDown.SetActive(false);
                        VariblesGlobal.ActionDropDown = 0;
                        this.MyHand.SetActive(false);
                        this.FPS_wheel.SetActive(false);
                        if (VariblesGlobal.ActionDropDown == 1)
                        {
                            VariblesGlobal.SelectObject = "";
                        }
                        this.gm_wheel1.SetActive(true);
                        this.gm_wheel1.transform.position = new Vector3(this.FPSimpulse.transform.position.x, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z);
                        this.AnimT2 = "";
                    }
                }
                else
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_wheel.SetActive(false);
                    VariblesGlobal.SelectObject = "";
                    this.AnimT2 = "";
                }
                break;
            case "wheel2":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_wheel.SetActive(true);
                this.SetAnimate("wood");
                if ((Object)GameObject.Find("NormalCar1_FrontRightWheel") == (Object)null)
                {
                    if (VariblesGlobal.ActionDropDown > 0)
                    {
                        this.IcoDropDown.SetActive(false);
                        VariblesGlobal.ActionDropDown = 0;
                        this.MyHand.SetActive(false);
                        this.FPS_wheel.SetActive(false);
                        if (VariblesGlobal.ActionDropDown == 1)
                        {
                            VariblesGlobal.SelectObject = "";
                        }
                        this.gm_wheel2.SetActive(true);
                        this.gm_wheel2.transform.position = new Vector3(this.FPSimpulse.transform.position.x, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z);
                        this.AnimT2 = "";
                    }
                }
                else
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_wheel.SetActive(false);
                    VariblesGlobal.SelectObject = "";
                    this.AnimT2 = "";
                }
                break;
            case "jerrycan":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_jerrycan.SetActive(true);
                this.SetAnimate("wood");
                if (VariblesGlobal.carBenzin == 0)
                {
                    if (VariblesGlobal.ActionDropDown > 0)
                    {
                        this.IcoDropDown.SetActive(false);
                        VariblesGlobal.ActionDropDown = 0;
                        this.MyHand.SetActive(false);
                        this.FPS_jerrycan.SetActive(false);
                        if (VariblesGlobal.ActionDropDown == 1)
                        {
                            VariblesGlobal.SelectObject = "";
                        }
                        this.gm_jerrycan.SetActive(true);
                        this.gm_jerrycan.transform.position = new Vector3(this.FPSimpulse.transform.position.x, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z);
                        this.AnimT2 = "";
                    }
                }
                else
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_jerrycan.SetActive(false);
                    VariblesGlobal.SelectObject = "";
                    this.AnimT2 = "";
                }
                break;
            case "keycar":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_keycar.SetActive(true);
                this.SetAnimate("keypos");
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_keycar.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_keycar.SetActive(true);
                    this.gm_keycar.transform.position = new Vector3(this.FPSimpulse.transform.position.x, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z);
                    this.AnimT2 = "";
                }
                break;
            case "canoePaddle":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_canoe_paddle.SetActive(true);
                this.SetAnimate("sawpos");
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_canoe_paddle.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_canoe_paddle.SetActive(true);
                    this.gm_canoe_paddle.transform.position = new Vector3(this.FPSimpulse.transform.position.x, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z);
                    this.AnimT2 = "";
                }
                break;
            case "map":
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_map.SetActive(true);
                this.SetAnimate("wood");
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_map.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_map.SetActive(true);
                    this.gm_map.transform.position = new Vector3(this.FPSimpulse.transform.position.x, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z);
                    this.AnimT2 = "";
                }
                break;
            case "meat":
                this.gm_meat.SetActive(false);
                this.IcoDropDown.SetActive(true);
                this.MyHand.SetActive(true);
                this.FPS_meat.SetActive(true);
                this.SetAnimate("wood");
                this.gm_meat.transform.position = VariblesGlobal.game2_meatCoor;
                if (VariblesGlobal.ActionDropDown > 0)
                {
                    this.IcoDropDown.SetActive(false);
                    VariblesGlobal.ActionDropDown = 0;
                    this.MyHand.SetActive(false);
                    this.FPS_meat.SetActive(false);
                    if (VariblesGlobal.ActionDropDown == 1)
                    {
                        VariblesGlobal.SelectObject = "";
                    }
                    this.gm_meat.SetActive(true);
                    this.gm_meat.transform.position = new Vector3(this.FPSimpulse.transform.position.x, this.FPSimpulse.transform.position.y, this.FPSimpulse.transform.position.z);
                    this.AnimT2 = "";
                }
                break;
            default:
                this.MyHand.SetActive(false);
                this.IcoDropDown.SetActive(false);
                VariblesGlobal.ActionDropDown = 0;
                this.AnimT2 = "";
                break;
        }
    }

    private void SetAnimate(string AnimT)
    {
        if (this.AnimT2 != AnimT)
        {
            this.AnimT2 = AnimT;
            this.MyHand.GetComponent<Animator>().SetBool("pistol", false);
            this.MyHand.GetComponent<Animator>().SetBool("keypos", false);
            this.MyHand.GetComponent<Animator>().SetBool("sawpos", false);
            this.MyHand.GetComponent<Animator>().SetBool("wood", false);
            this.MyHand.GetComponent<Animator>().SetBool(AnimT, true);
        }
    }

    private void TeleporteT(Vector3 PosTeleport)
    {
        base.GetComponent<NavMeshAgent>().isStopped = true;
        base.GetComponent<NavMeshAgent>().ResetPath();
        base.GetComponent<NavMeshAgent>().Warp(PosTeleport);
        base.GetComponent<NavMeshAgent>().SetDestination(PosTeleport);
        base.transform.position = PosTeleport;
    }
}


