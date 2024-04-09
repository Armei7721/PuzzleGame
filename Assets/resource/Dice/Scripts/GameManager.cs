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

    public GameObject[] players; // 플레이어들을 배열로 저장
    public GameObject spUI;
    public static GameObject[] Slut;
    public GameObject[] slots;
    public bool shakedice;
    public bool selectdice;

    public GameObject[] diceObjects;
    public GameObject[] conditiontransform;
    public List<GameObject> conditionDice = new List<GameObject>();

    public Phase currentPhase;
    public bool act;
    public bool player1;
    public bool player2;

    public enum Phase
    {
        throwPhase,
        selectPhase,
        rerolllPhase,
        scorePhase
    }

    private void Awake()
    {
        gamemanager = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        player1 = true;
        player2 = false;
        currentPhase = Phase.throwPhase;
        selectdice = false;
        ConditionDice();
        InsertDice();
        SlutEquip();

    }

    // Update is called once per frame
    void Update()
    {
        SelectPhase();
        ClickDice();
        Escape();
        StateChange();
        ChangeTurn();
    }

    void GameOver()
    {
        if(isGameOver)
        {

        }
    }
    // 현재 상태가 selectPhase상태면 UI 활성화
    void SelectPhase()
    {
        if (currentPhase== Phase.selectPhase)
        {
            spUI.SetActive(true);
        }
        else if (currentPhase != Phase.selectPhase)
        {
            spUI.SetActive(false);

        }
    }
    void ChangeTurn()
    {
        // 게임이 종료되었는지 확인 (예: 특정 조건에 따라)
        if (CheckForGameOver())
        {
            isGameOver = true;
         
        }
    }
    bool CheckForGameOver()
    {
        // 게임 종료 조건을 여기에 추가
        for (int i = 0; i < TextManager.text.isConfirmed.Length; i++)
        {
            if (TextManager.text.isConfirmed[i] == true && TextManager.text.isConfirmed[i] == true)
            {
                return true;
            }

            // 예를 들어, 모든 플레이어의 체력이 다 소진되면 게임 종료
        }
        return false;
    }

    void SlutEquip()
    {
        GameObject parentObject = GameObject.Find("DicePlane");
        if (parentObject != null)
        {
            Transform[] children = parentObject.GetComponentsInChildren<Transform>(true);

            // 자식 오브젝트들만 참조하는데, 부모 자신은 제외하기 위해 배열 크기를 조정합니다.
            Slut = new GameObject[children.Length - 1];

            // 첫 번째 요소는 부모 자신이므로 인덱스를 1부터 시작하여 자식 오브젝트들을 참조합니다.
            for (int i = 1; i < children.Length; i++)
            {
                Slut[i - 1] = children[i].gameObject;
            }
        }
        else
        {
            Debug.LogWarning("DicePlane을 찾을 수 없습니다.");
        }
    }
    void InsertDice()
    {
        // "Dice" 태그가 지정된 모든 게임 오브젝트를 찾습니다.
        diceObjects = GameObject.FindGameObjectsWithTag("Dice");
        foreach (GameObject diceObject in diceObjects)
        {
            // 각 게임 오브젝트에서 Dice 스크립트를 찾습니다.
            Dice diceComponent = diceObject.GetComponent<Dice>();
            if (diceComponent != null)
            {
                // Dice 스크립트가 있다면 해당 게임 오브젝트를 conditionDice 리스트에 추가합니다.
                conditionDice.Add(diceObject);
            }
        }
    }
    void ConditionDice()
    {   //conditionDice의 자식 오브젝트들을 가져옴
        GameObject parentObject = GameObject.Find("ConditionDice");
        if (parentObject != null)
        {
            Transform[] children = parentObject.GetComponentsInChildren<Transform>(true);

            // 자식 오브젝트들만 참조하는데, 부모 자신은 제외하기 위해 배열 크기를 조정합니다.
            conditiontransform = new GameObject[children.Length - 1];

            // 첫 번째 요소는 부모 자신이므로 인덱스를 1부터 시작하여 자식 오브젝트들을 참조합니다.
            for (int i = 1; i < children.Length; i++)
            {
                conditiontransform[i - 1] = children[i].gameObject;
            }
        }
        else
        {
            Debug.LogWarning("DicePlane을 찾을 수 없습니다.");
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
            // 다른 작업 수행...
        }
    }
    public void PlaceDiceInSlot(GameObject diceObject)
    {
        int emptySlotIndex = FindEmptySlotIndex(); // 빈 슬롯 인덱스 찾기
        if (emptySlotIndex != -1)
        {
            // 빈 슬롯이 있으면 해당 슬롯에 주사위를 넣음
            slots[emptySlotIndex] = diceObject;
            diceObject.transform.position = Slut[emptySlotIndex].transform.position;
            // 슬롯 상태를 채워짐으로 변경하거나 필요에 따라 관리
        }
    }

    // 빈 슬롯 인덱스를 찾는 함수
    private int FindEmptySlotIndex()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // 슬롯이 비어있는지 확인하고 빈 슬롯의 인덱스 반환
            if (slots[i] == null)
            {
                return i; // 비어 있는 슬롯의 인덱스 반환
            }
        }
        return -1; // 빈 슬롯을 찾지 못한 경우 -1 반환
    }
    // 현재 상태를 변경 시켜주는 함수
    public void StateChange()
    {
        switch (currentPhase)
        {
            case (Phase.selectPhase):
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    currentPhase = Phase.scorePhase;
                }
                else if(Input.GetKeyDown(KeyCode.RightArrow)&& CupShaking.rollChance!=0)
                {
                    Dice.dice.ResetDice();
                    currentPhase = Phase.throwPhase;
                    
                }
                break;
            case (Phase.scorePhase):
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    currentPhase = Phase.selectPhase;
                }
                break;     
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
