using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyPlayer : MonoBehaviour
{
    Animator animator;
    Rigidbody2D Flappy_rigidbody;

    public float flapForce = 6.0f;
    public float forWardSpeed = 3.0f;
    public bool isDead = false;
    float deathCooldown = 0f;
    private Vector2 initialPosition;
    bool isFlap = false;

    public bool godMod = false;

    FlappyGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;  // 초기 위치 저장
        gameManager = FlappyGameManager.Instance;

        animator = GetComponentInChildren<Animator>();
        Flappy_rigidbody = GetComponent<Rigidbody2D>();

        if (animator == null) Debug.Log("Not founded Animator");

        if (Flappy_rigidbody == null) Debug.Log("Not founded Rigidbody");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            deathCooldown -= Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = Flappy_rigidbody.velocity;
        velocity.x = forWardSpeed;

        if (isFlap)
        {
            velocity.y = flapForce;
            isFlap = false;
        }

        Flappy_rigidbody.velocity = velocity;

        float angle = Mathf.Clamp((Flappy_rigidbody.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMod) return;

        if (isDead) return;

        isDead = true;
        deathCooldown = 1f;

        //animator.SetInteger("IsDie", 1); // 애니메이션 상태 변경
        //animator.SetTrigger("Die");
        animator.Play("die", 0, 0f); // 강제로 애니메이션 첫 프레임부터 재생

        gameManager.GameOver();
    }

    public void ResetPlayer()
    {
        isDead = false;
        transform.position = initialPosition;  // 초기 위치로 돌아가기
        Flappy_rigidbody.velocity = Vector2.zero;  // 속도 초기화
        animator.Play("flap", 0, 0f); // 강제로 애니메이션 첫 프레임부터 재생
    }

}
