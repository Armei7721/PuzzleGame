using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject SelectPiece; // ���� ���õ� ���� ����
    public static DragAndDrop insatance;
    private bool isDragging = false; // ���� ������ �巡�� ������ ����

    private float zoomSpeed = 0.5f; // Ȯ�� �� ��� �ӵ�
    private float minZoom = 1f; // �ּ� Ȯ�� ����
    private float maxZoom = 10f; // �ִ� Ȯ�� ����

    // ī�޶��� ��踦 ������ ����
    private float minX, minY, maxX, maxY;

    void Start()
    {
        insatance = this;
        // ī�޶��� ����Ʈ ��ǥ�� ���� ��ǥ�� ��ȯ�Ͽ� ��� ����
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minX = min.x;
        minY = min.y;
        maxX = max.x;
        maxY = max.y;
    }

    void Update()
    {
        HandleInput();
        HandleDrag();
       // HandleZoom();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform != null)
            {
                if (hit.transform.CompareTag("Puzzle"))
                {
                    if (!hit.transform.GetComponent<PieceScript>().InRightPosition)
                    {
                        SelectPiece = hit.transform.gameObject;
                        SelectPiece.GetComponent<PieceScript>().selected = true;
                        isDragging = true;
                    }
                }
                else
                {
                    SelectPiece = null;
                    isDragging = false;
                }
            }
            else if (hit.transform == null)
            {
                return; // �ƹ� ������Ʈ�� Ŭ������ �ʾ��� �� �Լ� ����
            }
        }

        if (Input.GetMouseButtonUp(0) && SelectPiece != null)
        {
            SelectPiece.GetComponent<PieceScript>().selected = false;
            SelectPiece = null;
            isDragging = false;
        }
    }

    void HandleDrag()
    {
        if (isDragging && SelectPiece != null)
        {
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ������ ī�޶� ��� ���� ���� ���� �̵�
            float clampedX = Mathf.Clamp(mousePoint.x, minX, maxX);
            float clampedY = Mathf.Clamp(mousePoint.y, minY, maxY);

            SelectPiece.transform.position = new Vector3(clampedX, clampedY, 0);
        }
    }

    void HandleZoom()
    {
        float scroll = Input.mouseScrollDelta.y;
        float zoomAmount = scroll * zoomSpeed;

        // ���콺 ��ġ�� �������� Ȯ�� �� ���
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoomAmount, minZoom, maxZoom);

        // ���콺 ��ġ�� �������� ī�޶� �̵�
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.z);
        if (scroll != 0)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, 0.1f);
        }
    }
}
