using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("ターゲット設定")]
    public Transform target; // 追従対象（プレイヤー）
    public Vector3 offset = new Vector3(0, 2f, -4f); // プレイヤーからの距離

    [Header("回転設定")]
    public float mouseSensitivity = 2f;
    public float minPitch = -30f;
    public float maxPitch = 60f;

    private float yaw = 0f;   // 水平方向の回転
    private float pitch = 0f; // 垂直方向の回転

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        //マウスカーソルの固定と非表示
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if(!target) return;

        //入力取得
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Debug.Log($"Mouse Input: X={mouseX}, Y={mouseY}");

        //回転更新
        yaw += mouseX * mouseSensitivity;
        pitch -= mouseY * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        //カメラの回転
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        //位置をターゲットの後方に設定
        Vector3 desiredPosition = target.position + rotation * offset;
        transform.position = desiredPosition;
        transform.LookAt(target);
    }
}
