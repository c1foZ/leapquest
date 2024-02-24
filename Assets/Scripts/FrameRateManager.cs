using UnityEngine;

public class FramerateManager : MonoBehaviour
{
    private readonly int targetFPS = 60;

    private void Awake()
    {
        Application.targetFrameRate = targetFPS;
        QualitySettings.vSyncCount = 0;
    }
}