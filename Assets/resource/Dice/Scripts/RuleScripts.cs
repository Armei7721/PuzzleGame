using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RuleScripts : MonoBehaviour
{
    public static RuleScripts rule;
    public int kind = 0;
    public int[] diceValues;
    Dictionary<int, int> counts = new Dictionary<int, int>();
    // Start is called before the first frame update
    void Start()
    {
        rule = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.gamemanager.slots != null)
        {
           
            
            DiceSort();
            One();
            Two();
            Three();
            Four();
            Five();
            Six();
            Choice();
            FourOfAKind();
            FullHouse();
            SMS();
            LGS();
            Yacht();
        }

    }
    public void DiceSort()
    {
        
            for (int i = 0; i < GameManager.gamemanager.slots.Length; i++)
            {
                if (GameManager.gamemanager.slots[i] != null)
            {
                diceValues[i] = GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue;
                }
            }
            
        

    }
    public int FourOfAKind()
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
    public int One()
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
        return score;


    }
    public int Two()
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
        return score;

    }
    public int Three()
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
        return score;
    }
    public int Four()
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
        return score;
    }
    public int Five()
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
        return score;
    }
    public int Six()
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
        return score;
    }
    public int SubTotalPoint()
    {
        int score = 0;
        if (One() + Two() + Three() + Four() + Five() + Six() >= 63)
        {
            score = One() + Two() + Three() + Four() + Five() + Six() + 35;
        }

        return score;
    }
    public int Choice()
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
        return score;

    }



    // 예시 사용법

    public int FullHouse()
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
                threeOfKind = true;
            }
            else if (kvp.Value == 2) // 값이 2번 출현했는지 확인하여 pair를 true로 설정
            {
                pair = true;
            }

        }
        
        return threeOfKind && pair ? 25 : 0;
    }

    public int SMS()
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

        return score;
    }
    public int LGS()
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
                    score = 30;
                }
            }
        }
        return score;
    }
    public int Yacht()
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
        return score;
    }


}
