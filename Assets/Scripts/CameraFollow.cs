using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Set the camera's position to the player's position with an offset
            transform.position = playerTransform.position + offset;
        }
    }
}