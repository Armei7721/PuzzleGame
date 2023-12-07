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
                diceValues[i] = 0; // ������ ����ִ� ��� 0 �Ǵ� �ٸ� �⺻�� ����
            }
        }

        Array.Sort(diceValues);
        
        

    }
   
    public int One()
    {
        int score = 0; // score ������ 0���� �ʱ�ȭ

        foreach (int value in diceValues)
        {
            if (value == 1)
            {
                if (counts.ContainsKey(value))
                {
                    counts[value]++;
                    score += value; // counts[value] ��� value ���� �����Ͽ� score�� �߰�
                }
                else
                {
                    counts[value] = 1;
                    score = value; // counts[value]�� ���� �߰��Ǵ� ��쿡�� value ���� �����Ͽ� score�� �߰�
                }
            }
        }
        return score;


    }
    public int Two()
    {

        int score = 0; // score ������ 0���� �ʱ�ȭ

        foreach (int value in diceValues)
        {
            if (value == 2)
            {
                if (counts.ContainsKey(value))
                {
                    counts[value]++;
                    score += value; // counts[value] ��� value ���� �����Ͽ� score�� �߰�
                }
                else
                {
                    counts[value] = 1;
                    // counts[value]�� ���� �߰��Ǵ� ��쿡�� value ���� �����Ͽ� score�� �߰�
                }
            }
        }
        return score;

    }
    public int Three()
    {

        int score = 0; // score ������ 0���� �ʱ�ȭ

        foreach (int value in diceValues)
        {
            if (value == 3)
            {
                if (counts.ContainsKey(value))
                {
                    counts[value]++;
                    score += value; // counts[value] ��� value ���� �����Ͽ� score�� �߰�
                }
                else
                {
                    counts[value] = 1;
                    // counts[value]�� ���� �߰��Ǵ� ��쿡�� value ���� �����Ͽ� score�� �߰�
                }
            }
        }
        return score;
    }
    public int Four()
    {

        int score = 0; // score ������ 0���� �ʱ�ȭ

        foreach (int value in diceValues)
        {
            if (value == 4)
            {
                if (counts.ContainsKey(value))
                {
                    counts[value]++;
                    score += value; // counts[value] ��� value ���� �����Ͽ� score�� �߰�
                }
                else
                {
                    counts[value] = 1;
                    // counts[value]�� ���� �߰��Ǵ� ��쿡�� value ���� �����Ͽ� score�� �߰�
                }
            }
        }
        return score;
    }
    public int Five()
    {
        int score = 0; // score ������ 0���� �ʱ�ȭ

        foreach (int value in diceValues)
        {
            if (value == 5)
            {
                if (counts.ContainsKey(value))
                {
                    counts[value]++;
                    score += value; // counts[value] ��� value ���� �����Ͽ� score�� �߰�

                }
                else
                {
                    counts[value] = 1;
                    // counts[value]�� ���� �߰��Ǵ� ��쿡�� value ���� �����Ͽ� score�� �߰�
                }
            }
        }
        return score;
    }
    public int Six()
    {
        int score = 0; // score ������ 0���� �ʱ�ȭ

        foreach (int value in diceValues)
        {
            if (value == 6)
            {
                if (counts.ContainsKey(value))
                {
                    counts[value]++;
                    score += value; // counts[value] ��� value ���� �����Ͽ� score�� �߰�
                }
                else
                {
                    counts[value] = 1;
                    // counts[value]�� ���� �߰��Ǵ� ��쿡�� value ���� �����Ͽ� score�� �߰�
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
                score += value; // counts[value] ��� value ���� �����Ͽ� score�� �߰�
            }
            else
            {
                counts[value] = 1;
                // counts[value]�� ���� �߰��Ǵ� ��쿡�� value ���� �����Ͽ� score�� �߰�
            }
        }
        return score;

    }

        public int FourOfAKind()
        {

            // �� �ֻ��� ���� ���� ���� ī��Ʈ
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

            // �ֻ��� ���� �� �� 4���� ���� ��� ���� ���
            foreach (var pair in counts)
            {
                if (pair.Value >= 4)
                {   
                    fourOfAKindFound = true;
                    score = pair.Key * 4; // 4���� �ֻ��� ���� ���� ������ ������ ���� ���
                    break;
                }
            }

            // 4���� �ֻ����� ���� ���� ���� �ش��ϴ� ������ ��ȯ
            return fourOfAKindFound ? score : 0;

        }

    public int FullHouse()
    {
        bool threeOfAKind = false;
        bool twoOfAKind = false;

        // ��ȸ�ϸ鼭 Ǯ�Ͽ콺���� Ȯ���մϴ�.
        for (int i = 0; i < diceValues.Length - 2; i++)
        {
            // �� ���� ������ ���� ã���ϴ�.
            if (diceValues[i] != 0 && diceValues[i] == diceValues[i + 1] && diceValues[i + 1] == diceValues[i + 2])
            {
                threeOfAKind = true;
                i += 2; // �̹� �� ���� ���� ã�����Ƿ� �ε����� �����մϴ�.
            }
            // �� ���� ������ ���� ã���ϴ�.
            else if (diceValues[i] != 0 && i < diceValues.Length - 1 && diceValues[i] == diceValues[i + 1])
            {
                twoOfAKind = true;
                i++; // �� ���� ���� ã�����Ƿ� �ε����� �����մϴ�.
            }
        }

        return threeOfAKind && twoOfAKind ? 25 : 0; // Ǯ�Ͽ콺�� ��� 25�� ��ȯ�մϴ�.
    }


    public int SMS()
    {
        int score = 0;
        int straight = 0;
        bool smallStraight = false;

        // Small Straight ���� �Ǻ�
        for (int i = 0; i < diceValues.Length - 1; i++)
        {
            // ���ӵ� ���� Ȯ��
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
                continue; // ���� ������ ��쵵 ���� ���ڷ� �̵�
            }
            else
            {
                // ���ӵ��� �ʴ� ��찡 �ϳ��� ������ Small Straight�� �ƴ�
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

        // Small Straight ���� �Ǻ�
        for (int i = 0; i < diceValues.Length - 1; i++)
        {
            // ���ӵ� ���� Ȯ��
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
                // ���ӵ��� �ʴ� ��찡 �ϳ��� ������ Small Straight�� �ƴ�
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
            if (pair.Value >=  5) // 5ȸ �̻� �����ϴ��� Ȯ��
            {
                Debug.Log(pair);
                found = true;
                break;
               
            }
        }
        
        score = found ? 50 : 0; // 5���� �ֻ��� ���� ���� ������ ���� ������ 50 ��ȯ, �ƴϸ� 0 ��ȯ
        return score;
    }
    public int TotalScore()
    {
        return SubTotalPoint() + Choice() + FourOfAKind() + FullHouse() + SMS() + LGS() + Yacht();
    }

}
