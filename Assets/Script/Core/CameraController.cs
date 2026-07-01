using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Zoom Settings")]
    [SerializeField] private float defaultZoom = 5f;
    private float currentPosX;
    private float currentPosY;
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = defaultZoom;

        currentPosX = transform.position.x;
        currentPosY = transform.position.y;
    }

    public void MoveToNewRoom(Transform _newRoom, float _zoomSize)
    {
        currentPosX = _newRoom.position.x;
        currentPosY = _newRoom.position.y;

        // snap position and zoom instantly
        transform.position = new Vector3(currentPosX, currentPosY, transform.position.z);
        cam.orthographicSize = _zoomSize;
    }
}