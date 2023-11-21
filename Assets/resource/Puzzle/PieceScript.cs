// PieceScript.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    private Vector3 rightPosition; // 조각이 올바른 위치
    private Quaternion rightRotation;
    private float rightRotatinX;
    private float rightRotatinY;
    private float rightRotatinZ;
    public bool InRightPosition; // 현재 조각이 올바른 위치에 있는지 여부
    public bool selected; // 현재 조각이 선택되었는지 여부

    // Start is called before the first frame update
    private void Awake()
    {
        rightPosition = transform.position;
        rightRotation = transform.rotation;
    }
    void Start()
    {
        // 초기 올바른 위치 설정
      
       
        // 랜덤한 위치에 조각 배치
        transform.position = new Vector3(Random.Range(-4, 3), Random.Range(2.5f, -7));
        RandomRotation();
    }

    // Update is called once per frame
    void Update()
    {   rightRotatinX = Mathf.DeltaAngle(rightRotation.eulerAngles.x, transform.rotation.eulerAngles.x);
        rightRotatinY = Mathf.DeltaAngle(rightRotation.eulerAngles.y, transform.rotation.eulerAngles.y);
        rightRotatinZ = Mathf.DeltaAngle(rightRotation.eulerAngles.z, transform.rotation.eulerAngles.z);
        // 조각이 올바른 위치에 거의 도달하고 선택되지 않은 경우
        if (Vector3.Distance(transform.position, rightPosition) < 0.5f && !selected && rightRotatinX <0.5 && rightRotatinY< 0.5 && rightRotatinZ<0.5)
        {
            // 올바른 위치로 조각 이동
            transform.position = rightPosition;
            transform.rotation = rightRotation; // 올바른 회전으로 조각 회전
            InRightPosition = true; // 올바른 위치에 있다고 표시
        }
        Flip();
        Rotate();
    }

    void RandomRotation()
    {
        int randomAngle = Random.Range(0, 4) * 90; // 0, 90, 180, 270 중 하나의 각도 선택
                                                   //   transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, randomAngle));
        transform.Rotate(0, 0, randomAngle); 
    }

    void Rotate()
    {
        if (Input.GetMouseButtonUp(1) && selected)
        {
            // 현재 회전 각도를 가져와서 90도씩 더한 후 설정
            float currentRotation = transform.eulerAngles.z;
            //transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, currentRotation + 90));
            transform.Rotate(0, 0, 90);
        }
    }
    void Flip()
    {
        if(Input.GetKeyDown(KeyCode.Q) && selected)
        {
            transform.Rotate(180, 0, 0);
        }
        else if(Input.GetKeyDown(KeyCode.W) && selected)
        {
            transform.Rotate(0, 180, 0);
        }
    }
}
