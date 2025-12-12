using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaItem : MonoBehaviour
{
    public float staminaResolve = 10f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatus status = other.GetComponent<PlayerStatus>();
            if (status != null)
            {
                status.currentStamina += staminaResolve;
            }
        }

        Destroy(gameObject);
    }
}
