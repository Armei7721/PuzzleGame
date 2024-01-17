using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public Collider2D cd;
    public Rigidbody2D rb;
    private float distanceY; 
    private float distanceX;
    public Transform player;
    public float moveSpeed = 1f;
    public SpriteRenderer spr;
    public bool dead = false;
    
    public Animator animator;
    public GameObject triggerEffectPrefab; // 이펙트 프리팹
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // 기다리고자 하는 시간(예: 3초)
        float delayTime = 3f;

        // 적 캐릭터 생성 후 몇 초 동안 대기
        StartCoroutine(WaitAndStartMovement(delayTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            Distance();
            ColliderCheck();
            LookAtPlayer();
            Move();
        }
    }
    public void Move()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }
    void Distance()
    {

        distanceY = Vector2.Distance(new Vector2(0f, player.position.y), new Vector2(0f, transform.position.y)); ;
        distanceX = Vector2.Distance(new Vector2( player.position.x,0f), new Vector2(transform.position.x,0f)); ;
    }
    public void Walk()
    {
        rb.velocity = new Vector2(0f, 0f);
        animator.SetBool("Walk", true);
    }
    IEnumerator WaitAndStartMovement(float delay)
    {
        // 대기
        yield return new WaitForSeconds(delay);

        // 움직임 시작
        dead = false;
    }
    void ColliderCheck()
    {
        Vector2 playerdircetion = distanceX < 0 ? Vector2.left : Vector2.right;
        //콜라이더의 위치 값
        Vector2 colliderPosition = cd.bounds.center;
        //frontVec이라는 변수에 콜라이더의 위치값 + nextMove값 *0.5, y는 오브젝트의 y값
        Vector2 frontVec = new Vector2(colliderPosition.x * playerdircetion.x* 2f, transform.position.y);

        //개발화면 Raycast의 위치를 보여주는 로직
        Debug.DrawRay(frontVec, Vector2.down * 3f, new Color(0, 1, 0));
        Debug.DrawRay(frontVec, Vector2.down, new Color(1, 1, 0));
        // 보이진 않지만 실제 레이캐스트를 쏴주는 로직
        RaycastHit2D CheckGround = Physics2D.Raycast(frontVec, Vector2.down, 0f, LayerMask.GetMask("Ground"));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 3f, LayerMask.GetMask("Ground"));
        RaycastHit2D rayWHit = Physics2D.Raycast(frontVec, Vector3.down, 0f, LayerMask.GetMask("Wall"));
        //만약 레이캐스트가 없다면(땅이 더이상 없다면)
        //if (CheckGround.collider != null)
        //{
        //    Debug.Log("Vector2.right 그라운드에 충돌 날경우");
        //    nextMove *= -1;
        //}
        //if (rayWHit.collider != null)
        //{
        //    nextMove *= -1;
        //}

        //if (rayHit.collider == null)
        //{
        //    nextMove *= -1;
        //}
        //else if (rayHit.collider != null)
        //{
        //    return;
        //}

    }
    void LookAtPlayer()
    {
        // 플레이어와 적의 상대 위치 계산
        Vector3 playerDirection = player.position - transform.position;
        // x 축만 고려하여 회전 방향 결정
        if (player.position.x < transform.position.x)
        {
            spr.flipX = false ;// 왼쪽 방향을 바라봄
        }
        else
        {
            spr.flipX = true; // 오른쪽 방향을 바라봄
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Attack"))
        {
            StartCoroutine(Dead());
        }
    }
    IEnumerator Dead()
    {
        gameObject.layer = 12;
        animator.SetTrigger("Death");
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
