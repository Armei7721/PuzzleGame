using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class Boss_Controller : MonoBehaviour
{
    public static Boss_Controller BS;
    Animator animator;
    public bool paturn;
    public Transform head;
    public GameObject energyball;
    public Transform player;
    private Vector3 initialHeadPosition;
    // Start is called before the first frame update

    [Header("보스 능력치 관련")]
    private float max_hp=300;
    public float currentHealth;
    public Slider BS_hpBar;

    public Transform parentTransform;
    public Transform[] children;
    public GameObject parentleft_Arm;
    public GameObject parentright_Arm;
    public Collider2D[] left_Arm;
    public Collider2D[] right_Arm;

    public bool isDie = false;
    public GameObject triggerEffectPrefab; // 이펙트 프리팹
    public GameObject sandstorm;

    public GameObject skeleton;
    public TilemapCollider2D tcollider;
    void Start()
    {
        StartCoroutine(SummonEnemy());
        BS = this;
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
        
        left_Arm[0].enabled = true;
        right_Arm[0].enabled = true;
        yield return new WaitForSeconds(5f);

        int randAction = Random.Range(0, 3);

        if (!isDie)
        {
            switch (randAction)
            {

                case 0:
                   
                    left_Arm[0].enabled = false;
                    right_Arm[0].enabled = false;
                    StartCoroutine(Sweep());

                    break;
                case 1:
                   
                    left_Arm[0].enabled = false;
                    right_Arm[0].enabled = false;
                    StartCoroutine(Mount());

                    break;
                case 2:
                    
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

        // 대기 시간
        yield return new WaitForSeconds(0.1f); // 원하는 대기 시간으로 조정

        // 애니메이션이 끝날 때까지 대기
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Boss_Ent_Sweep") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        // 애니메이션이 끝났을 때 실행할 코드
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
        // 대기 시간
       

        // 애니메이션이 끝날 때까지 대기
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Boss_Ent_Magic") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        // 애니메이션이 끝났을 때 실행할 코드
        StartCoroutine(Think());
        left_Arm[2].enabled = false;
        right_Arm[2].enabled = false;
        parentleft_Arm.tag = "Idle";
        parentright_Arm.tag = "Idle";
    }
    public IEnumerator Mount()
    {
        animator.SetTrigger("Mount");
        left_Arm[1].enabled = true;
        right_Arm[1].enabled = true;
        // 대기 시간
        yield return new WaitForSeconds(0.1f); // 원하는 대기 시간으로 조정

        // 애니메이션이 끝날 때까지 대기
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Boss_Ent_Mount") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        // 애니메이션이 끝났을 때 실행할 코드
        StartCoroutine(Think());
        left_Arm[1].enabled = false;
        right_Arm[1].enabled = false;
        parentleft_Arm.tag = "Idle";
        parentright_Arm.tag = "Idle";
    }
  
    public void PartsChild()
    {
        // 부모 오브젝트의 자식들을 가져옴
        parentTransform = transform;
        children = new Transform[parentTransform.childCount];

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            children[i] = parentTransform.GetChild(i);
        }
        
        //팔의 콜라이더를 참조하는 부문
        Collider2D[] left_Arm_colliders = parentleft_Arm.GetComponents<Collider2D>();
        Collider2D[] right_Arm_colliders = parentright_Arm.GetComponents<Collider2D>();

        // left_Arm 배열 초기화
        left_Arm = new Collider2D[left_Arm_colliders.Length];
        right_Arm = new Collider2D[right_Arm_colliders.Length];

        // 가져온 Collider2D 배열을 left_Arm 배열에 할당
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
    public void SandStormEffect()
    {
        GameObject SandStormprefab = Instantiate(sandstorm, (parentleft_Arm.transform.position+parentright_Arm.transform.position)/2, Quaternion.identity);
        if (SandStormprefab.GetComponent<Animator>().GetAnimatorTransitionInfo(0).IsName("SandStorm_Effect"))
        {
            Destroy(SandStormprefab);
        }

    }
    public IEnumerator SummonEnemy()
    {   tcollider = tcollider.GetComponent<TilemapCollider2D>();
        Vector2 colliderCenter = (Vector2)transform.position + tcollider.offset;

        // 박스 콜라이더의 윗변 중앙 위치
        Vector2 colliderTopCenter = colliderCenter + new Vector2(0f, tcollider.bounds.size.y * 0.5f);
        GameObject skeleton_Prefab = Instantiate(skeleton,colliderTopCenter,Quaternion.identity );
        yield return new WaitForSeconds(5f);
        StartCoroutine(SummonEnemy());
    }
    public IEnumerator EnergyBall()
    {
        int speed = 5;
        GameObject energyballprefab = Instantiate(energyball, initialHeadPosition, Quaternion.identity);
        Vector3 direction = (player.position - transform.position).normalized;
        energyballprefab.GetComponent<Rigidbody2D>().velocity = direction * speed;
        yield return new WaitForSeconds(1f);
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
        animator.speed = 1f;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if(other.CompareTag("Attack") && !isDie)
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);
            GameObject effectprefab=Instantiate(triggerEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effectprefab, 2);
            currentHealth -= 10;
        }
    }

}
