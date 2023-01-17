using System;
using System.Collections;
using System.Collections.Generic;
using script;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : Attractable
{
    
    #region Attributes
    
    [SerializeField, Range(0, 100F)] private float groundSpeed = 10f;
    [SerializeField, Range(0, 100F)] private float airSpeed = 5f;

    [SerializeField, Range(0, 1F)] private float groundFriction = 0.9f;
    
    [SerializeField, Range(0, 200F)] public float jumpForce = 5f;
    
    private Rigidbody2D rb;
    private Vector2 velocity;
    private float horizontal;
    
    private bool grounded;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        grounded = this.IsGrounded();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        grounded = this.IsGrounded();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Direction != null)
        {
            Vector2 moveDirection = Vector2.Perpendicular(-Direction);

            float speed = grounded ? groundSpeed : airSpeed;

            velocity = moveDirection * speed * horizontal;

            rb.velocity = Vector2.Lerp(rb.velocity, velocity, Time.deltaTime);
            
            if (rb.velocity.y == 0)
            {
                rb.velocity *= groundFriction;
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                Debug.Log("Jump");
                rb.AddForce(Direction*jumpForce, ForceMode2D.Impulse);
            }

            transform.rotation = Quaternion.LookRotation(Vector3.forward, Direction);

            Direction = new Vector3(0, 0, 0);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Direction, 0.8f);
        Debug.DrawRay(transform.position, -Direction*0.7f, Color.red);
        return hit.collider != null;
    }


}
