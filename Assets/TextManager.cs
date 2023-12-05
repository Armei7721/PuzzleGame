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
}
