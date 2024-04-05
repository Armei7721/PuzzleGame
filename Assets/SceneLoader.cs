using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public MenuState select;
    public TextMeshProUGUI[] textChild;
    public GameObject readMe;
    private string sceneName;

    public enum MenuState
    {
        Start,
        HowPlay,
        Exit
    }

    // Start is called before the first frame update
    void Start()
    {
        select = MenuState.Start;
        UpdateTextColors();
    }

    // Update is called once per frame
    void Update()
    {
        Handler();
    }
    private void Handler()
    {
        if (!readMe.activeSelf)
        {
            // 방향키 입력 감지
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeMenuState(select - 1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeMenuState(select + 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 선택된 메뉴 상태에 따른 동작 처리
            switch (select)
            {
                case MenuState.Start:
                    
                        SceneManager.LoadScene("Dice");
                    
                    break;
                case MenuState.HowPlay:
                    readMe.SetActive(!readMe.activeSelf);
                    // How to Play 메뉴 동작 추가
                    break;
                case MenuState.Exit:
                    Application.Quit();
                    break;
            }
        }
    }
    // 선택된 메뉴 상태에 따라 텍스트 색상을 업데이트
    private void UpdateTextColors()
    {
        for (int i = 0; i < textChild.Length; i++)
        {
            if (textChild[i] != null)
            {
                // 선택된 메뉴 항목의 색상을 빨간색으로, 나머지 항목의 색상을 흰색으로 설정
                if ((int)select == i)
                {
                    textChild[i].color = Color.red;
                }
                else
                {
                    textChild[i].color = Color.white;
                }
            }
        }
    }

    // 선택된 메뉴 항목을 변경
    public void ChangeMenuState(MenuState newState)
    {
        // 선택된 메뉴 항목이 유효한지 확인하고 업데이트
        if (newState >= MenuState.Start && newState <= MenuState.Exit)
        {
            select = newState;
            UpdateTextColors();
        }
    }
}
