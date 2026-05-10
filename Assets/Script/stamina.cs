using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Slider staminaSlider;

    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaCostPerJump = 25f;
    [SerializeField] private float regenPerSecond = 10f;

    private float currentStamina;

    void Start()
    {
        currentStamina = maxStamina;

        // Tenta achar o PlayerMovement se estiver vazio no Inspector
        if (playerMovement == null)
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
            if (playerMovement == null)
            {
                Debug.LogWarning("StaminaManager: PlayerMovement nao foi atribuido e nao foi encontrado na cena!");
            }
        }

        // Tenta achar o Slider se estiver vazio no Inspector
        if (staminaSlider == null)
        {
            staminaSlider = FindObjectOfType<Slider>();
            if (staminaSlider == null)
            {
                Debug.LogWarning("StaminaManager: Slider de Estamina nao foi atribuido e nao foi encontrado na cena!");
            }
        }

        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = maxStamina;
        }
    }

    void Update()
    {
        // Se o player morreu, para de regenerar
        if (playerMovement != null && playerMovement.morreu)
        {
            return;
        }

        // Regenera com o tempo
        currentStamina = Mathf.Min(currentStamina + regenPerSecond * Time.deltaTime, maxStamina);

        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }
    }

    // Chamado pelo PlayerMovement ao pular
    public bool TryConsumeStamina()
    {
        if (currentStamina < staminaCostPerJump)
        {
            return false; // Sem estamina, nao pode pular
        }

        currentStamina -= staminaCostPerJump;
        
        // Atualiza a barra imediatamente
        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }
        
        return true;
    }

    public bool HasStamina()
    {
        return currentStamina >= staminaCostPerJump;
    }
}
