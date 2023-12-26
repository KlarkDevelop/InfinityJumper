using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D ArrowR;
    public float Force;
    public bool rotateToPlayer = true;
    public bool shootLeft = true;
    void Awake()
    {
        ArrowR = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector2(-1,1);
        if(rotateToPlayer == true)
        {
            if (PlayerJump.InstancePJ.player.transform.position.x < transform.position.x)
            {
                ArrowR.AddForce(new Vector2(-4, 1) * Force, ForceMode2D.Impulse);
            }
            else
            {
                ArrowR.AddForce(new Vector2(4, 1) * Force, ForceMode2D.Impulse);
            }
        }
        else
        {
            if(shootLeft == true)
            {
                ArrowR.AddForce(new Vector2(-4, 1) * Force, ForceMode2D.Impulse);
            }
            else
            {
                ArrowR.AddForce(new Vector2(4, 1) * Force, ForceMode2D.Impulse);
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Button"))
        {
            Debug.Log(collision.tag);
            if (collision.TryGetComponent(out Button butt))
            {
                butt.DoPress();
            }
            else if (collision.TryGetComponent(out ButtonMulti buttM))
            {
                buttM.DoPress();
            }
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        RotateOnVelocity();
        delayAuroDestroy -= Time.deltaTime;
        if (delayAuroDestroy < 0)
        {
            Destroy(gameObject);
        }
    }
    private float Ragtangle;
    private float delayAuroDestroy = 30;
    private void RotateOnVelocity()
    {
        Ragtangle = Mathf.Atan2(ArrowR.velocity.y, ArrowR.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, Ragtangle);
    }
}
