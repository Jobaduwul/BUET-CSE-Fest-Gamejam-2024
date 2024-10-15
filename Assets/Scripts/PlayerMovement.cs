using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 50f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int damage = 10;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isFacingRight = true;
    private bool isDashing = false;
    private bool isGrounded = true; // To check if player is grounded

    private string currentAbility = "Fire"; // Default ability

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        // Handle movement direction and flip
        if (movement.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && isFacingRight)
        {
            Flip();
        }

        // Handle jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Attempting to Jump");
            Jump();
        }

        // Handle dash input
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            StartCoroutine(Dash());
        }

        // Handle attack input
        if (Input.GetKeyDown(KeyCode.L))
        {
            Attack();
        }

        // Handle special ability input
        if (Input.GetKeyDown(KeyCode.P))
        {
            UseSpecialAbility();
        }

        // Switch abilities using number keys 1, 2, 3
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchAbility("Fire");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchAbility("Ice");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchAbility("Thunder");
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Jump functionality
    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isGrounded = false; // Set to false immediately after jumping
    }

    // Dash functionality using a coroutine
    System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        float originalSpeed = moveSpeed;
        moveSpeed = dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        moveSpeed = originalSpeed;
        isDashing = false;
    }

    // Basic attack functionality
    void Attack()
    {
        Debug.Log("attack done");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
        }
    }

    // Use special ability
    void UseSpecialAbility()
    {
        Debug.Log("Using special ability: " + currentAbility);
        switch (currentAbility)
        {
            case "Fire":
                break;
            case "Ice":
                break;
            case "Thunder":
                break;
        }
    }

    // Switch abilities based on key press
    void SwitchAbility(string ability)
    {
        currentAbility = ability;
        Debug.Log("Switched to ability: " + currentAbility);
    }

    // Check if the player is grounded based on collision with objects tagged "Ground"
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Set isGrounded to true if touching ground
            Debug.Log("Player is grounded");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Set isGrounded to false if not touching ground anymore
            Debug.Log("Player left the ground");
        }
    }

    // To visualize the attack range in the Scene view (optional)
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
