using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shot : MonoBehaviour
{
    private PlayerStatus status;
    public GameObject shot;

    void Start()
    {
        status = GameObject.Find("Player").GetComponent<PlayerStatus>();
    }

    void Update()
    {
        Shot();
    }

    void Shot()
    {
        Instantiate(shot, transform.position, transform.rotation);
    }
}
