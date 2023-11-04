using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position =
            new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}