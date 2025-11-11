using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //プレイヤーの回転
        transform.Rotate(0, Input.GetAxis("Horizontal2"), 0);

        //カメラの親のオブジェクトの回転
        GameObject CameraParent = Camera.main.transform.parent.gameObject;
        CameraParent.transform.Rotate(Input.GetAxis("Vertical2"), 0, 0);
        
    }
}
