using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private BoxCollider2D currentRoom;
    [SerializeField] private new Camera camera;

    void Update()
    {
        float minX = currentRoom.offset.x - (currentRoom.size.x / 2f) + (camera.orthographicSize * camera.aspect);
        float maxX = currentRoom.offset.x + (currentRoom.size.x / 2f) - (camera.orthographicSize * camera.aspect);
        float x = Mathf.Clamp(transform.position.x, minX, maxX);
        float minY = currentRoom.offset.y - (currentRoom.size.y / 2f) + camera.orthographicSize;
        float maxY = currentRoom.offset.y + (currentRoom.size.y / 2f) - camera.orthographicSize;
        float y = Mathf.Clamp(transform.position.y, minY, maxY);

        camera.transform.position = new Vector3(x, y, camera.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Room"))
        {
            currentRoom = collision.gameObject.GetComponent<BoxCollider2D>();
        }
    }
}
