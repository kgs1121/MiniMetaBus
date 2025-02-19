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
    public BoxCollider2D mapBoundary; // 맵 경계 콜라이더 카메라
    public BoxCollider2D mapBoundary2; // 맵 경계 콜라이더 플레이어
    public BoxCollider2D flappyCollider; // 미니 게임
    private Vector2 lastPosition;


    public SpriteRenderer playerRenderer;

    protected Animation animationHandler;
    protected Stat statHandler;

    private GameManager gameManager;
    public int canEnter;

    public Vector3 savedPosition;

    private float playerWidth;
    private float playerHeight;

    private bool isChangingScene = false;  // 씬 전환 확인

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

    // 저장된 위치로 돌아가기
    private void ReturnToSavedPosition()
    {
        float x = PlayerPrefs.GetFloat("PlayerPosX", 0f);  // 기본값은 0
        float y = PlayerPrefs.GetFloat("PlayerPosY", 0f);

        transform.position = new Vector2(x, y);  // 저장된 위치로 이동
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
        if (collision.gameObject.CompareTag("Flappy")) // 특정 오브젝트에 닿았는지 확인
        {
            if (canEnter == 1)
            {
                SaveCurrentPosition();  // 현재 위치 저장

                isChangingScene = true; // 씬 전환

                SceneManager.LoadScene("FlappyDescription");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isChangingScene) return; // 씬이 바뀌는 중이면 실행하지 않음.

        if (collision.gameObject.CompareTag("Flappy"))
        {
            canEnter = 1;
            PlayerPrefs.DeleteKey("CanEnter");
        }
    }

    
    public void CantOverMap()
    {
        // 맵의 경계 크기를 가져옴
        Vector2 mapMin = mapBoundary2.bounds.min;
        Vector2 mapMax = mapBoundary2.bounds.max;

        // 플레이어 위치
        Vector2 playerPosition = transform.position;

        // 맵 경계를 넘어가지 않도록 이동 제한
        float clampedX = Mathf.Clamp(playerPosition.x, mapMin.x + playerWidth / 2, mapMax.x - playerWidth / 2);
        float clampedY = Mathf.Clamp(playerPosition.y, mapMin.y + playerHeight / 2, mapMax.y - playerHeight / 2);

        // 제한된 위치로 플레이어 위치를 설정
        _rigidbody.position = new Vector2(clampedX, clampedY);
    }



}
