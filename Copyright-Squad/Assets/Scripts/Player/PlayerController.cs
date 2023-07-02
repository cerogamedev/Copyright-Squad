using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private float moveHorizontal, moveVertical;
    private Rigidbody2D rb;
    private SpriteRenderer spr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Input handling for movement
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Normalize movement vector to prevent faster diagonal movement
        movement.Normalize();

        // Apply movement to the rigidbody
        rb.velocity = movement * moveSpeed;

        // Stop the player immediately when no movement input is detected
        if (movement == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
        Flip();
    }
    private void Flip()
    {
        if (moveHorizontal > 0)
        {
            spr.flipX = false;
            this.transform.GetChild(0).transform.position = new Vector2(transform.position.x + 0.18f, transform.position.y - 0.54f);
        }
        else if (moveHorizontal < 0)
        {
            spr.flipX = true;
            this.transform.GetChild(0).transform.position = new Vector2(transform.position.x-0.18f, transform.position.y - 0.54f);

        }
        else
            return;
    }
}
