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
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(moveX, 0 ,moveZ).normalized;
        Vector3 targetVelocity = moveDir * moveSpeed;

        rb.velocity = Vector3.MoveTowards(rb.velocity, targetVelocity, Time.deltaTime * (rb.velocity.magnitude < targetVelocity.magnitude ? acceleration : deacceleration));
    }
}
