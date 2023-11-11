using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{

    [SerializeField] private GameObject rocket;
    [SerializeField] private float power;

    [SerializeField] private Vector2 rocketRangeX;
    [SerializeField] private float posY;
    [SerializeField] private float minDelay;
    [SerializeField] private float maxDelay;
    public void RocketLaucher()
    {
        for (int i = 0; i < 3; i++)
        {
            float delay = Random.Range(minDelay, maxDelay);
            StartCoroutine(Shoot(delay));
        }
    }

    private IEnumerator Shoot(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject clone = Instantiate(rocket, this.transform);
        clone.transform.position = new Vector3(Random.Range(rocketRangeX.x, rocketRangeX.y), posY);
        Vector3 dir = -clone.transform.up.normalized * power;
        clone.GetComponent<Rigidbody2D>().AddForce(dir);
    }

}
