using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    protected Vector2 movementDirection = Vector2.zero;
    protected Rigidbody2D _rigidbody;

    public Transform mover;  // 탑승물을 움직이는 트랜스폼
    protected Animation animationHandler;
    protected Stat statHandler;

    public SpriteRenderer moverSprite;

    private Player player;  // 플레이어 스크립트

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        statHandler = GetComponent<Stat>();
        animationHandler = GetComponent<Animation>();
    }

    private void FixedUpdate()
    {
        Movement(movementDirection);
    }


    // 플레이어가 탑승할 때 호출
    public void OnPlayerEnter(Player player)
    {
        this.player = player;
        player.transform.SetParent(this.transform);  // 플레이어를 탑승물의 자식으로 설정
        player.GetComponent<Rigidbody2D>().simulated = false;  // 탑승물에 타면 플레이어의 물리 계산을 끄기
    }

    // 플레이어가 탑승을 취소할 때 호출
    public void OnPlayerExit()
    {
        player.transform.SetParent(null);  // 플레이어의 부모를 없애어 탑승물에서 분리
        player.GetComponent<Rigidbody2D>().simulated = true;  // 플레이어의 물리 계산을 다시 켬
        player = null;  // 플레이어 참조 해제
    }


    public void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
        // 좌우 반전 (왼쪽이면 flipX = true, 오른쪽이면 flipX = false)
        if (movementDirection.x > 0)
        {
            moverSprite.flipX = false;  // 오른쪽
        }
        else if (movementDirection.x < 0)
        {
            moverSprite.flipX = true;   // 왼쪽
        }
    }


    private void Movement(Vector2 direction)
    {
        direction = direction * statHandler.Speed;

        _rigidbody.velocity = direction;
        animationHandler.MoverMove(movementDirection);
    }
}
