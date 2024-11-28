using UnityEngine;
using System.Collections;  // To use IEnumerator

public class LevelChanger : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private Animator animator;
    [SerializeField] private float fadeDuration = 2f;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    public void FadeToLevel()
    {
        animator.SetTrigger("FadeOut");
        StartCoroutine(WaitForFadeToComplete());
    }

    private IEnumerator WaitForFadeToComplete()
    {
        yield return new WaitForSeconds(fadeDuration);

        OnFadeComplete();
    }

    public void OnFadeComplete()
    {
        gameManager.LoadNextLevel();
    }
}
