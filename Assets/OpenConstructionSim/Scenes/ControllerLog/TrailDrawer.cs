using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private int pointIndex = 0;

    void Start()
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer が設定されていません！");
            return;
        }

        // 最初の頂点を設定
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }

    void Update()
    {
        // 現在位置を頂点として追加
        pointIndex++;
        lineRenderer.positionCount = pointIndex + 1;
        lineRenderer.SetPosition(pointIndex, transform.position);
    }
}
