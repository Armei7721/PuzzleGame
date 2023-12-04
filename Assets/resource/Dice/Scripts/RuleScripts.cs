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
            Debug.Log("�ߵ�");
            //FourOfKind();
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

        Debug.Log("1�� ���� ���ΰ�: " + score);

    }
    public void Two()
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
        Debug.Log("2�� ���� ���ΰ�: " + score);
    }
    public void Three()
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
        Debug.Log("3�� ���� ���ΰ�: " + score);
    }
    public void Four()
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
        Debug.Log("4�� ���� ���ΰ�: " + score);
    }
    public void Five()
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
        Debug.Log("5�� ���� ���ΰ�: " + score);
    }
    public void Six()
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
        Debug.Log("6�� ���� ���ΰ�: " + score);
    }

    public void Choice()
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
        Debug.Log("choice�� ���� ���ΰ�: " + score);
    }

    public int CalculateFourOfAKind(int[] diceValues)
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

    // ���� ����
    void CalculateScore()
    {

        int[] diceValues = new int[GameManager.gamemanager.slots.Length];
        for (int i = 0; i < GameManager.gamemanager.slots.Length; i++)
        {
            // �� �ֻ����� ���� ���� �迭�� ����
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
        bool fivecard = true;
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
                Debug.Log(counts + "�̰� counts�� ��");
                Debug.Log(kvp);
                threeOfKind = true;
            }
            else if (kvp.Value == 2) // ���� 2�� �����ߴ��� Ȯ���Ͽ� pair�� true�� ����
            {
                pair = true;
            }
            else if (kvp.Value ==5)
            {
                fivecard = true;
            }
        }

        // Ǯ�Ͽ콺 ���ο� ���� ��� ���
        if (threeOfKind && pair ||fivecard)
        {
            Debug.Log("Full House! Score: 25");
        }
        else
        {
            Debug.Log("No Full House. Score: 0");
        }
    }

    public void SMS()
    {
        
    }
    public void LGS()
    {

    }
    public void Yacht()
    {

    }
}
