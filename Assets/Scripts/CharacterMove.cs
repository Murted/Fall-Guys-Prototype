using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float platformThreshold;

    private Rigidbody rb;
    private Animator animator;
    private Vector3 movementInput;
    private bool isGrounded;
    private bool isFalling = false;
    private bool alreadyJumped = false;
    private Transform currentPlatform;
    private HitPlatform hit;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void KeyboardInput()
    {
        isGrounded = Physics.CheckSphere(transform.position + Vector3.down * 0.1f, 0.2f, groundLayer);

        if (isGrounded)
        {            
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            movementInput = new Vector3(horizontal, 0, vertical).normalized;

            if (Input.GetButtonDown("Jump"))
            {                
                Jump();                
            }
        }
    }

    public void MovePlayer()
    {
        if (!isFalling)
        {
            alreadyJumped = false;
            Vector3 moveVelocity = movementInput * moveSpeed;
            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

            if (movementInput != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementInput, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 0.1f);
            }
        }
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        alreadyJumped = true;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void UpdateAnimationState()
    {
        bool isMoving = movementInput.magnitude > 0;

        animator.SetBool("isGrounded", isGrounded);

        if (isMoving && isGrounded)
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("isIdle", false);
        }
        else if (!isMoving && isGrounded)
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isMoving", false);
        }
        else if (!isGrounded && rb.velocity.y < 0)
        {
            if (!alreadyJumped)
            {
                animator.SetTrigger("Jump");
            }
            animator.SetBool("isIdle", false);
            animator.SetBool("isMoving", false);
        }
    }

    public void CheckFallCondition()
    {
        if (currentPlatform != null && !isGrounded)
        {
            //var hit = currentPlatform.GetComponent<HitPlatform>;
            if(hit != null)
            {

            }
            if (transform.position.y < currentPlatform.position.y + platformThreshold)
            {
                isFalling = true;
                animator.SetTrigger("Jump");                
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            SetCurrentPlatform(collision.transform);
            isFalling = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlatform = null;
        }
    }

    private void SetCurrentPlatform(Transform platform)
    {
        currentPlatform = platform;
    }
}