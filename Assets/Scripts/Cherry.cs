using UnityEngine;

public class Cherry : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCherry();
            Destroy(gameObject);
        }
    }
    private void CollectCherry()
    {
        Debug.Log("Cherry collected!");
        // Add your logic for what happens when the cherry is collected
    }
}
