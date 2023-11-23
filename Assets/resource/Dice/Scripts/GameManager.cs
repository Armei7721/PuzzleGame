using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager gamemanager;
    public bool Selectable = false;
    private int currentPlayerIndex = 0;
    private bool isGameOver = false;

    public GameObject[] players; // �÷��̾���� �迭�� ����

    public static GameObject[] Slut;
    public bool shakedice;
    public bool selectdice;

    public static GameObject[] ChooseDice;
    private void Awake()
    {
        gamemanager = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        SlutEquip();
        StartCoroutine(StartTurns());
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartTurns()
    {
        while (!isGameOver)
        {
            yield return StartCoroutine(PlayerTurn());
            ChangeTurn();
        }
    }
    IEnumerator PlayerTurn()
    {
        Debug.Log("Player " + (currentPlayerIndex + 1) + "'s turn");

        // ���⿡ �ش� �÷��̾��� �� ������ �߰�
        
        // ���� ���, �÷��̾��� �������̳� �ൿ ���� ������ �� �ֽ��ϴ�.

        // �÷��̾��� ���� ���� ������ ���
        yield return new WaitForSeconds(3f);
    }

    void ChangeTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;

        // ������ ����Ǿ����� Ȯ�� (��: Ư�� ���ǿ� ����)
        if (CheckForGameOver())
        {
            isGameOver = true;
            Debug.Log("Game Over");
        }
    }
    bool CheckForGameOver()
    {
        // ���� ���� ������ ���⿡ �߰�
        // ���� ���, ��� �÷��̾��� ü���� �� �����Ǹ� ���� ����
        return false;
    }
    
    void SlutEquip()
    {
        GameObject parentObject = GameObject.Find("DicePlane");
        if (parentObject != null)
        {
            Transform[] children = parentObject.GetComponentsInChildren<Transform>(true);

            // �ڽ� ������Ʈ�鸸 �����ϴµ�, �θ� �ڽ��� �����ϱ� ���� �迭 ũ�⸦ �����մϴ�.
            Slut = new GameObject[children.Length - 1];

            // ù ��° ��Ҵ� �θ� �ڽ��̹Ƿ� �ε����� 1���� �����Ͽ� �ڽ� ������Ʈ���� �����մϴ�.
            for (int i = 1; i < children.Length; i++)
            {
                Slut[i - 1] = children[i].gameObject;
            }
        }
        else
        {
            Debug.LogWarning("DicePlane�� ã�� �� �����ϴ�.");
        }
    }
    void SelectingDice()
    {
        if(Dice.thrown== true)
        {

        }
    }
}
