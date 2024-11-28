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
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        if (activeScene < sceneCount - 1)
        {
            SceneManager.LoadScene(activeScene + 1);
            StartCoroutine(InitializeAfterSceneLoad());
        }
        else
        {
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBGL
        Application.ExternalEval("if (document.fullscreenElement) { document.exitFullscreen(); }");
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
