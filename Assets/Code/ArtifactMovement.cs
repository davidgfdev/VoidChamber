using UnityEngine;

public class ArtifactMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject throwPosition;
    [SerializeField] float followSpeed;
    [SerializeField] Vector2 throwForce;
    [SerializeField] float throwMultiplier;
    private CircleCollider2D circleCollider2D;
    private Rigidbody2D rb;
    private bool isTravelling;
    private float originalGravityScale;
    private Jump jump;
    private PlayerMovement playerMovement;
    private bool isTaken = false;

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        jump = player.GetComponent<Jump>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        originalGravityScale = rb.gravityScale;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
        GoBackToPlayer();
        if (Input.GetKey(KeyCode.Q) && jump.GetIsGrounded())
        {
            isTravelling = true;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            isTravelling = false;
            circleCollider2D.enabled = true;
            rb.gravityScale = originalGravityScale;
        }

        if (Input.GetKey(KeyCode.E) && Vector3.Distance(transform.position, player.transform.position) <= 1)
        {
            playerMovement.Immobilize();
            transform.position = throwPosition.transform.position;
            isTaken = true;
        }
        else if (Input.GetKeyUp(KeyCode.E) && isTaken)
        {
            playerMovement.ResetSpeed();
            rb.AddForce(new Vector2(throwForce.x * Mathf.Sign(throwPosition.transform.right.x), throwForce.y) * throwMultiplier * 1000);
            isTaken = false;
        }

        circleCollider2D.enabled = rb.velocity.y < 0.1;
    }

    private void GoBackToPlayer()
    {
        if (isTravelling)
        {
            circleCollider2D.enabled = false;
            rb.gravityScale = 0;
            Vector3 target = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * followSpeed);

            if (Vector3.Distance(transform.position, target) <= 0.1f)
            {
                isTravelling = false;
                circleCollider2D.enabled = true;
                rb.gravityScale = originalGravityScale;
            }
        }

        player.GetComponent<Animator>().SetBool("Concentrating", isTravelling);
    }
}
