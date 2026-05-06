using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 3f;
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private float rightLimit = 5.5f;
    [SerializeField] private float leftLimit = -5.5f;

    [SerializeField] private StaminaManager staminaManager; // Referência à estamina

    private Rigidbody rb;
    private float horizontalInput;
    private bool canJump;
    public bool morreu;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;
        morreu = false;
    }

    void Update()
    {
        if (morreu)
        {
            return;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");

        bool querPular = Input.GetKeyDown(KeyCode.Space)
                      || Input.GetKeyDown(KeyCode.W)
                      || Input.GetKeyDown(KeyCode.UpArrow);

        bool temEstamina = staminaManager == null || staminaManager.TryConsumeStamina();

        if (canJump && querPular && temEstamina)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }

    void FixedUpdate()
    {
        if (morreu)
        {
            return;
        }

        Vector3 forwardMove = Vector3.forward * forwardSpeed;
        Vector3 horizontalMove = Vector3.right * horizontalInput * horizontalSpeed;

        rb.linearVelocity = new Vector3(horizontalMove.x, rb.linearVelocity.y, forwardMove.z);

        float clampedX = Mathf.Clamp(transform.position.x, leftLimit, rightLimit);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (morreu)
        {
            return;
        }

        if (IsObstacle(collision.gameObject))
        {
            Die();
            return;
        }

        if (IsGround(collision.gameObject))
        {
            canJump = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (morreu || !IsGround(collision.gameObject))
        {
            return;
        }

        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) < 30f)
            {
                canJump = true;
            }
        }
    }

    void Die()
    {
        morreu = true;
        horizontalInput = 0f;
        canJump = false;
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
    }

    bool IsObstacle(GameObject obj)
    {
        return obj.CompareTag("Obstacle") || obj.name.ToUpperInvariant().Contains("OBSTACULO");
    }

    bool IsGround(GameObject obj)
    {
        return obj.CompareTag("Ground") || obj.CompareTag("GROUND") || obj.name.ToUpperInvariant().Contains("CHAO");
    }
}