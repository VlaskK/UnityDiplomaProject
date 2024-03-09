using System;
using UnityEngine;

public class PlayerMovement : DifficultySettings
{
    private static readonly int SpeedRun = Animator.StringToHash("SpeedRun");
    public float moveSpeed = PlayerMoveSpeed;

    public Rigidbody2D rb;
    public Camera cam;
    private Animator anime;
    private Vector2 mousePos;
    private Vector2 movement;
    private float currentAngle;


    public static event Action<int, int> OnRotationStatistic;
    private bool isEnemyEngaged = false;
    private int activeEnemies = 0;
    private int killedEnemies = 0;

    private float prevRotation;
    private int rotationCount = 0;


    private void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anime.SetBool("Speed", true);
        }
        else
        {
            anime.SetBool("Speed", false);
        }
        
        if (activeEnemies == 0 && isEnemyEngaged)
        {
            Debug.Log("Бой окончен поворотов за бой: " + rotationCount + "Врагов убито(или ушли) за бой" + killedEnemies);
            OnRotationStatistic.Invoke(rotationCount, killedEnemies);
            isEnemyEngaged = false;
            rotationCount = 0;
            killedEnemies = 0;
        }
    }

    private void FixedUpdate()
    {
        prevRotation = rb.rotation;

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        currentAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rb.rotation = currentAngle - 90f;

        if (isEnemyEngaged && Math.Abs(prevRotation - rb.rotation) > 10)
        {
            rotationCount++;
        }
    }

    private void OnEnable()
    {
        EnemyFollowShoot.OnEnemyEngaged += HandleStatisticsCount;
        EnemyHealth.OnEnemyEngaged += HandleStatisticsCount;
    }

    private void OnDisable()
    {
        EnemyFollowShoot.OnEnemyEngaged -= HandleStatisticsCount;
        EnemyHealth.OnEnemyEngaged -= HandleStatisticsCount;
    }

    private void HandleStatisticsCount(bool enemyEngaged)
    {
        if (enemyEngaged)
        {
            isEnemyEngaged = true;
            activeEnemies++;
        }
        else
        {
            activeEnemies--;
            killedEnemies++;
        }
    }
}