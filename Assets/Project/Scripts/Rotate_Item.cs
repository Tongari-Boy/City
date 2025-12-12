using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Item : MonoBehaviour
{
    public float rot = 2.0f;

    void Update()
    {
        RotateItem();
    }

    void RotateItem()
    {
        transform.Rotate(0, rot, 0);
    }
}
