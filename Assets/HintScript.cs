using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hint()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }
}
