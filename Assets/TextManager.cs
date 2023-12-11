using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public static TextManager text;
    public bool dicideDice;
    
    public TextMeshProUGUI[] arraytextmeshpro;
    public string[] methodNames = { "One", "Two", "Three", "Four", "Five", "Six", "SubTotalPoint" ,"Choice","FourOfAKind","FullHouse","SMS","LGS","Yacht","TotalScore"};
    public TextMeshProUGUI rollChance;
    public bool[] isConfirmed;
    public int subtotal;
    public int haptotal;
    // Start is called before the first frame update
    void Start()
    {
        text = this;
        InsertText();
        isConfirmed = new bool[arraytextmeshpro.Length];
        for (int i = 0; i < isConfirmed.Length; i++)
        {
            isConfirmed[i] = false; // �ʱ⿡�� ��� ������ ��Ȯ�� �����Դϴ�.
        }

    }

    // Update is called once per frame
    void Update()
    {
        RollChane();
        
        Score();
    }
 
    public void Score()
    {
        for (int i = 0; i < arraytextmeshpro.Length; i++)
        {
            if (!isConfirmed[i])
            {
                int score = (int)typeof(RuleScripts).GetMethod(methodNames[i]).Invoke(RuleScripts.rule, null);
                arraytextmeshpro[i].text = score.ToString();
            }
            else
                continue;
         }
    }
 
    public void InsertText()
    {
        GameObject parentObject = GameObject.Find("ScoreText");
        if (parentObject != null)
        {
           
            TextMeshProUGUI[] children = parentObject.GetComponentsInChildren<TextMeshProUGUI>(true);

            // �ڽ� ������Ʈ�鸸 �����ϴµ�, �θ� �ڽ��� �����ϱ� ���� �迭 ũ�⸦ �����մϴ�.
            arraytextmeshpro = new TextMeshProUGUI[children.Length];

            // ù ��° ��Ҵ� �θ� �ڽ��̹Ƿ� �ε����� 1���� �����Ͽ� �ڽ� ������Ʈ���� �����մϴ�.
            for (int i = 0; i < children.Length; i++)
            {   
                arraytextmeshpro[i] = children[i];
            }
        }
        else
        {
            Debug.LogWarning("DicePlane�� ã�� �� �����ϴ�.");
        }
    }
    public void RollChane()
    {
        rollChance.text = "Reroll Chance : "+CupShaking.rollChance.ToString();
    }

    public void ChangeDecideStatus(int index)
    {
        if (index >= 0 && index < isConfirmed.Length)
        {
            if (!isConfirmed[index]) // ������ ���� Ȯ������ ���� ���
            {
                isConfirmed[index] = true; // �ش� �ε����� ������ Ȯ�� ���·� �����մϴ�.
                UpdateText(index); // Ȯ�� ������ �ؽ�Ʈ ������Ʈ
            }
            else
            {
                Debug.Log("�̹� Ȯ���� �����Դϴ�."); // �̹� Ȯ���� ������ ��� �޽��� ���
            }
        }

       
    }

    public void UpdateText(int index)
    {
        // ����� ������ ������ ������Ʈ�ϴ� ������ �����ϵ��� �߽��ϴ�.
        // Ȯ���� ������ �ð����� ������ ���⿡ �߰��� �� �ֽ��ϴ�.
        if (index >= 0 && index < arraytextmeshpro.Length && methodNames[6] != methodNames[index] && methodNames[13] != methodNames[index])
        {
            int score = (int)typeof(RuleScripts).GetMethod(methodNames[index]).Invoke(RuleScripts.rule, null);
            subtotal += score;
            arraytextmeshpro[index].text = score.ToString();
        }
        else
            return;
    }
}
