using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; 
public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private GameManager gameManager;

    private bool isPaused = false;

    private void Start()
    {
        gameManager = GameManager.instance;
        pauseMenuUI.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; 
        pauseMenuUI.SetActive(true); 
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenuUI.SetActive(false);
        gameManager.RestartGameFromMenu();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Stop playing the game in the editor
        #else
        Application.Quit();
        #endif
    }
}
