using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
	public static Dice dice;
	private Rigidbody rb; // �ֻ����� ������ٵ�
	static bool hasLanded; // �ֻ����� ���� �����ߴ��� Ȯ���ϴ� ����
	public static bool thrown; // �ֻ����� ���������� Ȯ���ϴ� ����
	Vector3 initPosition; // �ֻ����� �ʱ� ��ġ
	Vector3 rememeberDice; // 

	public GameObject SelectDice; // ���� ���õ� ���̽�
	public bool isSelected;
	public bool[] inSlot;

	public static int diceValue; // �ֻ����� ���� ���� ����
	
	Vector3 MouseDownPos;  // ���콺 Ŭ�� ��ġ
	int slotValue;

	public static bool resetPosition;  // �����ǿ� ������ �־����� �ֻ��� ��ġ ��ü �ʱ�ȭ
	public bool SetDice = false;
	int a = 0;
	public float timer;

	// Start is called before the first frame update
	void Start()
	{
		inSlot = new bool[] { false, false, false, false, false };
		dice = this;
		// ���� ���� �ʱ�ȭ
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
								Debug.Log("�ߵ��� �ǳ�");
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
				// �ٸ� �۾� ����...
			}



			if (Input.GetKeyDown(KeyCode.R)) // R�� �������� ���Կ� ���ٸ�? ó����ġ(�ٽñ�����)�� �̵�
			{
				ResetDice();
			}
			// �� ���ǹ��� ������ : �Ʒ��Ŵ� �����ǿ� ���� �־����� ����. ���Ժ��忡 �����ϰ� ��ü �ʱ�ȭ. �����Ŵ� ���Ժ���� ����
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
				// ���Կ��� ������ �ֻ��� ��ġ�� �������ֱ� ���ؼ� ���� �ֻ��� ��ġ�� ����
				//GameManager.gamemanager.conditionDice.Remove(SelectDice.gameObject);
				// �ֻ��� ��ġ�� �������� �̵�

				SelectDice.transform.position = GameManager.Slut[i].transform.position;
				inSlot[i] = true;
				break;
			}
			
		}
	}
	void PopSlot(RaycastHit hit)
	{
		Debug.Log("�̰� �� �ߵ��� �ɱ��");

		GameManager.gamemanager.conditionDice.Add(SelectDice.gameObject);

		SelectDice.GetComponent<Dice>().SetDice = false;
    }
}
