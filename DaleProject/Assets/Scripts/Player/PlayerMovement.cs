using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStatsData playerStats;

    //movement variables
    public float jumpForce;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private bool isGrounded;
    public float fallMultiplier;
    public float lowjumpMultiplier;
    public SpriteRenderer spriteRenderer;

    //Pickaxe and Sprite Rotation
    public Transform playerCenter; // Empty GameObject at the center of the player
    public GameObject pickaxe; // The pickaxe GameObject

    //Animation
    private Animator playerAnim;

    Camera cam;
    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }
    void Update()
    {
        Movement();
        Jumping();
        Smoothjump();
        pickaxerotation();
        PlayerWalkingAnimation();
    }


    void Movement()
    {
        // Movement
        float moveDirection = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveDirection * playerStats.speed, rb.velocity.y);
    }
    void Jumping()
    {
        // Check if the character is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    void Smoothjump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowjumpMultiplier - 1) * Time.deltaTime;
        }

    }

    void pickaxerotation()
    {
        {
            // Calculate the angle between playerCenter and mouse position
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - playerCenter.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the playerCenter (and the attached pickaxe)
            playerCenter.rotation = Quaternion.Euler(0f, 0f, angle);

            if (mousePos.x < playerCenter.position.x)
            {
                spriteRenderer.flipX = true;
                pickaxe.GetComponent<SpriteRenderer>().flipY = true;
            }
            else
            {
                spriteRenderer.flipX = false;
                pickaxe.GetComponent<SpriteRenderer>().flipY = false;
            }

        }
    }

    void PlayerWalkingAnimation()
    {
        if(rb.velocity.x != 0)
        {
            playerAnim.SetBool("isWalking", true);
        }else
        {
            playerAnim.SetBool("isWalking", false);
        }
    }
}
