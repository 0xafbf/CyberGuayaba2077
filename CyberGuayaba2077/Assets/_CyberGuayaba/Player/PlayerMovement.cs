using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float deacceleration = 10f;

    public bool IsMoving => rb.velocity.magnitude > 0;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(moveX, moveY).normalized;
        Vector2 targetVelocity = moveDir * moveSpeed;

        rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, Time.deltaTime * (rb.velocity.magnitude < targetVelocity.magnitude ? acceleration : deacceleration));
    }
}
