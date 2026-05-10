using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 3f;
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private float rightLimit = 5.5f;
    [SerializeField] private float leftLimit = -5.5f;

    private Rigidbody rb;
    private float horizontalInput;
    private bool canJump;
    public bool morreu;
    private float lastJumpTime; // Cooldown para evitar pulos multiplos

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;
        morreu = false;
        lastJumpTime = 0f;
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

        // O jogador so pode pular se tiver canJump e ja tiver passado 0.2s desde o ultimo pulo
        if (canJump && querPular && Time.time >= lastJumpTime + 0.2f)
        {
            // Usa o Singleton para consumir estamina
            bool temEstamina = true;
            if (StaminaManager.Instance != null)
            {
                temEstamina = StaminaManager.Instance.TryConsumeStamina();
            }
            else
            {
                Debug.LogWarning("StaminaManager.Instance nao encontrada! O pulo ocorrera sem custo de estamina.");
            }

            if (temEstamina)
            {
                // Zera a velocidade Y para um pulo consistente, caso esteja caindo de leve
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                
                canJump = false;
                lastJumpTime = Time.time;
            }
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

        if (IsGround(collision.gameObject) && Time.time >= lastJumpTime + 0.2f)
        {
            CheckGroundContact(collision);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (morreu || !IsGround(collision.gameObject) || Time.time < lastJumpTime + 0.2f)
        {
            return;
        }

        CheckGroundContact(collision);
    }

    void CheckGroundContact(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            // Verifica se a colisao e com algo que esta pelo menos parcialmente voltado para cima (o chao real)
            if (Vector3.Angle(contact.normal, Vector3.up) < 45f)
            {
                canJump = true;
                return;
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
