using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
	public static Dice dice;
	static Rigidbody rb; // �ֻ����� ������ٵ�
	static bool hasLanded; // �ֻ����� ���� �����ߴ��� Ȯ���ϴ� ����
	static bool thrown; // �ֻ����� ���������� Ȯ���ϴ� ����
	Vector3 initPosition; // �ֻ����� �ʱ� ��ġ
	public static int diceValue; // �ֻ����� ���� ���� ����
	public static bool[] inSlot;
	Vector3 MouseDownPos;  // ���콺 Ŭ�� ��ġ
	public GameObject[] Slots;
	int slotValue;
	public static bool resetPosition;  // �����ǿ� ������ �־����� �ֻ��� ��ġ ��ü �ʱ�ȭ
	Vector3 DicePosition;
	Vector3 upDice;
	bool SetDice = false;
	// Start is called before the first frame update
	void Start()
	{
		dice = this;
		// ���� ���� �ʱ�ȭ
		rb = GetComponent<Rigidbody>();
		initPosition = transform.position;
		//rb.useGravity = true;
		thrown = false;
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
				Debug.Log("Ŭ�� ��!!");
				MouseDownPos = Input.mousePosition;
				Ray ray = Camera.main.ScreenPointToRay(MouseDownPos);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					GameObject hitObject = hit.collider.gameObject;
					if (hitObject.CompareTag("Dice"))
                    {
						if (slotValue<5)
						{
							slotValue++;
							SetDice = true;
							PutSlot(hit);
						}
						else if(SetDice= true)
                        {
							PopSlot(hit,slotValue-1);
                        }
					}
					else if(hitObject==null)
                    {
						return;
                    }
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.R) && slotValue == 0) // R�� �������� ���Կ� ���ٸ�? ó����ġ(�ٽñ�����)�� �̵�
		{
			ResetDice();
		}
		// �� ���ǹ��� ������ : �Ʒ��Ŵ� �����ǿ� ���� �־����� ����. ���Ժ��忡 �����ϰ� ��ü �ʱ�ȭ. �����Ŵ� ���Ժ���� ����
		if (resetPosition)
		{
			GameManger.gamemanager.selectdice = false;
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
				DicePosition = hit.transform.position;
				// ���Կ��� ������ �ֻ��� ��ġ�� �������ֱ� ���ؼ� ���� �ֻ��� ��ġ�� ����

				// �ֻ��� ��ġ�� �������� �̵�
				transform.position = GameManger.Slut[i].transform.position;
				inSlot[i] = true;
				break;
			}
		}
	}
	void PopSlot(RaycastHit hit, int value)
	{
		hit.transform.position = DicePosition;
		inSlot[value] = false;
	}
	void ResetDice()
	{
		thrown = false;
		hasLanded = false;
		transform.position = initPosition;
		diceValue = 0;
	}
}
