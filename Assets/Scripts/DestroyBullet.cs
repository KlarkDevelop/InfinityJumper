using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
         Destroy(gameObject);
    }
    public int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Button"))
        {
            if (collision.TryGetComponent(out Button button))
            {
                button.DoPress();
            }
            if (collision.TryGetComponent(out ButtonMulti buttonMulti))
            {
                buttonMulti.DoPress();
            }
            Destroy(gameObject);
        }
    }

    private Rigidbody2D Bullet;
    private void Awake()
    {
        Bullet = GetComponent<Rigidbody2D>();
        Bullet.AddForce(ScreanThrow.InstanceT.vecT, ForceMode2D.Impulse);
    }
}
