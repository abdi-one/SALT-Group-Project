using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    [Header("Room Zoom Sizes")]
    [SerializeField] private float previousRoomZoom = 5f;
    [SerializeField] private float nextRoomZoom = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.transform.position.x < transform.position.x)
                cam.MoveToNewRoom(nextRoom, nextRoomZoom);
            else
                cam.MoveToNewRoom(previousRoom, previousRoomZoom);
        }
    }
}