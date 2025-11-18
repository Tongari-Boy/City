using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour
{
    [Header("ジャンプ設定")]
    public float jumpForce = 5f; // ジャンプの強さ
    public LayerMask groundLayer; // 地面レイヤー
    public float groundCheckDistance; // 地面チェックの距離

    [Header("地面チェック")]
    public float groundRayOffset;

    private Rigidbody rb;
    private Animator animator;
    private PlayerStatus status;

    private bool jumpPressed;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        status = GetComponent<PlayerStatus>();
    }

    void Update()
    {
        jumpPressed = Input.GetKeyDown(KeyCode.Space);

        CheckGrounded();

        // Animatorにパラメータを送る
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("Speed", new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude);

    }

    void FixedUpdate()
    {
        if (jumpPressed && isGrounded)
        {
            Jump();
        }
    }   

    void CheckGrounded()
    {
        Vector3 origin = transform.position + Vector3.up * groundRayOffset;

        isGrounded = Physics.Raycast(
            origin,
            Vector3.down,
            groundCheckDistance,
            groundLayer
        );

        status.isGrounded = isGrounded;
    }

    void Jump()
    {
        //ダッシュ中ならダッシュを解除
        status.isDashing = false;

        //Y速度初期化→ジャンプ力付与
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // ジャンプトリガーをセット
        animator.SetTrigger("JumpTrigger");
    }

    // デバッグ用にRayをSceneビューに表示
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 origin = transform.position + Vector3.up * groundRayOffset;

        Gizmos.DrawLine(
            origin,
            origin + Vector3.down * (groundCheckDistance + 0.2f)
        );
    }
}