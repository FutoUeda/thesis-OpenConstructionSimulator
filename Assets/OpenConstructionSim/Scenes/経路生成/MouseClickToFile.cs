using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveClickCoordinates : MonoBehaviour
{
    public Camera orthographicCamera;  // 平行投影カメラ
    public Terrain terrain;           // 対象のTerrain
    private string filePath = @"C:\Users\futol\Downloads\Simulator-system-main\click_positions.txt";

    void Start()
    {
        // ディレクトリ確認と作成
        string directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // ファイルが存在しない場合はヘッダーを追加
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Click Positions (x, z)\n");
        }
    }

    void Update()
    {
        // 左クリックまたはタップを検知
        if (Input.GetMouseButtonDown(0))
        {
            // スクリーン座標を取得
            Vector3 screenPosition = Input.mousePosition;

            // スクリーン座標をRayに変換
            Ray ray = orthographicCamera.ScreenPointToRay(screenPosition);

            // RaycastでTerrainとの交点を検出
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 worldPosition = hit.point;

                // x, z座標を取得
                float x = worldPosition.x;
                float z = worldPosition.z;

                // テキストファイルに保存
                SavePositionToFile(x, z);

                Debug.Log($"クリックされた位置: x = {x}, z = {z}");
            }
        }
    }

    // 座標をファイルに保存
    private void SavePositionToFile(float x, float z)
    {
        try
        {
            string data = $"{x}, {z}\n";
            File.AppendAllText(filePath, data);
        }
        catch (IOException ex)
        {
            Debug.LogError($"ファイル書き込みエラー: {ex.Message}");
        }
    }
}
