using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaMaterialChanger : MonoBehaviour
{
    public string[] targetTags; // 必須タグのリスト
    public Material newMaterial; // 条件を満たした場合のマテリアル
    private Material originalMaterial; // 元のマテリアル
    private Renderer areaRenderer; // エリアオブジェクトのRenderer
    private Dictionary<string, bool> tagPresence; // タグごとのエリア内存在フラグ

    private void Start()
    {
        // エリアオブジェクトのRendererを取得し、元のマテリアルを保存
        areaRenderer = GetComponent<Renderer>();
        if (areaRenderer != null)
        {
            originalMaterial = areaRenderer.material;
        }
        else
        {
            Debug.LogError("Rendererがこのオブジェクトに存在しません。");
        }

        // タグごとの存在フラグを初期化
        tagPresence = new Dictionary<string, bool>();
        foreach (string tag in targetTags)
        {
            tagPresence[tag] = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // エリア内に入ったオブジェクトのタグを確認
        if (tagPresence.ContainsKey(other.tag))
        {
            tagPresence[other.tag] = true; // 該当タグのオブジェクトがエリア内に存在
            UpdateMaterial();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // エリア外に出たオブジェクトのタグを確認
        if (tagPresence.ContainsKey(other.tag))
        {
            tagPresence[other.tag] = false; // 該当タグのオブジェクトがエリア外に移動
            UpdateMaterial();
        }
    }

    private void UpdateMaterial()
    {
        // すべてのタグがエリア内に存在するか確認
        foreach (string tag in targetTags)
        {
            if (!tagPresence[tag])
            {
                // 条件未達成の場合、元のマテリアルに戻す
                areaRenderer.material = originalMaterial;
                return;
            }
        }

        // 条件を満たした場合、新しいマテリアルを適用
        areaRenderer.material = newMaterial;
    }
}
