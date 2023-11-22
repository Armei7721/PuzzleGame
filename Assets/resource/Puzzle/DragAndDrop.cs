using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject SelectPiece; // 현재 선택된 퍼즐 조각
    public static DragAndDrop instance; // DragAndDrop의 인스턴스

    private bool isDragging = false; // 현재 조각을 드래그 중인지 여부

    private float zoomSpeed = 0.5f; // 확대 및 축소 속도
    private float minZoom = 1f; // 최소 확대 정도
    private float maxZoom = 10f; // 최대 확대 정도

    // 카메라의 경계를 저장할 변수
    private float minX, minY, maxX, maxY;

    void Start()
    {
        instance = this; // 자신의 인스턴스를 설정

        // 카메라의 뷰포트 좌표를 월드 좌표로 변환하여 경계 설정
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minX = min.x; // 최소 X 좌표
        minY = min.y; // 최소 Y 좌표
        maxX = max.x; // 최대 X 좌표
        maxY = max.y; // 최대 Y 좌표
    }

    void Update()
    {
        HandleInput(); // 입력 처리 함수 호출
        HandleDrag(); // 드래그 처리 함수 호출
        // HandleZoom(); // 확대 및 축소 처리 함수 (주석 처리됨)
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
                    // 퍼즐이 맞는 위치에 있지 않으면 선택 가능
                    if (!hit.transform.GetComponent<PieceScript>().InRightPosition)
                    {
                        SelectPiece = hit.transform.gameObject;
                        SelectPiece.GetComponent<PieceScript>().selected = true; // 선택된 상태로 설정
                        isDragging = true; // 드래그 중으로 설정
                    }
                }
                else
                {
                    // 퍼즐이 아닌 다른 오브젝트를 클릭한 경우 선택 해제
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
            // 마우스 버튼을 놓았을 때 선택 해제
            SelectPiece.GetComponent<PieceScript>().selected = false; // 선택 해제
            SelectPiece = null;
            isDragging = false; // 드래그 상태 해제
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

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoomAmount, minZoom, maxZoom);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.z);

        if (scroll != 0)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, 0.1f);
        }
    }
}
