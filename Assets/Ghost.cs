using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // ��Ʈ ������Ʈ ���� ����
    public PlayerController player;
    public Rigidbody2D player1;
    public float ghostDelay;
    private float ghostDelayTime;

    // ��Ʈ ������
    public GameObject ghost;

    // ��Ʈ ���� ���θ� �����ϴ� �Ҹ��� ����
    public bool makeGhost;

    void Start()
    {
        // �ʱ� ������ �ð� ����
        this.ghostDelayTime = this.ghostDelay;
    }

    void FixedUpdate()
    {
        // ��Ʈ ������ Ȱ��ȭ�Ǿ����� Ȯ��
        if (this.makeGhost)
        {
            // ���� ��Ʈ ������ �ð��� 0���� ũ��, �ð��� ���� ���Դϴ�.
            if (this.ghostDelayTime > 0)
            {
                this.ghostDelayTime -= Time.deltaTime;
            }
            else
            {
                if (player1.velocity.x >= 0)
                {// ��Ʈ ������ �ð��� 0�� �����ϸ�, ��Ʈ ������Ʈ�� �����մϴ�.
                    GameObject currentGhost = Instantiate(this.ghost, this.transform.position, Quaternion.Euler(0, 0, 0));

                    // ��Ʈ�� ũ��� ��������Ʈ�� ���� ������Ʈ�� �����ϰ� �����մϴ�.
                    Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                    currentGhost.transform.localScale = transform.localScale;
                    currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;

                    // ���� ��Ʈ ������ ���� ��Ʈ ������ �ð��� �缳���մϴ�.
                    this.ghostDelayTime = this.ghostDelay;

                    // 1�� �Ŀ� ��Ʈ ������Ʈ�� �����մϴ�.
                    Destroy(currentGhost, 0.5f);
                }
                else if (player1.velocity.x <= 0)
                {
                    GameObject currentGhost = Instantiate(this.ghost, this.transform.position, Quaternion.Euler(0, 180, 0));

                    // ��Ʈ�� ũ��� ��������Ʈ�� ���� ������Ʈ�� �����ϰ� �����մϴ�.
                    Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                    currentGhost.transform.localScale = transform.localScale;
                    currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;

                    // ���� ��Ʈ ������ ���� ��Ʈ ������ �ð��� �缳���մϴ�.
                    this.ghostDelayTime = this.ghostDelay;

                    // 1�� �Ŀ� ��Ʈ ������Ʈ�� �����մϴ�.
                    Destroy(currentGhost, 0.5f);
                }
            }
        }
    }
}