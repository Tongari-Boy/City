using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_PV : MonoBehaviour
{
    public float breakRange = 2f;   //UŒ‚‚Ì“Í‚­”ÍˆÍ

    public float attackInterval = 5f;
    public float attackWindup = 1f;
    public float attackDuration = 3f;

    public Animator animator;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {

        //UŒ‚ƒAƒjƒ[ƒVƒ‡ƒ“
        animator.SetTrigger("Attack");

        //UŒ‚—­‚ß
        yield return new WaitForSeconds(attackWindup);

        //Œš•¨”j‰ó
        ApplyDamageToBuildings();

        //UŒ‚ƒ‚[ƒVƒ‡ƒ“c‚èŠÔ
        yield return new WaitForSeconds(Mathf.Max(0f, attackDuration - attackWindup));
    }

    void ApplyDamageToBuildings()
    {   
        Debug.Log("‚ ");

        //”ÍˆÍ“à‚Ì Collider ‚ğ‚·‚×‚Äæ“¾
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            breakRange
        );

        Debug.Log("‚¢");

        foreach (Collider hit in hits)
        {
            BuidingDurration_PV building =
                hit.GetComponent<BuidingDurration_PV>();

            Debug.Log("‚¤");

            if (building != null)
            {
                building.BreakBuilding();
            }
        }
    }
}
