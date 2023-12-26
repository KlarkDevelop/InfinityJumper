using UnityEngine;

public class Dash : MonoBehaviour
{
    public Rigidbody2D playerD;
    public Animator PLayerAnimD;
    private Vector2 vecD;
    public float DashForce;
    public bool DoDash = false;
    public float DashDist;
    private Vector2 VectDist;
    private Vector2 OldPlPos;
    public float time;
    public float ResetTime;
    public Joystick joystickD;
    public AudioSource playerSource;
    public AudioClip sound;

    public static Dash InstanceD { get; set; }
    public void Awake()
    {
        InstanceD = this;
    }
    public TrajectoryRenderer Trajectory;
    public int TrajectoryPoints;
    public float TrajectoryDistance;
    public float DeathZone = 0.45f;

    public Color TrajectoryColor;
    public float TrajectoryStartAlpha;
    public float TrajectoryEndAlpha;

    void Update()
    {
        if (ColisionPL.Instance.GunCount > 0)//Проверка на наличие бонуса
        {
            if (clickD == true)
            {
                vecD = MousePosD - new Vector2(joystickD.Horizontal, joystickD.Vertical); //Вектор для толчка
                Trajectory.ShowDashTrajectory(playerD.transform.position, vecD, DashForce,
                                          TrajectoryPoints, TrajectoryDistance, DeathZone);
                Trajectory.ChangeLineColor(TrajectoryColor, TrajectoryStartAlpha, TrajectoryEndAlpha);
            }
        }

        
        PLayerAnimD.SetBool("Dash", DoDash);

        if (DoDash == true) // Зависание в воздухе
        {
            TimerMin -= Time.deltaTime;
            VectDist = OldPlPos - (Vector2)playerD.transform.position;

            if (ColisionPL.Instance.DoJ == true && TimerMin < 0)
            {
                OffFreez();
                workFAD = true;
            }

            if (VectDist.magnitude >= DashDist)
            {
                PlayerJump.InstancePJ.ChangeColider(3);
                playerD.velocity = new Vector2(0, 0);
                playerD.bodyType = RigidbodyType2D.Kinematic;
                time -= Time.deltaTime;
            }
            if (time < 0)
            {
                OffFreez();
            }
        }
        else
        {
            playerD.bodyType = RigidbodyType2D.Dynamic;
        }
        FixAfterDash();
    }

    private float timerFAD;
    private float ResetTimerFAD = 0.2f;
    public bool workFAD = false;
    private void FixAfterDash()
    {
        if(DoDash == false && workFAD == true)
        {
            playerD.velocity = Vector3.zero;
            playerD.angularVelocity = 0;
            playerD.Sleep();
            timerFAD -= Time.deltaTime;
            if(timerFAD < 0)
            {
                workFAD = false;
                timerFAD = ResetTimerFAD;
                playerD.WakeUp();
            }
        }
        
    }

    public void OffFreez()
    {
        DoDash = false;
        DoRotate = false;
        playerD.transform.localScale = new Vector2(playerD.transform.localScale.x, 1);
        TimerMin = ResetTimerMin;
        time = ResetTime;
        playerD.bodyType = RigidbodyType2D.Dynamic;
        PlayerJump.InstancePJ.ChangeColider(0);
    }

    public float TimerMin;
    public float ResetTimerMin;

    public bool clickD;
    private Vector2 MousePosD = new Vector2(0, 0);

    public void RightScreanDown()
    {
        clickD = true;
    }

    public bool DoRotate = false;
    public float Ragtangl;
    private bool AfterWoll = false;

    public void RightScreanUp()
    {
        clickD = false;

        if (ColisionPL.Instance.GunCount > 0 && vecD.magnitude > DeathZone && PlayerJump.InstancePJ.click == false)//рывок при наличий бонуса
        {
            if (ColisionPL.Instance.animOnW == true) //Фикс прыжков от стены
            {
                ColisionPL.Instance.animOnW = false;
                if (PlayerJump.InstancePJ.faceLeft == false)
                {
                    playerD.transform.position = new Vector2(playerD.transform.position.x + 0.5f, playerD.transform.position.y);
                }
                else
                {
                    playerD.transform.position = new Vector2(playerD.transform.position.x - 0.5f, playerD.transform.position.y);
                }
                AfterWoll = true;
            }

            time = ResetTime;
            playerD.bodyType = RigidbodyType2D.Dynamic;
            DoDash = false;

            if (ColisionPL.Instance.PlAtack == true)
            {
                playerD.velocity = new Vector2(0, 0);
                ColisionPL.Instance.EnemyClass.PlAtack = false;
                ColisionPL.Instance.DoJ = false;
                ColisionPL.Instance.DoDoublleJ = true;
                ColisionPL.Instance.OnEnemy = false;
            }
            PlayerJump.InstancePJ.DoublJump(0, false);
            PlayerJump.InstancePJ.ChangeColider(3);
            playerD.velocity = Vector2.zero;
            playerD.AddForce(vecD * DashForce, ForceMode2D.Impulse);
            playerSource.PlayOneShot(sound);

            if(AfterWoll == false) RotationDash();

            if (ColisionPL.Instance.PlAtack == true)
            {
                ColisionPL.Instance.PlAtack = false;
                ColisionPL.Instance.DoJ = false;
                ColisionPL.Instance.DoDoublleJ = true;
                ColisionPL.Instance.OnEnemy = false;
            }
            DoDash = true;

            ColisionPL.Instance.GunCount -= 1;
            OldPlPos = playerD.transform.position;
            AfterWoll = false;
            Trajectory.ResetTrajectory();
        }
    }

    private void RotationDash()
    {
        //Поворот персонажа в сторону рывка
        Ragtangl = Mathf.Atan2(vecD.y, vecD.x) * Mathf.Rad2Deg; // Угол поворота
        playerD.transform.rotation = Quaternion.Euler(0, 0, Ragtangl);
        if (Ragtangl > 90 || Ragtangl < -90)
        {
            playerD.transform.localScale = new Vector2(1, -1);
            DoRotate = true;
        }
        else if (Ragtangl < 90 || Ragtangl > -90 && PlayerJump.InstancePJ.faceLeft == true)
        {
            playerD.transform.localScale = new Vector2(1, 1);
            PlayerJump.InstancePJ.faceLeft = false;
        }
    }
}
