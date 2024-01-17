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
    public GameObject triggerEffectPrefab; // ����Ʈ ������
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // ��ٸ����� �ϴ� �ð�(��: 3��)
        float delayTime = 3f;

        // �� ĳ���� ���� �� �� �� ���� ���
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
        // ���
        yield return new WaitForSeconds(delay);

        // ������ ����
        dead = false;
    }
    void ColliderCheck()
    {
        Vector2 playerdircetion = distanceX < 0 ? Vector2.left : Vector2.right;
        //�ݶ��̴��� ��ġ ��
        Vector2 colliderPosition = cd.bounds.center;
        //frontVec�̶�� ������ �ݶ��̴��� ��ġ�� + nextMove�� *0.5, y�� ������Ʈ�� y��
        Vector2 frontVec = new Vector2(colliderPosition.x * playerdircetion.x* 2f, transform.position.y);

        //����ȭ�� Raycast�� ��ġ�� �����ִ� ����
        Debug.DrawRay(frontVec, Vector2.down * 3f, new Color(0, 1, 0));
        Debug.DrawRay(frontVec, Vector2.down, new Color(1, 1, 0));
        // ������ ������ ���� ����ĳ��Ʈ�� ���ִ� ����
        RaycastHit2D CheckGround = Physics2D.Raycast(frontVec, Vector2.down, 0f, LayerMask.GetMask("Ground"));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 3f, LayerMask.GetMask("Ground"));
        RaycastHit2D rayWHit = Physics2D.Raycast(frontVec, Vector3.down, 0f, LayerMask.GetMask("Wall"));
        //���� ����ĳ��Ʈ�� ���ٸ�(���� ���̻� ���ٸ�)
        //if (CheckGround.collider != null)
        //{
        //    Debug.Log("Vector2.right �׶��忡 �浹 �����");
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
        // �÷��̾�� ���� ��� ��ġ ���
        Vector3 playerDirection = player.position - transform.position;
        // x �ุ ����Ͽ� ȸ�� ���� ����
        if (player.position.x < transform.position.x)
        {
            spr.flipX = false ;// ���� ������ �ٶ�
        }
        else
        {
            spr.flipX = true; // ������ ������ �ٶ�
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
