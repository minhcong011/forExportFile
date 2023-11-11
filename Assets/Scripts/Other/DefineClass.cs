using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum kindCanon
{
    normal,
    gatling
}
public interface IDrown
{
    public void DrowningShip();
}
public interface IShoot
{
    public void Shoot();
}
public interface IDead
{
    public void Reload();
}

public interface ISkill
{
    public void Health();
    public void Rocket();
    public void GatlingGun();
}

public enum shoot
{
    None , 
    Reload,
    Reloaded,
    Aim,
    Shoot
}

public enum kindEnemy
{
    normal,
    fly,
    attack,
    shield,
}
[System.Serializable]
public class wave
{
    public float time = 3f;
    public kindEnemy kindEnemy;
}

[System.Serializable]
public class level
{
    public List<waves> levelWaves = new List<waves>();
}

[System.Serializable]
public class waves
{
    public List<wave> wavesList = new List<wave>();
}
[System.Serializable]
public class Archive
{
    public int bossID;
    public bool isPassed;

    public Archive SetID(int id)
    {
        this.bossID = id;
        return this;
    }
    public Archive SetValue( bool isPassed)
    {
        this.isPassed = isPassed;
        return this;
    }
}

[System.Serializable]
public class SpawnTime
{
    public float maxSpawnTime;
    public float minSpawnTime;
}

[System.Serializable]
public class ListArchive
{
    public List<Archive> archivement = new List<Archive>();

    public ListArchive()
    {
        archivement = new List<Archive>();
        for (int i = 0; i < 5; i++)
        {
            Archive archive = new Archive().SetID(i).SetValue(false);
            archivement.Add(archive);
        }
    }
    public void Set(int bossID , bool isPassed)
    {
        if (bossID >= 5) return;
        archivement[bossID].SetValue(isPassed);
    }

}

public enum ship
{
    player,
    enemy
}

public enum sceneEvent
{
    mainmenu,
    gameovermenu
}

public enum soundEffect
{
    throwEffect,
    gunShoot,
    explotion,
    click,
    gameover,
    start,
    gameplay
}

public enum BossKind
{
    oneCanon,
    twoCanon
}
[System.Serializable]
public class AdmodUserID
{
    public string userID;
    public string bannerID;
    public string interID;
    public string rewardID;
}
public enum AdmodIDs
{
    TestID,
    RealID
}

public enum Skill
{
    heal, 
    gatling, 
    rocket
}

/// <summary>
/// Singleton for mono behavior object
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T singleton;

    public static bool IsInstanceValid() { return singleton != null; }

    void Reset()
    {
        gameObject.name = typeof(T).Name;
    }

    public static T Instance
    {
        get
        {
            if (SingletonMono<T>.singleton == null)
            {
                SingletonMono<T>.singleton = (T)FindObjectOfType(typeof(T));
                if (SingletonMono<T>.singleton == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "[@" + typeof(T).Name + "]";
                    SingletonMono<T>.singleton = obj.AddComponent<T>();
                }
            }

            return SingletonMono<T>.singleton;
        }
    }

}
