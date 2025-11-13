using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerCrouch : MonoBehaviour
{
    [Header("設定項目")]
    public Animator animator;
    public float standingHeight = 1.2f;
    public float crouchingHeight = 1.0f;
    public float crouchSpeedMultiplier = 0.5f;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public float transitionSpeed = 8f; // スムーズな高さ補間

    [Header("参照")]
    public PlayerMovement movementScript; // moveSpeedを参照

    private Rigidbody rb;
    private CapsuleCollider capsule;
    private bool isCrouching = false;
    private float targetHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();

        if (movementScript == null)
            movementScript = GetComponent<PlayerMovement>();

        targetHeight = standingHeight;
    }

    void Update()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            ToggleCrouch();
        }

        // スムーズに高さを補間
        float newHeight = Mathf.Lerp(capsule.height, targetHeight, Time.deltaTime * transitionSpeed);
        capsule.height = newHeight;

        // 中心位置も調整（しゃがむとき沈む感じ）
        capsule.center = new Vector3(0, newHeight / 2f, 0);
    }

    void ToggleCrouch()
    {
        isCrouching = !isCrouching;

        targetHeight = isCrouching ? crouchingHeight : standingHeight;

        // アニメーション切り替え
        if (animator != null)
            animator.SetBool("isCrouching", isCrouching);

        // 移動速度変更
        if (movementScript != null)
        {
            if (isCrouching)
                movementScript.moveSpeed *= crouchSpeedMultiplier;
            else
                movementScript.moveSpeed /= crouchSpeedMultiplier;
        }
    }
}