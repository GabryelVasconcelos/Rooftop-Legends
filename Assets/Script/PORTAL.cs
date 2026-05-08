using UnityEngine;
using UnityEngine.SceneManagement;
public class PORTAL : MonoBehaviour
{
    public string nomeDaFase;

    private void OggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           SceneManager.LoadScene(nomeDaFase);
        }
    }
}
