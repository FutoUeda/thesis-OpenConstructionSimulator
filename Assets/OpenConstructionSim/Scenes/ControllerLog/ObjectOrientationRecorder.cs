using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class ObjectOrientationRecorder : MonoBehaviour
{
    public Transform targetObject; // 記録対象のオブジェクト
    public string saveDirectory = @"C:\Users\futol\Downloads\Simulator-system-main"; // 保存先ディレクトリ
    public float samplingInterval = 0.01f; // サンプリング間隔（10ms = 0.01秒）

    private List<Quaternion> recordedRotations = new List<Quaternion>(); // 記録された回転情報
    private List<float> recordedTimestamps = new List<float>(); // 記録されたタイムスタンプ
    private float lastSampleTime = 0f; // 最後にサンプリングした時間
    private DateTime startTime; // 実行開始時刻

    void Start()
    {
        // 実行開始時刻を記録
        startTime = DateTime.Now;

        if (targetObject == null)
        {
            Debug.LogError("Target object is not assigned!");
        }
    }

    void Update()
    {
        // サンプリング間隔を満たす場合のみ記録
        if (Time.time - lastSampleTime >= samplingInterval && targetObject != null)
        {
            RecordOrientation();
            lastSampleTime = Time.time; // 最後にサンプリングした時間を更新
        }
    }

    private void RecordOrientation()
    {
        // オブジェクトの回転を記録
        recordedRotations.Add(targetObject.rotation);
        recordedTimestamps.Add(Time.time); // タイムスタンプを記録
    }

    private void OnApplicationQuit()
    {
        SaveOrientationsToCSV();

        // 実行時間を計算してログに出力
        TimeSpan elapsedTime = DateTime.Now - startTime;
        Debug.Log($"Execution Time: {elapsedTime.TotalSeconds} seconds");
    }

    private void SaveOrientationsToCSV()
    {
        try
        {
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"RecordedOrientations_{timestamp}.csv";
            string fullPath = Path.Combine(saveDirectory, fileName);

            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine("Time,X,Y,Z,W"); // ヘッダーを書き込み
                for (int i = 0; i < recordedRotations.Count; i++)
                {
                    Quaternion rotation = recordedRotations[i];
                    float time = recordedTimestamps[i];
                    writer.WriteLine($"{time},{rotation.x},{rotation.y},{rotation.z},{rotation.w}");
                }
            }

            Debug.Log($"Orientations saved to: {fullPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save orientations to CSV: {e.Message}");
        }
    }
}
