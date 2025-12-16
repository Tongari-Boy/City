using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public Light sun;
    public float dayLength = 120f;//一日の長さ

    void Update()
    {
        float t = (Time.time / dayLength) % 1f;

        // 太陽の回転
        float angle = Mathf.Lerp(-90f, 270f, t);
        sun.transform.rotation = Quaternion.Euler(angle, 170f, 0f);

        // 強度（最低0.3を保証）
        float intensity = Mathf.Cos(t * Mathf.PI * 2f) * 0.5f + 0.7f;
        sun.intensity = Mathf.Clamp(intensity, 0.3f, 1.2f);

        // 色変化（朝夕を暖色に）
        Color dayColor = Color.white;
        Color sunsetColor = new Color(1f, 0.6f, 0.4f);
        float sunset = Mathf.Abs(t - 0.5f) * 2f;
        sun.color = Color.Lerp(sunsetColor, dayColor, sunset);
    }
}
