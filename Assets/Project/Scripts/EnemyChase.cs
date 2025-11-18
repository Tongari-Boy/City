using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public GameObject target;

    void Start()
    {
        target = GameObject.Find("Target");
    }

    void Update()
    {
        transform.LookAt(target.transform);
    }
}
