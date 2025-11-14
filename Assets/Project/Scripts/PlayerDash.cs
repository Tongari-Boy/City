using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDash : MonoBehaviour
{
    [Header("ダッシュ設定")]
    public float dashForce = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;


    private Animator animator;
    private Rigidbody rb;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float cooldownTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldownTimer <= 0 && !isDashing)
        {
            StartCoroutine(DoDash());
        }
    }

    private System.Collections.IEnumerator DoDash()
    {
        Debug.Log("Dash!");
        
        isDashing = true;
        cooldownTimer = dashCooldown;

        Vector3 dashDirection = transform.forward;
        float timer = 0f;

        while (timer < dashDuration)
        {
            rb.velocity = dashDirection * dashForce;
            timer += Time.deltaTime;
            yield return null;
        }

        // ダッシュアニメーションを再生
        if (animator != null)
        {
            animator.SetTrigger("DashTrigger");
        }

        isDashing = false;
    }
}