using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolveItem : MonoBehaviour
{
    public float healthResolve = 10f;
    public float rot = 2.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatus status = other.GetComponent<PlayerStatus>();
            if (status != null)
            {
                status.Resolve(healthResolve);
            }
        }

        Destroy(gameObject);
    }
}