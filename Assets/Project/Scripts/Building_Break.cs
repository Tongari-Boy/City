using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Break : MonoBehaviour
{
    public float viewAngle = 60f; // 視野角
    public float viewDistance = 10f; // 視界距離
    public Transform target; // ターゲット
    public LayerMask obstacleMask; // 障害物(建物、壁とか)レイヤー

    void Update()
    {

    }

    public bool IsInView()
    {
        //ターゲットまでの方向と距離
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        //距離判定
        if (distanceToTarget > viewDistance)
            return false;

        //視野角判定
        float angle = Vector3.Angle(transform.forward, dirToTarget);
        if (angle > viewAngle * 0.5f)
            return false;

        //レイキャストで障害物チェック
        if (Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
            return false;

        return true;
    }

}
