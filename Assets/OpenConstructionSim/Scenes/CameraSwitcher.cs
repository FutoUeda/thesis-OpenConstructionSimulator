using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // 切り替えたいカメラの配列
    private int currentCameraIndex = 0;

    void Start()
    {
        // 初期設定で最初のカメラをアクティブにする
        SetCameraActive(currentCameraIndex);
    }

    void Update()
    {
        // Enterキーでカメラ切り替え
        if (Input.GetKeyDown(KeyCode.Return)) // Enterキーに変更
        {
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            SetCameraActive(currentCameraIndex);
        }
    }

    private void SetCameraActive(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == index);
        }
    }
}
