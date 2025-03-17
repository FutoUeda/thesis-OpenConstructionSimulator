using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FisheyeCamera : MonoBehaviour
{
    public Material fisheyeMaterial;  // 魚眼効果を適用するマテリアル
    [Range(180f, 360f)]
    public float fieldOfView = 180f;  // 視野角の調整

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (fisheyeMaterial != null)
        {
            // 視野角をシェーダに渡す
            fisheyeMaterial.SetFloat("_Fov", fieldOfView);
            Graphics.Blit(src, dest, fisheyeMaterial);
        }
        else
        {
            // シェーダが設定されていない場合はそのまま描画
            Graphics.Blit(src, dest);
        }
    }

    void Update()
    {
        // 視野角をスライダーなどで調整できるように
        if (fisheyeMaterial != null)
        {
            fisheyeMaterial.SetFloat("_Fov", fieldOfView);
        }
    }
}
