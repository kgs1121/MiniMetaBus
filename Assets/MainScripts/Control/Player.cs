using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    protected Vector2 movementDirection = Vector2.zero;
    protected Rigidbody2D _rigidbody;
    private BoxCollider2D playerCollider;
    public BoxCollider2D mapBoundary; // �� ��� �ݶ��̴� ī�޶�
    public BoxCollider2D mapBoundary2; // �� ��� �ݶ��̴� �÷��̾�
    public BoxCollider2D flappyCollider; // �̴� ����
    private Vector2 lastPosition;


    public SpriteRenderer playerRenderer;

    protected Animation animationHandler;
    protected Stat statHandler;

    private GameManager gameManager;
    public int canEnter;

    public Vector3 savedPosition;

    private float playerWidth;
    private float playerHeight;

    private bool isChangingScene = false;  // �� ��ȯ Ȯ��

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

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        canEnter = PlayerPrefs.GetInt("CanEnter", 1);
        if (savedPosition != null) ReturnToSavedPosition();
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
    }



    private void SaveCurrentPosition()
    {
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
        canEnter = 0;
        PlayerPrefs.SetInt("CanEnter", canEnter);
    }

    // ����� ��ġ�� ���ư���
    private void ReturnToSavedPosition()
    {
        float x = PlayerPrefs.GetFloat("PlayerPosX", 0f);  // �⺻���� 0
        float y = PlayerPrefs.GetFloat("PlayerPosY", 0f);

        transform.position = new Vector2(x, y);  // ����� ��ġ�� �̵�
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
        CantOverMap();
    }


    private void Movement(Vector2 direction)
    {
        direction = direction * statHandler.Speed;

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Flappy")) // Ư�� ������Ʈ�� ��Ҵ��� Ȯ��
        {
            if (canEnter == 1)
            {
                SaveCurrentPosition();  // ���� ��ġ ����

                isChangingScene = true; // �� ��ȯ

                SceneManager.LoadScene("FlappyDescription");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isChangingScene) return; // ���� �ٲ�� ���̸� �������� ����.

        if (collision.gameObject.CompareTag("Flappy"))
        {
            canEnter = 1;
            PlayerPrefs.DeleteKey("CanEnter");
        }
    }

    
    public void CantOverMap()
    {
        // ���� ��� ũ�⸦ ������
        Vector2 mapMin = mapBoundary2.bounds.min;
        Vector2 mapMax = mapBoundary2.bounds.max;

        // �÷��̾� ��ġ
        Vector2 playerPosition = transform.position;

        // �� ��踦 �Ѿ�� �ʵ��� �̵� ����
        float clampedX = Mathf.Clamp(playerPosition.x, mapMin.x + playerWidth / 2, mapMax.x - playerWidth / 2);
        float clampedY = Mathf.Clamp(playerPosition.y, mapMin.y + playerHeight / 2, mapMax.y - playerHeight / 2);

        // ���ѵ� ��ġ�� �÷��̾� ��ġ�� ����
        _rigidbody.position = new Vector2(clampedX, clampedY);
    }



}
