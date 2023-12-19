using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject Wall;
    public static GameManager gamemanager;
    public bool Selectable = false;
    private int currentPlayerIndex = 0;
    private bool isGameOver = false;

    public GameObject[] players; // �÷��̾���� �迭�� ����
    public GameObject spUI;
    public static GameObject[] Slut;
    public GameObject[] slots;
    public bool shakedice;
    public bool selectdice;

    public GameObject[] conditiontransform;
    public List<GameObject> conditionDice = new List<GameObject>();

    public bool throwPhase ;
    public bool selectPhase;
    public bool rerolllPhase;
    public bool scorePhase;
    public bool act;
    public bool player1;
    public bool player2;

    private void Awake()
    {
        gamemanager = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        player1 = true;
        player2 = false;
        throwPhase = true;
        selectdice = false;
        ConditionDice();
        InsertDice();
        SlutEquip();
        StartCoroutine(StartTurns());

    }

    // Update is called once per frame
    void Update()
    {
        SelectPhase();
        ClickDice();
        Escape();
        StateChange();
        Debug.Log(player1+" �÷��̾� 1�� bool ����");
        Debug.Log(player2 + " �÷��̾� 2�� bool ����");
    }
    
    IEnumerator StartTurns()
    {
        while (!isGameOver)
        {
            yield return StartCoroutine(PlayerTurn());
            //ChangeTurn();
        }
    }
    IEnumerator PlayerTurn()
    {
        Debug.Log("Player " + (currentPlayerIndex + 1) + "'s turn");
        if(player1==true)
        {

        }
        else if(player2 ==true)
        {

        }
        // ���⿡ �ش� �÷��̾��� �� ������ �߰�

        // ���� ���, �÷��̾��� �������̳� �ൿ ���� ������ �� �ֽ��ϴ�.

        // �÷��̾��� ���� ���� ������ ���
        yield return new WaitForSeconds(3f);
    }

    void SelectPhase()
    {
        if (selectPhase)
        {
            spUI.SetActive(true);
        }
        else if(!selectPhase)
        {
            spUI.SetActive(false);

        }
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
    void InsertDice()
    {
        // "Dice" �±װ� ������ ��� ���� ������Ʈ�� ã���ϴ�.
        GameObject[] diceObjects = GameObject.FindGameObjectsWithTag("Dice");
        foreach (GameObject diceObject in diceObjects)
        {
            // �� ���� ������Ʈ���� Dice ��ũ��Ʈ�� ã���ϴ�.
            Dice diceComponent = diceObject.GetComponent<Dice>();
            if (diceComponent != null)
            {
                // Dice ��ũ��Ʈ�� �ִٸ� �ش� ���� ������Ʈ�� conditionDice ����Ʈ�� �߰��մϴ�.
                conditionDice.Add(diceObject);
            }
        }
    }
    void ConditionDice()
    {   //conditionDice�� �ڽ� ������Ʈ���� ������
        GameObject parentObject = GameObject.Find("ConditionDice");
        if (parentObject != null)
        {
            Transform[] children = parentObject.GetComponentsInChildren<Transform>(true);

            // �ڽ� ������Ʈ�鸸 �����ϴµ�, �θ� �ڽ��� �����ϱ� ���� �迭 ũ�⸦ �����մϴ�.
            conditiontransform = new GameObject[children.Length - 1];

            // ù ��° ��Ҵ� �θ� �ڽ��̹Ƿ� �ε����� 1���� �����Ͽ� �ڽ� ������Ʈ���� �����մϴ�.
            for (int i = 1; i < children.Length; i++)
            {
                conditiontransform[i - 1] = children[i].gameObject;
            }
        }
        else
        {
            Debug.LogWarning("DicePlane�� ã�� �� �����ϴ�.");
        }
    }

    public void ClickDice()
    {
        if (Dice.dice.rb.IsSleeping() && Dice.hasLanded && Dice.thrown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Dice.dice.ClickDice();
                
            }
           
        }
        if (Input.GetMouseButtonUp(0) && Dice.dice.SelectDice != null)
        {
            Dice.dice.SelectDice.GetComponent<Dice>().isSelected = false;
            Dice.dice.SelectDice = null;
            // �ٸ� �۾� ����...
        }
    }
    public void PlaceDiceInSlot(GameObject diceObject)
    {
        int emptySlotIndex = FindEmptySlotIndex(); // �� ���� �ε��� ã��
        if (emptySlotIndex != -1)
        {
            // �� ������ ������ �ش� ���Կ� �ֻ����� ����
            slots[emptySlotIndex] = diceObject;
            diceObject.transform.position = Slut[emptySlotIndex].transform.position;
            // ���� ���¸� ä�������� �����ϰų� �ʿ信 ���� ����
        }
    }

    // �� ���� �ε����� ã�� �Լ�
    private int FindEmptySlotIndex()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // ������ ����ִ��� Ȯ���ϰ� �� ������ �ε��� ��ȯ
            if (slots[i]== null)
            {
                return i; // ��� �ִ� ������ �ε��� ��ȯ
            }
        }
        return -1; // �� ������ ã�� ���� ��� -1 ��ȯ
    }
    public void StateChange()
    {
        if(selectPhase && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log(scorePhase);
            selectPhase = false;
            scorePhase = true;
        }
        else if(selectPhase && Input.GetKeyDown(KeyCode.RightArrow) && CupShaking.rollChance!=0)
        {
            Debug.Log(CupShaking.rollChance);
            Dice.dice.ResetDice();
            selectPhase = false;
            throwPhase = true;
        }
        else if(scorePhase && Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectPhase = true;
            scorePhase = false;
        }
        
    }

    private void Escape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
