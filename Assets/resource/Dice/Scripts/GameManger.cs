using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public bool Selectable = false;
    public static List<GameObject> array = new List<GameObject>();
    public List<GameObject> Dice = new List<GameObject>();
    private int currentPlayerIndex = 0;
    private bool isGameOver = false;

    public GameObject[] players; // �÷��̾���� �迭�� ����
    
    [Header("���̽� ��")]
    static Rigidbody rb;
    public static Vector3 diceVelocity;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTurns());
    }

    // Update is called once per frame
    void Update()
    {
        Diceroll();
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
    public void Diceroll()
    {
        diceVelocity = rb.velocity;

        if (Input.GetMouseButton(0))
        {
            float dirX = Random.Range(0, 500);
            float dirY = Random.Range(0, 500);
            float dirZ = Random.Range(0, 500);
            for (int i = 0; i < Dice.Count; i++)
            {
                transform.position = new Vector3(0, 2, 0);
                transform.rotation = Quaternion.identity;
                rb.AddForce(transform.up * 10);
                rb.AddTorque(dirX, dirY, dirZ);
            }
        }
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
