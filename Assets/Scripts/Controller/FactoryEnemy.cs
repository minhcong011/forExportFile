using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryEnemy : MonoBehaviour
{
    [SerializeField] private GameObject normalEnemy;
    [SerializeField] private GameObject flyEnemy;
    [SerializeField] private GameObject shieldEnemy;
    [SerializeField] private GameObject attackEnemy;
    [SerializeField] private GameObject[] boss = new GameObject[5];
    public GameObject GetEnemy(kindEnemy kindEnemy)
    {
        switch (kindEnemy)
        {
            case kindEnemy.normal:
                GameObject enemy = Instantiate(normalEnemy, this.transform);
                enemy.GetComponent<BaseEnemy>().Init();
                return enemy;
            case kindEnemy.fly:
                return FlyEnemyPos(Instantiate(flyEnemy, this.transform));
            case kindEnemy.attack:
               GameObject enemyAttack = Instantiate(attackEnemy, this.transform);
                enemyAttack.GetComponent<BaseEnemy>().Init();
                return enemyAttack; 
            case kindEnemy.shield:
                GameObject enemyShield = Instantiate(shieldEnemy, this.transform);
                enemyShield.GetComponent<BaseEnemy>().Init();
                return enemyShield;
            default:
                GameObject enemyDefault = Instantiate(normalEnemy, this.transform);
                enemyDefault.GetComponent<BaseEnemy>().Init();
                return enemyDefault;
        }
    }

    public GameObject GetBoss(int index)
    {
        if (index >= 5)
            return Instantiate(boss[Random.Range(0, boss.Length)], this.transform);
        else
            return Instantiate(boss[index], this.transform);
    } 

    private GameObject FlyEnemyPos( GameObject gameObject)
    {
        float posY = Random.Range(0, 2f);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, posY);
        return gameObject;
    }
}
