using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RuleScripts : MonoBehaviour
{
    public int kind = 0;
    public int[] diceValues = new int[5];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("¹ßµ¿");
            FourOfKind();
        }
    }
    public int One()
    {

        for (int i = 0; i < 5; i++)
        {
            if (GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue == 1)
            {
                DiceNumberCheck.diceNumberCheck.dicenum += GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue;
            }
        }
        return DiceNumberCheck.diceNumberCheck.dicenum;
    }
    public void Two()
    {
        
        for (int i = 0; i < 5; i++)
        {
            if (GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue == 2)
            {
                DiceNumberCheck.diceNumberCheck.dicenum += GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue;
            }
        }
        Debug.Log(DiceNumberCheck.diceNumberCheck.dicenum);
    }
    public void Three()
    {

        for (int i = 0; i < 5; i++)
        {
            if (GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue == 3)
            {
                DiceNumberCheck.diceNumberCheck.dicenum += GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue;
            }
            
        }
        Debug.Log(DiceNumberCheck.diceNumberCheck.dicenum);
    }
    public void Four()
    {

        for (int i = 0; i < 5; i++)
        {
            if (GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue == 4)
            {
                DiceNumberCheck.diceNumberCheck.dicenum += GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue;
            }
        }
        Debug.Log(DiceNumberCheck.diceNumberCheck.dicenum);
    }
    public void Five()
    {
        for (int i = 0; i < 5; i++)
        {
            if (GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue == 5)
            {
                DiceNumberCheck.diceNumberCheck.dicenum += GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue;
            }
        }
        Debug.Log(DiceNumberCheck.diceNumberCheck.dicenum);
    }
    public void Six()
    {
        for (int i = 0; i < 5; i++)
        {
            if (GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue == 6)
            {
                DiceNumberCheck.diceNumberCheck.dicenum += GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue;
            }
        }
        Debug.Log(DiceNumberCheck.diceNumberCheck.dicenum);
    }

    public void Choice()
    {
        for (int i = 0; i < 5; i++)
        {          
            
            DiceNumberCheck.diceNumberCheck.dicenum += GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue;            
        }
        Debug.Log(DiceNumberCheck.diceNumberCheck.dicenum);
    }

    public void FourOfKind()
    {
        for (int i = 0; i < 5; i++)
        {            
            diceValues[i] = GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue;         
        }
        Array.Sort(diceValues);

        for (int i = 0; i < 4; i++)
        {
            if (diceValues[i] == diceValues[i + 1])
            {
                kind++;
            }
            //else if (diceValues[(i + 1)] == diceValues[(i+1)+1])
            //{
            //    kind++;
            //}
        }
        if(kind==3 || kind ==4)
        {
            for(int i= 0; i <= ((kind %2)+2); i++)
            {
                DiceNumberCheck.diceNumberCheck.dicenum+= GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue;
                
            }
            Debug.Log(DiceNumberCheck.diceNumberCheck.dicenum);
        }
    }
    public void FullHouse()
    {
        for(int i = 0; i < 5; i++)
        {
           
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
