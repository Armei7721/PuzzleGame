using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    static Rigidbody rb;
    public static Vector3 diceVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
       // Diceroll();
    }
    private void FixedUpdate()
    {
        Diceroll();
    }
    public void Diceroll()
    {
        diceVelocity = rb.velocity;

        
        
            float dirX = Random.Range(0, 500);
            float dirY = Random.Range(0, 500);
            float dirZ = Random.Range(0, 500);
            
            transform.Rotate(dirX*Time.deltaTime,dirY * Time.deltaTime, dirZ * Time.deltaTime);
            rb.AddForce(transform.up * 10);
            rb.AddTorque(dirX, dirY, dirZ);
        
    }
    
}
