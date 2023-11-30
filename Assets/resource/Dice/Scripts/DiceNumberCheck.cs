using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceNumberCheck : MonoBehaviour
{
    Vector3 diceVelocity;
    static int One=0;
    static int two=0;
    static int three = 0;
    static int four = 0;
    static int five = 0;
    static int six = 0;
    private void FixedUpdate()
    {
    }
    private void Update()
    {
        Debug.Log(One);
        Debug.Log(two);
        Debug.Log(three);
        Debug.Log(four);
        Debug.Log(five);
        Debug.Log(six);

    }
    // Start is called before the first frame update
    private void OnTriggerStay(Collider number)
    {
        if(Dice.dice.SetDice)
        {
            switch (number.gameObject.name)
            {
                case "One":
                    One += 1;
                    break;
                case "Two":
                    two += 1;
                    break;
                case "Three":
                    three += 1;
                    break;
                case "Four":
                    four += 1;
                    break;
                case "Five":
                    five += 1;
                    break;
                case "Six":
                    six += 1;
                    break;
                


            }
            
        }
    }
}
