using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject painelVocePerdeu;
    [SerializeField] private string nomeDaCenaMenu = "MENU";

    private bool painelJaFoiMostrado;

    void Start()
    {
        painelJaFoiMostrado = false;

        if (painelVocePerdeu != null)
        {
            painelVocePerdeu.SetActive(false);
        }
    }

    void Update()
    {
        if (playerMovement == null || painelVocePerdeu == null)
        {
            Debug.LogWarning($"Referência nula! player={playerMovement}, painel={painelVocePerdeu}");
            return;
        }

        if (playerMovement.morreu && !painelJaFoiMostrado)
        {
            painelVocePerdeu.SetActive(true);
            painelJaFoiMostrado = true;
            Debug.Log($"Painel ativo: {painelVocePerdeu.activeInHierarchy}");
        }
    }

    public void ReiniciarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene(nomeDaCenaMenu);
    }
}