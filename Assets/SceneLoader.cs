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
            // ����Ű �Է� ����
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
            // ���õ� �޴� ���¿� ���� ���� ó��
            switch (select)
            {
                case MenuState.Start:
                    
                        SceneManager.LoadScene("Dice");
                    
                    break;
                case MenuState.HowPlay:
                    readMe.SetActive(!readMe.activeSelf);
                    // How to Play �޴� ���� �߰�
                    break;
                case MenuState.Exit:
                    Application.Quit();
                    break;
            }
        }
    }
    // ���õ� �޴� ���¿� ���� �ؽ�Ʈ ������ ������Ʈ
    private void UpdateTextColors()
    {
        for (int i = 0; i < textChild.Length; i++)
        {
            if (textChild[i] != null)
            {
                // ���õ� �޴� �׸��� ������ ����������, ������ �׸��� ������ ������� ����
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

    // ���õ� �޴� �׸��� ����
    public void ChangeMenuState(MenuState newState)
    {
        // ���õ� �޴� �׸��� ��ȿ���� Ȯ���ϰ� ������Ʈ
        if (newState >= MenuState.Start && newState <= MenuState.Exit)
        {
            select = newState;
            UpdateTextColors();
        }
    }
}
