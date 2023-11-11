using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MotherShip : MonoBehaviour,ISkill
{
    public CanonController controller;
    public RocketController rocketController; 

    public float maxHealth = 100;
    public float currentHealth;

    public Animator animator;

    private void Start()
    {
        ResetHealth();
    }


 
    public void ReadyToShoot()
    {
        controller.pharse = shoot.Reloaded;
    }

    //su kien con thuyen va cham vao thuyen cua player
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("enemyShip"))
        {
  
            BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();
            if (!enemy)
            {
                return;
            }

            animator.SetTrigger("Hit");

            GameController.controller.CreateExpolotion(collision.transform.position);
            GameController.controller.DropCombo();
            GameController.controller.PlaySound(soundEffect.explotion);
            float dame = enemy.health;
            Destroy(collision.gameObject);
            TakeDame(dame);
        }
    }

    public void TakeDame(float dame)
    {
        currentHealth -= dame;
        if (currentHealth <= 0)
        {
          
            DrowningShip();
            GameController.controller.GameoverEvent();
            return;
        }

        float percent = currentHealth / maxHealth;

        GameController.controller.UpdateHealthUI(percent);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void DrowningShip()
    {
        ChangePharseCanon(shoot.None);
        this.transform.DOMoveY(transform.position.y - 5f, 3f);
    }

    public void ChangePharseCanon(shoot shoot)
    {
        controller.pharse = shoot;
    }
    public void Health()
    {
        currentHealth += 100;    
        if (currentHealth > maxHealth)
        currentHealth = maxHealth;

        float percent = currentHealth / maxHealth;

        GameController.controller.UpdateHealthUI(percent);
    }

    public void Rocket()
    {
        rocketController.RocketLaucher();
    }

    public void GatlingGun()
    {
        controller.StartGatling();
    }

    public void DisablePath()
    {
        controller.DisablePath();
        controller.DestroyBullet();
    }

    public bool CanHealth()
    {
        return !(currentHealth == maxHealth);
    }
}
