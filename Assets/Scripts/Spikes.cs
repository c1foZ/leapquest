using UnityEngine;

public class Spikes : MonoBehaviour
{
    private  GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DeathOnSpikes();
            gameManager.RestartGame();
        }
    }
    private void DeathOnSpikes()
    {
        Debug.Log("You're dead!");
        // Add your logic for what happens when the cherry is collected
    }
}