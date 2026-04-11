using UnityEngine;
using UnityEngine.SceneManagement;

public class MENU_PRINCIPAL_MANAGER : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuinicial;
    [SerializeField] private GameObject painelOpções;

    public void Jogar()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }
    
    public void AbrirOpções()
    {
        painelMenuinicial.SetActive(false);
        painelOpções.SetActive(true);
    }

    public void FecharOpções()
    {
        painelOpções.SetActive(false);
        painelMenuinicial.SetActive(true);
    }

    public void SairJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}