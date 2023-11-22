using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
	static Rigidbody rb; // �ֻ����� ������ٵ�
	static bool hasLanded; // �ֻ����� ���� �����ߴ��� Ȯ���ϴ� ����
	static bool thrown; // �ֻ����� ���������� Ȯ���ϴ� ����
	Vector3 initPosition; // �ֻ����� �ʱ� ��ġ
	public GameObject SelectDice; // ���� ���õ� ���̽�
	public bool isSelected;

	public static int diceValue; // �ֻ����� ���� ���� ����
	public bool[] inSlot;
	
	
	Vector3 MouseDownPos;  // ���콺 Ŭ�� ��ġ
	public GameObject[] Slots;
	int slotValue;


	public static bool resetPosition;  // �����ǿ� ������ �־����� �ֻ��� ��ġ ��ü �ʱ�ȭ
	Vector3 DicePosition;
	Quaternion DiceRotation;
	Vector3 upDice;
	public bool SetDice = false;

	// Start is called before the first frame update
	void Start()
	{
		// ���� ���� �ʱ�ȭ
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
				// �ٸ� �۾� ����...
			}

		}

		if (Input.GetKeyDown(KeyCode.R) && slotValue == 0) // R�� �������� ���Կ� ���ٸ�? ó����ġ(�ٽñ�����)�� �̵�
		{
			ResetDice();
		}
		// �� ���ǹ��� ������ : �Ʒ��Ŵ� �����ǿ� ���� �־����� ����. ���Ժ��忡 �����ϰ� ��ü �ʱ�ȭ. �����Ŵ� ���Ժ���� ����
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
				// ���Կ��� ������ �ֻ��� ��ġ�� �������ֱ� ���ؼ� ���� �ֻ��� ��ġ�� ����
				DicePosition = hit.transform.position;
				DiceRotation = hit.transform.rotation;
				// �ֻ��� ��ġ�� �������� �̵�
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
