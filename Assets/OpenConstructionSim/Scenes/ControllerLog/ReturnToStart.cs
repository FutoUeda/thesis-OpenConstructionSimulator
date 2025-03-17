using UnityEngine;

public class ReturnToStart : MonoBehaviour
{
    [SerializeField] private Collider startPointCollider; // 出発地点のCollider（エディタで指定）
    [SerializeField] private GameObject targetObject;     // ショベルカーなどの対象オブジェクト（エディタで指定）

    private bool hasExited = false;  // 出発地点を出たかどうかを記録するフラグ
    private bool hasStarted = false; // 対象オブジェクトが初期位置に存在していたかを記録するフラグ

    private void Start()
    {
        // 必要なコンポーネントが設定されているか確認
        if (startPointCollider == null)
        {
            Debug.LogError("出発地点のColliderが設定されていません。Inspectorで指定してください。");
        }

        if (targetObject == null)
        {
            Debug.LogError("対象オブジェクトが設定されていません。Inspectorで指定してください。");
        }
    }

    private void Update()
    {
        if (startPointCollider == null || targetObject == null)
            return;

        // 対象オブジェクトが出発地点のCollider内に存在するか確認
        if (startPointCollider.bounds.Contains(targetObject.transform.position))
        {
            if (!hasStarted)
            {
                // 初期位置に対象オブジェクトが存在する場合
                Debug.Log("対象オブジェクトが初期位置に存在しています。ゲーム開始を記録します。");
                hasStarted = true;
            }
            else if (hasExited)
            {
                // すでに出発して戻ってきた場合
                Debug.Log("対象オブジェクトが帰着しました。ゲームを終了します。");
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Unityエディタ用
#endif
            }
        }
        else if (hasStarted)
        {
            // 対象オブジェクトが出発地点を離れた場合
            hasExited = true;
            Debug.Log("対象オブジェクトが出発地点を出発しました。");
        }
    }
}
