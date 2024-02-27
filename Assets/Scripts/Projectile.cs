using UnityEngine;

public class Projectile : MonoBehaviour
{
    private readonly float speed = 5f;
    private Vector2 direction;

    // Set the direction of the projectile
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }


    private void Update()
    {
        // Move the projectile in the specified direction
        transform.Translate(speed * Time.deltaTime * direction);

        // Destroy the projectile when it goes off-screen
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision logic here
        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyPlayer(collision.gameObject);
            Destroy(gameObject);
            GameManager.instance.RestartGame();
        }
    }

    private void DestroyPlayer(GameObject player)
    {
        // Destroy the player GameObject
        Destroy(player);
    }
}
