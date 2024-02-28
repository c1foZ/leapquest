using UnityEngine;

public class Button : MonoBehaviour
{
    private bool IsPressed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !IsPressed)
        {
            GameObject movingPlatform = GameObject.FindGameObjectWithTag("MovingPlatform");

            if (movingPlatform != null)
            {
                if (movingPlatform.TryGetComponent<MovingPlatform>(out var platformScript))
                {
                    IsPressed = true;
                    platformScript.StartMoving();
                }
            }
        }
    }
}
