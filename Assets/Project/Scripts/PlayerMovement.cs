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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //入力方向を「キャラのローカル空間」で扱う
        moveInput = new Vector3(horizontal, 0, vertical);
        moveInput = Vector3.ClampMagnitude(moveInput, 1f);

        // アニメーション制御
        float speed = moveInput.magnitude * moveSpeed;
        if (animator != null)
            animator.SetFloat("Speed", speed);
    }

    void FixedUpdate()
    {
        //カメラ基準ではなく、キャラの向きで移動
        moveDirection = transform.TransformDirection(moveInput);
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;

        if (moveInput != Vector3.zero)
        {
            //入力方向に回転
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            //停止中はゆっくりカメラ方向を向く
            Vector3 camForward = cam.forward;
            camForward.y = 0;
            if (camForward.sqrMagnitude > 0.01f)
            {
                Quaternion camRot = Quaternion.LookRotation(camForward);
                rb.rotation = Quaternion.Lerp(rb.rotation, camRot, idleAlignSpeed * Time.fixedDeltaTime);
            }
        }
    }
}