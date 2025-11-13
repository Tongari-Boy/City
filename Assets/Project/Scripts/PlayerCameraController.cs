using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("ターゲット")]
    public Transform target;  // プレイヤー

    [Header("カメラ設定")]
    public float distance = 5f;          // プレイヤーとの距離
    public float height = 2f;            // カメラの高さ
    public float followSmoothTime = 0.1f; // 追従スムーズ時間
    public float rotationSpeed = 3f;     // 回転速度

    private Vector3 currentVelocity;
    private float yaw;
    private float pitch;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("PlayerCameraController: ターゲットが設定されていません！");
            enabled = false;
            return;
        }

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        //カメラ回転処理（マウス操作）
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yaw += mouseX * rotationSpeed;
        pitch -= mouseY * rotationSpeed;
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        //プレイヤーを追従する位置（スムーズ補間）
        Vector3 targetPosition = target.position + Vector3.up * height - rotation * Vector3.forward * distance;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, followSmoothTime);

        transform.rotation = rotation;
    }
}