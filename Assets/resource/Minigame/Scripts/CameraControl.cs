using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl cc;
    private Camera mainCamera;
    private float playerHalfWidth;
    private float playerHalfHeight;
    public Transform target; // 캐릭터의 Transform 컴포넌트

    public float smoothSpeed = 0.125f; // 카메라 이동의 부드러움 정도
    public Vector3 offset; // 카메라와 캐릭터 간의 거리 조절

    public Transform cameraTransform; // 흔들림 효과를 줄 카메라의 Transform
    public float shakeDuration = 0.5f; // 흔들림 지속 시간
    public float shakeMagnitude = 1f; // 흔들림 세기

    Vector3 originalPosition;

    public Transform playerTransform;
    public Transform bossTransform;
    public float cameraSpeed = 5.0f;
    public float maxDistancex = 10.0f;
    public float maxDistancey = 4.0f;

    public bool bossFight = false;
    public bool shake = false;
    void Start()
    {
        cc = this;
        mainCamera = Camera.main; // 메인 카메라에 대한 참조 가져오기

        // 캐릭터가 SpriteRenderer 컴포넌트를 가지고 있다고 가정합니다.
        playerHalfHeight = target.GetComponent<SpriteRenderer>().bounds.extents.y;
        playerHalfWidth = target.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    private void Update()
    {
      
        CameraTest();
        if (shake == true)
        {
            ShakeCamera();
        }

    }
    void LateUpdate()
    {
        FollowTarget();
    }
    void FollowTarget()
    {
        offset = new Vector3(0f, 3f, -10f);
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        ClampCamera();
    }

    void ClampCamera()
    {

        Vector3 playerPosition = transform.position;
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(playerPosition);

        viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.0f + playerHalfWidth / mainCamera.pixelWidth, 1.0f - playerHalfWidth / mainCamera.pixelWidth);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0.0f + playerHalfHeight / mainCamera.pixelHeight, 1.0f - playerHalfHeight / mainCamera.pixelHeight);

        playerPosition = mainCamera.ViewportToWorldPoint(viewportPosition);
        transform.position = playerPosition;


    }
    void CameraTest()
    {
        if (bossFight)
        {
            float distanceX = Mathf.Abs(playerTransform.position.x - bossTransform.position.x);
            float distanceY = Mathf.Abs(playerTransform.position.y - bossTransform.position.y);

            if (distanceX > maxDistancex || distanceY > maxDistancey)
            {
                // 플레이어와 보스 간의 중간 지점 계산
                Vector3 middlePoint = new Vector3((playerTransform.position.x + bossTransform.position.x) / 2.0f, (playerTransform.position.y + bossTransform.position.y) / 4f, -10f);
                // 카메라를 중간 지점으로 부드럽게 이동
                transform.position = Vector3.Lerp(transform.position, new Vector3(middlePoint.x, middlePoint.y, transform.position.z), Time.deltaTime * cameraSpeed);
            }
            else
            {
                // 플레이어를 따라가도록 설정
                transform.position = Vector3.Lerp(transform.position, new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z), Time.deltaTime);
            }
        }
    }
    public void ShakeCamera()
    {
        StartCoroutine(Shake());
    }
    IEnumerator Shake()
    {
        float elapsedTime = 0.0f;
        Vector3 randomPos;
        originalPosition = cameraTransform.transform.position;
        while (elapsedTime < shakeDuration)
        {
            // 무작위한 위치에 흔들림 효과 추가 (Z 축 이동 없이)
            randomPos = originalPosition + new Vector3(Random.Range(-shakeMagnitude, shakeMagnitude), Random.Range(-shakeMagnitude, shakeMagnitude), 0.0f);

            cameraTransform.localPosition = randomPos;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        cameraTransform.transform.position = originalPosition;
        yield return new WaitForSeconds(0.5f);
        shake = false;
    }
}
