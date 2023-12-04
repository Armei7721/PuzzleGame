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
		Throw();
		ResetDice();
	}
	public void RollDice()
	{
		if (!thrown && !hasLanded)
		{
			thrown = true;
			rb.AddForce(Random.Range(3000, 4000), 0, Random.Range(2500, 4500));
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
				transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0f, transform.rotation.eulerAngles.z);

				for (int i = 0; i < GameManager.gamemanager.conditionDice.Count; i++)
				{
					GameObject diceObject = GameManager.gamemanager.conditionDice[i];
					Rigidbody rb = diceObject.GetComponent<Rigidbody>();

					if (rb != null)
					{
						rb.isKinematic = true;
					}
				}
				
				
				if (timer >= 3.0f && a == 0)
				{
					for (int i = 0; i < GameManager.gamemanager.conditionDice.Count; i++)
					{
						if (GameManager.gamemanager.conditionDice.Contains(gameObject) != false)
						{
							GameManager.gamemanager.conditionDice[i].transform.position = Vector3.Lerp(GameManager.gamemanager.conditionDice[i].transform.position, GameManager.gamemanager.conditiontransform[i].transform.position, 0.6f);

							if (GameManager.gamemanager.conditionDice[i].transform.position == GameManager.gamemanager.conditiontransform[i].transform.position)
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
                    else if (SelectDice.GetComponent<Dice>().SetDice == true )
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
		if (Input.GetKeyDown(KeyCode.R)) // R�� �������� ���Կ� ���ٸ�? ó����ġ(�ٽñ�����)�� �̵�
		{

			if (SetDice == false)
			{
				for (int i = 0; i < GameManager.gamemanager.conditionDice.Count; i++)
				{
					GameObject diceObject = GameManager.gamemanager.conditionDice[i];
					Rigidbody rb = diceObject.GetComponent<Rigidbody>();

					if (rb != null)
					{
						rb.isKinematic = false;
					}
				}
				GameManager.gamemanager.selectdice = false;
				thrown = false;
				hasLanded = false;
				GameManager.gamemanager.Wall.SetActive(true);
				transform.position = initPosition;
				diceValue = 0;
			}
		}
	}


    public void PutSlot(RaycastHit hit)
    {

        for (int i = 0; i < GameManager.Slut.Length; i++)
        {
            if (GameManager.gamemanager.slots==null)
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
			if (GameManager.gamemanager.slots !=null)
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
	
}
