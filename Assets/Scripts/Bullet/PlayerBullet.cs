using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private CanonController canonController;

    [SerializeField] private Sprite[] bullet;
    [SerializeField] private SpriteRenderer bulletSprite;
    [SerializeField] private BulletRotation playerBullet;

    private kindCanon kindCanon;
  public void Init(CanonController canonController)
    {
     
        this.canonController = canonController;
        bulletSprite.sprite = bullet[Random.Range(0, bullet.Length)];
    }
    public void KindBullet(kindCanon kindCanon)
    {
        this.kindCanon = kindCanon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemyShip"))
        {

            if(kindCanon == kindCanon.normal)
            canonController.pharse = shoot.Reloaded;

            playerBullet.canRotate = false;

            GameController.controller.PlaySound(soundEffect.explotion);
            GameController.controller.CreateExpolotion(this.transform.position);

            Destroy(this.gameObject);
        }
    }
}
