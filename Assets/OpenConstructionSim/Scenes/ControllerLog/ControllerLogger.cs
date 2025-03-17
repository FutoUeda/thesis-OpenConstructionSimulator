using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerLogger : MonoBehaviour
{
    private string logFilePath;

    void Start()
    {
        // 指定のディレクトリにログファイルを保存
        string directoryPath = @"C:\Users\futol\Downloads\Simulator-system-main"; ;
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath); // ディレクトリが存在しない場合は作成
        }
        logFilePath = Path.Combine(directoryPath, "ControllerLog.txt");

        // ファイル名を作成
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        logFilePath = Path.Combine(directoryPath, $"ControllerLog_{timestamp}.txt");

        LogMessage("=== Controller Log Started ===");
    }

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        // 操作の取得
        Vector2 leftStick = gamepad.leftStick.ReadValue();
        Vector2 rightStick = gamepad.rightStick.ReadValue();
        float leftTrigger = gamepad.leftTrigger.ReadValue();
        float rightTrigger = gamepad.rightTrigger.ReadValue();

        bool l1Button = gamepad.leftShoulder.isPressed; // L1ボタン
        bool r1Button = gamepad.rightShoulder.isPressed; // R1ボタン

        // ログメッセージの生成
        string logMessage = $"Time: {Time.time:F2}, " +
                            $"ブーム上下・旋回: {leftStick}, アーム上下・バケット開閉: {rightStick}, " +
                            $"クローラー前進（左）: {leftTrigger:F2}, クローラー前進（右）: {rightTrigger:F2}, " +
                            $"クローラー逆転(左): {l1Button}, クローラー逆転(右): {r1Button}";

        // ログを記録
        LogMessage(logMessage);
    }

    private void LogMessage(string message)
    {
        Debug.Log(message); // Unityのコンソールに出力
        File.AppendAllText(logFilePath, message + "\n"); // ファイルに追記
    }

    void OnApplicationQuit()
    {
        LogMessage("=== Controller Log Ended ===");
    }
}
