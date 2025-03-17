using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class ObjectPositionRecorder : MonoBehaviour
{
    public GameObject targetObject; // 記録対象のオブジェクト
    private string saveDirectory = @"C:\Users\futol\Downloads\Simulator-system-main\実験データ\佐藤";
    public float samplingInterval = 0.01f; // サンプリング間隔（10ms = 0.01秒）

    private float lastSampleTime = 0f; // 最後にサンプリングした時間
    private List<string> recordedData = new List<string>(); // 記録されたデータ（文字列リスト）
    private DateTime startTime; // 実行開始時間
    private string executionLogFileName = "ExecutionLog_sato.txt"; // 実行時間を記録するファイル名

    void Start()
    {
        // 実行開始時間を記録
        startTime = DateTime.Now;

        // ヘッダーをリストに追加
        recordedData.Add("X,Y,Z");

        // ターゲットオブジェクトが設定されていない場合、警告を出す
        if (targetObject == null)
        {
            Debug.LogWarning("No target object specified. Position recording will not proceed.");
        }
        else
        {
            Debug.Log("Position recorder started for object: " + targetObject.name);
        }

        // 実行開始時間をログファイルに記録
        LogExecutionTime("Application started");
    }

    void Update()
    {
        // ターゲットオブジェクトが設定されていない場合は何もしない
        if (targetObject == null)
            return;

        // サンプリング間隔を満たしている場合のみ記録
        if (Time.time - lastSampleTime >= samplingInterval)
        {
            RecordPosition();
            lastSampleTime = Time.time; // 最後にサンプリングした時間を更新
        }
    }

    private void RecordPosition()
    {
        // ターゲットオブジェクトの現在座標を取得
        Vector3 position = targetObject.transform.position;

        // 座標をフォーマットしてリストに追加
        string dataLine = $"{position.x:F3},{position.y:F3},{position.z:F3}";
        recordedData.Add(dataLine);

        Debug.Log($"Recorded for {targetObject.name}: {dataLine}");
    }

    private void OnApplicationQuit()
    {
        SaveToCSV();

        // 実行終了時間をログファイルに記録
        LogExecutionTime("Application ended");
    }

    private void SaveToCSV()
    {
        try
        {
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"ObjectPosition_{timestamp}.csv";
            string fullPath = Path.Combine(saveDirectory, fileName);

            // リスト内のデータをファイルに書き出す
            File.WriteAllLines(fullPath, recordedData);

            Debug.Log($"Position data saved to: {fullPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save position data: {e.Message}");
        }
    }

    private void LogExecutionTime(string eventDescription)
    {
        try
        {
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            string logFilePath = Path.Combine(saveDirectory, executionLogFileName);

            // 実行時間を計算
            TimeSpan elapsedTime = DateTime.Now - startTime;

            // ログメッセージを作成
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {eventDescription}. Elapsed time: {elapsedTime.TotalSeconds:F2} seconds";

            // ファイルに追記
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);

            Debug.Log(logMessage);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to log execution time: {e.Message}");
        }
    }
}
