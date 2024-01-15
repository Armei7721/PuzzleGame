using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // 고스트 오브젝트 생성 간격
    public PlayerController player;
    public Rigidbody2D player1;
    public float ghostDelay;
    private float ghostDelayTime;

    // 고스트 프리팹
    public GameObject ghost;

    // 고스트 생성 여부를 제어하는 불리언 변수
    public bool makeGhost;

    void Start()
    {
        // 초기 딜레이 시간 설정
        this.ghostDelayTime = this.ghostDelay;
    }

    void FixedUpdate()
    {
        // 고스트 생성이 활성화되었는지 확인
        if (this.makeGhost)
        {
            // 만약 고스트 딜레이 시간이 0보다 크면, 시간에 따라 줄입니다.
            if (this.ghostDelayTime > 0)
            {
                this.ghostDelayTime -= Time.deltaTime;
            }
            else
            {
                if (player1.velocity.x >= 0)
                {// 고스트 딜레이 시간이 0에 도달하면, 고스트 오브젝트를 생성합니다.
                    GameObject currentGhost = Instantiate(this.ghost, this.transform.position, Quaternion.Euler(0, 0, 0));

                    // 고스트의 크기와 스프라이트를 원본 오브젝트와 동일하게 설정합니다.
                    Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                    currentGhost.transform.localScale = transform.localScale;
                    currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;

                    // 다음 고스트 생성을 위해 고스트 딜레이 시간을 재설정합니다.
                    this.ghostDelayTime = this.ghostDelay;

                    // 1초 후에 고스트 오브젝트를 제거합니다.
                    Destroy(currentGhost, 0.5f);
                }
                else if (player1.velocity.x <= 0)
                {
                    GameObject currentGhost = Instantiate(this.ghost, this.transform.position, Quaternion.Euler(0, 180, 0));

                    // 고스트의 크기와 스프라이트를 원본 오브젝트와 동일하게 설정합니다.
                    Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                    currentGhost.transform.localScale = transform.localScale;
                    currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;

                    // 다음 고스트 생성을 위해 고스트 딜레이 시간을 재설정합니다.
                    this.ghostDelayTime = this.ghostDelay;

                    // 1초 후에 고스트 오브젝트를 제거합니다.
                    Destroy(currentGhost, 0.5f);
                }
            }
        }
    }
}