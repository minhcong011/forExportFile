using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 startPos;
    TrailRenderer trailRenderer;
    Rigidbody2D playerRb;

    AudioManager2 audioManager;

    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        playerRb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager2>();
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            audioManager.PlaySFX(audioManager.death);
            Die();
        }
        if (collision.CompareTag("Teleport"))
        {
            audioManager.PlaySFX(audioManager.teleport);
            StartCoroutine(Teleport(1f));
        }
    }

    void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Teleport(float duration)
    {
        trailRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        trailRenderer.enabled = true;
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.simulated = false;
        playerRb.velocity = new Vector2(0, 0);
        transform.localScale = new Vector3(0,0,0);
        trailRenderer.enabled = false;

        yield return new WaitForSeconds(duration);
        transform.position = startPos;

        transform.localScale = new Vector3(1, 1, 1);
        trailRenderer.enabled = true;
        playerRb.simulated = true;
    }
}
