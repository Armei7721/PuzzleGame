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
            if (i==6 && i == 13)
            {
                isConfirmed[i] = true; ; // 초기에는 모든 점수가 미확정 상태입니다.
            }
            else
            {
                isConfirmed[i] = false;
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
        GameObject parentObject = GameObject.Find("ScoreText");
        if (parentObject != null)
        {
           
            TextMeshProUGUI[] children = parentObject.GetComponentsInChildren<TextMeshProUGUI>(true);

            // 자식 오브젝트들만 참조하는데, 부모 자신은 제외하기 위해 배열 크기를 조정합니다.
            arraytextmeshpro = new TextMeshProUGUI[children.Length];

            // 첫 번째 요소는 부모 자신이므로 인덱스를 1부터 시작하여 자식 오브젝트들을 참조합니다.
            for (int i = 0; i < children.Length; i++)
            {   
                arraytextmeshpro[i] = children[i];
            }
        }
        else
        {
            Debug.LogWarning("DicePlane을 찾을 수 없습니다.");
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
            if (!isConfirmed[index]) // 점수가 아직 확정되지 않은 경우
            {
                isConfirmed[index] = true; // 해당 인덱스의 점수를 확정 상태로 변경합니다.
                UpdateText(index); // 확정 상태의 텍스트 업데이트
            }
            else
            {
                Debug.Log("이미 확정된 점수입니다."); // 이미 확정된 점수인 경우 메시지 출력
            }
        }

       
    }

    public void UpdateText(int index)
    {
        // 현재는 간단히 점수를 업데이트하는 로직만 포함하도록 했습니다.
        // 확정된 점수의 시각적인 변경은 여기에 추가할 수 있습니다.
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
    }
}
