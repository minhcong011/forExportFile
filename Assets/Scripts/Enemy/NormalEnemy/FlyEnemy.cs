using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FlyEnemy : BaseEnemy, IDead, IDrown
{

    public float durationDead;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public override void TakeDame()
    {
        Reload(); 
        boxCollider.enabled = false;
        GameController.controller.UpdateScore(1);
        GameController.controller.UpdateCombo();
        GameController.controller.CreateEmote(this.transform.position);
    }
    public void DrowningShip()
    {
        transform.DORotate(new Vector3(0, 0, 90), durationDead);
        transform.DOLocalMoveX(transform.localPosition.x - 2f, durationDead);
        transform.DOLocalMoveY(transform.localPosition.y - 10f, durationDead * 2).OnComplete(() => {
            Destroy(this.gameObject);
        });
    }

    public void Reload()
    {
        canMove = false;
        DrowningShip();
    }
}
