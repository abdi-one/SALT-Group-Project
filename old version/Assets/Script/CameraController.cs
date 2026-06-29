using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float zoomSpeed = 1f;
    private float currentPosX;
    private float currentSize;
    private float sizeVelocity;
    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        currentPosX = cam.orthographicSize;
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, currentSize, ref sizeVelocity, zoomSpeed);
    }

    public void MoveToNewRoom(Transform newRoom)
    {
        currentPosX = newRoom.position.x;
        
        Room room = newRoom.GetComponent<Room>();
        if (room != null)
            currentSize = room.cameraSize;
    }
}
