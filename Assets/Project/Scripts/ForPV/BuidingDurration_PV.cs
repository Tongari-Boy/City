using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuidingDurration_PV : MonoBehaviour
{
    Rigidbody rb;

    public Vector3 direction = Vector3.forward; //“|‚ê‚é•ûŒü
    public float force = 500f; //“|‚ê‚é—Í
    public float forceHeight = 200f; //“|‚ê‚é—Í‚Ì‚‚³(dS)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void BreakBuilding()
    {
        //Œš•¨‚ª“|‚ê‚éˆ—
        Debug.Log("‚¦");

        Vector3 forcePoint =
            transform.position + Vector3.up * forceHeight;

        rb.AddForceAtPosition(
            direction.normalized * force,
            forcePoint,
            ForceMode.Impulse
        );
    }
}
