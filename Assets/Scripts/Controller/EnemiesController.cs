using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{

    public List<level> levels = new List<level>();
    public int currentLevel = -1;

    public FactoryEnemy factory;
    public List<SpawnTime> spawnTimes = new List<SpawnTime>();

    public List<Sprite> bossSprite = new List<Sprite>();

    public List<RuntimeAnimatorController> animatorController;
    private void StartWave(int currentLevel)
    {
        //se lay ngau nhien 1 list wave trong 1 level
        if (currentLevel >= 5) currentLevel = Random.Range(0, 5);
        int currentWave = Random.Range(0, levels[currentLevel].levelWaves.Count);
        StartCoroutine(SpawnWave(0, currentLevel,currentWave, levels[currentLevel].levelWaves[currentWave].wavesList.Count));
    }

    public void ResetWave()
    {
        currentLevel = -1;
    }

    private IEnumerator SpawnWave(int index,int level,int curentWave,int countEnemies)
    {
      
        if(index >= countEnemies)
        {
        
            EventDoneWave();
            yield break;
        }

        //float timeDelay = levels[level].levelWaves[curentWave].wavesList[index].time ;
        float maxSpawnTime = currentLevel >= 5  ? spawnTimes[spawnTimes.Count-1].maxSpawnTime : spawnTimes[level].maxSpawnTime;
        float minSpawnTime = currentLevel >= 5 ? spawnTimes[spawnTimes.Count - 1].minSpawnTime : spawnTimes[level].minSpawnTime;

        float timeDelay = maxSpawnTime - (maxSpawnTime - minSpawnTime) * (float)((float)index / (float)countEnemies);

        kindEnemy kind = levels[level].levelWaves[curentWave].wavesList[index].kindEnemy;
       
        yield return new WaitForSeconds(timeDelay);

        if (GameController.controller.isGameover) yield break;
        GameObject go = factory.GetEnemy(kind);

        go.transform.position = new Vector3(this.transform.position.x, go.transform.position.y);
        index++;

        StartCoroutine(SpawnWave(index, level, curentWave, countEnemies));
    }


    private void EventDoneWave()
    {
        //summon boss
        StartCoroutine(SpawnBoss());
            
    }

    //Khi start game , spawn wave nay se chay dau tien.
    public void SpawnWave()
    {
        currentLevel++;
        StartWave(currentLevel);
    }

    private IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(3f);
        if (GameController.controller.isGameover) yield break;
        GameObject go = factory.GetBoss(currentLevel);
        go.GetComponent<Boss>().Init(this,currentLevel);
        //go.GetComponent<Boss>().SetSprite(bossSprite[currentWave]);
        go.transform.position = this.transform.position;
    }


    public void DeleteAllShip()
    {
        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            IDrown enemy = transform.GetChild(i).GetComponent<IDrown>();
      
            if(enemy != null)
            {
                enemy.DrowningShip();
            }
        }
    }

    public RuntimeAnimatorController GetAnimatorController()
    {
        return animatorController[Random.Range(0, animatorController.Count)];
    }
}

