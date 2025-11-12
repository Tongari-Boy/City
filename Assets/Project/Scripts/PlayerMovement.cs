using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour

{

    [Header("移動設定")]
    public float moveSpeed = 5f;     // 通常の移動速度
    public float rotationSpeed = 10f; // 回転の速さ

    private Rigidbody rb;
    private Vector3 moveDirection;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()

    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // カメラ方向を基準にした移動方向
        Transform cam = Camera.main.transform;
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * vertical + right * horizontal).normalized;

        // アニメーション制御（Speed値を更新)
        float speed = moveDirection.magnitude * moveSpeed;
        if (animator != null)
            animator.SetFloat("Speed", speed);

        //シフトキーでダッシュ
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection *= 2; // ダッシュ時に速度を2倍に
            if (animator != null)
                animator.SetBool("IsRunning", true);
        }
        else
        {
            if (animator != null)
                animator.SetBool("IsRunning", false);
        }
    }

    void FixedUpdate()
    {
        // 実際の移動処理
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = rb.velocity.y; // ジャンプ中の縦方向速度を保持
        rb.velocity = velocity;

        // 移動方向を向く
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
