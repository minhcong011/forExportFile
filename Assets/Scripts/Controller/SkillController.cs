using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillController : MonoBehaviour
{
    private const string HEALT_KEY = "HEATHKEY";
    private const string ROCKET_KEY = "ROCKET_KEY";
    private const string GATLING_KEY = "GATLING_KEY";

    public int healthCount;
    public int rocketCount;
    public int gatlingGunCount;

    public Button healthBtn;
    public Button rocketBtn;
    public Button gatlingGunBtn;

    public Text healTxt;
    public Text rocketTxt;
    public Text gatlingGunTxt;


    [Header("game object")]
    public BaseSkill healthSkill;
    public BaseSkill gatlingSkill;
    public BaseSkill rocketSkill;

    public float posY;
    public Vector2 rangeX;
    public float cooldownDrop;

    private void Start()
    {
        GetData();
        Init();
    }

    public int GetHealthCount()
    {
        return PlayerPrefs.GetInt(HEALT_KEY);
    }
    public int GetRocketCount()
    {
        return PlayerPrefs.GetInt(ROCKET_KEY);
    }
    public int GetGatlingCount()
    {
        return PlayerPrefs.GetInt(GATLING_KEY);
    }

    private void GetData()
    {
        healthCount = PlayerPrefs.GetInt(HEALT_KEY);
        rocketCount = PlayerPrefs.GetInt(ROCKET_KEY);
        gatlingGunCount = PlayerPrefs.GetInt(GATLING_KEY);
    }

    private void Init()
    {
        healthBtn.onClick.AddListener(() => {
            if(healthCount <= 0)
            {
              
                return;
            }
            if (!GameController.controller.CheckMotherShipHeal()) {
              
                return;
            }
            GameController.controller.GetPlayerSkill().Health();
            UpdateHealth(false);
        });

        rocketBtn.onClick.AddListener(()=> {
            if(rocketCount <= 0)
            {
              
                return;
            }

            GameController.controller.GetPlayerSkill().Rocket();
            UpdateRocket(false);
        });

        gatlingGunBtn.onClick.AddListener(() => {
            if (gatlingGunCount <= 0)
            {
               
                return;
            }

            GameController.controller.GetPlayerSkill().GatlingGun();
            UpdateGatlingGun(false);
        });

        healTxt.text = healthCount.ToString();
        rocketTxt.text = rocketCount.ToString();
        gatlingGunTxt.text = gatlingGunCount.ToString();
    }

    public void UpdateText()
    {
        healTxt.text = healthCount.ToString();
        rocketTxt.text = rocketCount.ToString();
        gatlingGunTxt.text = gatlingGunCount.ToString();
    }

    public void UpdateHealth(bool increase)
    {
        if (increase) healthCount++;
        else healthCount--;
        healTxt.text = healthCount.ToString();
        PlayerPrefs.SetInt(HEALT_KEY,healthCount);
    }
    public void UpdateRocket(bool increase)
    {
        if (increase) rocketCount++;
        else rocketCount--;
        rocketTxt.text = rocketCount.ToString();
        PlayerPrefs.SetInt(ROCKET_KEY,rocketCount);
    }
    public void UpdateGatlingGun(bool increase)
    {
        if (increase) gatlingGunCount++;
        else gatlingGunCount--;
        gatlingGunTxt.text = gatlingGunCount.ToString();
        PlayerPrefs.SetInt(GATLING_KEY, gatlingGunCount); 
    }
    public void UpdateGatlingGun(int increaseValue)
    {
        gatlingGunCount += increaseValue;
        PlayerPrefs.SetInt(GATLING_KEY, gatlingGunCount);
    }
    public void UpdateRocket(int increaseValue)
    {
        rocketCount += increaseValue;
        PlayerPrefs.SetInt(ROCKET_KEY, rocketCount);
    }
    public void UpdateHealth(int increaseValue)
    {
        healthCount += increaseValue;
        PlayerPrefs.SetInt(HEALT_KEY, healthCount);
    }


    public void StartDropSkill()
    {
        StartCoroutine(DropSkill()); 
    }

    private IEnumerator DropSkill()
    {
        if (GameController.controller.isGameover)
        {
            StopCoroutine(DropSkill());
            yield break;
        }

        yield return new WaitForSeconds(cooldownDrop);

        if (GameController.controller.isGameover)
        {
            StopCoroutine(DropSkill());
            yield break;
        }
        BaseSkill skill = GetRandomSkill();
        skill.transform.position = new Vector3(Random.Range(rangeX.x, rangeX.y), posY);

        StartCoroutine(DropSkill());
    }

    private BaseSkill GetRandomSkill()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                return Instantiate(healthSkill, transform);
            case 1:
                return Instantiate(gatlingSkill, transform);
            case 2:
                return Instantiate(rocketSkill, transform);
            default:
                return Instantiate(healthSkill, transform);
        }
    }

}
