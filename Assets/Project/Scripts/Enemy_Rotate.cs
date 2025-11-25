using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rotate : MonoBehaviour
{
    //回転スピード
    public float rot = 2.0f;

    public GameObject target;
    private EnemySearch search;

    void Start()
    {
        target = GameObject.Find("Player");
        search = GetComponent<EnemySearch>();
    }

    void Update()
    {
        EnemyRotate();
    }

    void EnemyRotate()
    {
        if (search.IsInView())
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            dir.y = 0;

            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rot * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, rot, 0);
        }
    }
}