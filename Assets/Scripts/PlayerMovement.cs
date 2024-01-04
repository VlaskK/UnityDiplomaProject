using UnityEngine;

public class PlayerMovement : DifficultySettings
{
    private static readonly int SpeedRun = Animator.StringToHash("SpeedRun");
    public float moveSpeed = DifficultySettings.PlayerMoveSpeed;

    public Rigidbody2D rb;
    public Camera cam;
    private Animator anime;
    private Vector2 mousePos;
    private Vector2 movement;
    private float currentAngle;
    // private float lastAngle;
    // public int turnCounter = 0;
    // public float turnThreshold = 10f; // Угол, который игрок должен преодолеть для поворота
    //
    // private float turnTimer = 0f;
    // private float turnTimerThreshold = 5f;

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
        
        // Debug.Log("Поворотов сделано: " + turnCounter);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        currentAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rb.rotation = currentAngle - 90f;

    }
}