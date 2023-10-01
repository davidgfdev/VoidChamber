using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentSpeed;
    private Rigidbody2D rigidBody;
    private int movementDirection = 1;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentSpeed = speed;
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        Flip();
    }

    private void MoveCharacter()
    {
        rigidBody.velocity = new Vector2(GetInputX() * currentSpeed * Time.deltaTime, rigidBody.velocity.y);
        GetComponent<Animator>().SetFloat("MoveX", rigidBody.velocity.normalized.sqrMagnitude);
    }

    private void Flip()
    {
        if (GetInputX() > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (GetInputX() < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public float GetInputX()
    {
        return Input.GetAxis("Horizontal") * movementDirection;
    }

    public void ApplyWallSpeed(float wallSpeedReducer)
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * wallSpeedReducer);
    }

    public float GetVerticalSpeed()
    {
        return rigidBody.velocity.y;
    }

    public void ResetMovementDirection()
    {
        movementDirection = 1;
    }

    public void InverseMovementDirection()
    {
        movementDirection = -movementDirection;
    }

    public void Immobilize()
    {
        currentSpeed = 0;
    }

    public void ResetSpeed()
    {
        currentSpeed = speed;
    }
}
