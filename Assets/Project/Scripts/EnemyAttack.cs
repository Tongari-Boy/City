using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    public float damange = 10f;
    public float attackInterval = 1f;

    private EnemyChase chase;
    private Animator animator;
    private float lastAttackTime = 0f;

    public float attackDuration = 1f;   //攻撃アニメーションの長さ
    public float attackingDuration = 1f;    //攻撃が当たったと判定される時間


    void Start()
    {
        chase = GetComponent<EnemyChase>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        JudgeAttack();
    }

    void JudgeAttack()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chase.agent.stoppingDistance &&
            Time.time >= lastAttackTime + attackInterval)
        {
            StartAttack();
        }       
    }

    public void StartAttack()
    {
        lastAttackTime = Time.time;

        chase.StopChase();

        //アニメーション
        animator.SetBool("isAttacking",true);

        StartCoroutine(AttackingCoroutine());

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chase.agent.stoppingDistance)
        {
            //ダメージ処理
            PlayerStatus status = player.GetComponent<PlayerStatus>();
            if (status != null)
            {
                status.TakeDamage(damange);
            }
        }

        StartCoroutine(AttackCoroutine());
    }

    public void StopAttack()
    { 
        animator.SetBool("isAttacking", false);
    }

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(attackDuration);

        StopAttack();
    }

    IEnumerator AttackingCoroutine()
    {
        yield return new WaitForSeconds(attackingDuration);
    }
}