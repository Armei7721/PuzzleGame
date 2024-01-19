using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Damage_TextInfo : MonoBehaviour
{   public static Damage_TextInfo DT;
    public TextMeshPro damageTextPrefab; // UI Text 프리팹
    public Transform canvasTransform; // UI Text가 표시될 Canvas의 Transform

    // Start is called before the first frame update
    void Start()
    {
        DT = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowDamagePopup(Vector3 position, int damage)
    {
        TextMeshPro damageText = Instantiate(damageTextPrefab, canvasTransform);
        damageText.transform.position = Camera.main.WorldToScreenPoint(position);
        damageText.text = damage.ToString();

        // 적당한 시간이 지난 후 텍스트를 제거하고 메모리 누수를 방지합니다.
        Destroy(damageText.gameObject, 1f);
    }
}
