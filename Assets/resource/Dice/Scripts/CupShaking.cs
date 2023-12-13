using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CupShaking : MonoBehaviour
{
    Animator animator;
    public static CupShaking cupshaking;
    float rotationTime = 0f;
    float rotationTimer = 0;
    bool rotate = false;
    Rigidbody mugRb;  // �ӱ����� ������ٵ�
    Vector3 mugDir;  // �ӱ����� �̵� ����
    float shakeTime = 0.1f;  // �ӱ����� �̵���ų �ð�
    float shakeTimer = 0;  // �ӱ��� �̵� Ÿ�̸�
    bool shake = false;  // �ӱ��� �̵� Ʈ����
    float shakeSpeed = 0.5f;  // �ӱ��� �̵� �ӵ�
    int shakeCount = 0;
    bool shakeDelay = false;
    float delayTime = 1.0f;
    float delayTimer = 0;
    public static int rollChance;  // �ֻ����� ���� �� �ִ� Ƚ��
    
    //public TMP_Text rollChanceText;

    //public AudioSource shakeAudioSource;
    //public AudioClip shakeAudioClip;

    void Start()
    {
        animator = GetComponent<Animator>();
        cupshaking = this;
        mugRb = GetComponent<Rigidbody>(); // �ӱ����� Rigidbody ��������
        mugDir = Vector3.zero; // �ʱ�ȭ
        rollChance = 3; // �ʱ� �ֻ��� ���� �� �ִ� Ƚ�� ����
        
    }

    void Update()
    {
    
        //rollChanceText.text = "Roll Chance : " + rollChance.ToString(); // �ֻ��� ���� �� �ִ� Ƚ���� UI�� ǥ��

        // Space Ű�� ������ �ӱ����� ȸ����Ŵ
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.gamemanager.throwPhase)
        {
            Debug.Log(GameManager.gamemanager.throwPhase+" ����");
            if (rotate == false)
            {
                if (rollChance > 0)
                {
                    rotate = true;
                    rotationTimer = rotationTime;
                    shakeCount = 0;
                    rollChance--;
                }
                GameManager.gamemanager.throwPhase = false;
            }
           
        }

        // S Ű�� ������ �ӱ����� ��� �ֻ��� ���� �� �ִ� Ƚ���� �ø�
        if (Input.GetKeyDown(KeyCode.S) && GameManager.gamemanager.throwPhase)
        {
            if (shake == false )
            {
                shake = true;
                shakeTimer = shakeTime;
                shakeCount++;
                //shakeAudioSource.PlayOneShot(shakeAudioClip);
            }
           
           
        }

        // �ֻ��� ���� �� �ִ� Ƚ���� �ʱ�ȭ�ϴ� ������ ����
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
        // �ӱ��� ȸ�� ó��
        if (rotate)
        {
            // ȸ�� Ÿ�̸Ӹ� ������ �� ����� �ð���ŭ ���ҽ�ŵ�ϴ�.
            rotationTimer += Time.deltaTime;

            // rotationTimer�� 2.6���� 1.8 ���̿� �ִ��� Ȯ���մϴ�.
            if (rotationTimer < 3.6f && rotationTimer > 2.8f)
            {
                //animator.SetBool("diceAfter", true);
                // mugRb(Rigidbody)�� Z���� �߽����� 1�ʿ� 145���� �ӵ��� ȸ����ŵ�ϴ�.
                mugRb.rotation = mugRb.rotation * Quaternion.Euler(0f, 175.0f * Time.deltaTime,  0f);
            }
            else if(rotationTimer >1.5f)
            {
                GameManager.gamemanager.Wall.SetActive(false);
                Dice.dice.RollDice();
            }
            // rotationTimer�� 0.8���� 0 ���̿� �ִ��� Ȯ���մϴ�.
            else if (rotationTimer < 1.4f && rotationTimer > 0.6f)
            {
                // mugRb(Rigidbody)�� Z���� �߽����� 1�ʿ� -145���� �ӵ��� ȸ����ŵ�ϴ�.
                mugRb.rotation = mugRb.rotation * Quaternion.Euler(0f, -175.0f * Time.deltaTime, 0f);
            }
            // rotationTimer�� 0 ���Ͽ� �����ϸ�
            if (rotationTimer >4f)
            {
                // �� �̻��� ȸ���� ���߱� ���� 'rotate'�� false�� �����մϴ�.
                rotate = false;

            }
        }

        // �ӱ��� ���� ó��
        if (shake)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer > 0.05)
            {
                mugDir.x = -1;
                //transform.forward = mugDir;
                mugRb.MovePosition(mugRb.position - mugDir * shakeSpeed);
            }
            else if (shakeTimer < 0.05 && shakeTimer > 0)
            {
                mugDir.x = -1;
               //transform.forward = mugDir;
                mugRb.MovePosition(mugRb.position + mugDir * shakeSpeed);
            }
            else if (shakeTimer < 0)
            {
                shake = false;
            }
        }
    }
}
