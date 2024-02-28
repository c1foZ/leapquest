using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float minXLimit = -2.3f;
    public float maxXLimit = 10f;
    public float minYLimit = -2f;
    public float maxYLimit = -1f;

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 targetPosition = playerTransform.position + offset;

            targetPosition.x = Mathf.Clamp(targetPosition.x, minXLimit, maxXLimit);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minYLimit, maxYLimit);

            transform.position = targetPosition;
        }
    }
}
