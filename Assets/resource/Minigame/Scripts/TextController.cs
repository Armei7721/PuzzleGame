using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextController : MonoBehaviour
{   
    public static TextController tc;
    public TMP_Text damageTextPrefab;
    public void Start()
    {
        tc = this;
    }
    public void ShowDamageText(Vector3 position, int damageAmount)
    {
        // Text Mesh Pro �ؽ�Ʈ ������Ʈ ����
        TMP_Text damageText = Instantiate(damageTextPrefab, position, Quaternion.identity);

        // ������ ǥ�� ����
        damageText.text =damageAmount.ToString();
        
        // �ؽ�Ʈ�� �� �� �Ŀ� �������� ���� (��: 2�� ��)
        Destroy(damageText.gameObject, damageText.GetComponent < Animator >().GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }

}
