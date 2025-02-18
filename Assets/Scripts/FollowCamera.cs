using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // �÷��̾�
    public BoxCollider2D mapBoundary; // �� ��� �ݶ��̴�
    private Camera cameraComponent;

    private void Awake()
    {
        cameraComponent = GetComponent<Camera>(); // ī�޶� ������Ʈ ��������
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        // ī�޶��� orthographic size�� ������� ȭ�� ���� ���
        float cameraHeight = cameraComponent.orthographicSize * 2;
        float cameraWidth = cameraHeight * cameraComponent.aspect;

        // �� ���(BoxCollider2D)�� �Ѱ踦 ���ϱ�
        float minX = mapBoundary.bounds.min.x + cameraWidth / 2; // ���� ��� (ī�޶� ũ�� ���)
        float maxX = mapBoundary.bounds.max.x - cameraWidth / 2; // ������ ��� (ī�޶� ũ�� ���)
        float minY = mapBoundary.bounds.min.y + cameraHeight / 2; // �Ʒ� ��� (ī�޶� ũ�� ���)
        float maxY = mapBoundary.bounds.max.y - cameraHeight / 2; // �� ��� (ī�޶� ũ�� ���)

        // ī�޶��� ��ǥ ��ġ
        Vector3 targetPosition = target.position;
        Vector3 desiredPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);

        // ī�޶��� ��ġ�� ���� (�� ��踦 ����� �ʵ���)
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // ī�޶��� ���ο� ��ġ�� ����
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

}
