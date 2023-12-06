using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Vector3 movement;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float gravityScale = 5f;
    
    float normalSpeed = 10f;
    bool grounded;
    bool sprint;

    float verticalInput;
    float horizontalInput;

    public bool activeGrapple;

    

    private bool enableMovementOnNextTouch;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sprint = false;
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") );
        if (isGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprint = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprint = false;
        }
    }

    private void FixedUpdate()
    {
        if (sprint && speed == normalSpeed)
        {
            speed = speed * 1.5f;
        }
        else if (!sprint){ speed = normalSpeed; }

        
        moveCharacter(movement);
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
        transform.Rotate(new Vector3(0, Camera.main.transform.rotation.y, 0));
    }

    void moveCharacter(Vector3 direction)
    {
        movement = Camera.main.transform.forward * direction.z + Camera.main.transform.right * direction.x;
        movement.y = 0f;
        rb.velocity = movement.normalized * speed;
    }

    bool isGrounded()
    {
        return rb.velocity.y == 0;
    }

    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.2f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);

        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));


        return velocityXZ + velocityY;
    }
    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();
            GrapplingGun.instance.StopGrappling();
        }

    }

    public void ResetRestrictions()
    {
        activeGrapple = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rocket"))
        {
            rb.AddExplosionForce(20f, other.transform.position, 30f);
        }
    }
}
