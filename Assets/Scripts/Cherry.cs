using UnityEngine;

public class Cherry : MonoBehaviour
{
    private GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager not found in the scene.");
        }
    }

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
        if (gameManager != null)
        {
            gameManager.IncreaseScore(1);
        }
    }
}
