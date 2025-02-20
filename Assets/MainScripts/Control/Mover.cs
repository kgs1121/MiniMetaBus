using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 5f;  // 탑승물 속도
    private bool isPlayerInVehicle = false;  // 플레이어가 탑승했는지 확인
    private Player player;  // 플레이어 스크립트

    // 탑승물의 이동
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

    // 플레이어가 탑승할 때
    public void OnPlayerEnter(Player player)
    {
        isPlayerInVehicle = true;
        this.player = player;
        player.transform.SetParent(transform);  // 플레이어를 탑승물의 자식으로 설정
        player.GetComponent<Rigidbody2D>().simulated = false;  // 플레이어의 물리 계산을 끄기
    }

    // 플레이어가 탑승을 취소할 때
    public void OnPlayerExit()
    {
        isPlayerInVehicle = false;
        player.transform.SetParent(null);  // 플레이어의 부모를 없애어 탑승물에서 분리
        player.GetComponent<Rigidbody2D>().simulated = true;  // 플레이어의 물리 계산을 다시 켬
    }
}
