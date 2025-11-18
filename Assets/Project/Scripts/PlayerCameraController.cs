using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("ターゲット")]
    public Transform target;  // プレイヤー

    [Header("カメラ設定")]
    public float distance = 5f;             // プレイヤーとの距離
    public float height = 2f;               // カメラの高さ
    public float rotationSpeed = 3f;        // 回転速度

    [Header("遮蔽処理")]
    public float collisionRadius = 0.2f;    // カメラの当たり判定
    public LayerMask collisionMask;         // 地面・壁レイヤー

    private float yaw;
    private float pitch;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //カメラ回転処理（マウス操作）
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yaw += mouseX * rotationSpeed;
        pitch -= mouseY * rotationSpeed;
        pitch = Mathf.Clamp(pitch, -30f, 60f);
    }

    void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        //理想のカメラ位置を計算
        Vector3 targetBase = target.position + Vector3.up * height;
        Vector3 desiredPosition = targetBase - rotation * Vector3.forward * distance;

        //プレイヤ→理想位置へSphereCast(カメラ衝突)
        if (Physics.SphereCast(
                targetBase,
                collisionRadius,
                (desiredPosition - targetBase).normalized,
                out RaycastHit hit,
                distance,
                collisionMask))
        {
            //衝突地点の少し手前にカメラを寄せる
            desiredPosition = hit.point + hit.normal * collisionRadius;
        }

        //追従
        transform.position = desiredPosition;
        transform.rotation = rotation;
    }
}