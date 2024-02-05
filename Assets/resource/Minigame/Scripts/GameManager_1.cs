using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_1 : MonoBehaviour
{
    public GameObject[] boss_child;
    private string[][] originalSortingLayers;

    public bool bossEnCounter;
    // Start is called before the first frame update
    void Start()
    {
        Childlist();
        SaveOriginalSortingLayers();
        ChangeSortingLayer("MiddleGround");
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
            }
        }
    }

    void SaveOriginalSortingLayers()
    {
        originalSortingLayers = new string[boss_child.Length][];

        for (int i = 0; i < boss_child.Length; i++)
        {
            SpriteRenderer[] childSpriteRenderers = boss_child[i].GetComponentsInChildren<SpriteRenderer>();
            originalSortingLayers[i] = new string[childSpriteRenderers.Length];

            for (int j = 0; j < childSpriteRenderers.Length; j++)
            {
                originalSortingLayers[i][j] = childSpriteRenderers[j].sortingLayerName;
            }
        }
    }

    void ChangeSortingLayer(string newSortingLayer)
    {
        for (int i = 0; i < boss_child.Length; i++)
        {
            SpriteRenderer[] childSpriteRenderers = boss_child[i].GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer spriteRenderer in childSpriteRenderers)
            {
                spriteRenderer.sortingLayerName = newSortingLayer;
            }
        }
    }

    void RestoreOriginalSortingLayers()
    {
        for (int i = 0; i < boss_child.Length; i++)
        {
            SpriteRenderer[] childSpriteRenderers = boss_child[i].GetComponentsInChildren<SpriteRenderer>();

            for (int j = 0; j < childSpriteRenderers.Length; j++)
            {
                childSpriteRenderers[j].sortingLayerName = originalSortingLayers[i][j];
            }
        }
    }
}
