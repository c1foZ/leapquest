using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    private readonly float updateRate = 4.0f; // The number of updates per second

    private float deltaTime = 0.0f;

    private void Start()
    {
        if (fpsText == null)
        {
            Debug.LogError("FPS Text not assigned in the inspector!");
            enabled = false;
            return;
        }

        QualitySettings.vSyncCount = 0; // Ensure VSync is disabled for accurate FPS calculation
        Application.targetFrameRate = -1; // Unlock frame rate
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    private void LateUpdate()
    {
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;

        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        fpsText.text = text;

        // Limit the text update rate
        float timeInterval = 1.0f / updateRate;
        if (deltaTime >= timeInterval)
        {
            deltaTime = 0.0f;
        }
    }
}
