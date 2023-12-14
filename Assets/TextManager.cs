using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public static TextManager text;
    public bool dicideDice;
    public GameObject playerturntext;
    
    public TextMeshProUGUI[] arraytextmeshpro;
    public TextMeshProUGUI[] arraytextmeshpro2;
    public GameObject[] arraygameobject;
    public GameObject[] arraygameobject2;
    public string[] methodNames = { "One", "Two", "Three", "Four", "Five", "Six", "SubTotalPoint" ,"Choice","FourOfAKind","FullHouse","SMS","LGS","Yacht","TotalScore"};
    public string[] methodNames2 = { "One", "Two", "Three", "Four", "Five", "Six", "SubTotalPoint", "Choice", "FourOfAKind", "FullHouse", "SMS", "LGS", "Yacht", "TotalScore" };
    public TextMeshProUGUI rollChance;
    public bool[] isConfirmed;
    public bool[] isConfirmed2;
    public int subtotal;
    public int subtotal2;
    public int haptotal;
    public int haptotal2;
    // Start is called before the first frame update
    void Start()
    {
        text = this;
        InsertText();
        BoolConfirmed();
    }
    void PlayerTurnText() {
      gameObject.color
    }
    void BoolConfirmed()
    {
        isConfirmed = new bool[arraytextmeshpro.Length];
        isConfirmed2 = new bool[arraytextmeshpro2.Length];
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
                    arraytextmeshpro2[i].text = score.ToString();
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
            if (GameManager.gamemanager.player1)
            {
                if (!isConfirmed[i] && i != 6 && i != 13)
                {
                    arraytextmeshpro[i].color = Color.gray;
                }
                else
                {
                    arraytextmeshpro[i].color = Color.black;
                }
            }
            else if (GameManager.gamemanager.player2)
            {
                if (!isConfirmed2[i] && i != 6 && i != 13)
                {
                    arraytextmeshpro2[i].color = Color.gray;
                }
                else
                {
                    arraytextmeshpro2[i].color = Color.black;
                }
            }
        }
    }
    public void InsertText()
    {
        GameObject parentObject = GameObject.Find("ScoreText");
        GameObject parentObject2 = GameObject.Find("ScoreText2");

        if (parentObject != null && parentObject2 != null)
        {
            TextMeshProUGUI[] children = parentObject.GetComponentsInChildren<TextMeshProUGUI>(true);
            TextMeshProUGUI[] children2 = parentObject2.GetComponentsInChildren<TextMeshProUGUI>(true);

            arraytextmeshpro = new TextMeshProUGUI[children.Length];
            arraytextmeshpro2 = new TextMeshProUGUI[children2.Length]; // arraytextmeshpro2 �迭 �ʱ�ȭ

            for (int i = 0; i < children.Length; i++)
            {
                arraytextmeshpro[i] = children[i];
                arraytextmeshpro2[i] = children2[i];
            }

        }
        else
        {
            Debug.LogWarning("ScoreText �Ǵ� ScoreText2�� ã�� �� �����ϴ�.");
        }
    }

    public void RollChane()
    {
        rollChance.text = "Reroll Chance : "+CupShaking.rollChance.ToString();
    }

    public IEnumerator ChangeDecideStatus(int index)
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
        else if (index >= 0 && index < isConfirmed2.Length && GameManager.gamemanager.player2)
        {
            if (!isConfirmed2[index]) // ������ ���� Ȯ������ ���� ���
            {
                isConfirmed2[index] = true; // �ش� �ε����� ������ Ȯ�� ���·� �����մϴ�.
                UpdateText(index); // Ȯ�� ������ �ؽ�Ʈ ������Ʈ
            }
            else
            {
                Debug.Log("�̹� Ȯ���� �����Դϴ�."); // �̹� Ȯ���� ������ ��� �޽��� ���
            }
            
            
            }
        yield return new WaitForSeconds(0.5f);
        ChangeTurn();
    }
    public void ChangeTurn()
    {
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
    public void UpdateText(int index)
    {
        // ����� ������ ������ ������Ʈ�ϴ� ������ �����ϵ��� �߽��ϴ�.
        // Ȯ���� ������ �ð����� ������ ���⿡ �߰��� �� �ֽ��ϴ�.
        if (GameManager.gamemanager.player1)
        {
            if (index >= 0 && index < 6 && methodNames[6] != methodNames[index] && methodNames[13] != methodNames[index])
            {
                int score = (int)typeof(RuleScripts).GetMethod(methodNames[index]).Invoke(RuleScripts.rule, null);

                subtotal += score;
                arraytextmeshpro[index].text = score.ToString();
            }
            else if (index >= 6 && index < arraytextmeshpro.Length && methodNames[13] != methodNames[index])
            {
                int score = (int)typeof(RuleScripts).GetMethod(methodNames[index]).Invoke(RuleScripts.rule, null);
                haptotal += score;
                arraytextmeshpro[index].text = score.ToString();
            }
            else
                return;
        }
        else
        {
            if (index >= 0 && index < 6 && methodNames[6] != methodNames[index] && methodNames[13] != methodNames[index])
            {
                int score = (int)typeof(RuleScripts).GetMethod(methodNames[index]).Invoke(RuleScripts.rule, null);

                subtotal2 += score;
                arraytextmeshpro[index].text = score.ToString();
            }
            else if (index >= 6 && index < arraytextmeshpro.Length && methodNames[13] != methodNames[index])
            {
                int score = (int)typeof(RuleScripts).GetMethod(methodNames[index]).Invoke(RuleScripts.rule, null);
                haptotal2 += score;
                arraytextmeshpro[index].text = score.ToString();
            }
            else
                return;
        }
    }
}