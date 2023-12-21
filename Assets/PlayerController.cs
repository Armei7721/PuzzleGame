using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D PRigidBody;
    public float speed=5;
    public int jumpCount=0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PRigidBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
    //private void Jump()
    //{
    //    //�ִϸ����Ϳ� �ִ� Jump �Ķ���͸� isJump�� ���
    //    if (isDead)
    //    {
    //        // ��� �� ó���� �� �̻� �������� �ʰ� ����
    //        return;
    //    }
    //    //Jump ��ư�� ������ �� �̰� isJump false �϶� �ߵ�!
    //    if (Input.GetButtonDown("Jump") && jumpCount < 2 && !isWall)
    //    {
    //        jumpCount++;
    //        isGround = false;
    //        PRigidBody.velocity = Vector2.zero;
    //        //�÷��̾��� ������ �ٵ�  Vector2.up �������� jumpForce ����ŭ ���� �ش�.
    //        PRigidBody.velocity = (Vector2.up * jumpForce);
    //        //�ִϸ����� isJump�� Ʈ��!
    //        animator.SetTrigger("Jump");
    //    }
    //    if (PRigidBody.velocity.y < 0)
    //    {
    //        if (isGround == false)
    //        {
    //            animator.SetBool("Hovering", true);
    //        }
    //    }
    //}

}
