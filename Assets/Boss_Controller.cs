using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Controller : MonoBehaviour
{
    Animator animator;
    Transform[] boss_parts;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PartsChild();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator Mount()
    {

        yield return new WaitForSeconds(0.5f);
    }
    public void PartsChild()
    {
        boss_parts = gameObject.GetComponentsInChildren<Transform>(true);
        for (int i = 1; i < children.Length; i++)
        {
            Slut[i - 1] = children[i].gameObject;
        }
    }
}
