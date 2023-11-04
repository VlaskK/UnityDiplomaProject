using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    private void Start()
    {
        Destroy(gameObject, 4);
    }
}