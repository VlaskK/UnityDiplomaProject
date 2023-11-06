using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int SpeedRun = Animator.StringToHash("SpeedRun");
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;
    private Animator anime;
    private Vector2 mousePos;
    private Vector2 movement;

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
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        var lookDir = mousePos - rb.position;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        
    }
}