using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float idleAlignSpeed = 2f;

    private Rigidbody rb;
    private Animator animator;
    private Transform cam;
    private Vector3 moveInput;     // 入力された移動方向
    private Vector3 moveDirection; // 実際の移動方向

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        moveInput = new Vector3(h, 0, v);
        moveInput = Vector3.ClampMagnitude(moveInput, 1f);

        // カメラ基準の移動方向 ★
        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        moveDirection = camForward * moveInput.z + camRight * moveInput.x;
        moveDirection.Normalize();

        // Rigidbody 移動
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;

        // 移動方向がある時だけキャラを回転させる（TPSらしい挙動）★
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // アニメーション制御
        float speed = moveInput.magnitude * moveSpeed;
        if (animator != null)
            animator.SetFloat("Speed", speed);
    }
}