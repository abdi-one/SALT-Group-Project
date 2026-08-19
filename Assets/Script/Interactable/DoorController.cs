using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float moveDistance = 2f; // Distance to move up/down
    public float moveSpeed = 3f;    // Speed of movement
    public bool isOpen = false;

    private Vector2 _closedPosition;
    private Vector2 _openedPosition;
    private bool _isMoving;

    void Start()
    {
        _closedPosition = transform.position;
        _openedPosition = _closedPosition;
    }

    public void ToggleDoor()
    {
        if (_isMoving) return; // Don't interrupt current movement

        isOpen = !isOpen;

        if (isOpen)
        {
            // Move door up (negative Y direction)
            _openedPosition = new Vector2(_closedPosition.x, _closedPosition.y + moveDistance);
            Debug.Log("Door opened...");
        }
        else
        {
            // Move door down to original position
            _openedPosition = _closedPosition;
            Debug.Log("Door closed...");
        }

        _isMoving = true;
    }

    void Update()
    {
        if (_isMoving && Vector2.Distance(transform.position, _openedPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _openedPosition, moveSpeed * Time.deltaTime);
        }
        else if (_isMoving)
        {
            // Door has reached its target position
            _isMoving = false;
        }
    }
}