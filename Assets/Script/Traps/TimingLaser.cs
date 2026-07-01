using System;
using System.Collections;
using UnityEngine;

public class TimingLaser : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Time Settings")]
    [SerializeField] private float onTime;   // how long the laser stays on
    [SerializeField] private float offTime;  // how long the laser stays off

    private SpriteRenderer spriteRenderer;
    private bool isOn;
    private Collider2D playerInside;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(LaserCycle());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerInside = collision;
    }
    //laser hurt player
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerInside = null;
    }

    private IEnumerator LaserCycle()
    {
        while (true)
        {
            isOn = false;
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(offTime);
            isOn = true;
            spriteRenderer.color = Color.blue;
            float timer = 0f;
            while (timer < onTime)
            {
                if (playerInside != null)
                    playerInside.GetComponent<Health>().TakeDamage(damage);
                
                yield return new WaitForSeconds(0.5f);
                timer += 0.5f;
            }
        }
    }
}