using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    public float viewAngle = 60f; // 視野角
    public float viewDistance = 10f; // 視界距離
    public Transform target; // ターゲット
    public LayerMask obstacleMask; // 障害物(建物、壁とか)レイヤー

    void Update()
    {
        if (IsInView())
        {
            Debug.Log("発見");
        }
    }

    //敵視野にターゲットがいるか 判定
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
        if(angle > viewAngle * 0.5f)
            return false;

        //レイキャストで障害物チェック
        if (Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
            return false;

        return true;
    }

    //デバック用視界
    void OnDrawGizmosSelected()
    {
        // 視界距離
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        // 視野角の線
        Vector3 left = Quaternion.Euler(0, -viewAngle * 0.5f, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0, viewAngle * 0.5f, 0) * transform.forward;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + left * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + right * viewDistance);
    }
}