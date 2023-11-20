using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CupShaking : MonoBehaviour
{
    float rotationTime = 0f;
    float rotationTimer = 0;
    bool rotate = false;
    Rigidbody mugRb;  // 머그컵의 리지드바디
    Vector3 mugDir;  // 머그컵의 이동 방향
    float shakeTime = 0.1f;  // 머그컵을 이동시킬 시간
    float shakeTimer = 0;  // 머그컵 이동 타이머
    bool shake = false;  // 머그컵 이동 트리거
    float shakeSpeed = 0.5f;  // 머그컵 이동 속도
    int shakeCount = 0;
    bool shakeDelay = false;
    float delayTime = 1.0f;
    float delayTimer = 0;
    public static int rollChance;  // 주사위를 굴릴 수 있는 횟수
    public GameObject Wall;
    //public TMP_Text rollChanceText;

    //public AudioSource shakeAudioSource;
    //public AudioClip shakeAudioClip;

    void Start()
    {
        mugRb = GetComponent<Rigidbody>(); // 머그컵의 Rigidbody 가져오기
        mugDir = Vector3.zero; // 초기화
        rollChance = 3; // 초기 주사위 굴릴 수 있는 횟수 설정
    }

    void Update()
    {
      
        //rollChanceText.text = "Roll Chance : " + rollChance.ToString(); // 주사위 굴릴 수 있는 횟수를 UI에 표시

        // Space 키를 누르면 머그컵을 회전시킴
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rotate == false)
            {
                Wall.SetActive(true);
                if (rollChance > 0)
                {
                    rotate = true;
                    rotationTimer = rotationTime;
                    shakeCount = 0;
                    rollChance--;
                }
            }
        }

        // S 키를 누르면 머그컵을 흔들어서 주사위 굴릴 수 있는 횟수를 늘림
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (shake == false )
            {
                shake = true;
                shakeTimer = shakeTime;
                shakeCount++;
                //shakeAudioSource.PlayOneShot(shakeAudioClip);
            }
            //if (shakeDelay == false && shakeCount == 7)
            //{
            //    shakeDelay = true;
            //    delayTimer = delayTime;
            //}
        }

        // 주사위 굴릴 수 있는 횟수를 초기화하는 딜레이 설정
        if (shakeDelay)
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer < 0)
            {
                shakeCount = 0;
                shakeDelay = false;
            }
        }
    }

    void FixedUpdate()
    {
        // 머그컵 회전 처리
        if (rotate)
        {
            // 회전이 시작되면 'Wall' 게임 오브젝트를 비활성화합니다.
            Wall.SetActive(false);

            // 회전 타이머를 프레임 간 경과된 시간만큼 감소시킵니다.
            rotationTimer += Time.deltaTime;

            // rotationTimer가 2.6에서 1.8 사이에 있는지 확인합니다.
            if (rotationTimer < 3.6 && rotationTimer > 2.8)
            {
                // mugRb(Rigidbody)를 Z축을 중심으로 1초에 145도의 속도로 회전시킵니다.
                mugRb.rotation = mugRb.rotation * Quaternion.Euler(0f, 0f, -145.0f * Time.deltaTime);
            }
            // rotationTimer가 0.8에서 0 사이에 있는지 확인합니다.
            else if (rotationTimer < 0.8 && rotationTimer > 0)
            {
                // mugRb(Rigidbody)를 Z축을 중심으로 1초에 -145도의 속도로 회전시킵니다.
                mugRb.rotation = mugRb.rotation * Quaternion.Euler(0f, 0f, 145.0f * Time.deltaTime);
            }
            // rotationTimer가 0 이하에 도달하면
            else if (rotationTimer >3.6)
            {
                // 더 이상의 회전을 멈추기 위해 'rotate'를 false로 설정합니다.
                rotate = false;
            }
        }

        // 머그컵 흔들기 처리
        if (shake)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer > 0.05)
            {
                mugDir.x = -1;
                transform.forward = mugDir;
                mugRb.MovePosition(mugRb.position - mugDir * shakeSpeed);
            }
            else if (shakeTimer < 0.05 && shakeTimer > 0)
            {
                mugDir.x = -1;
                transform.forward = mugDir;
                mugRb.MovePosition(mugRb.position + mugDir * shakeSpeed);
            }
            else if (shakeTimer < 0)
            {
                shake = false;
            }
        }
    }
}
