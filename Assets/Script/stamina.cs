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

        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = maxStamina;
        }
    }

    void Update()
    {
        if (playerMovement == null || playerMovement.morreu)
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
            return false; // Sem estamina, não pode pular
        }

        currentStamina -= staminaCostPerJump;
        return true;
    }

    public bool HasStamina()
    {
        return currentStamina >= staminaCostPerJump;
    }
}