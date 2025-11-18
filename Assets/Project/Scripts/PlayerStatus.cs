using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [Header("ステータス設定")]
    public float maxHealth = 100f;
    public float maxStamina = 100f;

    public float currentHealth;
    public float currentStamina;

    [Header("状態フラグ")]
    public bool isDashing = false;
    public bool isGrounded = false;

    [Header("スタミナ回復設定")]
    public float staminaRegenPerSec = 10f;

    [Header("UI")]
    public Image staminaCircle;
    public CanvasGroup staminaUIGroup;
    public float fadeDuration = 0.5f;    //フェードアウト時間
    public float hideDelay = 0.5f;       //満タン後に消すまでの時間

    private float hideTimer = 0f;

    void Start()
    {
        currentStamina = maxStamina;

        //UI初期化
        if (staminaCircle != null)
        {
            staminaCircle.fillAmount = 1f; // 最大値
        }

        // 最初は完全非表示
        if (staminaUIGroup != null)
        {
            staminaUIGroup.alpha = 0f;
        }
    }

    void Update()
    {
        //ダッシュ中は回復しない
        if (!isDashing)
        {
            currentStamina = Mathf.Min(
                maxStamina,
                currentStamina + staminaRegenPerSec * Time.deltaTime
             );
        }

        //UI更新
        if (staminaCircle != null)
        {
            staminaCircle.fillAmount = currentStamina / maxStamina;
        }

        //スタミナUI表示制御
        if (currentStamina < maxStamina)
        {
            ShowUI();
            hideTimer = 0f;        // ディレイ用タイマーをリセット
            return;
        }

        //満タン：フェードアウト開始タイマー
        hideTimer += Time.deltaTime;

        //ディレイ時間未満なら何もしない
        if (hideTimer < hideDelay)
            return;

        //ディレイが終わったのでフェードアウト開始
        FadeOutUI();
    }

    //UI表示
    void ShowUI()
    {
        if (staminaUIGroup != null)
        {
            staminaUIGroup.alpha = 1f;
        }
    }

    //フェードアウト処理
    void FadeOutUI()
    {
        if (staminaUIGroup == null)
            return;

        if (staminaUIGroup.alpha > 0f)
        {
            staminaUIGroup.alpha -= Time.deltaTime / fadeDuration;
        }
    }
}