using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Damage_TextInfo : MonoBehaviour
{   public static Damage_TextInfo DT;
    public TextMeshPro damageTextPrefab; // UI Text ������
    public Transform canvasTransform; // UI Text�� ǥ�õ� Canvas�� Transform

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

        // ������ �ð��� ���� �� �ؽ�Ʈ�� �����ϰ� �޸� ������ �����մϴ�.
        Destroy(damageText.gameObject, 1f);
    }
}
