using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 5f;  // ž�¹� �ӵ�
    private bool isPlayerInVehicle = false;  // �÷��̾ ž���ߴ��� Ȯ��
    private Player player;  // �÷��̾� ��ũ��Ʈ

    // ž�¹��� �̵�
    void Update()
    {
        if (isPlayerInVehicle)
        {
            MoveVehicle();
        }
    }

    private void MoveVehicle()
    {
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(move, 0, 0);
    }

    // �÷��̾ ž���� ��
    public void OnPlayerEnter(Player player)
    {
        isPlayerInVehicle = true;
        this.player = player;
        player.transform.SetParent(transform);  // �÷��̾ ž�¹��� �ڽ����� ����
        player.GetComponent<Rigidbody2D>().simulated = false;  // �÷��̾��� ���� ����� ����
    }

    // �÷��̾ ž���� ����� ��
    public void OnPlayerExit()
    {
        isPlayerInVehicle = false;
        player.transform.SetParent(null);  // �÷��̾��� �θ� ���־� ž�¹����� �и�
        player.GetComponent<Rigidbody2D>().simulated = true;  // �÷��̾��� ���� ����� �ٽ� ��
    }
}
