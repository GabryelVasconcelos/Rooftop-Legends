using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    // Singleton Instance
    public static StaminaManager Instance { get; private set; }

    [SerializeField] private Slider staminaSlider;

    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaCostPerJump = 25f;
    [SerializeField] private float regenPerSecond = 30f;
    [SerializeField] private float regenDelay = 1.0f; // Tempo em segundos antes de voltar a regenerar

    private float currentStamina;
    private float lastConsumeTime;
    private bool warningLogShown = false;

    void Awake()
    {
        // Singleton pattern implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        currentStamina = maxStamina;
        lastConsumeTime = -regenDelay; // Permite regenerar imediatamente no inicio se precisar

        // Tenta achar o Slider se estiver vazio no Inspector
        if (staminaSlider == null)
        {
            staminaSlider = FindObjectOfType<Slider>();
            if (staminaSlider == null && !warningLogShown)
            {
                Debug.LogWarning("StaminaManager: Nenhum Slider de Estamina foi encontrado na cena. A mecanica vai funcionar nos bastidores, mas sem barra visual.");
                warningLogShown = true;
            }
        }

        UpdateUI();
    }

    void Update()
    {
        // Se ja passou o tempo de delay desde o ultimo uso, regenera
        if (Time.time >= lastConsumeTime + regenDelay)
        {
            if (currentStamina < maxStamina)
            {
                currentStamina = Mathf.Min(currentStamina + regenPerSecond * Time.deltaTime, maxStamina);
                UpdateUI();
            }
        }
    }

    // Chamado pelo PlayerMovement ao pular
    public bool TryConsumeStamina()
    {
        if (currentStamina < staminaCostPerJump)
        {
            return false; // Sem estamina suficiente
        }

        currentStamina -= staminaCostPerJump;
        lastConsumeTime = Time.time; // Reseta o timer de delay para recarregar
        
        UpdateUI();
        return true;
    }

    private void UpdateUI()
    {
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }
    }
}
