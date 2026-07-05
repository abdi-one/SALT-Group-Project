using System;
using System.Collections;
using UnityEngine;

public class TripLaser : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Trip Laser Timer")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    [SerializeField] private float damageInterval = 0.5f;

    private SpriteRenderer spriteRenderer;
    private bool triggered;
    private Collider2D playerInside;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // disable animator permanently so it never interferes
        Animator anim = GetComponent<Animator>();
        if (anim != null) anim.enabled = false;
    }

    private void Start()
    {
        spriteRenderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInside = collision;
            if (!triggered)
                StartCoroutine(ActivateTripLaser());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerInside = null;
    }

    private IEnumerator ActivateTripLaser()
    {
        triggered = true;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(activationDelay);

        spriteRenderer.color = Color.green;

        float timer = 0f;
        while (timer < activeTime)
        {
            if (playerInside != null)
                playerInside.GetComponent<Health>().TakeDamage(damage);

            yield return new WaitForSeconds(damageInterval);
            timer += damageInterval;
        }

        triggered = false;
        spriteRenderer.color = Color.white;
        playerInside = null;
    }
}