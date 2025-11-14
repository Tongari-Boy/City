using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("ステータス設定")]
    //体力
    public float maxHealth = 100f;
    //スタミナ
    public float maxStamina = 100f;
    //現在の体力
    public float currentHealth;
    //現在のスタミナ
    public float currentStamina;


    void Start()
    {
        maxHealth = 100f;
        maxStamina = 100f;
    }

    void Update()
    {
        
    }
}
