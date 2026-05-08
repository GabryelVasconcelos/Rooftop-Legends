using UnityEditor.SceneManagement;
using UnityEngine;

public class LASER : MonoBehaviour
{
    public float tempoLigado = 1f;
    public float tempoDesligado = 1f;

    private Renderer rend;
    private Collider col;

    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        StartCoroutine(Laserloop());
    }
    System.Collections.IEnumerator Laserloop()
    {
        while (true)
        {
            // LIGAR
            rend.enabled = true;
            col.enabled = true;
            yield return new WaitForSeconds(tempoLigado);

            //DESLIGAR
            rend.enabled = false;
            col.enabled = false;
            yield return new WaitForSeconds(tempoDesligado);
        }    
    }
}
