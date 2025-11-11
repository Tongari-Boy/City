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

        // 入力を取得

        float horizontal = Input.GetAxis("Horizontal"); // A, Dキー または ← →

        float vertical = Input.GetAxis("Vertical");     // W, Sキー または ↑ ↓

        // カメラ方向を基準にした移動方向

        Vector3 cameraForward = Camera.main.transform.forward;

        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;

        cameraRight.y = 0;

        cameraForward.Normalize();

        cameraRight.Normalize();

        moveDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        // アニメーション制御（Speed値を更新）

        float speed = moveDirection.magnitude * moveSpeed;

        if (animator != null)

            animator.SetFloat("Speed", speed);

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
