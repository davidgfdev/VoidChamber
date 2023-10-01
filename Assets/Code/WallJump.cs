using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class WallJump : MonoBehaviour
{
    [SerializeField] private Vector2 wallJumpForce;
    [SerializeField] private float wallSpeedMultiplier;
    [SerializeField] private float travellingSpeed;
    private bool isAbleToWallJump = false;
    private Vector2 wallJumpNormal;
    private Vector2 wallJumpTargetLocation;
    private PlayerMovement playerMovement;
    private bool wallJumping = false;
    private Rigidbody2D rb;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (wallJumping)
        {
            transform.position = Vector2.MoveTowards(transform.position, wallJumpTargetLocation, travellingSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, wallJumpTargetLocation) <= 0.1f)
            {
                rb.gravityScale = 1;
                wallJumping = false;
            }
        }

        if (isAbleToWallJump)
        {
            if (Input.GetKeyDown(KeyCode.Space) && playerMovement.GetVerticalSpeed() < 0.1f)
            {
                wallJumpTargetLocation = new Vector2(transform.position.x + (wallJumpForce.x * Mathf.Sign(wallJumpNormal.x)), transform.position.y + wallJumpForce.y);
                wallJumping = true;
            }

            playerMovement.ApplyWallSpeed(wallSpeedMultiplier);
        }
        else
        {
            playerMovement.ApplyWallSpeed(1);
        }
    }

    private void OnCollisionStay2D(Collision2D collisionInfo)
    {

        if (!collisionInfo.gameObject.CompareTag("Player"))
        {

            isAbleToWallJump = CompareContactPoints(collisionInfo.contacts);
        }
    }

    private void OnCollisionExit2D(Collision2D collisionInfo)
    {
        if (!collisionInfo.gameObject.CompareTag("Player"))
        {
            isAbleToWallJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (!collisionInfo.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contactPoint in collisionInfo.contacts)
            {
                if (contactPoint.normal.y == 1)
                {
                    rb.gravityScale = 3;
                }
            }
            if (wallJumping)
            {
                wallJumping = false;
                rb.gravityScale = 1;
            }
        }
    }

    private bool CompareContactPoints(ContactPoint2D[] contacts)
    {
        bool isContact = false;

        foreach (ContactPoint2D contactPoint in contacts)
        {
            float normalX = contactPoint.normal.x;
            float playerInversedSign = Mathf.Sign(0 - playerMovement.GetInputX());
            float normalSign = Mathf.Sign(normalX);

            if (Mathf.Abs(normalX) > 0.8f && playerInversedSign == normalSign)
            {
                Debug.Log("colliding");
                isContact = true;
                wallJumpNormal = contactPoint.normal;
            }
        }

        return isContact;
    }
}