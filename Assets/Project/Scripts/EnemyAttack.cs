using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    public float damange = 10f;
    public float attackInterval = 1f;

    public float attackWindowup = 0.2f;
    public float attackHitTime = 0.35f;
    public float attackDuration = 1f;

    public bool IsAttacking { get;private set; }

    private EnemyChase chase;
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent agent;
    private float lastAttackTime = 0f;

    void Start()
    {
        chase = GetComponent<EnemyChase>();
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        JudgeAttack();
    }

    void JudgeAttack()
    {
        if(IsAttacking || player == null || chase == null || agent == null)
            { return; }

        float distance = Vector3.Distance(transform.position, player.position);
        bool canAttack = distance <= this.agent.stoppingDistance && Time.time >= lastAttackTime + attackInterval;



        if (canAttack)
        {
            StartCoroutine(PerformAttack());
        }       
    }

    IEnumerator PerformAttack()
    {
        IsAttacking = true;
        lastAttackTime = Time.time;

        //�ǐՂ��~�߁ANavMesh�̎�����]���~�߂�
        if (agent != null)
        {
            agent.isStopped = true;
            agent.updateRotation = false;
        }

        //����̕���������
        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0f;
        if (lookDir.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(lookDir);
        }

        //�`�F�C�X�𖳌����A�U���J�n
        animator.SetBool("isChasing", false);
        animator.SetTrigger("Attack");

        //�\������
        yield return new WaitForSeconds(attackWindowup);

        //�q�b�g�A�j���[�V����
        ApplyDamageIfStillIRange();

        //�q�b�g��`�U���I���܂ő҂�
        float remainingAnim = Mathf.Max(0f, attackDuration - attackHitTime);
        yield return new WaitForSeconds(remainingAnim);

        //�㏈��
        IsAttacking = false;

        if (agent != null)
        {
            agent.updateRotation = true;
            agent.isStopped = false;
        }

        //�U����A���E�ɂ���ΒǐՂ��ĊJ
        if (chase != null && player != null)
        {
            chase.ChaseTarget();
        }
    }

    void ApplyDamageIfStillIRange()
    {
        if(player == null || agent == null)return;

        float distance = Vector3.Distance(transform.position, player.position);
        //�����]�T������
        if(distance <= agent.stoppingDistance + 0.1f)
        {
            var status = player.GetComponent<PlayerStatus>();
            if (status != null)
            {
                status.TakeDamage(damange);
            }
        }
    }
}