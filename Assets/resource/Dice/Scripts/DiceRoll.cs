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
        Diceroll();
    }

    public void Diceroll()
    {
        diceVelocity = rb.velocity;

        if(Input.GetMouseButton(0))
        {
            float dirX = Random.Range(0, 500);
            float dirY = Random.Range(0, 500);
            float dirZ = Random.Range(0, 500);
            transform.position = new Vector3(0, 2, 0);
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * 10);
            rb.AddTorque(dirX, dirY, dirZ);
        }
    }
    
}
