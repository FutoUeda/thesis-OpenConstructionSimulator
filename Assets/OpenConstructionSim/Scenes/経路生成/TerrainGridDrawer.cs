using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGridDrawer : MonoBehaviour
{
    public Terrain terrain;       // 対象のTerrain
    public float gridSize = 0.1f; // グリッドの幅 (10cm)

    void OnDrawGizmos()
    {
        if (terrain == null) return;

        // Terrainの範囲を取得
        Vector3 terrainPosition = terrain.transform.position;
        float terrainWidth = terrain.terrainData.size.x;
        float terrainHeight = terrain.terrainData.size.z;

        // グリッドの色を設定
        Gizmos.color = Color.green;

        // 水平方向のラインを描画
        for (float x = 0; x <= terrainWidth; x += gridSize)
        {
            Vector3 start = new Vector3(x, 0, 0) + terrainPosition;
            Vector3 end = new Vector3(x, 0, terrainHeight) + terrainPosition;

            start.y = terrain.SampleHeight(start) + terrainPosition.y;
            end.y = terrain.SampleHeight(end) + terrainPosition.y;

            Gizmos.DrawLine(start, end);
        }

        // 垂直方向のラインを描画
        for (float z = 0; z <= terrainHeight; z += gridSize)
        {
            Vector3 start = new Vector3(0, 0, z) + terrainPosition;
            Vector3 end = new Vector3(terrainWidth, 0, z) + terrainPosition;

            start.y = terrain.SampleHeight(start) + terrainPosition.y;
            end.y = terrain.SampleHeight(end) + terrainPosition.y;

            Gizmos.DrawLine(start, end);
        }
    }
}

