using UnityEngine;

public class ConsoleController : MonoBehaviour
{
    public bool consoleActivated;
    private SpriteRenderer _spriteRenderer;

    public GameObject door; // This should be assigned in Inspector
    public GameObject laser; // Add this for laser control

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UsedConsole()
    {
        consoleActivated = !consoleActivated;
        _spriteRenderer.color = consoleActivated ? Color.green : Color.white;

        if (consoleActivated)
        {
            Debug.Log("Console is activated...");

            // Activate door opening
            if (door != null)
            {
                DoorController doorCtrl = door.GetComponent<DoorController>();
                if (doorCtrl != null)
                {
                    doorCtrl.ToggleDoor();
                }
                Debug.Log("Door opened");
            }

            // Deactivate laser
            if (laser != null)
            {
                laser.SetActive(false);
                Debug.Log("Laser deactivated");
            }
        }
        else
        {
            Debug.Log("Console is deactivated...");

            // Activate door closing
            if (door != null)
            {
                DoorController doorCtrl = door.GetComponent<DoorController>();
                if (doorCtrl != null)
                {
                    doorCtrl.ToggleDoor();
                }
                Debug.Log("Door closed");
            }

            // Activate laser
            if (laser != null)
            {
                laser.SetActive(true);
                Debug.Log("Laser activated");
            }
        }
    }
}