using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //jumping variable
    public float speed;
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public GameObject jumpEffect;

    //private
    float moveInput;
    bool facingRight = true;


    Rigidbody2D rb;

    //checking if is on ground
    bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public int extraJumpValue;
    private int extraJump;

    // Start is called before the first frame update
    void Start()
    {
        extraJump = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded == true)
        {
            extraJump = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJump > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            Instantiate(jumpEffect, transform.position, Quaternion.identity);
            extraJump--;
        } else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJump > 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            Instantiate(jumpEffect, transform.position, Quaternion.identity);

        }

        BetterJump();
    }

    private void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed * Time.deltaTime, rb.velocity.y);
        if(facingRight == false && moveInput > 0)
        {
            Flip();
        } else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }


    void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if(rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
