using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RuleScripts : MonoBehaviour
{
    public int kind = 0;
    public int[] diceValues = new int[5];
    Dictionary<int, int> counts = new Dictionary<int, int>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("발동");
            CalculateScore();
            FullHouse();
            DiceSort();
            One();
            Two();
            Three();
            Four();
            Five();
            Six();
            Choice();
            SMS();
            LGS();
            Yacht();
        }

    }
    public void DiceSort()
    {

        for (int i = 0; i < GameManager.gamemanager.slots.Length; i++)
        {
            if (GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue != null)
            {
                diceValues[i] = GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue;
            }
        }
        Array.Sort(diceValues);


    }
    public void One()
    {
        int score = 0; // score 변수를 0으로 초기화

        foreach (int value in diceValues)
        {
            if (value == 1)
            {
                if (counts.ContainsKey(value))
                {
                    counts[value]++;
                    score += value; // counts[value] 대신 value 값을 누적하여 score에 추가
                }
                else
                {
                    counts[value] = 1;
                    score = value; // counts[value]가 새로 추가되는 경우에도 value 값을 누적하여 score에 추가
                }
            }
        }

        Debug.Log("1의 값은 몇인가: " + score);

    }
    public void Two()
    {

        int score = 0; // score 변수를 0으로 초기화

        foreach (int value in diceValues)
        {
            if (value == 2)
            {
                if (counts.ContainsKey(value))
                {
                    counts[value]++;
                    score += value; // counts[value] 대신 value 값을 누적하여 score에 추가
                }
                else
                {
                    counts[value] = 1;
                    // counts[value]가 새로 추가되는 경우에도 value 값을 누적하여 score에 추가
                }
            }
        }
        Debug.Log("2의 값은 몇인가: " + score);
    }
    public void Three()
    {

        int score = 0; // score 변수를 0으로 초기화

        foreach (int value in diceValues)
        {
            if (value == 3)
            {
                if (counts.ContainsKey(value))
                {
                    counts[value]++;
                    score += value; // counts[value] 대신 value 값을 누적하여 score에 추가
                }
                else
                {
                    counts[value] = 1;
                    // counts[value]가 새로 추가되는 경우에도 value 값을 누적하여 score에 추가
                }
            }
        }
        Debug.Log("3의 값은 몇인가: " + score);
    }
    public void Four()
    {

        int score = 0; // score 변수를 0으로 초기화

        foreach (int value in diceValues)
        {
            if (value == 4)
            {
                if (counts.ContainsKey(value))
                {
                    counts[value]++;
                    score += value; // counts[value] 대신 value 값을 누적하여 score에 추가
                }
                else
                {
                    counts[value] = 1;
                    // counts[value]가 새로 추가되는 경우에도 value 값을 누적하여 score에 추가
                }
            }
        }
        Debug.Log("4의 값은 몇인가: " + score);
    }
    public void Five()
    {
        int score = 0; // score 변수를 0으로 초기화

        foreach (int value in diceValues)
        {
            if (value == 5)
            {
                if (counts.ContainsKey(value))
                {
                    counts[value]++;
                    score += value; // counts[value] 대신 value 값을 누적하여 score에 추가
                    
                }
                else
                {
                    counts[value] = 1;
                    // counts[value]가 새로 추가되는 경우에도 value 값을 누적하여 score에 추가
                }
            }
        }
        Debug.Log("5의 값은 몇인가: " + score);
    }
    public void Six()
    {
        int score = 0; // score 변수를 0으로 초기화

        foreach (int value in diceValues)
        {
            if (value == 6)
            {
                if (counts.ContainsKey(value))
                {
                    counts[value]++;
                    score += value; // counts[value] 대신 value 값을 누적하여 score에 추가
                }
                else
                {
                    counts[value] = 1;
                    // counts[value]가 새로 추가되는 경우에도 value 값을 누적하여 score에 추가
                }
            }
        }
        Debug.Log("6의 값은 몇인가: " + score);
    }
    public void Bonus()
    {

    }

    public void Choice()
    {
        int score = 0;
        foreach (int value in diceValues)
        {
            
                if (counts.ContainsKey(value))
                {
                    counts[value]++;
                    score += value; // counts[value] 대신 value 값을 누적하여 score에 추가
                }
                else
                {
                    counts[value] = 1;
                    // counts[value]가 새로 추가되는 경우에도 value 값을 누적하여 score에 추가
                }
           
        }
        Debug.Log("choice의 값은 몇인가: " + score);
    }

    public int CalculateFourOfAKind(int[] diceValues)
    {
       

        // 각 주사위 눈금 값의 개수 카운트
        foreach (int value in diceValues)
        {
            if (counts.ContainsKey(value))
            {
                counts[value]++;
            }
            else
            {
                counts[value] = 1;
            }
        }

        int score = 0;
        bool fourOfAKindFound = false;

        // 주사위 눈금 값 중 4개가 같은 경우 점수 계산
        foreach (var pair in counts)
        {
            if (pair.Value >= 4)
            {
                fourOfAKindFound = true;
                score = pair.Key * 4; // 4개의 주사위 눈금 값과 동일한 값으로 점수 계산
                break;
            }
        }

        // 4개의 주사위가 같은 값일 때만 해당하는 점수를 반환
        return fourOfAKindFound ? score : 0;
    }

    // 예시 사용법
    void CalculateScore()
    {

        int[] diceValues = new int[GameManager.gamemanager.slots.Length];
        for (int i = 0; i < GameManager.gamemanager.slots.Length; i++)
        {
            // 각 주사위의 눈금 값을 배열에 저장
            diceValues[i] = GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue;
        }
        int score = CalculateFourOfAKind(diceValues);

        if (score > 0)
        {
            Debug.Log("Four of a Kind! Score: " + score);
        }
        else
        {
            Debug.Log("No Four of a Kind. Score: 0");
        }
    }
    public void FullHouse()
    {
        bool threeOfKind = false;
        bool pair = false;
        foreach (int value in diceValues)
        {
            if (counts.ContainsKey(value))
            {
                counts[value]++;
            }
            else
            {
                counts[value] = 1;
            }
        }
        // counts 딕셔너리의 값 확인하여 풀하우스 여부 판별
        foreach (var kvp in counts)
        {
            if (kvp.Value == 3) // 값이 3번 출현했는지 확인하여 threeOfKind를 true로 설정
            {
                Debug.Log(counts + "이건 counts의 값");
                Debug.Log(kvp);
                threeOfKind = true;
            }
            else if (kvp.Value == 2) // 값이 2번 출현했는지 확인하여 pair를 true로 설정
            {
                pair = true;
            }
            
        }

        // 풀하우스 여부에 따라 결과 출력
        if (threeOfKind && pair)
        {
            Debug.Log("Full House! Score: 25");
            Debug.Log(threeOfKind);
        }
        else
        {
            Debug.Log("No Full House. Score: 0");
        }
    }

    public void SMS()
    {
        int score = 0;
        bool smallStraight = false;

        // Small Straight 여부 판별
        for (int i = 0; i < diceValues.Length - 1; i++)
        {
            // 연속된 숫자 확인
            if (diceValues[i] == diceValues[i + 1] - 1)
            {
                smallStraight = true;
            }
            else if (diceValues[i] == diceValues[i + 1])
            {
                continue; // 같은 숫자인 경우도 다음 숫자로 이동
            }
            else
            {
                // 연속되지 않는 경우가 하나라도 있으면 Small Straight가 아님
                smallStraight = false;
                break;
            }
        }

        // Small Straight인 경우 결과 출력
        if (smallStraight)
        {
            Debug.Log("Small Straight! Score: [15]");
        }
        else
        {
            Debug.Log("No Small Straight. Score: 0");
        }
    }
    public void LGS()
    {
        int score = 0;
        foreach (int value in diceValues)
        {
            if (counts.ContainsKey(value))
            {
                counts[value]++;
             
            }
            else
            {
                counts[value] = 1;
                if (counts[value] == 1)
                {
                    score =30;
                }
            }
        }
        
        Debug.Log("LargeStraight : "+score);
    }
    public void Yacht()
    {
        int score = 0;

        foreach (int value in diceValues)
        {
            if (counts.ContainsKey(value))
            {
                counts[value]++;
            }
            else
            {
                counts[value] = 1;
            }
        }

        bool found = false;
        foreach (var pair in counts)
        {
            if (pair.Value >= 5) // 5회 이상 등장하는지 확인
            {
                found = true;
                break;
            }
        }

        score = found ? 50 : 0; // 5개의 주사위 눈금 값과 동일한 값이 있으면 50 반환, 아니면 0 반환
        Debug.Log(score + " Yacht 값");
    }


}
