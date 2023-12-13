using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextHandler : MonoBehaviour
{
    public TextManager textManager;
    public Button button;
    public int textIndex; // �� TextMeshProUGUI�� �ε����� ��Ÿ���ϴ�.]
    public void Start()
    {   
            button = GetComponent<Button>();
       
    }
    public void Update()
    {
        if(GameManager.gamemanager.scorePhase)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
    public void OnClick()
    {
            textManager.ChangeDecideStatus(textIndex);
            GameManager.gamemanager.act = true;
            Dice.dice.ResetDice();
        
          
        
    }
   
}
