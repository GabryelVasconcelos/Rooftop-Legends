using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportarFase : MonoBehaviour
{
    public string proximaFase;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene(proximaFase);
        }
    }
}