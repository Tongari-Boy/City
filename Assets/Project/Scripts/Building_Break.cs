using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Break : MonoBehaviour
{
    private EnemySearch search;
    public float damage = 100f;

    void Start()
    {
        search = GetComponent<EnemySearch>();
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (search.IsInView())
        {
            BreakBuilding();
        }
    }

    void BreakBuilding()
    {
        //建物が倒れる処理


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatus status = other.GetComponent<PlayerStatus>();
            if (status != null)
            {
                status.TakeDamage(damage);
            }
        }
    }
}
