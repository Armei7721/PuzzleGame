using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
	public static Dice dice;
	public Rigidbody rb; // 주사위의 리지드바디
	public static bool hasLanded; // 주사위가 땅에 도달했는지 확인하는 변수
	public static bool thrown; // 주사위가 던져졌는지 확인하는 변수
	public Vector3 initPosition; // 주사위의 초기 위치

	public GameObject SelectDice; // 현재 선택된 다이스
	public bool isSelected;

	public int diceValue; // 주사위의 눈을 담을 변수
	public Vector3 MouseDownPos;  // 마우스 클릭 위치


	public static bool resetPosition;  // 점수판에 점수를 넣었을때 주사위 위치 전체 초기화
	public bool SetDice = false;
	int a = 0;
	public float timer;
	public bool dicerotate = false;
	// Start is called before the first frame update
	void Start()
	{

		dice = this;
		// 각종 변수 초기화
		rb = GetComponent<Rigidbody>();
		initPosition = transform.position;
		thrown = false;
		resetPosition = false;

	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log("dicerotate 상태 " + dicerotate);
		Throw();
	}
	public void RollDice()
	{
		GameObject[] diceObjects = GameObject.FindGameObjectsWithTag("Dice");
		if (!thrown && !hasLanded)
		{
			thrown = true;
			foreach (GameObject diceObject in diceObjects)
			{
				Rigidbody rb = diceObject.GetComponent<Rigidbody>();

				if (rb != null)
				{
					rb.AddForce(Random.Range(-10000, -12000), 0, Random.Range(-6500, 2500));
				}
				else
				{
					continue;
				}
			}
		}
	}
	public void Throw()
	{
		// 모든 다이스가 멈춘 경우를 체크
		
		bool allDiceStopped = true;
		foreach (GameObject diceObject in GameManager.gamemanager.conditionDice)
		{
			Rigidbody diceRB = diceObject.GetComponent<Rigidbody>();
			if (!diceRB.IsSleeping())
			{
				allDiceStopped = false;
				break;
			}
		}

		// 모든 다이스가 멈추면서 아직 던진 상태이고, 아직 땅에 떨어지지 않은 경우
		if (allDiceStopped && !hasLanded && thrown)
		{
			hasLanded = true;
		}

		// 모든 다이스가 멈추면서 땅에 떨어진 경우이고, 아직 처리되지 않은 경우
		if (allDiceStopped && hasLanded && thrown && !SetDice)
		{
			timer += Time.deltaTime;

			// 2초가 경과했을 때
			if (timer >= 2.0f)
			{

				StartCoroutine(rotation());
				DiceKinmatic();
				// 3초가 경과했을 때
				if (timer >= 3.0f)
				{
					
					// 각 주사위를 목적지로 점진적으로 이동시킴
					for (int i = 0; i < GameManager.gamemanager.conditionDice.Count; i++)
					{
						GameObject diceObject = GameManager.gamemanager.conditionDice[i];
						if (GameManager.gamemanager.conditionDice.Contains(gameObject))
						{
							diceObject.transform.position = Vector3.Lerp(diceObject.transform.position, GameManager.gamemanager.conditiontransform[i].transform.position, 0.6f);
							if (!GameManager.gamemanager.scorePhase)
							{
								GameManager.gamemanager.selectPhase = true;
							}
						}
					}
				}
			}
		}
	}
	public IEnumerator rotation()
    {
		if (!dicerotate)
		{
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0f, transform.rotation.eulerAngles.z);
			yield return new WaitForSeconds(1f);
			dicerotate = true;
		}
	}
	public void DiceKinmatic()
    {	
		foreach (GameObject diceObject in GameManager.gamemanager.conditionDice)
		{
			Rigidbody diceRB = diceObject.GetComponent<Rigidbody>();
			if (diceRB != null)
			{
				diceRB.isKinematic = true;
			}
		}
		
	}
	public void ClickDice()
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
					SelectDice = hitObject;
					if (SelectDice.GetComponent<Dice>().SetDice == false)
					{
						SelectDice.GetComponent<Dice>().SetDice = true;
						if (GameManager.gamemanager.conditionDice.Contains(SelectDice.gameObject))
						{
							GameManager.gamemanager.conditionDice.Remove(SelectDice.gameObject);
							GameManager.gamemanager.PlaceDiceInSlot(SelectDice.gameObject);
							PutSlot(hit);
						}
					}
					else if (SelectDice.GetComponent<Dice>().SetDice == true)
					{
						for (int i = 0; i < GameManager.gamemanager.slots.Length; i++)
						{
							if (GameManager.gamemanager.slots[i] == SelectDice.gameObject)
							{
								GameManager.gamemanager.slots[i] = null;
							}
						}
						PopSlot(hit);
					}

				}


			}
			else if (hitObject == null)
			{
				return;
			}
		}
	}

	public void ResetDice()
	{
		if (GameManager.gamemanager.selectPhase && Input.GetKeyDown(KeyCode.RightArrow)) // R을 눌렀을때 슬롯에 없다면? 처음위치(다시굴리기)로 이동
		{
			for (int j = 0; j < GameManager.gamemanager.diceObjects.Length; j++)
			{
				if (GameManager.gamemanager.diceObjects[j].GetComponent<Dice>().SetDice == false)
				{
					for (int i = 0; i < GameManager.gamemanager.conditionDice.Count; i++)
					{
						GameObject diceObject = GameManager.gamemanager.conditionDice[i];
						Rigidbody rb = diceObject.GetComponent<Rigidbody>();

						if (rb != null)
						{
							rb.isKinematic = false;
						}
						diceObject.GetComponent<Dice>().transform.position = initPosition;
						diceObject.GetComponent<Dice>().timer = 0;
					}
					GameManager.gamemanager.selectdice = false;
					GameManager.gamemanager.Wall.SetActive(true);
					GameManager.gamemanager.throwPhase = true;
					GameManager.gamemanager.scorePhase = false;
					GameManager.gamemanager.selectPhase = false;
					dicerotate = false;
					thrown = false;
					hasLanded = false;


				}
			}
		}
		else if (GameManager.gamemanager.scorePhase && GameManager.gamemanager.act)
		{

			AllReset();
			GameManager.gamemanager.act = false;
		}
	}


	public void PutSlot(RaycastHit hit)
	{

		for (int i = 0; i < GameManager.Slut.Length; i++)
		{
			if (GameManager.gamemanager.slots == null)
			{
				SelectDice.transform.position = GameManager.Slut[i].transform.position;
				break;
			}

		}
	}
	public void PopSlot(RaycastHit hit)
	{
		for (int i = 0; i < GameManager.Slut.Length; i++)
		{
			if (GameManager.gamemanager.slots != null)
			{
				GameManager.gamemanager.conditionDice.Add(SelectDice.gameObject);
				SelectDice.GetComponent<Dice>().SetDice = false;
				break;
			}
		}

	}
	public void SetDiceValue(int value)
	{
		diceValue = value;
	}

	// 눈 값 반환 함수
	public int GetDiceValue()
	{
		return diceValue;
	}

	public void AllReset()
	{
		for (int i = 0; i < GameManager.gamemanager.slots.Length; i++)
		{
			if (GameManager.gamemanager.slots[i] != null)
			{
				GameManager.gamemanager.conditionDice.Add(GameManager.gamemanager.slots[i]);
				GameManager.gamemanager.slots[i] = null;
			}
		}
		for (int i = 0; i < GameManager.gamemanager.conditionDice.Count; i++)
		{
			GameObject diceObject = GameManager.gamemanager.conditionDice[i];
			Rigidbody rb = diceObject.GetComponent<Rigidbody>();

			if (rb != null)
			{
				rb.isKinematic = false;
			}
			diceObject.GetComponent<Dice>().transform.position = initPosition;
			diceObject.GetComponent<Dice>().SetDice = false;
			diceObject.GetComponent<Dice>().timer = 0;
		}
		CupShaking.rollChance = 3;
		GameManager.gamemanager.selectdice = false;
		GameManager.gamemanager.Wall.SetActive(true);
		GameManager.gamemanager.throwPhase = true;
		GameManager.gamemanager.scorePhase = false;
		GameManager.gamemanager.selectPhase = false;
		thrown = false;
		hasLanded = false;
		dicerotate = false;
		diceValue = 0;

	}
}
