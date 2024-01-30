using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneWind_Effect : MonoBehaviour
{
    GameObject Player;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Vector2 direction = Player.transform.position - transform.position;
        Vector2 normalizedDirection = direction.normalized;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(normalizedDirection.x, rb.velocity.y);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
