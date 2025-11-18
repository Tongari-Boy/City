using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDash : MonoBehaviour
{
    [Header("ダッシュ設定")]
    public float dashForce = 10f;
    public float dashStaminaCostPerSec = 20f; //毎秒消費するスタミナ
    public float dashStartCost = 10f;         //押した瞬間の初期消費(再びダッシュするときの判定に利用)

    [Header("スタミナ設定")]
    public float dashStaminaCost = 20f;

    private Animator animator;
    private Rigidbody rb;
    private PlayerStatus status;

    private bool dashPressed;
    private bool dashReleased;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        status = GetComponent<PlayerStatus>();
    }

    void Update()
    {
        dashPressed = Input.GetKeyDown(KeyCode.LeftShift);
        dashReleased = Input.GetKeyUp(KeyCode.LeftShift);

        //ダッシュ開始
        if (dashPressed)
        {
            TryStartDash();
        }

        //ダッシュ終了
        if (dashReleased)
        {
            StopDash();
        }
    }

    void FixedUpdate()
    {
            //ダッシュ中の処理
            if (status.isDashing)
        {
            DashUpdate();
        }
    }

    void TryStartDash()
    {
        //ジャンプ中はダッシュしない
        if(!status.isGrounded)
            return;

        //移動入力が無いならダッシュ開始しない
        if (rb.velocity.magnitude < 0.1f)
            return;

        //スタミナが足りているか
        if (status.currentStamina >= dashStartCost)
        {
            status.currentStamina -= dashStartCost;

            status.isDashing = true;

            if (animator != null)
            {
                animator.SetBool("IsDashing", true);
            }
        }
    }

    void StopDash()
    {
        status.isDashing = false;

        if (animator != null)
        {
            animator.SetBool("IsDashing", false);
        }
    }

    void DashUpdate()
    {
        //移動入力がなくなったらダッシュ終了
        if (!status.isGrounded)
        {
            StopDash();
            return;
        }

        //ダッシュ方向に速度を設定
        Vector3 dashVel = transform.forward * dashForce;
        dashVel.y = rb.velocity.y;
        rb.velocity = dashVel;

        //スタミナを消費
        float staminaCost = dashStaminaCostPerSec * Time.deltaTime;
        status.currentStamina -= staminaCost;

        //スタミナが尽きたらダッシュ終了
        if (status.currentStamina <= 0f)
        {
            status.currentStamina = 0f;
            StopDash();
        }
    }
}