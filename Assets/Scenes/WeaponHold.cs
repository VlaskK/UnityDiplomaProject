using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHold : MonoBehaviour
{
    public bool hold;
    public GameObject gun;
    public Transform holdPoint;
    public Transform linePoint;
    public Text nBull;

    private Animator anime;

    private readonly float dist = 3;

    private RaycastHit2D hit;
    private Vector2 direction2d;
    private Vector3 direction3d;


    // Start is called before the first frame update
    private void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        direction2d.Set(linePoint.position.x, linePoint.position.y);
        direction3d.Set(linePoint.position.x, linePoint.position.y, 0);

        anime.SetBool("Hold", hold);


        if (Input.GetKeyUp(KeyCode.F))
        {
            if (!hold)
            {
                pickGun();
            }
            else
            {
                dropGun();
            }
        }

        if (hold)
        {
            hit.collider.gameObject.transform.position = new Vector3(holdPoint.position.x, holdPoint.position.y, 1);
            hit.collider.gameObject.transform.rotation = transform.rotation;
        }
    }


    private void OnEnable()
    {
        LevelGenerator.OnNewLevel += dropGun;
    }

    private void OnDisable()
    {
        LevelGenerator.OnNewLevel -= dropGun;
    }

    private void pickGun()
    {
        Physics2D.queriesStartInColliders = false;
        hit = Physics2D.Raycast(transform.position, direction2d, dist);

        if (hit.collider != null && hit.collider.CompareTag("Gun"))
        {
            hold = true;
            Gun_Shooting GunShoot = hit.collider.gameObject.GetComponent<Gun_Shooting>();
            GunShoot.Active = true;
            hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 7;
            gameObject.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0)
                .gameObject.SetActive(false);
            nBull.text = $"{GunShoot.numBullet}";
        }
    }

    private void dropGun()
    {
        if (hold)
        {
            hold = false;
            Gun_Shooting GunShoot = hit.collider.gameObject.GetComponent<Gun_Shooting>();
            GunShoot.Active = false;
            hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            gameObject.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0)
                .gameObject.SetActive(true);
            nBull.text = "Нож";
            if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                hit.collider.gameObject.transform.position = new Vector3(transform.localScale.x,
                    transform.localScale.y, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, direction3d);
    }
}