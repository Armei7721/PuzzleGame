using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    public static TextManager text;
    public bool dicideDice;
    public Image textback;
    public Text turntext;
    
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
    public float timer=0;

    public Button helpbutton;
    public GameObject diceRule;
    // Start is called before the first frame update
    void Start()
    {
        text = this;
        InsertText();
        BoolConfirmed();
        helpbutton.onClick.AddListener(Helpbuttonactivate);
    }
   
    void BoolConfirmed()
    {
        isConfirmed = new bool[arraytextmeshpro.Length];
        isConfirmed2 = new bool[arraytextmeshpro2.Length];
        for (int i = 0; i < isConfirmed.Length; i++)
        {
            if (i == 6 && i == 13)
            {
                isConfirmed[i] = true; ; // 초기에는 모든 점수가 미확정 상태입니다.
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
        PlayerTurnText();
        if (diceRule.activeSelf && Input.GetMouseButton(0))
        {
            diceRule.SetActive(!diceRule.activeSelf);
        }
    }
    void PlayerTurnText()
    {
        timer += Time.deltaTime;
        float blinkDuration = 2.0f; // 블링크 주기를 정의합니다 (예: 1초)
        Color tc = new Color(0f, 0f, 0f, 0.9f);
        Color tc2 = new Color(0f, 0f, 0f, 0.4f);
        Color tc3 = new Color(255f, 255f, 255f, 0.9f);
        Color tc4 = new Color(255f, 255f, 255f, 0.4f);
        // 주기적으로 텍스트 블링크 효과를 주기 위해 타이머 값을 이용합니다.
        if (timer % (blinkDuration * 2) < blinkDuration){
            float t = Mathf.PingPong(timer, blinkDuration) / blinkDuration;
            // 첫 번째 절반 (색상 변경)

            textback.color = Color.Lerp(tc,tc2, t);
            turntext.color = Color.Lerp(tc3, tc4, t);
        }
        else
        {
            float t = Mathf.PingPong(timer, blinkDuration) / blinkDuration;
            // 두 번째 절반 (원래 색상으로 되돌림)
            textback.color = Color.Lerp(tc2,tc, 1-t);
            turntext.color = Color.Lerp(tc4, tc3, 1-t);
        }
        if(GameManager.gamemanager.player1)
        {
            turntext.text = "Player1 Turn...";
        }
        else
        {
            turntext.text = "Player2 Turn...";
        }
    }

    public void Score()
    {
        if (GameManager.gamemanager.player1){
            for (int i = 0; i < arraytextmeshpro.Length; i++){
                if (!isConfirmed[i]){
                    int score = (int)typeof(RuleScripts).GetMethod(methodNames[i]).Invoke(RuleScripts.rule, null);
                    arraytextmeshpro[i].text = score.ToString();
                }
                else
                    continue;
            }
        }
        else if (GameManager.gamemanager.player2){
            for (int i = 0; i < arraytextmeshpro.Length; i++){
                if (!isConfirmed2[i]){
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
            arraytextmeshpro2 = new TextMeshProUGUI[children2.Length]; // arraytextmeshpro2 배열 초기화

            for (int i = 0; i < children.Length; i++)
            {
                arraytextmeshpro[i] = children[i];
                arraytextmeshpro2[i] = children2[i];
            }
        }
        else
        {
            Debug.LogWarning("ScoreText 또는 ScoreText2를 찾을 수 없습니다.");
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
        else if (index >= 0 && index < isConfirmed2.Length && GameManager.gamemanager.player2)
        {
            if (!isConfirmed2[index]) // 점수가 아직 확정되지 않은 경우
            {
                isConfirmed2[index] = true; // 해당 인덱스의 점수를 확정 상태로 변경합니다.
                UpdateText(index); // 확정 상태의 텍스트 업데이트
            }
            else
            {
                Debug.Log("이미 확정된 점수입니다."); // 이미 확정된 점수인 경우 메시지 출력
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
        // 현재는 간단히 점수를 업데이트하는 로직만 포함하도록 했습니다.
        // 확정된 점수의 시각적인 변경은 여기에 추가할 수 있습니다.
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
        else if(GameManager.gamemanager.player2)
        {
            if (index >= 0 && index < 6 && methodNames[6] != methodNames[index] && methodNames[13] != methodNames[index])
            {
                int score = (int)typeof(RuleScripts).GetMethod(methodNames[index]).Invoke(RuleScripts.rule, null);

                subtotal2 += score;
                arraytextmeshpro2[index].text = score.ToString();
            }
            else if (index >= 6 && index < arraytextmeshpro.Length && methodNames[13] != methodNames[index])
            {
                int score = (int)typeof(RuleScripts).GetMethod(methodNames[index]).Invoke(RuleScripts.rule, null);
                haptotal2 += score;
                arraytextmeshpro2[index].text = score.ToString();
            }
            else
                return;
        }
    }
    public void Helpbuttonactivate()
    {
        diceRule.SetActive(!diceRule.activeSelf);
    }
}
