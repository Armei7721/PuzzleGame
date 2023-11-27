using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
	public static Dice dice;
	static Rigidbody rb; // �ֻ����� ������ٵ�
	static bool hasLanded; // �ֻ����� ���� �����ߴ��� Ȯ���ϴ� ����
	public static bool thrown; // �ֻ����� ���������� Ȯ���ϴ� ����
	Vector3 initPosition; // �ֻ����� �ʱ� ��ġ
	
	public GameObject SelectDice; // ���� ���õ� ���̽�
	public bool isSelected;

	public static int diceValue; // �ֻ����� ���� ���� ����
	public bool[] inSlot;
	
	Vector3 MouseDownPos;  // ���콺 Ŭ�� ��ġ
	int slotValue;

	public static bool resetPosition;  // �����ǿ� ������ �־����� �ֻ��� ��ġ ��ü �ʱ�ȭ
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
		// ���� ���� �ʱ�ȭ
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
		{	//������ٵ��� ���� �������� �ʾ��� ���
			if (Input.GetMouseButtonDown(0))
			{
				MouseDownPos = Input.mousePosition;// ���콺����Ʈ ��ġ�� ���
				Ray ray = Camera.main.ScreenPointToRay(MouseDownPos);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					GameObject hitObject = hit.transform.gameObject;
					if (hitObject.CompareTag("Dice"))
					{
						Dice diceScript = hitObject.GetComponent<Dice>();//diceScript�� �۸��� ������Ʈ�� ��ũ��Ʈ�� ��´�.
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
				// �ٸ� �۾� ����...
			}
		
		}
	}
	void PutSlot(RaycastHit hit)
	{
		
		for (int i = 0; i < 5; i++)
		{
			if (inSlot[i] == false)
			{
				// ���Կ��� ������ �ֻ��� ��ġ�� �������ֱ� ���ؼ� ���� �ֻ��� ��ġ�� ����
				// �ֻ��� ��ġ�� �������� �̵�
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
		if (Input.GetKeyDown(KeyCode.R)&& SetDice==false) // R�� �������� ���Կ� ���ٸ�? ó����ġ(�ٽñ�����)�� �̵�
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
		// "Dice" �±װ� ������ ��� ���� ������Ʈ�� ã���ϴ�.
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
}
