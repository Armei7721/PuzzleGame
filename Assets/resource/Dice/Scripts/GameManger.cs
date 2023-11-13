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

    public GameObject[] players; // 플레이어들을 배열로 저장
    
  
    
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
}
