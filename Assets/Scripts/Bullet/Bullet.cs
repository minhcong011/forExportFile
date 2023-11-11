using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.controller.PlaySound(soundEffect.explotion);
            GameController.controller.MotherShipTakeDame();
            GameController.controller.CreateExpolotion(this.transform.position);
			Destroy(this.gameObject);
		}
    }
}
