using UnityEngine;

public class HintScript : MonoBehaviour
{
    private Vector3 lastPosition;

    void Start()
    {
       
    }

    void Update()
    {
       
    }
    public void Hint()
    {
        if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }
}