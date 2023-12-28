using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlayerController : MonoBehaviour
{
    public bool canAttack;
    public float attackCooldown = 0.6f;
    public float special_attackCooldown = 1.5f;
    public Animator animator;
    public Rigidbody2D PRigidBody;
    public float speed = 5;
    public int jumpCount = 0;
    bool isGround = true;
    bool isDead;
    bool attack = false;


    public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        animator = GetComponent<Animator>();
        PRigidBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Dig());
        StartCoroutine(Attack());
        StartCoroutine(Special_Attack());
        Move();
        Jump();
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
            animator.SetBool("Attack", true);
            PRigidBody.velocity = new Vector2(0f,PRigidBody.velocity.y);
            yield return new WaitForSeconds(0.2f);
            animator.SetBool("Attack", false);
            yield return new WaitForSeconds(attackCooldown);

            canAttack = true;
        }
    }
    public IEnumerator Special_Attack()
    {
        if(Input.GetKeyDown(KeyCode.X) && canAttack)
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
            canAttack = false;
            animator.SetBool("Dig", true);

            yield return new WaitForSeconds(0.2f);
            animator.SetBool("Dig", false);
            Vector3Int playerGridPosition = tilemap.WorldToCell(transform.position + Vector3.down);
            Vector3Int cellPosition = tilemap.WorldToCell(transform.position + Vector3.down);
            Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPosition);
            Debug.DrawRay(cellCenter, Vector3.up * 0.5f, Color.red);
            TileBase tile = tilemap.GetTile(playerGridPosition);

            
            if (tile != null)
            {
                // Ÿ�ϸʿ��� �ش� ��ġ�� Ÿ���� �����մϴ�.
                tilemap.SetTile(playerGridPosition, null);
            }

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
        if (Input.GetButtonDown("Jump") && jumpCount<2)
        {
            int jumpForce = 5;
            jumpCount++;
            isGround = false;
            PRigidBody.velocity = Vector2.zero;
            //�÷��̾��� ������ �ٵ�  Vector2.up �������� jumpForce ����ŭ ���� �ش�.
            PRigidBody.velocity = (Vector2.up * jumpForce);
            //�ִϸ����� isJump�� Ʈ��!
            animator.SetTrigger("Jump");
        }

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("ground"))
        {
            jumpCount = 0;
            isGround = true;
        }
    }
}
