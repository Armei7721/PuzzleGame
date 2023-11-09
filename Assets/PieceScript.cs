using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    private Vector3 RightPosition; // ������ �ùٸ� ��ġ
    public bool InRightPosition; // ���� ������ �ùٸ� ��ġ�� �ִ��� ����
    public bool selected; // ���� ������ ���õǾ����� ����

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ� �ùٸ� ��ġ ����
        RightPosition = transform.position;

        // ������ ��ġ�� ���� ��ġ
        transform.position = new Vector3(Random.Range(5, 11), Random.Range(2.5f, -7));
    }

    // Update is called once per frame
    void Update()
    {
        // ������ �ùٸ� ��ġ�� ���� �����ϰ� ���õ��� ���� ���
        if (Vector3.Distance(transform.position, RightPosition) < 0.5f && !selected)
        {
            // �ùٸ� ��ġ�� ���� �̵�
            transform.position = RightPosition;
            InRightPosition = true; // �ùٸ� ��ġ�� �ִٰ� ǥ��
        }
    }
}
