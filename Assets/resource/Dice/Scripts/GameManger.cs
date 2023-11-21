using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger gamemanager;
    public bool Selectable = false;
    //public static List<GameObject> array = new List<GameObject>();
    //public List<GameObject> DiceList = new List<GameObject>();
    private int currentPlayerIndex = 0;
    private bool isGameOver = false;

    public GameObject[] players; // 플레이어들을 배열로 저장

    public static GameObject[] Slut;
    public bool shakedice;
    public bool selectdice;

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

        // 여기에 해당 플레이어의 턴 동작을 추가
        
        // 예를 들어, 플레이어의 움직임이나 행동 등을 수행할 수 있습니다.

        // 플레이어의 턴이 끝날 때까지 대기
        yield return new WaitForSeconds(3f);
    }

    void ChangeTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;

        // 게임이 종료되었는지 확인 (예: 특정 조건에 따라)
        if (CheckForGameOver())
        {
            isGameOver = true;
            Debug.Log("Game Over");
        }
    }
    bool CheckForGameOver()
    {
        // 게임 종료 조건을 여기에 추가
        // 예를 들어, 모든 플레이어의 체력이 다 소진되면 게임 종료
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
}
