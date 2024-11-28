using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    [SerializeField] private LevelChanger levelChanger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelChanger.FadeToLevel();

        }
    }
}