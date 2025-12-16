using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Break : MonoBehaviour
{
    private EnemySearch search;
    Rigidbody rb;

    public Vector3 direction = Vector3.forward; //倒れる方向
    public float force = 500f; //倒れる力
    public float forceHeight = 200f; //倒れる力の高さ(重心)

    public float damage = 100f;
    private bool isBroken = false;

    void Start()
    {
        search = GetComponent<EnemySearch>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (search.IsInView() && !isBroken)
        {
            BreakBuilding();
            isBroken = true;
        }
    }

    void BreakBuilding()
    {
        //建物が倒れる処理
        Vector3 forcePoint = transform.position + Vector3.up * forceHeight;
        rb.AddForceAtPosition(direction.normalized * force, forcePoint);
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
