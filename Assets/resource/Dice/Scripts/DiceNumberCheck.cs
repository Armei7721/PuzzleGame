using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceNumberCheck : MonoBehaviour
{
    public static DiceNumberCheck diceNumberCheck;
    public int dicenum;
    Vector3 diceVelocity;

    public void Start()
    {
        diceNumberCheck = this;
    }
    private void Update()
    {
        CheckNumber();    
    }
    public void CheckNumber(){
        for (int i = 0; i < GameManager.gamemanager.slots.Length; i++){
            if (GameManager.gamemanager.slots[i] != null){
                Dice currentDice = GameManager.gamemanager.slots[i].gameObject.GetComponent<Dice>();
                if (currentDice != null && currentDice.SetDice){
                    dicenum = GameManager.gamemanager.slots[i].GetComponent<Dice>().diceValue;
                }
            }
            else{
                break;
            }    
        }
    }
    private void OnTriggerStay(Collider number)
    {
        Dice currentDice = number.gameObject.GetComponentInParent<Dice>();
        if (currentDice != null)
        {
            switch (number.gameObject.name)
            {
                case "One":
                    currentDice.SetDiceValue(1);
                    break;
                case "Two":
                    currentDice.SetDiceValue(2);
                    break;
                case "Three":
                    currentDice.SetDiceValue(3);
                    break;
                case "Four":
                    currentDice.SetDiceValue(4);
                    break;
                case "Five":
                    currentDice.SetDiceValue(5);
                    break;
                case "Six":
                    currentDice.SetDiceValue(6);
                    break;
            }
        }
    }
}