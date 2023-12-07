using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RuleScripts : MonoBehaviour
{
    public static RuleScripts rule;
    public int kind = 0;
    public int[] diceValues;
    public bool[] decide;
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
            else
            {
                diceValues[i] = 0; // 슬롯이 비어있는 경우 0 또는 다른 기본값 설정
            }
        }

        Array.Sort(diceValues);
        
        

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
        else
        {
            score = One() + Two() + Three() + Four() + Five() + Six();
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

    public int FullHouse()
    {
        bool threeOfAKind = false;
        bool twoOfAKind = false;

        // 순회하면서 풀하우스인지 확인합니다.
        for (int i = 0; i < diceValues.Length - 2; i++)
        {
            // 세 개의 동일한 눈을 찾습니다.
            if (diceValues[i] != 0 && diceValues[i] == diceValues[i + 1] && diceValues[i + 1] == diceValues[i + 2])
            {
                threeOfAKind = true;
                i += 2; // 이미 세 개의 눈을 찾았으므로 인덱스를 조정합니다.
            }
            // 두 개의 동일한 눈을 찾습니다.
            else if (diceValues[i] != 0 && i < diceValues.Length - 1 && diceValues[i] == diceValues[i + 1])
            {
                twoOfAKind = true;
                i++; // 두 개의 눈을 찾았으므로 인덱스를 조정합니다.
            }
        }

        return threeOfAKind && twoOfAKind ? 25 : 0; // 풀하우스인 경우 25를 반환합니다.
    }


    public int SMS()
    {
        int score = 0;
        int straight = 0;
        bool smallStraight = false;

        // Small Straight 여부 판별
        for (int i = 0; i < diceValues.Length - 1; i++)
        {
            // 연속된 숫자 확인
            if (diceValues[i] != 0 && diceValues[i] == diceValues[i + 1] - 1)
            {
                straight++;
                if (straight >= 3)
                {
                    smallStraight = true;
                }
            }
            else if (diceValues[i] != 0 && diceValues[i] == diceValues[i + 1])
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
        score = smallStraight ? 15 : 0;
        return score;
    }
    public int LGS()
    {
        int score = 0;
        int straight = 0;
        bool largeStraight = false;

        // Small Straight 여부 판별
        for (int i = 0; i < diceValues.Length - 1; i++)
        {
            // 연속된 숫자 확인
            if (diceValues[i] != 0 && diceValues[i] == diceValues[i + 1] - 1)
            {
                straight++;
                if (straight >= 4)
                {
                    largeStraight = true;
                }
            }
            else
            {
                // 연속되지 않는 경우가 하나라도 있으면 Small Straight가 아님
                largeStraight = false;
                break;
            }
        }
        score = largeStraight ? 30 : 0;
        return score;
    }
    public int Yacht()
    {
        Dictionary<int, int> countst = new Dictionary<int, int>();
        int score = 0;
        bool found = false;
        foreach (int value in diceValues)
        {
            if (countst.ContainsKey(value))
            {if (counts[value] != counts[0])
                {
                    countst[value]++;
                }
            }
            else
            {
                countst[value] = 1;
            }
        }
        foreach (var pair in countst)
        {
            if (pair.Value >=  5) // 5회 이상 등장하는지 확인
            {
                Debug.Log(pair);
                found = true;
                break;
               
            }
        }
        
        score = found ? 50 : 0; // 5개의 주사위 눈금 값과 동일한 값이 있으면 50 반환, 아니면 0 반환
        return score;
    }
    public int TotalScore()
    {
        return SubTotalPoint() + Choice() + FourOfAKind() + FullHouse() + SMS() + LGS() + Yacht();
    }

}
