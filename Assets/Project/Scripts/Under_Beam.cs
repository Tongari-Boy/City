using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Under_Beam : MonoBehaviour
{
    public GameObject under_Beam;
    public float attackInterval = 2.0f;

    private EnemySearch search;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        search = GetComponent<EnemySearch>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!search.IsInView())
        {
            return;
        }

        timer += Time.deltaTime;
        if(timer >= attackInterval)
        {
            timer = 0f;
            FireRing();
        }
    }

    void FireRing()
    {
        Vector3 pos = transform.position;
        Instantiate(under_Beam,pos,Quaternion.identity);
    }
}
