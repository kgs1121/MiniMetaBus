using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    protected Vector2 movementDirection = Vector2.zero;
    protected Rigidbody2D _rigidbody;

    public Transform mover;  // ž�¹��� �����̴� Ʈ������
    protected Animation animationHandler;
    protected Stat statHandler;

    public SpriteRenderer moverSprite;

    private Player player;  // �÷��̾� ��ũ��Ʈ

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


    // �÷��̾ ž���� �� ȣ��
    public void OnPlayerEnter(Player player)
    {
        this.player = player;
        player.transform.SetParent(this.transform);  // �÷��̾ ž�¹��� �ڽ����� ����
        player.GetComponent<Rigidbody2D>().simulated = false;  // ž�¹��� Ÿ�� �÷��̾��� ���� ����� ����
    }

    // �÷��̾ ž���� ����� �� ȣ��
    public void OnPlayerExit()
    {
        player.transform.SetParent(null);  // �÷��̾��� �θ� ���־� ž�¹����� �и�
        player.GetComponent<Rigidbody2D>().simulated = true;  // �÷��̾��� ���� ����� �ٽ� ��
        player = null;  // �÷��̾� ���� ����
    }


    public void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
        // �¿� ���� (�����̸� flipX = true, �������̸� flipX = false)
        if (movementDirection.x > 0)
        {
            moverSprite.flipX = false;  // ������
        }
        else if (movementDirection.x < 0)
        {
            moverSprite.flipX = true;   // ����
        }
    }


    private void Movement(Vector2 direction)
    {
        direction = direction * statHandler.Speed;

        _rigidbody.velocity = direction;
        animationHandler.MoverMove(movementDirection);
    }
}
