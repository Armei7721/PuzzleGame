// PieceScript.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    private Vector3 rightPosition; // ������ �ùٸ� ��ġ
    private Quaternion rightRotation;
    private float rightRotatinX;
    private float rightRotatinY;
    private float rightRotatinZ;
    public bool InRightPosition; // ���� ������ �ùٸ� ��ġ�� �ִ��� ����
    public bool selected; // ���� ������ ���õǾ����� ����

    // Start is called before the first frame update
    private void Awake()
    {
        rightPosition = transform.position;
        rightRotation = transform.rotation;
    }
    void Start()
    {
        // �ʱ� �ùٸ� ��ġ ����
      
       
        // ������ ��ġ�� ���� ��ġ
        transform.position = new Vector3(Random.Range(-4, 3), Random.Range(2.5f, -7));
        RandomRotation();
    }

    // Update is called once per frame
    void Update()
    {   rightRotatinX = Mathf.DeltaAngle(rightRotation.eulerAngles.x, transform.rotation.eulerAngles.x);
        rightRotatinY = Mathf.DeltaAngle(rightRotation.eulerAngles.y, transform.rotation.eulerAngles.y);
        rightRotatinZ = Mathf.DeltaAngle(rightRotation.eulerAngles.z, transform.rotation.eulerAngles.z);
        // ������ �ùٸ� ��ġ�� ���� �����ϰ� ���õ��� ���� ���
        if (Vector3.Distance(transform.position, rightPosition) < 0.5f && !selected && rightRotatinX <0.5 && rightRotatinY< 0.5 && rightRotatinZ<0.5)
        {
            // �ùٸ� ��ġ�� ���� �̵�
            transform.position = rightPosition;
            transform.rotation = rightRotation; // �ùٸ� ȸ������ ���� ȸ��
            InRightPosition = true; // �ùٸ� ��ġ�� �ִٰ� ǥ��
        }
        Flip();
        Rotate();
    }

    void RandomRotation()
    {
        int randomAngle = Random.Range(0, 4) * 90; // 0, 90, 180, 270 �� �ϳ��� ���� ����
                                                   //   transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, randomAngle));
        transform.Rotate(0, 0, randomAngle); 
    }

    void Rotate()
    {
        if (Input.GetMouseButtonUp(1) && selected)
        {
            // ���� ȸ�� ������ �����ͼ� 90���� ���� �� ����
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
