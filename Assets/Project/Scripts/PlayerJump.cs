using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour
{
    [Header("ジャンプ設定")]
    public float jumpForce = 5f; // ジャンプの強さ
    public LayerMask groundLayer; // 地面レイヤー
    public float groundCheckDistance = 0.2f; // 地面チェックの距離

    private Rigidbody rb;
    private bool isGrounded;
    private Animator animator = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckGrounded();

        // Animatorにパラメータを送る
        float speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        animator.SetFloat("Speed", speed);
        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded)
        {
            Debug.Log("isGrounded");
        }

            // スペースキーでジャンプ
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Space Pressed!");
            Jump();
        }
    }

    void CheckGrounded()
    {
        // プレイヤーの足元から下方向にRayを飛ばして接地判定
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f, groundLayer);
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // ジャンプトリガーをセット
        animator.SetTrigger("JumpTrigger");
    }

    // デバッグ用にRayをSceneビューに表示
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (groundCheckDistance + 0.1f));
    }
}