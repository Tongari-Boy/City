using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{
    public Transform target; // 追従するキャラ
    // 画面に設定したい位置（UI座標オフセット）
    // 直す方法にかける時間が無いから、とりあえずベタ書きで対応
    public Vector3 offset = new Vector3(80f, 120f, 0f);

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        if (target == null) return;

        // キャラのワールド位置 → 画面座標へ
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);

        // UI をその位置へ移動
        rectTransform.position = screenPos + offset;
    }
}
