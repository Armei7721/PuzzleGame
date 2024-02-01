using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Manager : MonoBehaviour
{
    public static UI_Manager ui;
    public Image warningImage;
    // Start is called before the first frame update
    void Start()
    {
        ui = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Warning()
    {   
        warningImage.GetComponent<GameObject>().SetActive(true);
        yield return new WaitForSeconds(2f);
        warningImage.GetComponent<GameObject>().SetActive(false);
    }
}
