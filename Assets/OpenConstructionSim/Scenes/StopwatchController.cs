using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Input System を使用するために追加

public class StopwatchController : MonoBehaviour
{
    public Text stopwatchText; // UIのTextコンポーネント
    private float timeElapsed = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (isRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateStopwatchText();
        }

        // スペースキーの入力をチェック
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isRunning = !isRunning; // Toggle the stopwatch state
        }
    }

    private void UpdateStopwatchText()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        int milliseconds = Mathf.FloorToInt((timeElapsed * 100) % 100);
        stopwatchText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", minutes, seconds, milliseconds);
    }
}
