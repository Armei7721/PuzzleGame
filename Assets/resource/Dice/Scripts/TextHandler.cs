using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextHandler : MonoBehaviour
{
    public TextManager textManager;
    public Button button;
   
    public int textIndex; // �� TextMeshProUGUI�� �ε����� ��Ÿ���ϴ�.]
    Transform parent;
    public void Start()
    {   
            button = GetComponent<Button>();
       Transform parent = transform.parent;
    }
    public void Update()
    {
        if(GameManager.gamemanager.scorePhase &&GameManager.gamemanager.player1)
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
        StartCoroutine(textManager.ChangeDecideStatus(textIndex));
         
            GameManager.gamemanager.act = true;
            Dice.dice.ResetDice();

    }
   
}
