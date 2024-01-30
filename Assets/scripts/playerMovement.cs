using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script handles the player movements including jumping and switching gravity
/// </summary>

public class playerMovement : MonoBehaviour
{

    private float horizontal;
    private float speed = 8;
    private float jumpingPower = 15;
    public float maxspeed = 80f;
    public float acceleration = 2000f;
    public float playerSpeed = 0;
    private bool isFacingRight = true;
    private bool isFacingLeft = false;
    private bool isJumping = false;
    private bool doubleJump = false;
    private float doubleJumpCooldown = 3f;
    private float doubleJumpTimer;
    private bool canDoubleJump = false;
    public bool isGravityNormal = true;
    private float gravityMultiplier = 1;
    private bool grounded = false;
    public Text visualCooldown;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    private void Start()
    {
        Physics2D.gravity = new Vector2(0, -9.8f);
        doubleJumpTimer = 3;

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                Jump();
                isJumping = true;
            }
            else if (canDoubleJump && !doubleJump)
            {
                doubleJump = true;
                playerDoubleJumpAvalible();
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && grounded)
        {
            isGravityNormal = !isGravityNormal;
            UpdateGravity();
            Debug.Log("flippar");
        }
        Flip();

        if (!canDoubleJump)
        {
            doubleJumpCooldown -= Time.deltaTime;
            //Debug.Log(doubleJumpCooldown);

            if (doubleJumpCooldown <= 0f && isJumping == true)
            {

                canDoubleJump = true;
            }
        }
        if (doubleJumpTimer > 0f)
        {
            doubleJumpCooldown -= Time.deltaTime;

        }
        else
        {
            doubleJumpTimer = 0f;
            canDoubleJump = true;
        }

    }
    private void FixedUpdate()
    {
        if (horizontal != 0f)
        {
            playerSpeed += acceleration * Time.fixedDeltaTime;
            playerSpeed = Mathf.Clamp(playerSpeed, 0, maxspeed);
        }
        else
        {
            playerSpeed = 0f;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    // fliping player direction
    private void Flip()
    {
        if (isFacingRight && horizontal > 0f)
        {
            isFacingRight = false;
            isFacingLeft = true;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        else if (isFacingLeft && horizontal < 0f)
        {
            isFacingRight = true;
            isFacingLeft = false;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    //checking for collision
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isJumping = false;
            Debug.Log("grounded");
            grounded = true;
            doubleJump = false;

        }

        if (!grounded)
        {
            Debug.Log("inte groundad");
        }
    }
    // checking if no longer collition
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = false;
        }
    }
    private void ResetDoubleJumpCooldown()
    {
        canDoubleJump = true;
        doubleJumpCooldown = 3f;
        doubleJumpTimer = doubleJumpCooldown;
    }
    private void Jump()
    {
        if (!isJumping)
        {
            //isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            Debug.Log("hoppar");

            if (!isGravityNormal)
            {
                jumpingPower = -10f;
                Debug.Log("skummt hopp");
            }
            else
            {
                jumpingPower = 10f;
                Debug.Log("normal hopp");
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        else if (canDoubleJump)
        {
            doubleJump = true;
            playerDoubleJumpAvalible();
            isJumping = false;

        }
    }

    private void playerDoubleJumpAvalible()
    {
        Debug.Log("double jumping");
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        canDoubleJump = false;
        doubleJumpCooldown = 3f;
        doubleJumpTimer = doubleJumpCooldown;
        doubleJump = false;
    }
    void UpdateGravity()
    {

        Physics2D.gravity = isGravityNormal ? new Vector2(0, -9.8f * gravityMultiplier) : new Vector2(0, 9.8f * gravityMultiplier);

        Vector3 localScale = transform.localScale;
        localScale.y *= -1f;
        transform.localScale = localScale;

    }

    //switching the double jump cooldown visual every frame
    private void LateUpdate()
    {
        switch (doubleJumpCooldown > 0)
        {
            case true:
                visualCooldown.text = Mathf.Ceil(doubleJumpCooldown).ToString();

                break;
            case false:
                visualCooldown.text = "ready";
                break;

        }
        if (isGravityNormal)
        {
            Debug.Log("normal gravitation");
        }
        if (!isGravityNormal)
        {
            Debug.Log("inte normal gravitation");
        }

    }

}