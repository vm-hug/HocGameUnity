using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Chỉ số di chuyển")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;
    private float moveX;

    private bool isGrounded;
    private int groundContacts;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Player không có Rigidbody2D!");
        }
    }

    private void Update()
    {
        if (rb == null || Keyboard.current == null)
            return;

        // =========================
        // DI CHUYỂN
        // =========================

        moveX = 0f;

        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
        {
            moveX = -1f;
        }
        else if (Keyboard.current.dKey.isPressed ||
                 Keyboard.current.rightArrowKey.isPressed)
        {
            moveX = 1f;
        }

        // =========================
        // NHẢY
        // =========================

        if (isGrounded &&
            (Keyboard.current.wKey.wasPressedThisFrame ||
             Keyboard.current.spaceKey.wasPressedThisFrame ||
             Keyboard.current.upArrowKey.wasPressedThisFrame))
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            isGrounded = false;
        }

        // =========================
        // LẬT NHÂN VẬT
        // =========================

        if (moveX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        if (rb == null)
            return;

        rb.linearVelocity = new Vector2(
            moveX * moveSpeed,
            rb.linearVelocity.y
        );
    }

    // =========================
    // CHẠM ĐẤT
    // =========================

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Chỉ tính là đứng đất khi có tiếp xúc từ phía dưới
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                groundContacts++;
                isGrounded = true;
                break;
            }
        }
    }

    // =========================
    // RỜI ĐẤT
    // =========================

    private void OnCollisionExit2D(Collision2D collision)
    {
        groundContacts--;

        if (groundContacts <= 0)
        {
            groundContacts = 0;
            isGrounded = false;
        }
    }
}