using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rigidBody;
    private int movementDirection = 1;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        rigidBody.velocity = new Vector2(GetInputX() * speed * Time.deltaTime, rigidBody.velocity.y);
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
}
