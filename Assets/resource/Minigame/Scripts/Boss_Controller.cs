using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss_Controller : MonoBehaviour
{
    Animator animator;
    public bool paturn;
    public Transform head;
    public GameObject energyball;
    public Transform player;
    private Vector3 initialHeadPosition;
    // Start is called before the first frame update

    [Header("���� �ɷ�ġ ����")]
    private float max_hp=1;
    private float currentHealth;
    public Slider BS_hpBar;

    public Transform parentTransform;
    public Transform[] children;
    public GameObject parentleft_Arm;
    public GameObject parentright_Arm;
    public Collider2D[] left_Arm;
    public Collider2D[] right_Arm;

    public bool isDie = false;
    public GameObject triggerEffectPrefab; // ����Ʈ ������
    void Start()
    {
        
        animator = GetComponent<Animator>();
        PartsChild();
        BS_hpBar.maxValue = max_hp;
        currentHealth = max_hp;
        initialHeadPosition = head.position;
        //StartCoroutine(EnergyBall());
        StartCoroutine(Think());
    }
    // Update is called once per frame
    void Update()
    {
        BS_hpBar.value = currentHealth;
        Dead();
    }
    public IEnumerator Think()
    {
        Debug.Log("�ߵ�");
        left_Arm[0].enabled = true;
        right_Arm[0].enabled = true;
        yield return new WaitForSeconds(5f);

        int randAction = Random.Range(0, 3);

        if (!isDie)
        {
            switch (randAction)
            {

                case 0:
                    Debug.Log("case 0 �ߵ�");
                    left_Arm[0].enabled = false;
                    right_Arm[0].enabled = false;
                    StartCoroutine(Sweep());

                    break;
                case 1:
                    Debug.Log("case 1 �ߵ�");
                    left_Arm[0].enabled = false;
                    right_Arm[0].enabled = false;
                    StartCoroutine(Mount());

                    break;
                case 2:
                    Debug.Log("case 2 �ߵ�");
                    left_Arm[0].enabled = false;
                    right_Arm[0].enabled = false;
                    StartCoroutine(Magic());
                    break;
            }
        }
        else
        {
            StartCoroutine(Think());
        }

    }
    public IEnumerator Sweep()
    {
        animator.SetTrigger("Sweep");  
        left_Arm[3].enabled = true;
        right_Arm[3].enabled = true;
        parentleft_Arm.tag = "enemy";
        parentright_Arm.tag = "enemy";

        // ��� �ð�
        yield return new WaitForSeconds(0.1f); // ���ϴ� ��� �ð����� ����

        // �ִϸ��̼��� ���� ������ ���
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Boss_Ent_Sweep") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        // �ִϸ��̼��� ������ �� ������ �ڵ�
        StartCoroutine(Think());
        left_Arm[3].enabled = false;
        right_Arm[3].enabled = false;
        parentleft_Arm.tag = "Idle";
        parentright_Arm.tag = "Idle";
    }

    public IEnumerator Magic()
    {
        animator.SetTrigger("Magic");
        left_Arm[2].enabled = true;
        right_Arm[2].enabled = true;
        // ��� �ð�
        yield return new WaitForSeconds(0.1f); // ���ϴ� ��� �ð����� ����

        // �ִϸ��̼��� ���� ������ ���
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Boss_Ent_Sweep") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        // �ִϸ��̼��� ������ �� ������ �ڵ�
        StartCoroutine(Think());
        left_Arm[2].enabled = false;
        right_Arm[2].enabled = false;
        parentleft_Arm.tag = "Idle";
        parentright_Arm.tag = "Idle";
    }
    public IEnumerator Mount()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("YourAnimationName") && !animator.IsInTransition(0))
        {
            StartCoroutine(Think());
        }
        yield return new WaitForSeconds(0.5f);
    }
    public void PartsChild()
    {
        // �θ� ������Ʈ�� �ڽĵ��� ������
        parentTransform = transform;
        children = new Transform[parentTransform.childCount];

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            children[i] = parentTransform.GetChild(i);
        }
        
        //���� �ݶ��̴��� �����ϴ� �ι�
        Collider2D[] left_Arm_colliders = parentleft_Arm.GetComponents<Collider2D>();
        Collider2D[] right_Arm_colliders = parentright_Arm.GetComponents<Collider2D>();

        // left_Arm �迭 �ʱ�ȭ
        left_Arm = new Collider2D[left_Arm_colliders.Length];
        right_Arm = new Collider2D[right_Arm_colliders.Length];

        // ������ Collider2D �迭�� left_Arm �迭�� �Ҵ�
        for (int i = 0; i < left_Arm_colliders.Length; i++)
        {
            left_Arm[i] = left_Arm_colliders[i];
            left_Arm[i].enabled = false;          
        }
        for (int i = 0; i < right_Arm_colliders.Length; i++)
        {
            right_Arm[i] = right_Arm_colliders[i];
            right_Arm[i].enabled = false;
        }

    }

    public IEnumerator EnergyBall()
    {
        int speed = 5;
        GameObject energyballprefab = Instantiate(energyball, initialHeadPosition, Quaternion.identity);
        Vector3 direction = (player.position - transform.position).normalized;
        energyballprefab.GetComponent<Rigidbody2D>().velocity = direction * speed;
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnergyBall());

        Destroy(energyballprefab, 5f);

    }
    public void Dead()
    {
        if(currentHealth<0 && !isDie)
        {
            isDie = true;
            animator.SetTrigger("Dead");
        }
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
            Vector3 collisionPoint = other.ClosestPoint(transform.position);
            GameObject effectprefab=Instantiate(triggerEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effectprefab, 2);
            currentHealth -= 10;
        }
    }

}
