using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // ファイル操作に必要
using System;

public class PathWithLinearInterpolation : MonoBehaviour
{
    public List<Collider> taskColliders;
    public LineRenderer lineRenderer;
    public float lineWidth = 0.1f;
    public Color gizmoLineColor = Color.green;

    private List<Vector3> pathPoints = new List<Vector3>();
    private List<Vector3> smoothPathPoints = new List<Vector3>();

    // 保存先ディレクトリを指定
    public string saveDirectory = @"C:\Users\futol\Downloads\Simulator-system-main";

    void Start()
    {
        if (lineRenderer != null)
        {
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
        }

        // 経路を生成
        GeneratePath();
        GenerateSmoothPath();

        // LineRendererで経路を描画
        if (lineRenderer != null)
        {
            DrawPathWithLineRenderer();
        }

        // 経路を保存
        SavePathToCSV();
    }

    void OnDrawGizmos()
    {
        if (taskColliders == null || taskColliders.Count < 2)
        {
            return;
        }

        GeneratePath();
        GenerateSmoothPath();
        DrawPathWithGizmos();
    }

    void GeneratePath()
    {
        pathPoints.Clear();

        foreach (var collider in taskColliders)
        {
            if (collider != null)
            {
                pathPoints.Add(collider.bounds.center);
            }
        }
    }

    void GenerateSmoothPath()
    {
        smoothPathPoints.Clear();

        if (pathPoints.Count < 2) return;

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Vector3 p0 = pathPoints[i];
            Vector3 p1 = pathPoints[i + 1];

            for (int j = 0; j <= 10; j++)
            {
                float t = j / 10.0f;
                smoothPathPoints.Add(Vector3.Lerp(p0, p1, t));
            }
        }
    }

    void DrawPathWithGizmos()
    {
        Gizmos.color = gizmoLineColor;

        for (int i = 0; i < smoothPathPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(smoothPathPoints[i], smoothPathPoints[i + 1]);
        }
    }

    void DrawPathWithLineRenderer()
    {
        if (smoothPathPoints.Count < 2)
        {
            Debug.LogWarning("滑らかな経路を描画するには2つ以上のポイントが必要です。");
            return;
        }

        lineRenderer.positionCount = smoothPathPoints.Count;

        for (int i = 0; i < smoothPathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, smoothPathPoints[i]);
        }
    }

    // 経路をCSVに保存
    void SavePathToCSV()
    {
        try
        {
            // ディレクトリが存在しない場合は作成
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            // ファイル名を生成
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"SmoothPath_{timestamp}.csv";
            string fullPath = Path.Combine(saveDirectory, fileName);

            // ファイルに書き込み
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine("X,Y,Z"); // ヘッダー行
                foreach (var point in smoothPathPoints)
                {
                    writer.WriteLine($"{point.x},{point.y},{point.z}");
                }
            }

            Debug.Log($"Path saved to: {fullPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save path to CSV: {e.Message}");
        }
    }
}
