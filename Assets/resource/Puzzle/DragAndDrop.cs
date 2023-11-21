using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject SelectPiece; // 현재 선택된 퍼즐 조각
    public static DragAndDrop insatance;
    private bool isDragging = false; // 현재 조각을 드래그 중인지 여부

    private float zoomSpeed = 0.5f; // 확대 및 축소 속도
    private float minZoom = 1f; // 최소 확대 정도
    private float maxZoom = 10f; // 최대 확대 정도

    // 카메라의 경계를 저장할 변수
    private float minX, minY, maxX, maxY;

    void Start()
    {
        insatance = this;
        // 카메라의 뷰포트 좌표를 월드 좌표로 변환하여 경계 설정
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
                return; // 아무 오브젝트를 클릭하지 않았을 때 함수 종료
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

            // 퍼즐이 카메라 경계 내에 있을 때만 이동
            float clampedX = Mathf.Clamp(mousePoint.x, minX, maxX);
            float clampedY = Mathf.Clamp(mousePoint.y, minY, maxY);

            SelectPiece.transform.position = new Vector3(clampedX, clampedY, 0);
        }
    }

    void HandleZoom()
    {
        float scroll = Input.mouseScrollDelta.y;
        float zoomAmount = scroll * zoomSpeed;

        // 마우스 위치를 기준으로 확대 및 축소
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoomAmount, minZoom, maxZoom);

        // 마우스 위치를 기준으로 카메라 이동
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.z);
        if (scroll != 0)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, 0.1f);
        }
    }
}
