using UnityEngine;

public class LineRendererRecorder : MonoBehaviour
{
    public LineRenderer lineRenderer; // LineRendererをアタッチ

    void Start()
    {
        if (lineRenderer != null)
        {
            Debug.Log("LineRenderer initialized.");
        }
        else
        {
            Debug.LogError("LineRenderer not assigned!");
        }
    }

    void Update()
    {
        if (lineRenderer != null)
        {
            MonitorLineRenderer();
        }
    }

    private void MonitorLineRenderer()
    {
        // LineRendererの頂点数を取得
        int vertexCount = lineRenderer.positionCount;

        // デバッグ用に頂点情報を出力（必要に応じて削除）
        Debug.Log($"LineRenderer vertex count: {vertexCount}");
    }
}
