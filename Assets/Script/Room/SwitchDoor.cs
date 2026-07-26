using System.Collections;
using UnityEngine;

public class SwitchDoor : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private float moveDistance = 3f; // how far up the door slides
    [SerializeField] private float moveSpeed = 3f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Collider2D doorCollider;
    private Coroutine moveRoutine;

    private void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
        closedPosition = transform.position;
        openPosition = closedPosition + new Vector3(0, moveDistance, 0);
    }

    public void ToggleDoor(bool _open)
    {
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(MoveDoor(_open ? openPosition : closedPosition, _open));
    }

    private IEnumerator MoveDoor(Vector3 _target, bool _open)
    {
        // disable collider immediately when opening so the player can't get stuck mid-slide
        if (_open)
            doorCollider.enabled = false;

        while (Vector3.Distance(transform.position, _target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = _target;

        // only re-enable the collider once fully closed
        if (!_open)
            doorCollider.enabled = true;
    }
}