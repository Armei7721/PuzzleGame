using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceNumberCheck : MonoBehaviour
{
    Vector3 diceVelocity;

    private void FixedUpdate()
    {
        diceVelocity = DiceRoll.diceVelocity;
    }
    private void Update()
    {
        
    }
    // Start is called before the first frame update
    private void OnTriggerStay(Collider number)
    {
        if(diceVelocity.x ==0f && diceVelocity.y == 0f && diceVelocity.z ==0f)
        {
            switch (number.gameObject.name)
            {
                case "One":
                    DiceNumberText.diceNumber = 1;
                    break;
                case "Two":
                    DiceNumberText.diceNumber = 2;
                    break;
                case "Three":
                    DiceNumberText.diceNumber = 3;
                    break;
                case "Four":
                    DiceNumberText.diceNumber = 4;
                    break;
                case "Five":
                    DiceNumberText.diceNumber = 5;
                    break;
                case "Six":
                    DiceNumberText.diceNumber = 6;
                    break;
                


            }
            
        }
    }
}
