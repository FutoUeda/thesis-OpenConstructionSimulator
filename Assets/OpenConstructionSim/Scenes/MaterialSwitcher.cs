using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitcher : MonoBehaviour
{
    public Material material1; // 範囲内にオブジェクトがいるときのマテリアル
    public Material material2; // 範囲内にオブジェクトがいないときのマテリアル
    public string targetTag = "TargetObject"; // 検知するタグ
    private Renderer planeRenderer; // 平面オブジェクトのRenderer
    private bool isObjectInRange = false; // オブジェクトが範囲内にいるかどうか

    void Start()
    {
        // 平面オブジェクトのRendererを取得
        planeRenderer = GetComponent<Renderer>();
        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        // オブジェクトの有無に応じてマテリアルを更新
        planeRenderer.material = isObjectInRange ? material1 : material2;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 指定したタグのオブジェクトが入った場合
        if (other.CompareTag(targetTag))
        {
            isObjectInRange = true;
            UpdateMaterial();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 指定したタグのオブジェクトが出た場合
        if (other.CompareTag(targetTag))
        {
            isObjectInRange = false;
            UpdateMaterial();
        }
    }
}
