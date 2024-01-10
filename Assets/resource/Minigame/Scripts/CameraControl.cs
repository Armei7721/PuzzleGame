using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera mainCamera;
    private float playerHalfWidth;
    private float playerHalfHeight;
    public Transform target; // ĳ������ Transform ������Ʈ

    public float smoothSpeed = 0.125f; // ī�޶� �̵��� �ε巯�� ����
    public Vector3 offset; // ī�޶�� ĳ���� ���� �Ÿ� ����

    public Transform cameraTransform; // ��鸲 ȿ���� �� ī�޶��� Transform
    public float shakeDuration = 0.2f; // ��鸲 ���� �ð�
    public float shakeMagnitude = 0.5f; // ��鸲 ����

    Vector3 originalPosition;

    void Start()
    {
        mainCamera = Camera.main; // ���� ī�޶� ���� ���� ��������

        // ĳ���Ͱ� SpriteRenderer ������Ʈ�� ������ �ִٰ� �����մϴ�.
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
            // �������� ��ġ�� ��鸲 ȿ�� �߰� (Z �� �̵� ����)
            randomPos = originalPosition + new Vector3(Random.Range(-shakeMagnitude, shakeMagnitude), Random.Range(-shakeMagnitude, shakeMagnitude), 0.0f);

            cameraTransform.localPosition = randomPos;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        cameraTransform.transform.position = originalPosition;
    }
}
