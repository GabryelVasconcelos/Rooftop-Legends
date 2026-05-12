using UnityEngine;

public class Animação : MonoBehaviour
{
    public Animator anim;
    Rigidbody rb;
    bool noChao;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(Input.GetAxisRaw("Vertical") > 0 && noChao)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 7, rb.linearVelocity.z);
            anim.SetBool("isJumping", true);
            noChao = false;
        }   
    }
    private void OnCollisionEnter(Collision collision)
    {
        noChao = true;
        anim.SetBool("isJumping", false);
    }
}
