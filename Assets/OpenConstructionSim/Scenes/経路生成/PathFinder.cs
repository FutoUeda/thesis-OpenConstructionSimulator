using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public Terrain terrain;
    public Vector3 startPoint; // スタート地点
    public Vector3 goalPoint;  // ゴール地点
    public List<List<Vector3>> nodeGroups; // 複数のノード群

    private List<Vector3> path = new List<Vector3>(); // 経路リスト

    // ノード群の範囲
    public Vector2[] nodeGroup1Range; // 第1ノード群
    public Vector2[] nodeGroup2Range; // 第2ノード群
    public Vector2[] nodeGroup3Range;
    public Vector2[] nodeGroup4Range;
    public Vector2[] nodeGroup5Range;
    public Vector2[] nodeGroup6Range;
    public Vector2[] nodeGroup7Range;
    public Vector2[] nodeGroup8Range;
    public Vector2[] nodeGroup9Range;
    public Vector2[] nodeGroup10Range;
    public Vector2[] nodeGroup11Range;
    public Vector2[] nodeGroup12Range;
    public Vector2[] nodeGroup13Range;
    public Vector2[] nodeGroup14Range;
    public Vector2[] nodeGroup15Range;
    // 追加のノード群があればここに定義

    void Start()
    {
        nodeGroups = new List<List<Vector3>>();

        // ノード群を生成
        GenerateNodeGroup(nodeGroup1Range);
        GenerateNodeGroup(nodeGroup2Range);
        // 必要に応じて他のノード群を追加

        GeneratePath();
    }

    // ノード群を指定された範囲に基づいて生成
    void GenerateNodeGroup(Vector2[] range)
    {
        List<Vector3> nodesInGroup = new List<Vector3>();

        // 範囲に基づいてノードを生成
        for (float x = range[0].x; x <= range[1].x; x += 0.1f) // 0.1 Unity単位 (10cm)
        {
            for (float z = range[0].y; z <= range[1].y; z += 0.1f)
            {
                // 高さを取得
                float y = terrain.SampleHeight(new Vector3(x, 0, z));

                // ノードを作成してリストに追加
                Vector3 node = new Vector3(x, y, z);
                nodesInGroup.Add(node);
            }
        }

        nodeGroups.Add(nodesInGroup);
    }

    void GeneratePath()
    {
        // 最初のスタート地点を経路に追加
        path.Add(startPoint);
        Vector3 currentPoint = startPoint;

        // すべてのノード群を通過するまで繰り返す
        foreach (var nodeGroup in nodeGroups)
        {
            // 現在の地点から最も近いノードを選ぶ
            Vector3 closestNode = GetClosestNode(currentPoint, nodeGroup);
            path.Add(closestNode);

            // 選んだノードを次のスタート地点として設定
            currentPoint = closestNode;
        }

        // 最後にゴール地点に最も近いノードを追加
        Vector3 closestGoalNode = GetClosestNode(currentPoint, new List<Vector3> { goalPoint });
        path.Add(closestGoalNode);
    }

    // 現在の位置からノード群内で最も近いノードを選ぶ
    Vector3 GetClosestNode(Vector3 currentPoint, List<Vector3> nodeGroup)
    {
        Vector3 closestNode = nodeGroup[0];
        float closestDistance = Vector3.Distance(currentPoint, closestNode);

        foreach (var node in nodeGroup)
        {
            float distance = Vector3.Distance(currentPoint, node);
            if (distance < closestDistance)
            {
                closestNode = node;
                closestDistance = distance;
            }
        }

        return closestNode;
    }

    // 経路の可視化
    void OnDrawGizmos()
    {
        if (path.Count > 1)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < path.Count - 1; i++)
            {
                Gizmos.DrawLine(path[i], path[i + 1]);
            }
        }
    }
}
