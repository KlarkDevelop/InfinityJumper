using UnityEngine;

public class SwordMan : Enemy
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
        timerAtk = ResetTimerAtc;
    }
    void Update()
    {
        UnderPlayerCheker();
        if(PlAtack == false && FollDeathCheker == false)
        {
            EnemyLogic();
            SwordAtack();
        }
    }

    private float timerAtk = 1;
    public float ResetTimerAtc = 1;
    private float timerIdl = 8;
    public float ResetTimerIdl = 8;
    public GameObject AtackZone;
    private bool onePlay = true;
    private void SwordAtack()
    {
        if (timerAtk < 0)
        {
            DoAtack = false;
            ColisionPL.Instance.OneKick = true;
            ChangeAnim("IdleEnemy");
            timerIdl = ResetTimerIdl;
            timerAtk = ResetTimerAtc;
        }
        timerIdl -= Time.deltaTime;
        if (timerIdl < 1f && DoAtack == false)
        {
            ChangeAnim("ZamahSword");
            onePlay = true;
        }
        if (timerIdl < 0)
        {
            DoAtack = true;
            ChangeAnim("SwordAtack");
            if (onePlay == true)
            {
                enemySource.Play();
                onePlay = false;
            }
            timerAtk -= Time.deltaTime;
        }
    }
}
