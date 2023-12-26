using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 VecForce;
    public bool PlAtack = false;
    private Animator EnemyA;
    private BoxCollider2D EnemyC;
    private Rigidbody2D EnemyR;
    private SpriteRenderer EmemySpR;
    public GameObject deathEffect;
    public int health = 3;

    public bool DoAtack = false;

    public static Enemy InstanceE { get; set; }
    private void Awake()
    {
        InstanceE = this;
    }
    public void AwakeSettings()
    {
        PlAtack = false;
        Physics2D.IgnoreLayerCollision(7, 8);
    }
    public void GetNeededComponents()
    {
        EnemyA = GetComponent<Animator>();
        EnemyC = GetComponent<BoxCollider2D>();
        EnemyR = GetComponent<Rigidbody2D>();
        EmemySpR = GetComponent<SpriteRenderer>();
    }

    private float grean = 255;
    private float blue = 255;

    public void OnCollisionEntrCheker(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && Dash.InstanceD.DoDash == false)
        {
            PlAtack = true;
        }
        else if (collision.collider.tag == "Player" && Dash.InstanceD.DoDash == true)
        {
            Death();
        }
        if (collision.collider.tag != "Player" && FollDeathCheker == true)
        {
            PlAtack = false;
            ColisionPL.Instance.PlAtack = false;
            ColisionPL.Instance.OnEnemy = false;
            Death();
        }
        if (collision.collider.tag == "Bullet")
        {
            EnemyR.velocity = new Vector2(0, 0);
            
            health -= collision.collider.GetComponent<DestroyBullet>().damage;
            grean = 0;
            blue = 0;
        }
    }
    private float delayWork = 0.5f;
    public void OnCollisionStayCheker(Collision2D collision)
    {
        if (collision.collider.tag != "Player" && FollDeathCheker == true)
        {
            delayWork -= Time.deltaTime;
            if(delayWork < 0)
            {
                PlAtack = false;
                ColisionPL.Instance.PlAtack = false;
                ColisionPL.Instance.OnEnemy = false;
                Death();
            }
        }
    }

    private string CurrentAnim = "IdleEnemy";
    public void ChangeAnim(string animation)
    {
        if (CurrentAnim == animation) return;

        EnemyA.Play(animation);
        CurrentAnim = animation;
    }

    public float Force;
    private bool OneForce = true;
    public bool FollDeathCheker = false;
    public Vector2 offsetOnEnemy = new Vector2(0, -1.11f);

    public void UnderPlayerCheker()
    {
        if (PlAtack == true)
        {
            if (OneForce == true)
            {
                PlayerJump.InstancePJ.player.velocity = new Vector2(0, 0);
                PlayerJump.InstancePJ.DoublJump(0, false);
                if (PlayerJump.InstancePJ.faceLeft == false) PlayerJump.InstancePJ.player.AddForce(VecForce * Force, ForceMode2D.Impulse);
                if (PlayerJump.InstancePJ.faceLeft == true) PlayerJump.InstancePJ.player.AddForce(new Vector2(-VecForce.x, VecForce.y) * Force, ForceMode2D.Impulse);
                OneForce = false;
                EnemyC.offset = new Vector2(0.27f, -0.2f);
                EnemyC.size = new Vector2(1.99f, 1);
            }
            ChangeAnim("UnderPlayer");
            transform.position = (Vector2)PlayerJump.InstancePJ.player.transform.position + offsetOnEnemy;
            FollDeathCheker = true;
        }
        else
        {
            FollAfterAtack(FollDeathCheker);
        }
    }

    private void FollAfterAtack(bool Cheker)
    {
        if (Cheker == true)
        {
            gameObject.layer = 6;
            ChangeAnim("FollDownEnemy");
        }
    }

    public bool rotateToPlayer = true;
    public void EnemyLogic()
    {
        grean += Time.deltaTime; //Возвращение норамльного цвета после урона
        blue += Time.deltaTime;
        EmemySpR.color = new Color(255, grean, blue);

        if(rotateToPlayer == true)
        {
            if (PlayerJump.InstancePJ.player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
        } 

        if (health == 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
        Instantiate(deathEffect, transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
    }

}
