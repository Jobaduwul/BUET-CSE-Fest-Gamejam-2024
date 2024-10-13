using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float horizontalOffset;
    public float leftLimit = -10f;
    public float rightLimit = 10f;

    private float originalYPosition;

    void Start()
    {
        originalYPosition = transform.position.y;
    }

    void LateUpdate()
    {
        Vector3 playerPosition = player.position;

        if (playerPosition.x > leftLimit && playerPosition.x < rightLimit)
        {
            transform.position = new Vector3(playerPosition.x + horizontalOffset, originalYPosition, transform.position.z);
        }
    }
}
