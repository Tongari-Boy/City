using UnityEngine;

public class LedgeGrab : MonoBehaviour
{
    [Header("”»’èİ’è")]
    public Transform ledgeCheckPoint;   //è‚ÌˆÊ’u
    public float ledgeCheckDistance = 0.5f;
    public float groundCheckDistance = 1.0f;
    public LayerMask whatIsGround;

    [Header("“®ìİ’è")]
    public float grabOffsetY = -0.2f; //ŠR‚Ìã’[‚É‡‚í‚¹‚é•â³
    public float climbUpTme = 0.5f;

    private Rigidbody rb;
    private Animator anim;
    private bool isGrabbing;
    private Vector3 ledgePosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbing)
        {
            HandleGrabInput();
            return;
        }

        DetectLedge();
    }

    void DetectLedge()
    {
        RaycastHit frontHit;
        bool wall = Physics.Raycast(ledgeCheckPoint.position, transform.forward, out frontHit, ledgeCheckDistance, whatIsGround);
        bool groundBelow = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, whatIsGround);

        //•Ç‚Í‚ ‚é‚ªA‰º‚É’n–Ê‚ª‚È‚¢¨ŠR‚Â‚©‚Ü‚èŒó•â
        if (wall && !groundBelow)
        {
            StartGrab(frontHit);
        }
    }

    void StartGrab(RaycastHit hit)
    {
        isGrabbing = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        //ŠRã’[‚ğ„‘ª‚µ‚ÄˆÊ’u‚ğŒÅ’è
        ledgePosition = hit.point + (-transform.forward * 0.3f) + (Vector3.up * grabOffsetY);
        transform.position = ledgePosition;

        anim.SetBool("isGrabbing", true);
    }

    void HandleGrabInput()
    {
        if(Input.GetKeyDown(KeyCode.W)) //“o‚é
        {
            anim.SetTrigger("Climb");
            Invoke(nameof(FinishClimb),climbUpTme);
        }
        else if(Input.GetKeyDown(KeyCode.S)) //—£‚·
        {
            ReleaseGrab();
        }
    }

    void FinishClimb()
    {
        anim.SetBool("isGrabbing", false);
        transform.position += Vector3.up * 1.2f; //ŠR‚Ìã‚ÉˆÚ“®
        rb.useGravity = true;
        isGrabbing = false;
    }

    void ReleaseGrab()
    {
        anim.SetBool("isGrabbing", false);
        rb.useGravity = true;
        isGrabbing = false;
    }
}
