using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera mainCamera;
    private float playerHalfWidth;
    private float playerHalfHeight;
    public Transform target; // 캐릭터의 Transform 컴포넌트

    public float smoothSpeed = 0.125f; // 카메라 이동의 부드러움 정도
    public Vector3 offset; // 카메라와 캐릭터 간의 거리 조절

    public Transform cameraTransform; // 흔들림 효과를 줄 카메라의 Transform
    public float shakeDuration = 0.2f; // 흔들림 지속 시간
    public float shakeMagnitude = 0.5f; // 흔들림 세기

    Vector3 originalPosition;

    void Start()
    {
        mainCamera = Camera.main; // 메인 카메라에 대한 참조 가져오기

        // 캐릭터가 SpriteRenderer 컴포넌트를 가지고 있다고 가정합니다.
        playerHalfHeight = target.GetComponent<SpriteRenderer>().bounds.extents.y;
        playerHalfWidth = target.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    private void Update()
    {
        ShakeCamera();
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
    }
}
