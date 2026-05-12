using UnityEngine;

public class Animação : MonoBehaviour
{
    public Animator anim;
    Rigidbody rb;
    bool noChao = true;

    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(Input.GetAxisRaw("Vertical") > 0)
        {
            rb.angularVelocity = new Vector3(rb.angularVelocity.x, 7, rb.angularVelocity.z);
            anim.SetBool("isJumping", true);
            noChao = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        noChao = true;
        anim.SetBool("isJumping", false);
    }
}
