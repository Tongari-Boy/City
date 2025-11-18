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

    private Vector3 moveInput;     //入力された移動方向
    private Vector3 moveDirection; //実際の移動方向

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cam = Camera.main.transform;
    }

    //マウス入力受付
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(h, 0, v).normalized;

        //カメラ基準の移動方向
        Vector3 camForward = cam.forward; camForward.y = 0; camForward.Normalize();
        Vector3 camRight = cam.right; camRight.y = 0; camRight.Normalize();

        moveDirection = camForward * moveInput.z + camRight * moveInput.x;
    }

    //プレイヤの回転と移動
    void FixedUpdate()
    {
        //ダッシュがないときだけ移動処理
        if (!GetComponent<PlayerStatus>().isDashing)
        {
            Vector3 vel = moveDirection * moveSpeed;
            vel.y = rb.velocity.y;
            rb.velocity = vel;
        }


        //移動方向がある時だけキャラを回転 
        if (moveDirection.sqrMagnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Lerp(rb.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);
        }

        //アニメーション制御
        if (animator != null)
            animator.SetFloat("Speed", moveInput.magnitude * moveSpeed);
    }
}