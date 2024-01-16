using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public GameObject[] childlist;
    public bool canAttack;
    public float attackCooldown = 0.6f;
    public float special_attackCooldown = 1.5f;
    public Animator animator;
    public Rigidbody2D PRigidBody;
    public float speed = 5;
    public int jumpCount = 0;
    public float hittime;

    bool isGround = true;
    bool isDead = false;
    bool attack = false;
    bool baldong = false;
    bool hitting = false;
    bool isHit = false;
    public Tilemap tilemap;


    [Header("대쉬관련")]
    public float dashTime;
    public float maxDashTime = 0.2f;
    public bool isDash;
    public Ghost ghost;
    private float playerMoveSpeed = 15;
    public bool canDash = true;

    [Header("플레이어 능력치 관련")]
    private float max_hp = 100;
    private float currentHealth;
    public Slider player_hpBar;
    public TextMeshProUGUI hp_txt;
    // Start is called before the first frame update
    void Start()
    {
        player_hpBar.maxValue = max_hp;
        currentHealth = max_hp;
        canAttack = true;
        animator = GetComponent<Animator>();
        PRigidBody = GetComponent<Rigidbody2D>();
        Childlist();
    }
    
    // Update is called once per frame
    void Update()
    {  
        player_hpBar.value = currentHealth;
        hp_txt.text = "HP: " + currentHealth.ToString("0") + " / " + max_hp.ToString("0");
        if (!hitting)
        {
            Move();
            Jump();
            Ghost();
            StartCoroutine(Dig());
            StartCoroutine(Attack());
            StartCoroutine(Special_Attack());
            
        }
        Dead();
        HitDuring();
       
    }
    //플레이어 움직임 컨트롤
    private void Move()
    {

        float h = Input.GetAxisRaw("Horizontal");
        //공기저항 같은 힘으로 인한 속력 멈춤
        //Horizontal 방향의 키를 눌렀다 땠을때
        if (Input.GetButtonUp("Horizontal"))
        {
            //플레이어의 리지드바디 현재 속도 = 플레이어 리지드바디 현재속력의 벡터 크기 1 * 0.5이다.
            PRigidBody.velocity = new Vector2(PRigidBody.velocity.normalized.x * 0.5f, PRigidBody.velocity.y);
        }
        // 우측 방향 최대 스피드 설정
        PRigidBody.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        if (PRigidBody.velocity.x > speed)
        {   // //플레이어의 현재속도 = 벡터 2 생성자(maxSpeed, 플레이어의 현재 y 값)으로 대입
            PRigidBody.velocity = new Vector2(speed, PRigidBody.velocity.y);
        }
        // 좌측 방향 최대 스피드 설정
        else if (PRigidBody.velocity.x < -speed)
        {   //플레이어의 현재속도 = 벡터 2 생성자(-maxSpeed, 플레이어의 현재 y 값)으로 대입
            PRigidBody.velocity = new Vector2(-speed, PRigidBody.velocity.y);

        }
        //Horizontal 버튼을 눌렀을때!
        if (Input.GetButton("Horizontal"))
        {   //플레이어의 X의 방향으로 뒤집는다. 언제? Horizontal이 -1일때
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

        }
        //플레이어의 현재 속력 x의 크기를 1초기화한 값이 0.3.보다 작을때
        if (Mathf.Abs(PRigidBody.velocity.normalized.x) < 0.3)
        {   //애니메이터의 Idle을 true으로 바꿈

            animator.SetBool("Run", false);
        }
        else
        {   //애니메이터의 idle를 fasle로 바꿈

            animator.SetBool("Run", true);
        }

    }

    public IEnumerator Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z) && canAttack)
        {
            canAttack = false;
            attack = true;
            animator.SetBool("Attack", attack);
            PRigidBody.velocity = new Vector2(0f, PRigidBody.velocity.y);
            yield return new WaitForSeconds(0.2f);
            attack = false; ;
            animator.SetBool("Attack", attack);
            yield return new WaitForSeconds(attackCooldown);

            canAttack = true;
        }
    }
    public void ColliderControl()
    {
        if (childlist[0].activeSelf)
        {
            childlist[0].SetActive(false);
        }
        else
        {
            childlist[0].SetActive(true);
        }
    }
    public void SpecialColldier()
    {
        if (childlist[1].activeSelf)
        {
            childlist[1].SetActive(false);
        }
        else
        {
            childlist[1].SetActive(true);
        }
    }
    public IEnumerator Special_Attack()
    {
        if (Input.GetKeyDown(KeyCode.X) && canAttack)
        {
            canAttack = false;
            animator.SetBool("Special_Attack", true);
            yield return new WaitForSeconds(0.2f);
            animator.SetBool("Special_Attack", false);
            yield return new WaitForSeconds(attackCooldown);

            canAttack = true;
        }
    }
    public IEnumerator Dig()
    {
        if (Input.GetKeyDown(KeyCode.C) && canAttack && Input.GetKey(KeyCode.DownArrow))
        {
           
            Vector3Int playerGridPosition = tilemap.WorldToCell(transform.position + Vector3.down);
            Vector3Int cellPosition = tilemap.WorldToCell(transform.position + Vector3.down);
            Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPosition);
            TileBase tile = tilemap.GetTile(playerGridPosition);


            if (tile != null)
            {
                canAttack = false;
                animator.SetBool("Dig", true);
                // 타일맵에서 해당 위치의 타일을 삭제합니다.
                
                yield return new WaitForSeconds(0.2f);
                
                animator.SetBool("Dig", false);
                tilemap.SetTile(playerGridPosition, null);
            }

            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }
        if (Input.GetKeyDown(KeyCode.C) && canAttack && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            Vector3 playerdirection = PRigidBody.velocity.x > 0 ? Vector2.right : Vector2.left;


            Vector3Int playerGridPosition = tilemap.WorldToCell(transform.position + playerdirection);
            Vector3Int cellPosition = tilemap.WorldToCell(transform.position + playerdirection);
            Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPosition);
            TileBase tile = tilemap.GetTile(playerGridPosition);


            if (tile != null)
            {
                canAttack = false;
                animator.SetBool("Dig", true);

                yield return new WaitForSeconds(0.2f);
                animator.SetBool("Dig", false);
                tilemap.SetTile(playerGridPosition, null);
                yield return new WaitForSeconds(attackCooldown);
                canAttack = true;
                // 타일맵에서 해당 위치의 타일을 삭제합니다.
                
            
            }

            
        }
    }
    private void Jump()
    {
        //애니메이터에 있는 Jump 파라미터를 isJump로 명명
        if (isDead)
        {
            // 사망 시 처리를 더 이상 진행하지 않고 종료
            return;
        }
        //Jump 버튼을 눌렀을 때 이고 isJump false 일때 발동!
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            int jumpForce = 10;
            jumpCount++;

            PRigidBody.velocity = Vector2.zero;
            //플레이어의 리지드 바디에  Vector2.up 방향으로 jumpForce 힘만큼 힘을 준다.
            PRigidBody.velocity = (Vector2.up * jumpForce);
            animator.SetBool("isGround", false);
            //애니메이터 isJump는 트루!
            animator.SetBool("Jump", true);
        }

    }
    public void HitDuring()
    {
        if (!isDead)
        {
            // 피격 상태일 때의 처리
            if (isHit)
            {
                float hitDuration = 1;
                gameObject.layer = 12;
                hitting = true;
                animator.SetBool("hurt", hitting);
                hittime += Time.deltaTime;
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                Vector2 knockBackDirection = -transform.right; // 넉백되는 방향 (캐릭터의 뒤쪽)
                float knockBackForce = 2.0f;
                if (hittime < 0.5f)
                {
                    PRigidBody.velocity = Vector2.zero;
                    PRigidBody.velocity = new Vector2(knockBackDirection.x * knockBackForce, knockBackForce);
                }
                if (hittime >= hitDuration)
                {
                    hitting = false;
                    animator.SetBool("hurt", hitting);
                    if (hittime >= hitDuration + 3f)
                    {
                        hitting = false;
                        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
                        gameObject.layer = 11;
                        isHit = false;
                        hittime = 0;
                    }
                }
                return; // 피격 상태일 때는 이동 및 공격을 하지 않음
            }
            isHit = false;
        }
    }
    public void Childlist()
    {
        GameObject parentObject = GameObject.Find("Player");
        if (parentObject != null)
        {
            Transform[] children = parentObject.GetComponentsInChildren<Transform>(true);

            // 자식 오브젝트들만 참조하는데, 부모 자신은 제외하기 위해 배열 크기를 조정합니다.
            childlist = new GameObject[children.Length - 1];

            // 첫 번째 요소는 부모 자신이므로 인덱스를 1부터 시작하여 자식 오브젝트들을 참조합니다.
            for (int i = 1; i < children.Length; i++)
            {
                childlist[i - 1] = children[i].gameObject;
            }
        }
        else
        {
            Debug.LogWarning("DicePlane을 찾을 수 없습니다.");
        }
    }

    public void Ghost()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && PRigidBody.velocity.x != 0)
        {
            ghost.makeGhost = true;

            isDash = true;
        }

        if (isDash)
        {
            // 대시 동작 처리
            Dash();
        }
    }
    private void Dash()
    {
        if (isGround)
        {
            
            dashTime += Time.deltaTime;
            if (PRigidBody.velocity.x < 0)
            {

                PRigidBody.velocity = new Vector2(-playerMoveSpeed, PRigidBody.velocity.y);

                canDash = false;
            }
            else if (PRigidBody.velocity.x > 0)
            {

                PRigidBody.velocity = new Vector2(playerMoveSpeed, PRigidBody.velocity.y);

                canDash = false;
            }
            if (dashTime >= maxDashTime)
            {
                PRigidBody.velocity = new Vector2(0f, 0f);
                // 대시가 끝났을 때만 다시 대시 가능하도록 설정
                ghost.makeGhost = false;
                isDash = false;
                StartCoroutine(DashCooldown());
            }
        }
        else if (!isGround)
        {
            
            dashTime += Time.deltaTime;
            float currentVelocityY = 0f;
            if (PRigidBody.velocity.x < 0)
            {

                PRigidBody.velocity = new Vector2(-playerMoveSpeed * 5 * Time.deltaTime, currentVelocityY);
                canDash = false;
            }
            else if (PRigidBody.velocity.x > 0)
            {

                PRigidBody.velocity = new Vector2(playerMoveSpeed * 5 * Time.deltaTime, currentVelocityY);
                canDash = false;
            }
            if (dashTime >= maxDashTime)
            {
                // 대시가 끝났을 때만 다시 대시 가능하도록 설정
                ghost.makeGhost = false;
                isDash = false;
                StartCoroutine(DashCooldown());
            }
        }
    }
    private IEnumerator DashCooldown()
    {
        dashTime = 0;
        // 대시 쿨다운 시간(예: 1초) 만큼 대기
        yield return new WaitForSeconds(1f);

        canDash = true; // 대시 쿨다운 종료 후 다시 대시 가능 상태로 변경
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            jumpCount = 0;
            animator.SetBool("Jump", false);
            animator.SetBool("isGround", true);
        }
        else if (collision.collider.CompareTag("enemy"))
        {
            isHit = true;
            currentHealth -= 10;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("bullet") &&!isHit)
        {
            isHit = true;
            currentHealth -= 10;
        }
    }
    public void Dead()
    {
        if(currentHealth<= 0)
        {
            animator.SetTrigger("Death");
            isDead = true;
        }
    }
}
