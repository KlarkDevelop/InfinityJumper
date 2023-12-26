using UnityEngine;

public class vzlomScript : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public bool Work = false;
    public float speed;
    private void Update()
    {
        if(Work == true)
        {
            rb.transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, 100);
        }
    }
}
