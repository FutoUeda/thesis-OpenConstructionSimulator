using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCounter : MonoBehaviour
{
    public string[] objectTags; // カウント対象のタグの配列
    public Text uiText; // UIに表示するテキスト
    public Collider areaCollider; // エリアを表すCollider

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    // UIを更新するメソッド
    void UpdateUI()
    {
        if (uiText != null)
        {
            string displayText = "オブジェクトの個数:\n";
            foreach (string tag in objectTags)
            {
                int count = CountObjectsInArea(tag, areaCollider);
                displayText += $"{tag}: {count}\n";
            }
            uiText.text = displayText;
        }
        else
        {
            Debug.LogWarning("UI Text コンポーネントが設定されていません。");
        }
    }

    // 指定されたタグのオブジェクトの個数をエリア内で計算するメソッド
    int CountObjectsInArea(string tag, Collider area)
    {
        if (area == null)
        {
            Debug.LogWarning("エリアColliderが設定されていません。");
            return 0;
        }

        // エリアのBoundsを取得
        Bounds bounds = area.bounds;

        // 指定されたタグを持つ全オブジェクトを取得
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        int count = 0;

        // 各オブジェクトがエリア内にあるかをチェック
        foreach (GameObject obj in objects)
        {
            if (bounds.Contains(obj.transform.position))
            {
                count++;
            }
        }

        // 個数を返す
        return count;
    }
}
