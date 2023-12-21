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
    //private void Jump()
    //{
    //    //애니메이터에 있는 Jump 파라미터를 isJump로 명명
    //    if (isDead)
    //    {
    //        // 사망 시 처리를 더 이상 진행하지 않고 종료
    //        return;
    //    }
    //    //Jump 버튼을 눌렀을 때 이고 isJump false 일때 발동!
    //    if (Input.GetButtonDown("Jump") && jumpCount < 2 && !isWall)
    //    {
    //        jumpCount++;
    //        isGround = false;
    //        PRigidBody.velocity = Vector2.zero;
    //        //플레이어의 리지드 바디에  Vector2.up 방향으로 jumpForce 힘만큼 힘을 준다.
    //        PRigidBody.velocity = (Vector2.up * jumpForce);
    //        //애니메이터 isJump는 트루!
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
