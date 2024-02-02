using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_1 : MonoBehaviour
{
    public GameObject[] boss_child;
    public SpriteRenderer[] spriteRenderers;
    // Start is called before the first frame update
    void Start()
    {
        Childlist();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Childlist()
    {
        GameObject parentObject = GameObject.Find("Boss_Ent");
        if (parentObject != null)
        {
            Transform[] children = parentObject.GetComponentsInChildren<Transform>(true);

            // �ڽ� ������Ʈ�鸸 �����ϴµ�, �θ� �ڽ��� �����ϱ� ���� �迭 ũ�⸦ �����մϴ�.
            boss_child = new GameObject[children.Length - 1];

            // ù ��° ��Ҵ� �θ� �ڽ��̹Ƿ� �ε����� 1���� �����Ͽ� �ڽ� ������Ʈ���� �����մϴ�.
            for (int i = 1; i < children.Length; i++)
            {
                boss_child[i - 1] = children[i].gameObject;
                spriteRenderers = boss_child[i].GetComponents<SpriteRenderer>();

                //// ��� SpriteRenderer�� Sorting Layer�� �����մϴ�.
                //foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                //{
                //    spriteRenderer.sortingLayerName = "Default";
                //}
            }
        }
        else
        {
            return;
        }
    }
}
