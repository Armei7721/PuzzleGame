using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    public TextManager textManager;
    public int textIndex; // �� TextMeshProUGUI�� �ε����� ��Ÿ���ϴ�.

    public void OnClick()
    {
        textManager.ChangeDecideStatus(textIndex);
    }
   
}
