using UnityEngine;

public class Archer : Enemy
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionStayCheker(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEntrCheker(collision);
    }
    private AudioSource enemySource;
    private void Awake()
    {
        enemySource = GetComponent<AudioSource>();
        AwakeSettings();
        GetNeededComponents();
        timerIdl = ResetTimerIdl;
    }
    void Update()
    {
        UnderPlayerCheker();
        if (PlAtack == false && FollDeathCheker == false)
        {
            EnemyLogic();
            BowFire();
        }
    }

    private float timerIdl;
    public float ResetTimerIdl;
    public GameObject ArrowPoint;

    public GameObject Arrow;

    public float startDelay = 0;
    private void BowFire()
    {
        startDelay -= Time.deltaTime;
        if(startDelay < 0)
        {
            timerIdl -= Time.deltaTime;
            if (timerIdl < 3f && DoAtack == false)
            {
                ChangeAnim("AimingBow");
            }
            if (timerIdl < 0)
            {
                ColisionPL.Instance.OneKick = true;
                ChangeAnim("IdleEnemy");
                timerIdl = ResetTimerIdl;
                Instantiate(Arrow, ArrowPoint.transform.position, Quaternion.identity);
                enemySource.Play();
            }
        }
    }
}
