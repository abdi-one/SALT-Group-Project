using System;
using System.Collections;
using UnityEngine;

public class TripLaser : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Trip Laser Timer")] 
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private bool triggered;
    private bool active;

    private Collider2D playerInside;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    //laser hurt player
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
        active = true;
        anim.SetBool("activated", true);
        float timer = 0f;
        while (timer < activeTime)
        {
            if (playerInside != null)
                playerInside.GetComponent<Health>().TakeDamage(damage);

            timer += Time.deltaTime;
            yield return null;
        }

        active = false;
        triggered = false;
        anim.SetBool("activated", false);
        spriteRenderer.color = Color.white;
        playerInside = null;
    }
}