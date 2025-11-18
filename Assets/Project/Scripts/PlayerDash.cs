using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDash : MonoBehaviour
{
    [Header("ダッシュ設定")]
    public float dashForce = 10f;
    public float dashStaminaCostPerSec = 20f; //毎秒消費するスタミナ
    public float dashStartCost = 10f;         //押した瞬間の初期消費（任意）

    [Header("スタミナ設定")]
    public float dashStaminaCost = 20f;

    private Animator animator;
    private Rigidbody rb;
    private PlayerStatus status;

    private bool isDashing = false;
    private float cooldownTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        status = GetComponent<PlayerStatus>();
    }

    void Update()
    {
        //Shiftキーでダッシュ開始
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            TryStartDash();
        }

        //Shiftキー離したらダッシュ終了
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopDash();
        }

        //ダッシュ中の処理
        if (isDashing)
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

        //スタミナが足りているか確認
        if (status.currentStamina >= dashStartCost)
        {
            status.currentStamina -= dashStartCost;

            isDashing = true;
            status.isDashing = true;

            if (animator != null)
            {
                animator.SetBool("IsDashing", true);
            }
        }
    }

    void StopDash()
    {
        isDashing = false;
        status.isDashing = false;
        rb.velocity = Vector3.zero;

        if (animator != null)
        {
            animator.SetBool("IsDashing", false);
        }
    }

    void DashUpdate()
    {
        //移動入力がなくなったらダッシュ終了
        if (rb.velocity.magnitude < 0.1f)
        {
            StopDash();
            return;
        }

        //前方にダッシュ力を加える
        rb.velocity = transform.forward * dashForce;

        //スタミナを消費
        float staminaCost = dashStaminaCostPerSec * Time.deltaTime;
        status.currentStamina -= staminaCost;

        //ダッシュ中にジャンプしたとき
        if (!status.isGrounded)
        {
            StopDash();
            return;
        }

        //スタミナが尽きたらダッシュ終了
        if (status.currentStamina <= 0f)
        {
            status.currentStamina = 0f;
            StopDash();
        }
    }
}