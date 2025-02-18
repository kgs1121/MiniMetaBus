using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // 플레이어
    public BoxCollider2D mapBoundary; // 맵 경계 콜라이더
    private Camera cameraComponent;

    private void Awake()
    {
        cameraComponent = GetComponent<Camera>(); // 카메라 컴포넌트 가져오기
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        // 카메라의 orthographic size를 기반으로 화면 높이 계산
        float cameraHeight = cameraComponent.orthographicSize * 2;
        float cameraWidth = cameraHeight * cameraComponent.aspect;

        // 맵 경계(BoxCollider2D)의 한계를 구하기
        float minX = mapBoundary.bounds.min.x + cameraWidth / 2; // 왼쪽 경계 (카메라 크기 고려)
        float maxX = mapBoundary.bounds.max.x - cameraWidth / 2; // 오른쪽 경계 (카메라 크기 고려)
        float minY = mapBoundary.bounds.min.y + cameraHeight / 2; // 아래 경계 (카메라 크기 고려)
        float maxY = mapBoundary.bounds.max.y - cameraHeight / 2; // 위 경계 (카메라 크기 고려)

        // 카메라의 목표 위치
        Vector3 targetPosition = target.position;
        Vector3 desiredPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);

        // 카메라의 위치를 제한 (맵 경계를 벗어나지 않도록)
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // 카메라의 새로운 위치를 설정
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

}
