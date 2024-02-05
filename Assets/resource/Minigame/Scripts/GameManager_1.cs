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

            // �ڽ� ������Ʈ�鸸 �����ϴµ�, �θ� �ڽ��� �����ϱ� ���� �迭 ũ�⸦ �����մϴ�.
            boss_child = new GameObject[children.Length - 1];

            // ù ��° ��Ҵ� �θ� �ڽ��̹Ƿ� �ε����� 1���� �����Ͽ� �ڽ� ������Ʈ���� �����մϴ�.
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
