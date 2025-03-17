using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class PathWithBezierCurve : MonoBehaviour
{
    public List<Collider> taskColliders;
    public LineRenderer lineRenderer;
    public float lineWidth = 0.1f;
    public Color gizmoLineColor = Color.blue;
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
        GenerateBezierPath();
        DrawPathWithLineRenderer();
    }

    void OnDrawGizmos()
    {
        if (taskColliders == null || taskColliders.Count < 2) return;

        GeneratePath();
        GenerateBezierPath();
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

    void GenerateBezierPath()
    {
        smoothPathPoints.Clear();

        if (pathPoints.Count < 2) return;

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Vector3 p0 = pathPoints[i];
            Vector3 p1 = i + 1 < pathPoints.Count ? pathPoints[i + 1] : p0;
            Vector3 controlPoint = (p0 + p1) / 2 + Vector3.up; // ŠÈˆÕ“I‚È§Œä“_

            for (int j = 0; j <= resolution; j++)
            {
                float t = j / (float)resolution;
                smoothPathPoints.Add(Bezier(p0, controlPoint, p1, t));
            }
        }
    }

    Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
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
