using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;

    private bool isGamePaused = false;
    private int score = 0;
    private int health = 4;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }

        UpdateScoreText();
        UpdateHealthText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void RestartGame()
    {
        score = 0;
        health -= 1;

        if (health == 0)
        {
            QuitGame();
        }

        UpdateScoreText();
        UpdateHealthText();

        Invoke("ReloadScene", 0.1f);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit(); 
        #endif
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0f : 1f;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + score.ToString();
        }
        else
        {
            Debug.LogWarning("Score Text is not assigned!");
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "HEALTH: " + health.ToString();
        }
        else
        {
            Debug.LogWarning("Health Text is not assigned!");
        }
    }
}
