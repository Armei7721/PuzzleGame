using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    private Vector3 RightPosition; // 조각이 올바른 위치
    public bool InRightPosition; // 현재 조각이 올바른 위치에 있는지 여부
    public bool selected; // 현재 조각이 선택되었는지 여부

    // Start is called before the first frame update
    void Start()
    {
        // 초기 올바른 위치 설정
        RightPosition = transform.position;

        // 랜덤한 위치에 조각 배치
        transform.position = new Vector3(Random.Range(5, 11), Random.Range(2.5f, -7));
    }

    // Update is called once per frame
    void Update()
    {
        // 조각이 올바른 위치에 거의 도달하고 선택되지 않은 경우
        if (Vector3.Distance(transform.position, RightPosition) < 0.5f && !selected)
        {
            // 올바른 위치로 조각 이동
            transform.position = RightPosition;
            InRightPosition = true; // 올바른 위치에 있다고 표시
        }
    }
}
