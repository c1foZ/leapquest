using UnityEngine;

public class FramerateManager : MonoBehaviour
{
    [SerializeField] private int targetFPS = 60;

    private void Awake()
    {
        Application.targetFrameRate = targetFPS;
        QualitySettings.vSyncCount = 0; // Disable VSync for more precise control
    }
}