using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    protected Vector2 movementDirection = Vector2.zero;
    protected Rigidbody2D _rigidbody;
    private BoxCollider2D playerCollider;
    public BoxCollider2D mapBoundary; // 맵 경계 콜라이더
    public SpriteRenderer playerRenderer;

    protected Animation animationHandler;
    protected Stat statHandler;

    private GameManager gameManager;

    private float playerWidth;
    private float playerHeight;


    public Vector2 MovementDirection { get { return movementDirection; } }

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<Animation>();
        statHandler = GetComponent<Stat>();
        playerCollider = GetComponent<BoxCollider2D>();

        playerWidth = playerCollider.size.x;
        playerHeight = playerCollider.size.y;
    }


    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }


    public void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
        // 좌우 반전 (왼쪽이면 flipX = true, 오른쪽이면 flipX = false)
        if (movementDirection.x > 0)
        {
            playerRenderer.flipX = false;  // 오른쪽
        }
        else if (movementDirection.x < 0)
        {
            playerRenderer.flipX = true;   // 왼쪽
        }
    }


    private void FixedUpdate()
    {
        Movement(movementDirection);

        // 맵의 경계 크기를 가져옴
        Vector2 mapMin = mapBoundary.bounds.min;
        Vector2 mapMax = mapBoundary.bounds.max;


        // 플레이어 위치
        Vector2 playerPosition = transform.position;

        // 맵 경계를 넘어가지 않도록 이동 제한
        float clampedX = Mathf.Clamp(playerPosition.x, mapMin.x + playerWidth / 2, mapMax.x - playerWidth / 2);
        float clampedY = Mathf.Clamp(playerPosition.y, mapMin.y + playerHeight / 2, mapMax.y - playerHeight / 2);

        // 제한된 위치로 플레이어 위치를 설정
        _rigidbody.position = new Vector2(clampedX, clampedY);
    }


    private void Movement(Vector2 direction)
    {
        direction = direction * statHandler.Speed;

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }
}
