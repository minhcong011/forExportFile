using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NormalEnemy : BaseEnemy, IDead,IDrown
{
    [SerializeField] private RotatingImage rotateShip;

    public override void TakeDame()
    {
        boxCollider.enabled = false;
        Reload();
        GameController.controller.UpdateScore(1);
        GameController.controller.UpdateCombo();
        GameController.controller.CreateEmote(this.transform.position);
    }
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public void Reload()
    {
        DrowningShip();
    }
    public override void EventEndDrowing()
    {
        rotateShip.canRotate = false;
        Destroy(rotateShip);
        Destroy(this.gameObject);
    }

    public void DrowningShip()
    {
        canMove = false;
        this.transform.DOMoveY(transform.position.y - 5f, 3f).OnComplete(EventEndDrowing);
    }
}
