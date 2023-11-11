// ILSpyBased#2
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoad : MonoBehaviour
{
    private GameObject[] respawns;

    private int SelectSpawn;

    private Scene sceneName;

    private int StateSoundDog;

    private float TimeAll;

    private void Start()
    {
        Debug.Log("GameLoad");

        VariblesGlobal.PlayerHide = 0;
        VariblesGlobal.SelectObject = "";
        VariblesGlobal.SelectObjectOld = "";
        VariblesGlobal.Death = 0;
        VariblesGlobal.Ammo = 0;
        VariblesGlobal.BankaEnergy = 0;
        VariblesGlobal.EnergySpeed = 0f;
        VariblesGlobal.PauseGame = false;
        VariblesGlobal.carKoleso = 0;
        VariblesGlobal.carBenzin = 0;
        VariblesGlobal.carKluch = 0;
        VariblesGlobal.game2_Gvozdi = 0;
        VariblesGlobal.Wrench = 0;
        VariblesGlobal.Wrench2 = 0;
        VariblesGlobal.Wrench3 = 0;
        VariblesGlobal.Wrench4 = 0;
        VariblesGlobal.game2_OpenDoor = 0;
        VariblesGlobal.game2_PosGrandPA = 0;
        VariblesGlobal.SelectObject = "";
        this.TimeAll = 0f;
        this.StateSoundDog = 0;
        this.sceneName = SceneManager.GetActiveScene();
        VariblesGlobal.ShowADinter = true;

        //VariblesGlobal.ShowADinter = false;

        string name = this.sceneName.name;
        if (name == "game2")
        {
            VariblesGlobal.game2_meatCoor = GameObject.Find("gm_meat").transform.position;
        }
        else
        {
            UnityEngine.Object.Destroy(GameObject.Find("PanelBlock"));
        }
        GameObject.Find("CameraFPS").GetComponent<Camera>().enabled = false;
        GameObject.Find("CameraFPS").GetComponent<Camera>().enabled = true;
        VariblesGlobal.CODEnumberGenerate = string.Concat(this.RandomNumber(1001, 9999));
        GameObject.Find("textcode").GetComponent<TextMesh>().text = VariblesGlobal.CODEnumberGenerate;
        this.respawns = null;
        this.respawns = GameObject.FindGameObjectsWithTag("GreenKey");
        this.SelectSpawn = UnityEngine.Random.Range(0, this.respawns.Length);
        GameObject.Find("gm_KeyGreen").transform.position = this.respawns[this.SelectSpawn].transform.position;
        GameObject[] array = this.respawns;
        for (int i = 0; i < array.Length; i++)
        {
            UnityEngine.Object.Destroy(array[i]);
        }
        this.respawns = null;
        this.respawns = GameObject.FindGameObjectsWithTag("RedKey");
        this.SelectSpawn = UnityEngine.Random.Range(0, this.respawns.Length);
        GameObject.Find("gm_KeyRed").transform.position = this.respawns[this.SelectSpawn].transform.position;
        array = this.respawns;
        for (int i = 0; i < array.Length; i++)
        {
            UnityEngine.Object.Destroy(array[i]);
        }
        this.respawns = null;
        this.respawns = GameObject.FindGameObjectsWithTag("SpawnScrewdriver");
        this.SelectSpawn = UnityEngine.Random.Range(0, this.respawns.Length);
        GameObject.Find("gm_screwdriver").transform.position = this.respawns[this.SelectSpawn].transform.position;
        array = this.respawns;
        for (int i = 0; i < array.Length; i++)
        {
            UnityEngine.Object.Destroy(array[i]);
        }
        this.respawns = null;
        this.respawns = GameObject.FindGameObjectsWithTag("Kusachki");
        this.SelectSpawn = UnityEngine.Random.Range(0, this.respawns.Length);
        GameObject.Find("gm_kusachki").transform.position = this.respawns[this.SelectSpawn].transform.position;
        array = this.respawns;
        for (int i = 0; i < array.Length; i++)
        {
            UnityEngine.Object.Destroy(array[i]);
        }
        this.respawns = null;
        this.respawns = GameObject.FindGameObjectsWithTag("SpawnWheel");
        this.SelectSpawn = UnityEngine.Random.Range(0, this.respawns.Length);
        GameObject.Find("gm_wheel1").transform.position = this.respawns[this.SelectSpawn].transform.position;
        array = this.respawns;
        for (int i = 0; i < array.Length; i++)
        {
            UnityEngine.Object.Destroy(array[i]);
        }
        if (VariblesGlobal.GameMode == 0)
        {
            GameObject obj = UnityEngine.Object.Instantiate(Resources.Load("UI/icoGhost")) as GameObject;
            obj.name = "icoGhost";
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }
        if (VariblesGlobal.GameMode == 2)
        {
            GameObject obj2 = UnityEngine.Object.Instantiate(Resources.Load("UI/icoDevil")) as GameObject;
            obj2.name = "icoDevil";
            obj2.transform.SetParent(GameObject.Find("Canvas").transform, false);
            VariblesGlobal.SpeedKick = 750f;
        }
        if (VariblesGlobal.GameMode == 3)
        {
            GameObject obj3 = UnityEngine.Object.Instantiate(Resources.Load("UI/IcoChick")) as GameObject;
            obj3.name = "IcoChick";
            obj3.transform.SetParent(GameObject.Find("Canvas").transform, false);
            VariblesGlobal.SpeedKick = 250f;
        }
    }

    public int RandomNumber(int min, int max)
    {
        return new System.Random().Next(min, max);
    }

    private void Update()
    {
        this.TimeAll += Time.deltaTime;
        if (this.TimeAll > 15f && this.StateSoundDog == 0 && this.sceneName.name == "game2")
        {
            this.StateSoundDog = 1;
            UnityEngine.Object.Instantiate(Resources.Load("sound/soundDogVoice"), GameObject.Find("DogGrandPa").transform.position, Quaternion.identity);
        }
        if (this.TimeAll > 38f && this.StateSoundDog == 1 && this.sceneName.name == "game2")
        {
            this.StateSoundDog = 2;
            UnityEngine.Object.Instantiate(Resources.Load("sound/soundDogVoice"), GameObject.Find("DogGrandPa").transform.position, Quaternion.identity);
        }
        if (this.TimeAll > 50f)
        {
            this.TimeAll = 0f;
            this.StateSoundDog = 0;
        }
    }
}


