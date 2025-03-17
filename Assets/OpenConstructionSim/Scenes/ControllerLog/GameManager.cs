using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CaptureCamera captureCamera; // CaptureCamera スクリプトをアタッチ

    void Start()
    {
        Debug.Log("ゲーム開始");
    }

    void OnApplicationQuit()
    {
        // ゲーム終了時の処理
        Debug.Log("ゲーム終了");

        // スクリーンショットを保存
        if (captureCamera != null)
        {
            captureCamera.CaptureAndSave();
        }
        else
        {
            Debug.LogWarning("CaptureCamera スクリプトが設定されていません！");
        }
    }
}
