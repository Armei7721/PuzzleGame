using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
	public static Dice dice;
	private Rigidbody rb; // 주사위의 리지드바디
	static bool hasLanded; // 주사위가 땅에 도달했는지 확인하는 변수
	public static bool thrown; // 주사위가 던져졌는지 확인하는 변수
	Vector3 initPosition; // 주사위의 초기 위치
	Vector3 rememeberDice; // 

	public GameObject SelectDice; // 현재 선택된 다이스
	public bool isSelected;
	public bool[] inSlot;

	public static int diceValue; // 주사위의 눈을 담을 변수
	
	Vector3 MouseDownPos;  // 마우스 클릭 위치
	int slotValue;

	public static bool resetPosition;  // 점수판에 점수를 넣었을때 주사위 위치 전체 초기화
	public bool SetDice = false;
	int a = 0;
	public float timer;

	// Start is called before the first frame update
	void Start()
	{
		inSlot = new bool[] { false, false, false, false, false };
		dice = this;
		// 각종 변수 초기화
		rb = GetComponent<Rigidbody>();
		initPosition = transform.position;
		thrown = false;
		
		slotValue = 0;
		resetPosition = false;

		//InsertDice();

	}

	// Update is called once per frame
	void Update()
	{
		Throw();
		ClickDice();
	}
	public void RollDice()
	{
		if (!thrown && !hasLanded)
		{
			thrown = true;
			rb.AddForce(Random.Range(1000, 2000), 0, Random.Range(2500, 4500));
		}
	}
	public void Throw()
	{
		//timer += Time.deltaTime;
		if (rb.IsSleeping() && !hasLanded && thrown)
		{

			timer += Time.deltaTime;
			hasLanded = true;

		}
		if (rb.IsSleeping() && hasLanded && thrown && !SetDice)
		{
			timer += Time.deltaTime;
			if (timer >= 2.0f)
			{
				
				for (int i = 0; i < GameManager.gamemanager.conditionDice.Count; i++)
				{
					GameObject diceObject = GameManager.gamemanager.conditionDice[i];
					Rigidbody rb = diceObject.GetComponent<Rigidbody>();

					if (rb != null)
					{
						rb.isKinematic = true;
					}
				}
				transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0f, transform.rotation.eulerAngles.z);

				if (timer >= 3.0f && a==0)
				{
					for (int i = 0; i < GameManager.gamemanager.conditionDice.Count; i++)
					{
						if (GameManager.gamemanager.conditionDice.Contains(gameObject) != false)
						{
							GameManager.gamemanager.conditionDice[i].transform.position = Vector3.Lerp(GameManager.gamemanager.conditionDice[i].transform.position, GameManager.gamemanager.conditiontransform[i].transform.position, 0.6f);

                            if (GameManager.gamemanager.conditionDice[i].transform.position== GameManager.gamemanager.conditiontransform[i].transform.position)
                            {
								//a = 1;
                            }
						}

						else
						{
							continue;
						}
					}
					
				}


			}
		}
	}
	public void ClickDice()
	{
		if (rb.IsSleeping() && hasLanded && thrown)
		{
			if (Input.GetMouseButtonDown(0))
			{
				MouseDownPos = Input.mousePosition;
				Ray ray = Camera.main.ScreenPointToRay(MouseDownPos);
				RaycastHit hit;
				
				if (Physics.Raycast(ray, out hit))
				{
					//Debug.DrawLine(ray.origin, hit.point, Color.red, 1.0f);
					GameObject hitObject = hit.transform.gameObject;
					if (hitObject.CompareTag("Dice"))
					{
						Dice diceScript = hitObject.GetComponent<Dice>();
						if (diceScript != null && !diceScript.isSelected)
						{
							SelectDice = hit.transform.gameObject;
							if (slotValue < 5 && SelectDice.GetComponent<Dice>().SetDice == false)
							{
								if (GameManager.gamemanager.conditionDice.Contains(SelectDice.gameObject))
								{								
									GameManager.gamemanager.conditionDice.Remove(SelectDice.gameObject);
								}
								slotValue++;
								SelectDice.GetComponent<Dice>().SetDice = true;
								PutSlot(hit);
								Debug.Log("발동이 되네");
							}
							else if (SelectDice.GetComponent<Dice>().SetDice == true)
							{
								Debug.Log("Test");
								PopSlot(hit);
							}

						}
                        

                    }
					
					else if (hitObject == null)
					{
						return;
					}
					Debug.Log(hit.collider.gameObject.tag);
				}
			}
			if (Input.GetMouseButtonUp(0) && SelectDice != null)
			{
				SelectDice.GetComponent<Dice>().isSelected = false;
				SelectDice = null;
				// 다른 작업 수행...
			}



			if (Input.GetKeyDown(KeyCode.R)) // R을 눌렀을때 슬롯에 없다면? 처음위치(다시굴리기)로 이동
			{
				ResetDice();
			}
			// 두 조건문의 차이점 : 아래거는 숫자판에 값을 넣었을때 실행. 슬롯보드에 무관하게 전체 초기화. 위에거는 슬롯보드는 저장
			if (resetPosition)
			{
				
				thrown = false;
				hasLanded = false;
				transform.position = initPosition;
				resetPosition = false;
				diceValue = 0;

			}
		}
	}
	void ResetDice()
	{if (SetDice != false)
		{
			GameManager.gamemanager.selectdice = false;
			thrown = false;
			hasLanded = false;
			transform.position = initPosition;
			diceValue = 0;
		}
	}
	
	
	void PutSlot(RaycastHit hit)
	{
		
		for (int i = 0; i < 5; i++)
		{
			if (inSlot[i] == false)
			{
				// 슬롯에서 뺐을때 주사위 위치를 지정해주기 위해서 현재 주사위 위치를 저장
				//GameManager.gamemanager.conditionDice.Remove(SelectDice.gameObject);
				// 주사위 위치를 슬롯으로 이동

				SelectDice.transform.position = GameManager.Slut[i].transform.position;
				inSlot[i] = true;
				break;
			}
			
		}
	}
	void PopSlot(RaycastHit hit)
	{
		Debug.Log("이게 왜 발동이 될까요");

		GameManager.gamemanager.conditionDice.Add(SelectDice.gameObject);

		SelectDice.GetComponent<Dice>().SetDice = false;
    }
}
