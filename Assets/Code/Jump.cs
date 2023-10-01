using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float coyoteJump;
    [SerializeField] private float bufferTime;
    [SerializeField] private float stopJumpX;
    [SerializeField] private float stopJumpY;
    private Rigidbody2D rigidBody;
    private float currentCoyoteJump;
    private bool isGrounded = true;
    private float currentBufferTime;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CalcJumpVariables();
    }

    private void FixedUpdate()
    {
        if (currentBufferTime >= 0 && currentCoyoteJump > 0)
        {
            JumpUp();
        }
    }

    private void CalcJumpVariables()
    {
        if (isGrounded && rigidBody.velocity.y < 0.5f && rigidBody.velocity.y > -0.5f)
        {
            currentCoyoteJump = coyoteJump;
        }
        else
        {
            currentCoyoteJump -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentBufferTime = bufferTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopJump();
        }

        currentBufferTime -= Time.deltaTime;
        GetComponent<Animator>().SetBool("Falling", rigidBody.velocity.y < 0);
    }

    private void JumpUp()
    {
        isGrounded = false;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        rigidBody.AddForce(Vector2.up * jumpForce * 100 * Time.deltaTime);
        currentCoyoteJump = 0;
        currentBufferTime = 0;
        GetComponent<Animator>().SetTrigger("Jump");
    }

    private void StopJump()
    {
        if (isGrounded == false && rigidBody.velocity.y > 0)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x * stopJumpX, rigidBody.velocity.y * stopJumpY);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            isGrounded = true;
            GetComponent<Animator>().SetTrigger("Land");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            isGrounded = false;
        }
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }
}