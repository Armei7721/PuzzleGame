using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
	public static Dice dice;
	static Rigidbody rb; // 주사위의 리지드바디
	static bool hasLanded; // 주사위가 땅에 도달했는지 확인하는 변수
	public static bool thrown; // 주사위가 던져졌는지 확인하는 변수
	Vector3 initPosition; // 주사위의 초기 위치
	
	public GameObject SelectDice; // 현재 선택된 다이스
	public bool isSelected;

	public static int diceValue; // 주사위의 눈을 담을 변수
	public bool[] inSlot;
	
	Vector3 MouseDownPos;  // 마우스 클릭 위치
	int slotValue;

	public static bool resetPosition;  // 점수판에 점수를 넣었을때 주사위 위치 전체 초기화
	Vector3 DicePosition;
	Quaternion DiceRotation;
	Vector3 upDice;
	public bool SetDice = false;

	int a = 0;
	public GameObject[] conditiontransform;
	public List<GameObject> conditionDice = new List<GameObject>();
	public List<GameObject> setDicePlane = new List<GameObject>();
	public float timer;
	// Start is called before the first frame update
	void Start()
	{
		dice = this;
		// 각종 변수 초기화
		rb = GetComponent<Rigidbody>();
		initPosition = transform.position;
		thrown = false;
		inSlot = new bool[] { false, false, false, false, false };
		
		slotValue = 0;
		resetPosition = false;
		ConditionDice();
		InsertDice();
		
	}

	// Update is called once per frame
	void Update()
	{
		Throw();
		ClickDice();
		ResetDice();
	}
    public static void RollDice()
	{
		if (!thrown && !hasLanded)
		{
			thrown = true;
			rb.AddForce(Random.Range(3000, 3500), 0, Random.Range(4000, 6500));
		}
	}
	public void Throw()
    {
		if (rb.IsSleeping() && !hasLanded && thrown)
		{
			hasLanded = true;
		}
		if (rb.IsSleeping() && hasLanded && thrown && !SetDice)
		{
			timer += Time.deltaTime;
			if (timer >= 2.0f)
			{
				transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0f, transform.rotation.eulerAngles.z);

				for (int i = 0; i < conditionDice.Count; i++)
				{
					GameObject diceObject = conditionDice[i];
					Rigidbody rb = diceObject.GetComponent<Rigidbody>();

					if (rb != null)
					{
						rb.isKinematic = true;
					}
				}
				if (timer >= 3.0f)
				{
					if (a == 0)
					{
						for (int i = 0; i < conditiontransform.Length; i++)
						{
							conditionDice[i].transform.position = Vector3.Lerp(conditionDice[i].transform.position, conditiontransform[i].transform.position, 0.1f);
							//conditionDice[i].transform.position = conditiontransform[i].transform.position;
							if(conditionDice[i].transform.position== conditiontransform[i].transform.position)
                            {
								a = 1;
                            }
						}

					}
				}
				
				
			}
		}
	}
	public void ClickDice()
    {
		if (rb.IsSleeping() && hasLanded && thrown)
		{	//리지드바디의 힘이 가해지지 않았을 경우
			if (Input.GetMouseButtonDown(0))
			{
				MouseDownPos = Input.mousePosition;// 마우스포인트 위치를 담고
				Ray ray = Camera.main.ScreenPointToRay(MouseDownPos);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					GameObject hitObject = hit.transform.gameObject;
					if (hitObject.CompareTag("Dice"))
					{
						Dice diceScript = hitObject.GetComponent<Dice>();//diceScript에 글릭한 오브젝트의 스크립트를 담는다.
						if (diceScript != null && !diceScript.isSelected)
						{
							if (slotValue < 5)
							{
								SelectDice = hit.transform.gameObject;
								slotValue++;
								SelectDice.GetComponent<Dice>().SetDice = true;
								PutSlot(hit);
								
							}
						}
						else if(SetDice==true)
						{
							
							PopSlot(hit, slotValue - 1);
						}
					}
					else if (hitObject == null)
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
	}
	void PutSlot(RaycastHit hit)
	{
		
		for (int i = 0; i < 5; i++)
		{
			if (inSlot[i] == false)
			{
				// 슬롯에서 뺐을때 주사위 위치를 지정해주기 위해서 현재 주사위 위치를 저장
				// 주사위 위치를 슬롯으로 이동
				SelectDice.transform.position = GameManager.Slut[i].transform.position;
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
		if (Input.GetKeyDown(KeyCode.R)&& SetDice==false) // R을 눌렀을때 슬롯에 없다면? 처음위치(다시굴리기)로 이동
		{ 
		thrown = false;
		hasLanded = false;
		transform.position = initPosition;
		diceValue = 0;
		a = 0;
		timer = 0;
		CupShaking.cupshaking.Wall.SetActive(true);
			
			for (int i = 0; i < conditionDice.Count; i++)
			{
				GameObject diceObject = conditionDice[i];
				Rigidbody rb = diceObject.GetComponent<Rigidbody>();
				
				if (rb != null &&SetDice==false)
				{
					rb.isKinematic = false;
				}
			}
		}
	}
	void InsertDice()
	{
		// "Dice" 태그가 지정된 모든 게임 오브젝트를 찾습니다.
		GameObject[] diceObjects = GameObject.FindGameObjectsWithTag("Dice");
		for (int i = 0; i < diceObjects.Length; i++)
		{
			GameObject diceObject = diceObjects[i];
			Dice diceComponent = diceObject.GetComponent<Dice>();

			if (diceComponent != null)
			{
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
}
