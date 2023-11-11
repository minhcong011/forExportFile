using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : BaseSkill
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("playerBullet"))
        {
            GameController.controller.UpdateHealth();
            Fade();
        }
    }
}
