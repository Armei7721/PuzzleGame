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



    // ���� ����

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
        // counts ��ųʸ��� �� Ȯ���Ͽ� Ǯ�Ͽ콺 ���� �Ǻ�
        foreach (var kvp in counts)
        {
            if (kvp.Value == 3) // ���� 3�� �����ߴ��� Ȯ���Ͽ� threeOfKind�� true�� ����
            {
                threeOfKind = true;
            }
            else if (kvp.Value == 2) // ���� 2�� �����ߴ��� Ȯ���Ͽ� pair�� true�� ����
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

        // Small Straight ���� �Ǻ�
        for (int i = 0; i < diceValues.Length - 1; i++)
        {
            // ���ӵ� ���� Ȯ��
            if (diceValues[i] == diceValues[i + 1] - 1)
            {
                smallStraight = true;
            }
            else if (diceValues[i] == diceValues[i + 1])
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
            if (pair.Value >= 5) // 5ȸ �̻� �����ϴ��� Ȯ��
            {
                found = true;
                break;
            }
        }

        score = found ? 50 : 0; // 5���� �ֻ��� ���� ���� ������ ���� ������ 50 ��ȯ, �ƴϸ� 0 ��ȯ
        return score;
    }


}
