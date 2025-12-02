using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject shot;

    float shotInterval = 0;
    float shotIntervalMax = 0.25f;

    // Update is called once per frame
    void Update()
    {
        //”­ŽËŠÔŠu‚ðÝ’è
        shotInterval += Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            if (shotInterval > shotIntervalMax)
            {
                //’e‚Ì”­ŽË
                Instantiate(shot, transform.position, Camera.main.transform.rotation);
                shotInterval = 0;
            }
        }
    }
}
