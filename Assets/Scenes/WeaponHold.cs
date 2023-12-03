using System;
using UnityEngine;

public class WeaponHold : MonoBehaviour
{
    public bool hold;
    public Transform holdPoint;
    public Transform linePoint;

    private Animator anime;
    private Gun_Shooting shoot;
    private readonly float dist = 3;
    
    private RaycastHit2D hit;

    private Vector2 direction2d;
    private Vector3 direction3d;


    // Start is called before the first frame update
    private void Start()
    {
        anime = GetComponent<Animator>();
        shoot = GetComponent<Gun_Shooting>();
    }

    // Update is called once per frame
    private void Update()
    {
        direction2d.Set(linePoint.position.x, linePoint.position.y);
        direction3d.Set(linePoint.position.x, linePoint.position.y, 0);
        
        anime.SetBool("Hold", hold);

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!hold)
            {
                Physics2D.queriesStartInColliders = false;
                hit = Physics2D.Raycast(transform.position, direction2d, dist);
                // Debug.Log($"{hit.collider} -- {rotator2} -- {hit.point} -- {hit.distance}");

                if (hit.collider != null && hit.collider.tag == "Gun")
                {
                    hold = true;
                    hit.collider.gameObject.GetComponent<Gun_Shooting>().Active = true;
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 7;
                    // Debug.Log($"Active = {shoot.Active} -- GameObj = {hit.collider.gameObject.name}");
                }
            }
            else
            {
                hold = false;
                hit.collider.gameObject.GetComponent<Gun_Shooting>().Active = false;
                hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                
                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                    hit.collider.gameObject.transform.position = new Vector3(transform.localScale.x,
                        transform.localScale.y, 0);
                    
            }
        }

        if (hold)
        {
            hit.collider.gameObject.transform.position = new Vector3(holdPoint.position.x, holdPoint.position.y, 1);
            hit.collider.gameObject.transform.rotation = transform.rotation;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, direction3d);
    }
}