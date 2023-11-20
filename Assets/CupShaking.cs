using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CupShaking : MonoBehaviour
{
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
    public GameObject Wall;
    //public TMP_Text rollChanceText;

    //public AudioSource shakeAudioSource;
    //public AudioClip shakeAudioClip;

    void Start()
    {
        mugRb = GetComponent<Rigidbody>(); // �ӱ����� Rigidbody ��������
        mugDir = Vector3.zero; // �ʱ�ȭ
        rollChance = 3; // �ʱ� �ֻ��� ���� �� �ִ� Ƚ�� ����
    }

    void Update()
    {
      
        //rollChanceText.text = "Roll Chance : " + rollChance.ToString(); // �ֻ��� ���� �� �ִ� Ƚ���� UI�� ǥ��

        // Space Ű�� ������ �ӱ����� ȸ����Ŵ
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

        // S Ű�� ������ �ӱ����� ��� �ֻ��� ���� �� �ִ� Ƚ���� �ø�
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
            // ȸ���� ���۵Ǹ� 'Wall' ���� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
            Wall.SetActive(false);

            // ȸ�� Ÿ�̸Ӹ� ������ �� ����� �ð���ŭ ���ҽ�ŵ�ϴ�.
            rotationTimer += Time.deltaTime;

            // rotationTimer�� 2.6���� 1.8 ���̿� �ִ��� Ȯ���մϴ�.
            if (rotationTimer < 3.6 && rotationTimer > 2.8)
            {
                // mugRb(Rigidbody)�� Z���� �߽����� 1�ʿ� 145���� �ӵ��� ȸ����ŵ�ϴ�.
                mugRb.rotation = mugRb.rotation * Quaternion.Euler(0f, 0f, -145.0f * Time.deltaTime);
            }
            // rotationTimer�� 0.8���� 0 ���̿� �ִ��� Ȯ���մϴ�.
            else if (rotationTimer < 0.8 && rotationTimer > 0)
            {
                // mugRb(Rigidbody)�� Z���� �߽����� 1�ʿ� -145���� �ӵ��� ȸ����ŵ�ϴ�.
                mugRb.rotation = mugRb.rotation * Quaternion.Euler(0f, 0f, 145.0f * Time.deltaTime);
            }
            // rotationTimer�� 0 ���Ͽ� �����ϸ�
            else if (rotationTimer >3.6)
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
