using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TankEnemy : BaseEnemy,IDead, IDrown
{
    public bool isShield = true;

    public GameObject shield;

    [SerializeField] private RotatingImage rotateShip;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public override void TakeDame()
    {
        GameController.controller.UpdateScore(1);
        GameController.controller.UpdateCombo();
        if (isShield)
        {
         
            isShield = false;

            shield.SetActive(false);
            return;
        }
        GameController.controller.CreateEmote(this.transform.position);
        boxCollider.enabled = false;
        Reload();
    }
    public override void EventEndDrowing()
    {
        rotateShip.canRotate = false;
        Destroy(rotateShip);
        Destroy(this.gameObject);
    }

    public void Reload()
    {
      
        DrowningShip();
    }

    public void DrowningShip()
    {
        canMove = false;
        this.transform.DOMoveY(transform.position.y - 5f, 3f).OnComplete(EventEndDrowing);
    }
}
