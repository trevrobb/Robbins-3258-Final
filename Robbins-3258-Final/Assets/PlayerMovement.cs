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
    bool grounded;
    bool sprint;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sprint = false;
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (isGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

        }
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
    }

    void moveCharacter(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    bool isGrounded()
    {
        return rb.velocity.y == 0;
    }
}