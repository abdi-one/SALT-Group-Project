using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("Linked Door")]
    [SerializeField] private SwitchDoor linkedDoor;

    [Header("Interact Settings")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private bool playerInRange;
    private bool isOn;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerInRange = false;
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
            ToggleLever();
    }

    private void ToggleLever()
    {
        isOn = !isOn;

        // simple colour feedback for now - swap for an animation later if you like
        spriteRenderer.color = isOn ? Color.green : Color.white;

        if (linkedDoor != null)
            linkedDoor.ToggleDoor(isOn);
    }
}