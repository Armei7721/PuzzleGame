using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public GameObject[] childlist;
    public bool canAttack;
    public float attackCooldown = 0.6f;
    public float special_attackCooldown = 1.5f;
    public Animator animator;
    public Rigidbody2D PRigidBody;
    public CapsuleCollider2D cacd2d;
    public float speed = 5;
    public int jumpCount = 0;
    public float hittime;

    public GameObject triggerEffectPrefab; // 이펙트 프리팹


    bool isGround;
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
    public int damage;
    private float currentHealth;
    public Slider player_hpBar;
    public TextMeshProUGUI hp_txt;

    public float fallThroughPlatformDelay = 0.1f; // 플랫폼 아래로 내려가기 위한 딜레이
    public bool canFallThroughPlatform = false;
    public GameObject dig_Effect;
    public bool isUltimateActive = false;
    public GameObject allGameObjects;
    public GameObject shovelknight_Ex_Spin;
    public GameObject shovelknight_Ex_Spin_Effect;
    public float spawnDistanceRange = 5f;
    float aniSpeed;
    public GameObject shovelKnight_Ex_SpinPrefab;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        allGameObjects = GameObject.Find("Boss_Ent");
        cacd2d = GetComponent<CapsuleCollider2D>();
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
        damage = Random.Range(10, 15);
        player_hpBar.value = currentHealth;
        hp_txt.text = "HP: " + currentHealth.ToString("0") + " / " + max_hp.ToString("0");
        if (!isDead && !isUltimateActive)
        {
            if (!hitting)
            {
                //DownJump();
                Move();
                Jump();
                Ghost();
                StartCoroutine(ExSkill());
                StartCoroutine(Dig());
                StartCoroutine(Attack());
                StartCoroutine(Special_Attack());
                
            }
            Dead();
            HitDuring();
        }
        Hpcontroller();
    }
    public void Hpcontroller()
    {
        if(currentHealth<0)
        {
            currentHealth = 0;
        }
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
            float attackCooltime = animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(attackCooltime);

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
            float attackCooltime = animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(attackCooltime);

            canAttack = true;
        }
    }
    public IEnumerator Dig()
    {
        if (Input.GetKeyDown(KeyCode.C) &&  Input.GetKey(KeyCode.DownArrow))
        {
           
            Vector3Int playerGridPosition = tilemap.WorldToCell(transform.position + Vector3.down);
            Vector3Int cellPosition = tilemap.WorldToCell(transform.position + Vector3.down);
            Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPosition);
            TileBase tile = tilemap.GetTile(playerGridPosition);

           
            if (tile != null)
            {
                GameObject dig_Effect_Prefab = Instantiate(dig_Effect, transform.position, Quaternion.identity);
                Destroy(dig_Effect_Prefab, 0.5f);
               
                canAttack = false;
                animator.SetBool("Dig", true);
                // 타일맵에서 해당 위치의 타일을 삭제합니다.
                
                yield return new WaitForSeconds(0.2f);
                transform.position = cellCenter;
                animator.SetBool("Dig", false);
                tilemap.SetTile(playerGridPosition, null);
            
            }

            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) &&  (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
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
                transform.position = cellCenter;
                yield return new WaitForSeconds(attackCooldown);
                canAttack = true;
                // 타일맵에서 해당 위치의 타일을 삭제합니다.

                

            }

            
        }
        else if (Input.GetKeyDown(KeyCode.C) && (Input.GetKey(KeyCode.UpArrow)))
        {
            canAttack = false;
           

            // 현재 플레이어의 그리드 위치 가져오기
            Vector3Int playerGridPosition = tilemap.WorldToCell(transform.position);

            // 플레이어가 있는 열(column)의 위쪽에 있는 행(row)에 대해 반복
            for (int row = playerGridPosition.y + 1; row < tilemap.cellBounds.yMax; row++)
            {
                Vector3Int cellPosition = new Vector3Int(playerGridPosition.x, row, playerGridPosition.z);
                TileBase tile = tilemap.GetTile(cellPosition);

                if (tile != null)
                {
                    animator.SetBool("Dig_Up", true);
                    PRigidBody.velocity = Vector2.up * 10;
                    // 타일이 존재하면 파괴
                    tilemap.SetTile(cellPosition, null);
                    yield return new WaitForSeconds(0.2f);
                }
               

            
            }
           
           
            animator.SetBool("Dig_Up", false);

            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
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
            int jumpForce = 12;
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
       
            // 피격 상태일 때의 처리
            if (isHit && currentHealth>0)
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
    public IEnumerator ExSkill()
    {
        Collider2D playerCollider = GetComponent<Collider2D>();
        float rayLength = playerCollider.bounds.extents.y + 0.1f; // 레이의 길이 설정
        RaycastHit2D hit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground"));
        if (Input.GetKeyDown(KeyCode.V)&& !isUltimateActive && isGround &&hit.collider!=null ){
       
            PRigidBody.velocity = (Vector2.up * 10f);
            // 필살기 사용 중 플래그 설정
            isUltimateActive = true;
            animator.SetTrigger("ExSkill");
            // 플레이어 이외의 모든 게임 오브젝트를 찾아 정지 또는 비활성화
            
            ghost.makeGhost = true;
            
            // 플레이어를 제외한 모든 게임 오브젝트를 정지 또는 비활성화
            if (allGameObjects == GameObject.Find("Boss_Ent"))
                {
                    
                    Animator animator =allGameObjects.GetComponent<Animator>();
                    if (animator != null)
                    {
                        aniSpeed = animator.speed;
                        animator.speed = 0;
                    
                    }             
                    // 정지 또는 비활성화 로직을 추가해야 합니다.
                    // 예를 들어, Rigidbody의 경우는 rigidbody.velocity = Vector3.zero; 로 정지할 수 있습니다.
                    // 또는 게임 오브젝트를 비활성화하는 경우는 gameObject.SetActive(false); 로 비활성화할 수 있습니다.

                }

            GameObject skill = new GameObject("Skill1");
            GameObject shovelknight_Ex_Spin_Effect_Prefab = Instantiate(shovelknight_Ex_Spin_Effect, transform.position, Quaternion.identity);
            shovelknight_Ex_Spin_Effect_Prefab.transform.parent = skill.transform;
 
            for (int i = 0; i < 10; i++)
            {
                SpawnPrefabRandomPosition();
                shovelKnight_Ex_SpinPrefab.transform.parent = skill.transform;
            }
            yield return new WaitForSeconds(0.5f);
            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            Debug.Log(animationLength);
            // 필살기 지속 시간 동안 게임 로직 수행 (예: 특수 효과, 애니메이션 등)
            yield return new WaitForSeconds(animationLength);
            

            // 필살기 종료 시 플래그 해제
            isUltimateActive = false;
            ghost.makeGhost = false;
            Destroy(skill);
            // 모든 게임 오브젝트를 다시 활성화
            if (allGameObjects == GameObject.Find("Boss_Ent"))
            {

                Animator animator = allGameObjects.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.speed = aniSpeed;
                    Boss_Controller.BS.currentHealth -= 100;
                }
                // 정지 또는 비활성화 로직을 추가해야 합니다.
                // 예를 들어, Rigidbody의 경우는 rigidbody.velocity = Vector3.zero; 로 정지할 수 있습니다.
                // 또는 게임 오브젝트를 비활성화하는 경우는 gameObject.SetActive(false); 로 비활성화할 수 있습니다.

            }
            

        }
    }
    void SpawnPrefabRandomPosition()
    {
        // 현재 카메라의 위치와 방향을 가져옴
        Camera mainCamera = Camera.main;
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 cameraForward = mainCamera.transform.forward;

        // 카메라 앞쪽으로 일정한 거리만큼 떨어진 위치 계산
        Vector3 spawnPosition = cameraPosition + cameraForward * Random.Range(0f, spawnDistanceRange);

        // 랜덤한 위치로부터 x, y, z 각각에 대해 랜덤한 값을 더해줌
        spawnPosition += new Vector3(Random.Range(-spawnDistanceRange, spawnDistanceRange),
                                     Random.Range(-spawnDistanceRange, spawnDistanceRange),0f);

        // 프리팹을 해당 위치에 생성
        shovelKnight_Ex_SpinPrefab = Instantiate(shovelknight_Ex_Spin, spawnPosition, Quaternion.identity);

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
    private IEnumerator DashCooldown()
    {
        dashTime = 0;
        // 대시 쿨다운 시간(예: 1초) 만큼 대기
        yield return new WaitForSeconds(1f);

        canDash = true; // 대시 쿨다운 종료 후 다시 대시 가능 상태로 변경
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead)
        {
            if ((collision.collider.CompareTag("ground") || collision.collider.CompareTag("scaff")) && collision.contacts[0].normal.y > 0)
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
    }
    public void OnTriggerEnter2D(Collider2D collision )
    {

        if (!isDead)
        {
            if (collision.CompareTag("bullet") && !isHit)
            {
                isHit = true;
                currentHealth -= 10;
            }
            else if (collision.CompareTag("enemy") && !isHit)
            {
                isHit = true;
                currentHealth -= 10;
            }
        }
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("scaff") && collision.contacts[0].normal.y > 0)
        {
            StartCoroutine(DownJump());
            isGround = true;
        }
        else if (collision.collider.CompareTag("ground") && collision.contacts[0].normal.y > 0)
        {
            isGround = true;
        }
        else { isGround = false; }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 발판과 더 이상 충돌하지 않을 때 플랫폼 아래로 내려가기 허용 해제
        if (collision.collider.CompareTag("scaff"))
        {
            canFallThroughPlatform = false;
        }
    }
    public void Dead()
    {
        if(currentHealth<= 0)
        {
            animator.SetTrigger("Death");
            isDead = true;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        }
    }

    IEnumerator DownJump()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            canFallThroughPlatform = true;
        }
        if (canFallThroughPlatform)
        {
            cacd2d.isTrigger = true;

            yield return new WaitForSeconds(0.5f);
                canFallThroughPlatform = false;
                cacd2d.isTrigger = false;
            
        }
        

    }
}
