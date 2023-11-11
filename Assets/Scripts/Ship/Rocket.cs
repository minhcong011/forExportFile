using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemyShip"))
        {
            GameController.controller.CreateExpolotion(this.transform.position);
            Destroy(this.gameObject);
        }
    }
}
