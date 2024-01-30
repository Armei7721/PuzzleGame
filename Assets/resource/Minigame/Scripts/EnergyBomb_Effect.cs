using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBomb_Effect : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public bool burst;
    // Start is called before the first frame update
    void Start()
    {
        burst = false;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("ground"))
        {
            burst = true;
            CameraControl.cc.shake=true;
            animator.SetBool("Burst",burst);
            rb.velocity = Vector2.zero;
            gameObject.tag = "destroy";
            float ani_length = animator.GetCurrentAnimatorStateInfo(0).length+1;
            Destroy(gameObject, ani_length);
            
        }
    }
}
