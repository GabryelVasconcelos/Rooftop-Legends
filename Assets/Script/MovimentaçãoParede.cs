using UnityEngine;

public class MovimentaçãoParede : MonoBehaviour
{
    public float velocidade = 2f;
    public float distancia = 3f;

    private Vector3 posiçãoInicial;

    void Start()
    {
        posiçãoInicial = transform.position;
    }
    void Update()
    {
        float movimento = Mathf.PingPong(Time.time * velocidade, distancia * 2) - distancia;

        transform.position = posiçãoInicial + new Vector3(movimento, 0, 0);
    }
}
