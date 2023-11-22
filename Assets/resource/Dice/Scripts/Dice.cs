using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
	static Rigidbody rb; // 주사위의 리지드바디
	static bool hasLanded; // 주사위가 땅에 도달했는지 확인하는 변수
	static bool thrown; // 주사위가 던져졌는지 확인하는 변수
	Vector3 initPosition; // 주사위의 초기 위치
	public GameObject SelectDice; // 현재 선택된 다이스
	public bool isSelected;

	public static int diceValue; // 주사위의 눈을 담을 변수
	public bool[] inSlot;
	
	
	Vector3 MouseDownPos;  // 마우스 클릭 위치
	public GameObject[] Slots;
	int slotValue;


	public static bool resetPosition;  // 점수판에 점수를 넣었을때 주사위 위치 전체 초기화
	Vector3 DicePosition;
	Quaternion DiceRotation;
	Vector3 upDice;
	public bool SetDice = false;

	// Start is called before the first frame update
	void Start()
	{
		// 각종 변수 초기화
		rb = GetComponent<Rigidbody>();
		initPosition = transform.position;
		thrown = false;
		inSlot = new bool[] { false, false, false, false, false };
		slotValue = 0;
		resetPosition = false;
	}

	// Update is called once per frame
	void Update()
	{
		
		if (rb.IsSleeping() && !hasLanded && thrown)
		{
			hasLanded = true;
		}
		if (rb.IsSleeping() && hasLanded && thrown)
		{
			if (Input.GetMouseButtonDown(0))
			{
				
				MouseDownPos = Input.mousePosition;
				Ray ray = Camera.main.ScreenPointToRay(MouseDownPos);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					
					GameObject hitObject = hit.transform.gameObject;
					if (hitObject.CompareTag("Dice"))
					{
						Dice diceScript = hitObject.GetComponent<Dice>();

						if (diceScript != null && !diceScript.isSelected)
						{
							if (slotValue < 5 && SetDice == false)
							{
								SelectDice = hit.transform.gameObject;
								//Dice	SelectDice = hit.collider.GetComponent<Dice>();
								slotValue++;
								SelectDice.GetComponent<Dice>().SetDice = true;
								//SelectDice.SetDice = true;
								PutSlot(hit);
								Debug.Log(SelectDice);
							}
							
						}
						else if (SetDice == true)
						{
							PopSlot(hit, slotValue - 1);
						}
					}
					else if(hitObject==null)
                    {
						return;
                    }
				}
			}
			if (Input.GetMouseButtonUp(0) && SelectDice != null)
			{
				SelectDice.GetComponent<Dice>().isSelected = false;
				SelectDice = null;
				// 다른 작업 수행...
			}

		}

		if (Input.GetKeyDown(KeyCode.R) && slotValue == 0) // R을 눌렀을때 슬롯에 없다면? 처음위치(다시굴리기)로 이동
		{
			ResetDice();
		}
		// 두 조건문의 차이점 : 아래거는 숫자판에 값을 넣었을때 실행. 슬롯보드에 무관하게 전체 초기화. 위에거는 슬롯보드는 저장
		if (resetPosition)
		{
			GameManager.gamemanager.selectdice = false;
			thrown = false;
			hasLanded = false;
			transform.position = initPosition;
			resetPosition = false;
			diceValue = 0;
		
		}
	}

	public static void RollDice()
	{
		if (!thrown && !hasLanded)
		{
			thrown = true;
			rb.AddForce(Random.Range(1000, 2000), 0, Random.Range(2500, 4500));
		}
	}
	void PutSlot(RaycastHit hit)
	{
		for (int i = 0; i < 5; i++)
		{
			if (inSlot[i] == false)
			{
				// 슬롯에서 뺐을때 주사위 위치를 지정해주기 위해서 현재 주사위 위치를 저장
				DicePosition = hit.transform.position;
				DiceRotation = hit.transform.rotation;
				// 주사위 위치를 슬롯으로 이동
				SelectDice.transform.position = GameManager.Slut[i].transform.position;
				SelectDice.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0f, transform.rotation.eulerAngles.z);
				inSlot[i] = true;
				break;
			}
			
		}
	}
	void PopSlot(RaycastHit hit, int value)
	{
		hit.transform.position = DicePosition;
		hit.transform.rotation = DiceRotation;
		inSlot[value] = false;
		SetDice = false;
	}
	void ResetDice()
	{
		thrown = false;
		hasLanded = false;
		transform.position = initPosition;
		diceValue = 0;
	}
}
