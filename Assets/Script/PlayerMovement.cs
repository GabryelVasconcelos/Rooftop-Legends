using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 3f;
    [SerializeField] private float jumpForce = 8f;

    [Header("Boundaries")]
    [SerializeField] private float rightLimit = 5.5f;
    [SerializeField] private float leftLimit = -5.5f;

    private Rigidbody rb;
    private float horizontalInput;
    private bool canJump;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (canJump && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }

    void FixedUpdate()
    {
        Vector3 forwardMove = Vector3.forward * forwardSpeed;
        Vector3 horizontalMove = Vector3.right * horizontalInput * horizontalSpeed;

        rb.linearVelocity = new Vector3(horizontalMove.x, rb.linearVelocity.y, forwardMove.z);

        float clampedX = Mathf.Clamp(transform.position.x, leftLimit, rightLimit);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
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
        Time.timeScale = 0f;
        Invoke(nameof(RestartGame), 1.5f);
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
