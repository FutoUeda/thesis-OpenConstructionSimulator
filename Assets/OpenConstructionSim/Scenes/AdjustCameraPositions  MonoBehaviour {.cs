using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCameraPositions : MonoBehaviour
{
    public Camera leftEyeCamera;   // 左目用カメラ
    public Camera rightEyeCamera;  // 右目用カメラ
    public float xOffset = 0.1f;   // x座標に加えるオフセット

    void Start()
    {
        if (leftEyeCamera != null && rightEyeCamera != null)
        {
            // 左目カメラの位置を更新
            Vector3 leftEyePosition = leftEyeCamera.transform.position;
            leftEyeCamera.transform.position = new Vector3(leftEyePosition.x - xOffset, leftEyePosition.y, leftEyePosition.z);

            // 右目カメラの位置を更新
            Vector3 rightEyePosition = rightEyeCamera.transform.position;
            rightEyeCamera.transform.position = new Vector3(rightEyePosition.x + xOffset, rightEyePosition.y, rightEyePosition.z);
        }
        else
        {
            Debug.LogError("左目または右目のカメラが設定されていません。インスペクターでカメラを設定してください。");
        }
    }
}
