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

    [Header("���� �ɷ�ġ ����")]
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
    public GameObject triggerEffectPrefab; // ����Ʈ ������
    public GameObject sandstorm;
    public GameObject stonewind;

    public GameObject skeleton;
    public TilemapCollider2D tcollider;
    void Start()
    {
        //StartCoroutine(SummonEnemy());
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
        yield return new WaitForSeconds(5f);

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
        float ani_length = animator.GetCurrentAnimatorStateInfo(0).length;
        // �ִϸ��̼��� ���� ������ ���
        yield return new WaitForSeconds(ani_length);


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
        yield return new WaitForSeconds(0.1f); // ���ϴ� ��� �ð����� ����
        // ��� �ð�
        float sampleRate = animator.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate; // �ִϸ��̼��� ������ ����Ʈ
        float lengthInFrames = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length * sampleRate; // �ִϸ��̼��� �� ������ ��

        float ani_length = animator.GetCurrentAnimatorStateInfo(0).length;
        // �ִϸ��̼��� ���� ������ ���
        yield return new WaitForSeconds(lengthInFrames / sampleRate);
        Debug.Log(" sampleRate  :" + sampleRate);
        Debug.Log("lengthInFrames :" + lengthInFrames);
        Debug.Log("lengthInFrames / sampleRate : " + lengthInFrames / sampleRate);
        // �ִϸ��̼��� ������ �� ������ �ڵ�

        left_Arm[2].enabled = false;
        right_Arm[2].enabled = false;
        parentleft_Arm.tag = "Idle";
        parentright_Arm.tag = "Idle";
        StartCoroutine(Think());
      
    }
    public IEnumerator Mount()
    {
        animator.SetTrigger("Mount");
        left_Arm[1].enabled = true;
        right_Arm[1].enabled = true;

        // �ִϸ��̼��� ���� ������ ���
        float ani_length = animator.GetCurrentAnimatorStateInfo(0).length;
        // �ִϸ��̼��� ���� ������ ���
        yield return new WaitForSeconds(ani_length);


        // �ִϸ��̼��� ������ �� ������ �ڵ�
        StartCoroutine(Think());
        left_Arm[1].enabled = false;
        right_Arm[1].enabled = false;
        parentleft_Arm.tag = "Idle";
        parentright_Arm.tag = "Idle";
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
    public IEnumerator SandStormEffect()
    {
      
        GameObject SandStormprefab = Instantiate(sandstorm, (parentleft_Arm.transform.position+parentright_Arm.transform.position)/2, Quaternion.identity);
        float sandstormani = SandStormprefab.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(sandstormani);
        
        Destroy(SandStormprefab);
        

    }
    IEnumerator SummonEnemy()
    {   
        tcollider = GameObject.Find("Ground").GetComponent<TilemapCollider2D>();
        Vector2 colliderCenter = new Vector3(tcollider.bounds.center.x+ Random.Range(-2f, 2f), tcollider.bounds.center.y);

        // �ڽ� �ݶ��̴��� ���� �߾� ��ġ
        Vector2 colliderTopCenter = colliderCenter + new Vector2(0f, tcollider.bounds.size.y * 0.8f);
        Debug.Log("colliderCenter ��ġ :" + colliderCenter);
        Debug.Log("colliderTopCenter ��ġ :" + colliderTopCenter);
        // ���� Ÿ�ϸ� �ݶ��̴��� ���� �߾� ��ġ�� ��ȯ�մϴ�.
        GameObject skeleton_Prefab = Instantiate(skeleton, colliderTopCenter, Quaternion.identity);

        // 5�� �Ŀ� �ٽ� ���� �ڷ�ƾ�� ȣ���Ͽ� ���� ��ȯ�մϴ�.
        yield return new WaitForSeconds(5f);
        StartCoroutine(SummonEnemy());
    }
    public void EnergyBall()
    {
        int speed = 5;
        GameObject energyballprefab = Instantiate(energyball, initialHeadPosition, Quaternion.identity);
        Vector3 direction = (player.position - transform.position).normalized;
        energyballprefab.GetComponent<Rigidbody2D>().velocity = direction * speed;
    

    }
    public IEnumerator StoneWind()
    {
        Vector3 swposiiton = new Vector3(-9.5f, -1f, 0f);
        Vector3 swposiiton2 = new Vector3(9.5f, -1f, 0f);
        GameObject StoneWindprefab = Instantiate(stonewind,swposiiton,Quaternion.identity);
        GameObject StoneWindprefab2 = Instantiate(stonewind,swposiiton2,Quaternion.identity);
        yield return new WaitForSeconds(20f);
        Destroy(StoneWindprefab);
        Destroy(StoneWindprefab2);
        StartCoroutine(Think());
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
            TextController.tc.ShowDamageText(collisionPoint, PlayerController.instance.damage);
            Destroy(effectprefab, 2);
            currentHealth -= 10;
        }
    }

}
