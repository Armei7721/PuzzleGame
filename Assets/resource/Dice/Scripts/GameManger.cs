using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public bool Selectable = false;
    public static List<GameObject> array = new List<GameObject>();
    public GameObject Dice;
    public List<GameObject> DiceList = new List<GameObject>();
    private int currentPlayerIndex = 0;
    private bool isGameOver = false;

    public GameObject[] players; // �÷��̾���� �迭�� ����
    
  
    
    // Start is called before the first frame update
    void Start()
    {
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
    
    void DiceStart()
    {

    }
    void DiceSelect()
    {
        if (DiceRoll.diceVelocity == Vector3.zero)
        {
            Selectable = true;
        }
    }
}
