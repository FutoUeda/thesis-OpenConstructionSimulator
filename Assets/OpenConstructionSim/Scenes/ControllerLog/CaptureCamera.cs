using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CaptureCamera : MonoBehaviour
{
    public Camera mapCamera;
    public string saveDirectory = @"C:\Users\futol\Downloads\Simulator-system-main";



    public void CaptureAndSave()
    {
        // RenderTextureを用意
        RenderTexture renderTexture = new RenderTexture(1920, 1080, 24);
        mapCamera.targetTexture = renderTexture;

        // カメラに描画させる
        Texture2D screenShot = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        mapCamera.Render();

        // RenderTextureからテクスチャを取得
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(new Rect(0, 0, 1920, 1080), 0, 0);
        screenShot.Apply();

        // 保存用ファイル名を生成（日時に基づく）
        string timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fileName = $"Map_{timeStamp}.png";
        string fullPath = Path.Combine(saveDirectory, fileName);

        // 保存先ディレクトリが存在しない場合は作成
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }

        byte[] bytes = screenShot.EncodeToPNG();
        File.WriteAllBytes(fullPath, bytes);

        mapCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        Debug.Log($"画像が保存されました: {fullPath}");
    }
}
