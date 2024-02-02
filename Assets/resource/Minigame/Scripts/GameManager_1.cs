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

            // 자식 오브젝트들만 참조하는데, 부모 자신은 제외하기 위해 배열 크기를 조정합니다.
            boss_child = new GameObject[children.Length - 1];

            // 첫 번째 요소는 부모 자신이므로 인덱스를 1부터 시작하여 자식 오브젝트들을 참조합니다.
            for (int i = 1; i < children.Length; i++)
            {
                boss_child[i - 1] = children[i].gameObject;
                spriteRenderers = boss_child[i].GetComponents<SpriteRenderer>();

                //// 모든 SpriteRenderer의 Sorting Layer를 변경합니다.
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
