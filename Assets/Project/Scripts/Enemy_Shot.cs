using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shot : MonoBehaviour
{
    public GameObject shot;
    public Transform muzzle;
    public float shotInterval = 1f;

    private float timer = 0f;
    private EnemySearch search;

    void Start()
    {
        search = GetComponent<EnemySearch>();
    }

    void Update()
    {
        Shot();
    }

    void Shot()
    {
        if (search.IsInView())
        {
            timer += Time.deltaTime;

            if (timer >= shotInterval)
            {
                Instantiate(shot, muzzle.position, muzzle.rotation);
                timer = 0f;
            }
        }            
    }
}
