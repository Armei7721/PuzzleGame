using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    public TextManager textManager;
    public int textIndex; // 각 TextMeshProUGUI의 인덱스를 나타냅니다.

    public void OnClick()
    {
        textManager.ChangeDecideStatus(textIndex);
    }
   
}
