using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss_Controller : MonoBehaviour
{
    Animator animator;
    public Transform[] boss_parts;
    public GameObject[] Slut;
    public bool paturn;
    public Transform head;
    public GameObject energyball;
    public Transform player;
    private Vector3 initialHeadPosition;
    // Start is called before the first frame update

    [Header("보스 능력치 관련")]
    private float max_hp=100;
    private float currentHealth;
    public Slider BS_hpBar;
    void Start()
    {
        animator = GetComponent<Animator>();
        PartsChild();
        BS_hpBar.maxValue = max_hp;
        currentHealth = max_hp;
        initialHeadPosition = head.position;
    }

    // Update is called once per frame
    void Update()
    {
        BS_hpBar.value = currentHealth;
      
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
    public IEnumerator EnergyBall()
    {
        int speed = 5;
        GameObject energyballprefab = Instantiate(energyball, initialHeadPosition, Quaternion.identity);
        Vector3 direction = (player.position - transform.position).normalized;
        energyballprefab.GetComponent<Rigidbody2D>().velocity = direction * speed;
        Debug.Log(energyballprefab.transform.position);
        yield return new WaitForSeconds(1f);


        Destroy(energyballprefab, 5f);

    }
    public void AnimationSpeeddown()
    {
        animator.speed = 0.3f;
    }
    public void AniamtionSpeedUp()
    {
        animator.speed = 0.6f;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Attack"))
        {
            Debug.Log(BS_hpBar.value);
            currentHealth -= 10;
        }
    }
}
