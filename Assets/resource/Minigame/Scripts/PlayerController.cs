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

    public GameObject triggerEffectPrefab; // ����Ʈ ������


    bool isGround;
    bool isDead = false;
    bool attack = false;
    bool baldong = false;
    bool hitting = false;
    bool isHit = false;
    
    public Tilemap tilemap;


    [Header("�뽬����")]
    public float dashTime;
    public float maxDashTime = 0.2f;
    public bool isDash;
    public Ghost ghost;
    private float playerMoveSpeed = 15;
    public bool canDash = true;

    [Header("�÷��̾� �ɷ�ġ ����")]
    private float max_hp = 100;
    public int damage;
    private float currentHealth;
    public Slider player_hpBar;
    public TextMeshProUGUI hp_txt;

    public float fallThroughPlatformDelay = 0.1f; // �÷��� �Ʒ��� �������� ���� ������
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
    //�÷��̾� ������ ��Ʈ��
    private void Move()
    {

        float h = Input.GetAxisRaw("Horizontal");
        //�������� ���� ������ ���� �ӷ� ����
        //Horizontal ������ Ű�� ������ ������
        if (Input.GetButtonUp("Horizontal"))
        {
            //�÷��̾��� ������ٵ� ���� �ӵ� = �÷��̾� ������ٵ� ����ӷ��� ���� ũ�� 1 * 0.5�̴�.
            PRigidBody.velocity = new Vector2(PRigidBody.velocity.normalized.x * 0.5f, PRigidBody.velocity.y);
        }
        // ���� ���� �ִ� ���ǵ� ����
        PRigidBody.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        if (PRigidBody.velocity.x > speed)
        {   // //�÷��̾��� ����ӵ� = ���� 2 ������(maxSpeed, �÷��̾��� ���� y ��)���� ����
            PRigidBody.velocity = new Vector2(speed, PRigidBody.velocity.y);
        }
        // ���� ���� �ִ� ���ǵ� ����
        else if (PRigidBody.velocity.x < -speed)
        {   //�÷��̾��� ����ӵ� = ���� 2 ������(-maxSpeed, �÷��̾��� ���� y ��)���� ����
            PRigidBody.velocity = new Vector2(-speed, PRigidBody.velocity.y);

        }
        //Horizontal ��ư�� ��������!
        if (Input.GetButton("Horizontal"))
        {   //�÷��̾��� X�� �������� �����´�. ����? Horizontal�� -1�϶�
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

        }
        //�÷��̾��� ���� �ӷ� x�� ũ�⸦ 1�ʱ�ȭ�� ���� 0.3.���� ������
        if (Mathf.Abs(PRigidBody.velocity.normalized.x) < 0.3)
        {   //�ִϸ������� Idle�� true���� �ٲ�

            animator.SetBool("Run", false);
        }
        else
        {   //�ִϸ������� idle�� fasle�� �ٲ�

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
                // Ÿ�ϸʿ��� �ش� ��ġ�� Ÿ���� �����մϴ�.
                
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
                // Ÿ�ϸʿ��� �ش� ��ġ�� Ÿ���� �����մϴ�.

                

            }

            
        }
        else if (Input.GetKeyDown(KeyCode.C) && (Input.GetKey(KeyCode.UpArrow)))
        {
            canAttack = false;
           

            // ���� �÷��̾��� �׸��� ��ġ ��������
            Vector3Int playerGridPosition = tilemap.WorldToCell(transform.position);

            // �÷��̾ �ִ� ��(column)�� ���ʿ� �ִ� ��(row)�� ���� �ݺ�
            for (int row = playerGridPosition.y + 1; row < tilemap.cellBounds.yMax; row++)
            {
                Vector3Int cellPosition = new Vector3Int(playerGridPosition.x, row, playerGridPosition.z);
                TileBase tile = tilemap.GetTile(cellPosition);

                if (tile != null)
                {
                    animator.SetBool("Dig_Up", true);
                    PRigidBody.velocity = Vector2.up * 10;
                    // Ÿ���� �����ϸ� �ı�
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
        //�ִϸ����Ϳ� �ִ� Jump �Ķ���͸� isJump�� ���
        if (isDead)
        {
            // ��� �� ó���� �� �̻� �������� �ʰ� ����
            return;
        }
        //Jump ��ư�� ������ �� �̰� isJump false �϶� �ߵ�!
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            int jumpForce = 12;
            jumpCount++;

            PRigidBody.velocity = Vector2.zero;
            //�÷��̾��� ������ �ٵ�  Vector2.up �������� jumpForce ����ŭ ���� �ش�.
            PRigidBody.velocity = (Vector2.up * jumpForce);
            animator.SetBool("isGround", false);
            //�ִϸ����� isJump�� Ʈ��!
            animator.SetBool("Jump", true);
        }

    }
    public void HitDuring()
    {
       
            // �ǰ� ������ ���� ó��
            if (isHit && currentHealth>0)
            {
                float hitDuration = 1;
                gameObject.layer = 12;
                hitting = true;
                animator.SetBool("hurt", hitting);
                hittime += Time.deltaTime;
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                Vector2 knockBackDirection = -transform.right; // �˹�Ǵ� ���� (ĳ������ ����)
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
                return; // �ǰ� ������ ���� �̵� �� ������ ���� ����
            }
            isHit = false;
    }
    public IEnumerator ExSkill()
    {
        Collider2D playerCollider = GetComponent<Collider2D>();
        float rayLength = playerCollider.bounds.extents.y + 0.1f; // ������ ���� ����
        RaycastHit2D hit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground"));
        if (Input.GetKeyDown(KeyCode.V)&& !isUltimateActive && isGround &&hit.collider!=null ){
       
            PRigidBody.velocity = (Vector2.up * 10f);
            // �ʻ�� ��� �� �÷��� ����
            isUltimateActive = true;
            animator.SetTrigger("ExSkill");
            // �÷��̾� �̿��� ��� ���� ������Ʈ�� ã�� ���� �Ǵ� ��Ȱ��ȭ
            
            ghost.makeGhost = true;
            
            // �÷��̾ ������ ��� ���� ������Ʈ�� ���� �Ǵ� ��Ȱ��ȭ
            if (allGameObjects == GameObject.Find("Boss_Ent"))
                {
                    
                    Animator animator =allGameObjects.GetComponent<Animator>();
                    if (animator != null)
                    {
                        aniSpeed = animator.speed;
                        animator.speed = 0;
                    
                    }             
                    // ���� �Ǵ� ��Ȱ��ȭ ������ �߰��ؾ� �մϴ�.
                    // ���� ���, Rigidbody�� ���� rigidbody.velocity = Vector3.zero; �� ������ �� �ֽ��ϴ�.
                    // �Ǵ� ���� ������Ʈ�� ��Ȱ��ȭ�ϴ� ���� gameObject.SetActive(false); �� ��Ȱ��ȭ�� �� �ֽ��ϴ�.

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
            // �ʻ�� ���� �ð� ���� ���� ���� ���� (��: Ư�� ȿ��, �ִϸ��̼� ��)
            yield return new WaitForSeconds(animationLength);
            

            // �ʻ�� ���� �� �÷��� ����
            isUltimateActive = false;
            ghost.makeGhost = false;
            Destroy(skill);
            // ��� ���� ������Ʈ�� �ٽ� Ȱ��ȭ
            if (allGameObjects == GameObject.Find("Boss_Ent"))
            {

                Animator animator = allGameObjects.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.speed = aniSpeed;
                    Boss_Controller.BS.currentHealth -= 100;
                }
                // ���� �Ǵ� ��Ȱ��ȭ ������ �߰��ؾ� �մϴ�.
                // ���� ���, Rigidbody�� ���� rigidbody.velocity = Vector3.zero; �� ������ �� �ֽ��ϴ�.
                // �Ǵ� ���� ������Ʈ�� ��Ȱ��ȭ�ϴ� ���� gameObject.SetActive(false); �� ��Ȱ��ȭ�� �� �ֽ��ϴ�.

            }
            

        }
    }
    void SpawnPrefabRandomPosition()
    {
        // ���� ī�޶��� ��ġ�� ������ ������
        Camera mainCamera = Camera.main;
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 cameraForward = mainCamera.transform.forward;

        // ī�޶� �������� ������ �Ÿ���ŭ ������ ��ġ ���
        Vector3 spawnPosition = cameraPosition + cameraForward * Random.Range(0f, spawnDistanceRange);

        // ������ ��ġ�κ��� x, y, z ������ ���� ������ ���� ������
        spawnPosition += new Vector3(Random.Range(-spawnDistanceRange, spawnDistanceRange),
                                     Random.Range(-spawnDistanceRange, spawnDistanceRange),0f);

        // �������� �ش� ��ġ�� ����
        shovelKnight_Ex_SpinPrefab = Instantiate(shovelknight_Ex_Spin, spawnPosition, Quaternion.identity);

    }
    public void Childlist()
    {
        GameObject parentObject = GameObject.Find("Player");
        if (parentObject != null)
        {
            Transform[] children = parentObject.GetComponentsInChildren<Transform>(true);

            // �ڽ� ������Ʈ�鸸 �����ϴµ�, �θ� �ڽ��� �����ϱ� ���� �迭 ũ�⸦ �����մϴ�.
            childlist = new GameObject[children.Length - 1];

            // ù ��° ��Ҵ� �θ� �ڽ��̹Ƿ� �ε����� 1���� �����Ͽ� �ڽ� ������Ʈ���� �����մϴ�.
            for (int i = 1; i < children.Length; i++)
            {
                childlist[i - 1] = children[i].gameObject;
            }
        }
        else
        {
            Debug.LogWarning("DicePlane�� ã�� �� �����ϴ�.");
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
            // ��� ���� ó��
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
                // ��ð� ������ ���� �ٽ� ��� �����ϵ��� ����
                ghost.makeGhost = false;
                isDash = false;
                StartCoroutine(DashCooldown());
            }
        
    }
    private IEnumerator DashCooldown()
    {
        dashTime = 0;
        // ��� ��ٿ� �ð�(��: 1��) ��ŭ ���
        yield return new WaitForSeconds(1f);

        canDash = true; // ��� ��ٿ� ���� �� �ٽ� ��� ���� ���·� ����
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
        // ���ǰ� �� �̻� �浹���� ���� �� �÷��� �Ʒ��� �������� ��� ����
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
