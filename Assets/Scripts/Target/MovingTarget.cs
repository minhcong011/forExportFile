using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public float distance;
    public Vector2 pos;

    public CanonController canon;


    public float delay;
    public void StartMoving()
    {
        ResetTarget();
        StartCoroutine(MovingTg());
    }

    public void StopMoving()
    {
        StopAllCoroutines();
        ResetTarget();
    }
    private void ResetTarget()
    {
        transform.position = new Vector3(pos.x, transform.position.y);
        distance = Mathf.Abs(distance);
    }

    private IEnumerator MovingTg()
    {
        yield return new WaitForSeconds(delay);

        if (transform.position.x < pos.x || transform.position.x > pos.y)
        {
            distance *= -1; 
        }

        transform.position = new Vector3(transform.position.x + distance, transform.position.y);

        if (GameController.controller.isGameover) {
            StopMoving();
            yield break; }

        canon.UpdateTrajectory();
        StartCoroutine(MovingTg());
    }

  
}
