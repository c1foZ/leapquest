using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;

    private int score = 0;
    private int totalScore = 0;
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
    }

    private void Start()
    {
        InitializeTotalCherries();
        UpdateScoreText();
        UpdateHealthText();
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

    public void RestartGameFromMenu()
    {
        SceneManager.LoadScene(1);
        StartCoroutine(InitializeAfterSceneLoad());
        health = 4;
        UpdateHealthText();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(InitializeAfterSceneLoad());
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit(); 
        #endif
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
            scoreText.text = "SCORE: " + score.ToString() + " of " + totalScore.ToString();
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

    private void InitializeTotalCherries()
    {
        totalScore = GameObject.FindGameObjectsWithTag("Cherry").Length;
    }

    private System.Collections.IEnumerator InitializeAfterSceneLoad()
    {

        yield return null;

        InitializeTotalCherries();
        score = 0;
        UpdateScoreText();
    }
}
