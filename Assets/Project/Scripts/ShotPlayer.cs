using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //出現後一定時間で自動的弾を消す
        Destroy(gameObject, 2.0F);
    }

    // Update is called once per frame
    void Update()
    {
        //弾の前進
        transform.position += transform.forward * Time.deltaTime * 100;
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.name == "Terrain")
        {
            //地形(Terrain)と衝突したら弾を消す
             Destroy(gameObject);
        }
    }
}
