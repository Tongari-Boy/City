using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{
    public Transform target; // 追従するキャラ
    // 画面に設定したい位置(UI座標オフセット)
    public Vector3 offset = new Vector3(80f, 120f, 0f);

    private RectTransform rectTransform;
    private Canvas canvas;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    void LateUpdate()
    {
        if (target == null || canvas == null) return;

        //キャラのワールド位置→画面座標へ
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);

        //画面座標→UIキャンバス内ローカル座標
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            canvas.worldCamera,
            out localPoint);

        //UIのローカル座標を更新
        rectTransform.localPosition = localPoint + (Vector2)offset;
    }
}
