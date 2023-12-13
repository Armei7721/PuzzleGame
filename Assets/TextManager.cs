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
    public string[] methodNames2 = { "One", "Two", "Three", "Four", "Five", "Six", "SubTotalPoint", "Choice", "FourOfAKind", "FullHouse", "SMS", "LGS", "Yacht", "TotalScore" };
    public TextMeshProUGUI rollChance;
    public bool[] isConfirmed;
    public bool[] isConfirmed2;
    public int subtotal;
    public int haptotal;
    // Start is called before the first frame update
    void Start()
    {
        text = this;
        
        BoolConfirmed();
    }

    void BoolConfirmed()
    {
        isConfirmed = new bool[arraytextmeshpro.Length];
        isConfirmed2 = new bool[arraytextmeshpro.Length];
        for (int i = 0; i < isConfirmed.Length; i++)
        {
            if (i == 6 && i == 13)
            {
                isConfirmed[i] = true; ; // �ʱ⿡�� ��� ������ ��Ȯ�� �����Դϴ�.
                isConfirmed2[i] = true;
            }
            else
            {
                isConfirmed[i] = false;
                isConfirmed2[i] = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        RollChane();
        TextColor();
        Score();
        InsertText();
    }
 
    public void Score()
    {
        if (GameManager.gamemanager.player1)
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
        else if (GameManager.gamemanager.player2)
        {
            for (int i = 0; i < arraytextmeshpro.Length; i++)
            {
                if (!isConfirmed2[i])
                {
                    int score = (int)typeof(RuleScripts).GetMethod(methodNames2[i]).Invoke(RuleScripts.rule, null);
                    arraytextmeshpro[i].text = score.ToString();
                }
                else
                    continue;
            }
        }
    }
 
    public void TextColor()
    {
        for(int i = 0; i < arraytextmeshpro.Length; i++)
        {
            if(!isConfirmed[i] &&i!=6 && i!=13)
            {
                arraytextmeshpro[i].color = Color.gray;
            }
            else
            {
                arraytextmeshpro[i].color = Color.black;
            }
        }
    }
    public void InsertText()
    {
        if (GameManager.gamemanager.player1)
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
        else if(GameManager.gamemanager.player2)
        {
            GameObject parentObject = GameObject.Find("ScoreText2");
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

    }
    public void RollChane()
    {
        rollChance.text = "Reroll Chance : "+CupShaking.rollChance.ToString();
    }

    public void ChangeDecideStatus(int index)
    {
        if (index >= 0 && index < isConfirmed.Length && GameManager.gamemanager.player1)
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
        if (index >= 0 && index < isConfirmed.Length && GameManager.gamemanager.player2)
        {
            if (!isConfirmed2[index]) // ������ ���� Ȯ������ ���� ���
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
        if (index >= 0 && index < 6 && methodNames[6] != methodNames[index] && methodNames[13] != methodNames[index])
        {
            int score = (int)typeof(RuleScripts).GetMethod(methodNames[index]).Invoke(RuleScripts.rule, null);
            subtotal += score;
            arraytextmeshpro[index].text = score.ToString();
        }
        else if(index >=6 && index <arraytextmeshpro.Length && methodNames[13] != methodNames[index] )
        {
            int score = (int)typeof(RuleScripts).GetMethod(methodNames[index]).Invoke(RuleScripts.rule, null);
            haptotal += score;
            arraytextmeshpro[index].text = score.ToString();
        }
        else
            return;
        if (GameManager.gamemanager.player1)
        {
            GameManager.gamemanager.player1 = false;
            GameManager.gamemanager.player2 = true;
        }
        else
        {
            GameManager.gamemanager.player1 = true;
            GameManager.gamemanager.player2 = false;
        }

    }
}
