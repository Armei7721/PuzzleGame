using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DiceNumberText : MonoBehaviour
{
    TextMeshProUGUI text;
    public static int diceNumber;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = diceNumber.ToString();
    }
}
