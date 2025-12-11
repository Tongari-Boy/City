using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public float chaseSpeed = 1f;

    private EnemySearch search;
    private Enemy_Patrol patrol;
    public NavMeshAgent agent;
    private Animator animator;
    private EnemyAttack attack;

    void Start()
    {
        search = GetComponent<EnemySearch>();
        patrol = GetComponent<Enemy_Patrol>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        attack = GetComponent<EnemyAttack>();
    }

    void Update()
    {
        if (agent == null || !agent.isOnNavMesh || search == null || search.target == null) 
            return;

        if (attack != null && attack.IsAttacking)
        {
            StopChase();
            animator.SetFloat("moveSpeed", 0f);
            return;
        }

        if (search.IsInView())
        {
            ChaseTarget();
        }
        else
        {
            StopChase();
        }

        // 移動速度をAnimatorに送る
        animator.SetFloat("moveSpeed", agent.velocity.magnitude);
    }

    public void ChaseTarget()
    {
        // パトロールを停止
        if (patrol != null)
        {
            patrol.StopPatrol();
        }

        agent.speed = chaseSpeed;
        agent.isStopped = false;

        //パスがないとき、止まりすぎたときに更新
        if(!agent.hasPath || agent.remainingDistance > agent.stoppingDistance)
        {
            agent.SetDestination(search.target.position);
        }
        
        // チェイスアニメーションを有効にする
        animator.SetBool("isChasing", true);
    }

    public void StopChase()
    {
        //常に止める
        agent.isStopped = true;
        animator.SetBool("isChasing", false);
    }
}