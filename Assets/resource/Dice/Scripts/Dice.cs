using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
	public static Dice dice;
	public Rigidbody rb; // �ֻ����� ������ٵ�
	public static bool hasLanded; // �ֻ����� ���� �����ߴ��� Ȯ���ϴ� ����
	public static bool thrown; // �ֻ����� ���������� Ȯ���ϴ� ����
	public Vector3 initPosition; // �ֻ����� �ʱ� ��ġ

	public GameObject SelectDice; // ���� ���õ� ���̽�
	public bool isSelected;

	public int diceValue; // �ֻ����� ���� ���� ����
	public Vector3 MouseDownPos;  // ���콺 Ŭ�� ��ġ


	public static bool resetPosition;  // �����ǿ� ������ �־����� �ֻ��� ��ġ ��ü �ʱ�ȭ
	public bool SetDice = false;
	int a = 0;
	public float timer;
	public bool dicerotate = false;
	// Start is called before the first frame update
	void Start()
	{

		dice = this;
		// ���� ���� �ʱ�ȭ
		rb = GetComponent<Rigidbody>();
		initPosition = transform.position;
		thrown = false;
		resetPosition = false;

	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log("dicerotate ���� " + dicerotate);
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
		// ��� ���̽��� ���� ��츦 üũ
		
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

		// ��� ���̽��� ���߸鼭 ���� ���� �����̰�, ���� ���� �������� ���� ���
		if (allDiceStopped && !hasLanded && thrown)
		{
			hasLanded = true;
		}

		// ��� ���̽��� ���߸鼭 ���� ������ ����̰�, ���� ó������ ���� ���
		if (allDiceStopped && hasLanded && thrown && !SetDice)
		{
			timer += Time.deltaTime;

			// 2�ʰ� ������� ��
			if (timer >= 2.0f)
			{

				StartCoroutine(rotation());
				DiceKinmatic();
				// 3�ʰ� ������� ��
				if (timer >= 3.0f)
				{
					
					// �� �ֻ����� �������� ���������� �̵���Ŵ
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
		if (GameManager.gamemanager.selectPhase && Input.GetKeyDown(KeyCode.RightArrow)) // R�� �������� ���Կ� ���ٸ�? ó����ġ(�ٽñ�����)�� �̵�
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

	// �� �� ��ȯ �Լ�
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
