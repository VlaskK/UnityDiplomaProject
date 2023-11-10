using System;
using UnityEngine;

public class WeaponHold : MonoBehaviour
{
    public bool hold;
    public Transform holdPoint;
    public Transform linePoint;

    private Animator anime;
    private readonly float dist = 3;
    
    private RaycastHit2D hit;

    private Vector2 rotator2;
    private Vector3 rotator3;


    // Start is called before the first frame update
    private void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        rotator2.Set(linePoint.position.x, linePoint.position.y);
        rotator3.Set(linePoint.position.x, linePoint.position.y, 0);
        
        // Debug.Log(rotator2);
        
        anime.SetBool("Hold", hold);

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!hold)
            {
                Physics2D.queriesStartInColliders = false;
                hit = Physics2D.Raycast(transform.position, rotator2, dist);
                // Debug.Log($"{hit.collider} -- {rotator2} -- {hit.point} -- {hit.distance}");
                
                if (hit.collider != null && hit.collider.tag == "Gun") hold = true;
            }
            else
            {
                hold = false;

                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                    hit.collider.gameObject.transform.position = new Vector3(transform.localScale.x,
                        transform.localScale.y, transform.localScale.z);
            }
        }

        if (hold)
        {
            hit.collider.gameObject.transform.position = holdPoint.position;

            hit.collider.gameObject.transform.rotation = transform.rotation;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, rotator3);
    }
}