using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Controller : MonoBehaviour
{
    Animator animator;
    public Transform[] boss_parts;
    public GameObject[] Slut;
    public bool paturn;
    public Transform head;
    public GameObject energyball;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PartsChild();
        StartCoroutine(EnergyBall());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator Mount()
    {

        yield return new WaitForSeconds(0.5f);
    }
    public void PartsChild()
    {
        boss_parts = gameObject.GetComponentsInChildren<Transform>(true);
    }
    IEnumerator EnergyBall()
    {
        int speed = 5;
        GameObject energyballprefab = Instantiate(energyball, head.TransformPoint(Vector3.zero), Quaternion.identity);
        Vector3 direction = (player.position - transform.position).normalized;
        energyballprefab.GetComponent<Rigidbody2D>().velocity = direction * speed;
        Debug.Log(energyballprefab.transform.position);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnergyBall());
       
        Destroy(energyballprefab, 5f);

    }
}
