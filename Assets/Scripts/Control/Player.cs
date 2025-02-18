using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    protected Vector2 movementDirection = Vector2.zero;
    protected Rigidbody2D _rigidbody;
    private BoxCollider2D playerCollider;
    public BoxCollider2D mapBoundary; // �� ��� �ݶ��̴�
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
        // �¿� ���� (�����̸� flipX = true, �������̸� flipX = false)
        if (movementDirection.x > 0)
        {
            playerRenderer.flipX = false;  // ������
        }
        else if (movementDirection.x < 0)
        {
            playerRenderer.flipX = true;   // ����
        }
    }


    private void FixedUpdate()
    {
        Movement(movementDirection);

        // ���� ��� ũ�⸦ ������
        Vector2 mapMin = mapBoundary.bounds.min;
        Vector2 mapMax = mapBoundary.bounds.max;


        // �÷��̾� ��ġ
        Vector2 playerPosition = transform.position;

        // �� ��踦 �Ѿ�� �ʵ��� �̵� ����
        float clampedX = Mathf.Clamp(playerPosition.x, mapMin.x + playerWidth / 2, mapMax.x - playerWidth / 2);
        float clampedY = Mathf.Clamp(playerPosition.y, mapMin.y + playerHeight / 2, mapMax.y - playerHeight / 2);

        // ���ѵ� ��ġ�� �÷��̾� ��ġ�� ����
        _rigidbody.position = new Vector2(clampedX, clampedY);
    }


    private void Movement(Vector2 direction)
    {
        direction = direction * statHandler.Speed;

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }
}
