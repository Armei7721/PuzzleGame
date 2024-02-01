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
    private float max_hp=1000;
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
    public GameObject stonewind;

    public GameObject skeleton;
    public TilemapCollider2D tcollider;
    public Vector3 collisionPoint;
    public Vector3 collisionPoint2;

    public GameObject left_dust;
    public GameObject right_dust;

    public GameObject skeletonPrefab;
    public GameObject skeletonmagic;
    int randomX;
    Vector3 offset;

    private enum State
    {   Idle,
        Sweep,
        Mount,
        Magic,
        StoneWind,
        SummonEnemy

    }
    private State currentState = State.Idle;
    void Start()
    {
        
        BS = this;
        animator = GetComponent<Animator>();
        PartsChild();
        BS_hpBar.maxValue = max_hp;
        currentHealth = max_hp;
        initialHeadPosition = head.position;
        
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
        yield return new WaitForSeconds(3f);

        int randAction = Random.Range(0, 4);

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
                case 3:

                    left_Arm[0].enabled = false;
                    right_Arm[0].enabled = false;
                    StartCoroutine(StoneWind());
                    break;
                case 4:
                    left_Arm[0].enabled = false;
                    right_Arm[0].enabled = false;
                    StartCoroutine(SummonEnemy());
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
        currentState = State.Sweep;
        animator.SetTrigger("Sweep");  
        left_Arm[3].enabled = true;
        right_Arm[3].enabled = true;
        parentleft_Arm.tag = "enemy";
        parentright_Arm.tag = "enemy";

        // 대기 시간
        yield return new WaitForSeconds(0.1f); // 원하는 대기 시간으로 조정

        // 애니메이션이 끝날 때까지 대기
        float ani_length = animator.GetCurrentAnimatorStateInfo(0).length;
        // 애니메이션이 끝날 때까지 대기
        yield return new WaitForSeconds(ani_length);


        // 애니메이션이 끝났을 때 실행할 코드
        StartCoroutine(Think());
        left_Arm[3].enabled = false;
        right_Arm[3].enabled = false;
        parentleft_Arm.tag = "Idle";
        parentright_Arm.tag = "Idle";
    }

    public IEnumerator Magic()
    {
        currentState = State.Magic;
        animator.SetTrigger("Magic");
        left_Arm[2].enabled = true;
        right_Arm[2].enabled = true;
        yield return new WaitForSeconds(0.1f); // 원하는 대기 시간으로 조정
        // 대기 시간
        float sampleRate = animator.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate; // 애니메이션의 프레임 레이트
        float lengthInFrames = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length * sampleRate; // 애니메이션의 총 프레임 수

        float ani_length = animator.GetCurrentAnimatorStateInfo(0).length;
        // 애니메이션이 끝날 때까지 대기
        yield return new WaitForSeconds(lengthInFrames / sampleRate);
        Debug.Log(" sampleRate  :" + sampleRate);
        Debug.Log("lengthInFrames :" + lengthInFrames);
        Debug.Log("lengthInFrames / sampleRate : " + lengthInFrames / sampleRate);
        // 애니메이션이 끝났을 때 실행할 코드

        left_Arm[2].enabled = false;
        right_Arm[2].enabled = false;
        parentleft_Arm.tag = "Idle";
        parentright_Arm.tag = "Idle";
        StartCoroutine(Think());
      
    }
    public IEnumerator Mount()
    {
        currentState = State.Mount;
        animator.SetTrigger("Mount");
        left_Arm[1].enabled = true;
        right_Arm[1].enabled = true;
        
        float aniframe = animator.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate;
        float lengthInFrames = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; // 애니메이션의 총 프레임 수
        Debug.Log(lengthInFrames);
        if (aniframe == 25 && PlayerController.instance.isGround)
        {
            PlayerController.instance.HitDuring();
            PlayerController.instance.currentHealth = -20;
        }
        // 애니메이션이 끝날 때까지 대기
        float ani_length = animator.GetCurrentAnimatorStateInfo(0).length;
        // 애니메이션이 끝날 때까지 대기
        yield return new WaitForSeconds(ani_length);


        // 애니메이션이 끝났을 때 실행할 코드
        StartCoroutine(Think());
        left_Arm[1].enabled = false;
        right_Arm[1].enabled = false;
        parentleft_Arm.tag = "Idle";
        parentright_Arm.tag = "Idle";
    }
    public IEnumerator StoneWind()
    {
        currentState = State.StoneWind;
        animator.SetTrigger("StoneWind");
        left_Arm[1].enabled = true;
        right_Arm[1].enabled = true;
        float sampleRate = animator.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate; // 애니메이션의 프레임 레이트
        float lengthInFrames = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length * sampleRate; // 애니메이션의 총 프레임 수
        yield return new WaitForSeconds(lengthInFrames + 10f);
        StartCoroutine(Think());
    }
    IEnumerator SummonEnemy()
    {
        currentState = State.SummonEnemy;
        animator.SetTrigger("Summon");
        randomX = Random.Range(-15, 15);
        offset = new Vector3(randomX, 0, 0);
        
        // player가 null이 아니라면 스켈레톤 생성
        if (player != null)
        {
            Transform position_save = player;

            for (int i = 0; i < 4; i++)
            {
                // x 좌표에 랜덤값을 적용하여 스켈레톤 생성
                float randomXOffset = Random.Range(-10f, 10f);
                Vector3 skeletonposition = new Vector3(position_save.transform.position.x + randomXOffset, -2.5f, position_save.transform.position.z);
                GameObject skeleton = Instantiate(skeletonPrefab, skeletonposition, Quaternion.identity);
            }

        }

        yield return new WaitForSeconds(3f);


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
    public IEnumerator SandStormEffect()
    {
      
        GameObject SandStormprefab = Instantiate(sandstorm, (parentleft_Arm.transform.position+parentright_Arm.transform.position)/2, Quaternion.identity);
        float sandstormani = SandStormprefab.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(sandstormani);
        
        Destroy(SandStormprefab);
        

    }
    

    public void EnergyBall()
    {
        int speed = 5;
        GameObject energyballprefab = Instantiate(energyball, initialHeadPosition, Quaternion.identity);
        Vector3 direction = (player.position - transform.position).normalized;
        energyballprefab.GetComponent<Rigidbody2D>().velocity = direction * speed;
    

    }
 
    public IEnumerator StoneWindEffect()
    {
        Vector3 swposiiton = new Vector3(-9.5f, -1f, 0f);
        Vector3 swposiiton2 = new Vector3(9.5f, -1f, 0f);
        GameObject StoneWindprefab = Instantiate(stonewind, swposiiton, Quaternion.identity);
        GameObject StoneWindprefab2 = Instantiate(stonewind, swposiiton2, Quaternion.identity);
        yield return new WaitForSeconds(10f);
        Destroy(StoneWindprefab);
        Destroy(StoneWindprefab2);
        animator.speed = 1;
    }
    public void CameraShake()
    {
        CameraControl.cc.shake= true;

    }
    public void Dead()
    {
        if(currentHealth<0 && !isDie)
        {
            isDie = true;
            animator.SetTrigger("Dead");
        }
    }
    public void AnimationSpeedZero()
    {
        animator.speed = 0;
    }
    public void AnimationSpeeddown()
    {
        animator.speed = 0.3f;
    }
    public void AniamtionSpeedUp()
    { 
        animator.speed = 1f;
    }
    public void Left_DustStormSwitch()
    {
        if (left_dust.activeSelf)
        {
            left_dust.SetActive(false);
        }
        else if (!left_dust.activeSelf)
        {
            left_dust.SetActive(true);
        }
    }
    public void Right_DustStormSwitch()
    {
        if (right_dust.activeSelf)
        {
            right_dust.SetActive(false);
        }
        else if (!right_dust.activeSelf)
        {
            right_dust.SetActive(true);
        }
    }
   

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if(other.CompareTag("Attack") && !isDie)
        {
            collisionPoint = other.ClosestPoint(transform.position);
            GameObject effectprefab=Instantiate(triggerEffectPrefab, collisionPoint, Quaternion.identity);
            TextController.tc.ShowDamageText(collisionPoint, PlayerController.instance.damage);
            Destroy(effectprefab, 2);
            currentHealth -= 10;
        }
        else if(other.CompareTag("ground") && !isDie)
        {
            collisionPoint2 = other.ClosestPoint(transform.position);
            Debug.Log(collisionPoint2);
        }
    }

}
