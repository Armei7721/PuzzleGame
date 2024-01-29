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
        // Text Mesh Pro 텍스트 오브젝트 생성
        TMP_Text damageText = Instantiate(damageTextPrefab, position, Quaternion.identity);

        // 데미지 표시 설정
        damageText.text =damageAmount.ToString();
        
        // 텍스트를 몇 초 후에 제거할지 결정 (예: 2초 후)
        Destroy(damageText.gameObject, damageText.GetComponent < Animator >().GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }

}
