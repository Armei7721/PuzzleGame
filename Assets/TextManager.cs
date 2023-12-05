using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI[] arraytextmeshpro;
    string[] methodNames = { "One", "Two", "Three", "Four", "Five", "Six", "SubTotalPoint" ,"Choice","FourOfAKind","FullHouse","SMS","LGS","Yacht"};
    public TextMeshProUGUI rollChance;
    // Start is called before the first frame update
    void Start()
    {
        InsertText();
        
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
            int score = (int)typeof(RuleScripts).GetMethod(methodNames[i]).Invoke(RuleScripts.rule, null);
            arraytextmeshpro[i].text = score.ToString();
        }
    }
    public void InsertText()
    {
        GameObject parentObject = GameObject.Find("TextArray");
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
}
