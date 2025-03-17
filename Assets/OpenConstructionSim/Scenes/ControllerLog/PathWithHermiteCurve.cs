using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class PathWithHermiteCurve : MonoBehaviour
{
    public List<Collider> taskColliders;
    public LineRenderer lineRenderer;
    public float lineWidth = 0.1f;
    public Color gizmoLineColor = Color.red;
    public int resolution = 20;

    private List<Vector3> pathPoints = new List<Vector3>();
    private List<Vector3> smoothPathPoints = new List<Vector3>();

    void Start()
    {
        if (lineRenderer != null)
        {
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
        }

        GeneratePath();
        GenerateHermitePath();
        DrawPathWithLineRenderer();
    }

    void OnDrawGizmos()
    {
        if (taskColliders == null || taskColliders.Count < 2) return;

        GeneratePath();
        GenerateHermitePath();
        DrawPathWithGizmos();
    }

    void GeneratePath()
    {
        pathPoints.Clear();
        foreach (var collider in taskColliders)
        {
            if (collider != null)
                pathPoints.Add(collider.bounds.center);
        }
    }

    void GenerateHermitePath()
    {
        smoothPathPoints.Clear();

        if (pathPoints.Count < 2) return;

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Vector3 p0 = pathPoints[i];
            Vector3 p1 = pathPoints[i + 1];
            Vector3 t0 = Vector3.right; // ŠÈˆÕ“I‚ÈÚü
            Vector3 t1 = Vector3.left;

            for (int j = 0; j <= resolution; j++)
            {
                float t = j / (float)resolution;
                smoothPathPoints.Add(Hermite(p0, t0, p1, t1, t));
            }
        }
    }

    Vector3 Hermite(Vector3 p0, Vector3 t0, Vector3 p1, Vector3 t1, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        float h00 = 2 * t3 - 3 * t2 + 1;
        float h10 = t3 - 2 * t2 + t;
        float h01 = -2 * t3 + 3 * t2;
        float h11 = t3 - t2;

        return h00 * p0 + h10 * t0 + h01 * p1 + h11 * t1;
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
        if (lineRenderer == null || smoothPathPoints.Count < 2) return;

        lineRenderer.positionCount = smoothPathPoints.Count;

        for (int i = 0; i < smoothPathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, smoothPathPoints[i]);
        }
    }
}
