using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Under_Beam_expand : MonoBehaviour
{
    public float expandSpeed = 5f;
    public float maxRadius = 15f;
    public float damage = 10f;

    private float currentRadius = 0.5f;

    void Update()
    {
        currentRadius += expandSpeed * Time.deltaTime;

        //XZ‚¾‚¯Šg‘å
        transform.localScale = new Vector3(
            currentRadius,
            0.2f,
            currentRadius
        );

        //Å‘å‚Ü‚Ås‚Á‚½‚çÁ‚·
        if (currentRadius >= maxRadius)
        {
            Destroy(gameObject);
        }
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
