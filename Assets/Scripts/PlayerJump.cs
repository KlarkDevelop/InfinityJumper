 using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public bool click;
    public Rigidbody2D player;
    private Vector2 vec;
    public float Jforce;
    public Animator anim;
    public BoxCollider2D PlCol;
    public Joystick joystick;
    public static PlayerJump InstancePJ { get; set; }

    public void Awake()
    {
        InstancePJ = this;
        Physics2D.IgnoreLayerCollision(3, 6);
    }

    public TrajectoryRenderer Trajectory;
    public Color TrajectoryColor;
    public float TrajectoryStartAlpha;
    public float TrajectoryEndAlpha;
    public int TrajectoryLineLenght = 10;
    private void Update()
    {
        if (Dash.InstanceD.DoDash == false)
        {
            player.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        if (ColisionPL.Instance.DoJ == true || ColisionPL.Instance.DoDoublleJ ==true)//Проверка на обновление прыжка
        {
            if (click == true)
            {
                vec = new Vector2(-joystick.Horizontal, -joystick.Vertical); //Вектор для толчка
                Trajectory.ShowTrajectory(player.transform.position, vec * Jforce, TrajectoryLineLenght);
                Trajectory.ChangeLineColor(TrajectoryColor, TrajectoryStartAlpha, TrajectoryEndAlpha);
            }
        }
        if (ColisionPL.Instance.animJ == false)
        {
            anim.SetBool("AnimDouble", false);
            if(work == false) ChangeColider(0);
        } 
        else if (ColisionPL.Instance.animJ == true && ColisionPL.Instance.DoDoublleJ == true && Dash.InstanceD.DoDash == false && work == false)
        {
            ChangeColider(0);
        }
        FaceCheker();

        // Проверки - фикс изменения аницмаций на подкат на мгновение во время прыжков
        if(player.transform.localPosition.x < XposL || player.transform.localPosition.x > XposR)
        {
            Podcat();
            if (work ==false)
            {
                XposR = player.transform.localPosition.x + 0.7f;
                XposL = player.transform.localPosition.x - 0.7f;
            }
        }
        ThrowAnim();
        anim.SetBool("OnEnemy", ColisionPL.Instance.OnEnemy);
    }
    private float XposL = 0;
    private float XposR = 0;

    private float AnimThrowTime = 0.2f;
    private float ResetAnThrTime = 0.2f;
    private void ThrowAnim()
    {
        if (ScreanThrow.InstanceT.DoTrow == true) 
        {
            //Бросок на стене
            if (ColisionPL.Instance.animOnW == true)
            {
                anim.SetBool("ThrowOnWoll", true);
            }
            //Бросок в прыжке
            if (ColisionPL.Instance.animJ == true)
            {
                anim.SetBool("ThrowJump", true);
            }
            //Бросок в подкате
            if (work == true)
            {
                anim.SetBool("ThrowPodcat", true);
            }
            //Бросок в состояний покоя
            if (work == false && ColisionPL.Instance.animP == true)
            {
                anim.SetBool("DoThrow", true);
            }
            AnimThrowTime -= Time.deltaTime;
            if (AnimThrowTime < 0)
            {
                ScreanThrow.InstanceT.DoTrow = false;
                AnimThrowTime = ResetAnThrTime;
            }
        }
        else
        {
            anim.SetBool("ThrowOnWoll", false);
            anim.SetBool("ThrowJump", false);
            anim.SetBool("ThrowPodcat", false);
            anim.SetBool("DoThrow", false);
            AnimThrowTime = ResetAnThrTime;
        }
    }

    private void FixedUpdate()
    {
        OnWall();
        AnimJ();
        if(Dash.InstanceD.DoRotate == false && ColisionPL.Instance.animOnW == false)
        {
            Reflect();
        }
        anim.SetBool("AnimUP", AnimUP);
        anim.SetBool("AnimDown", AnimDown);
    }

    public void LeftScreanDown()
    {
        click = true;
    }

    private bool AfterEnemy = false;
    public void LeftScreanUp()
    {
        click = false;
        if ((ColisionPL.Instance.DoJ == true || ColisionPL.Instance.DoDoublleJ == true) && Dash.InstanceD.clickD == false)//Проверка на обновление прыжка
        {

            if (ColisionPL.Instance.animOnW == true) //Фикс прыжков от стены
            {
                ColisionPL.Instance.animOnW = false;
                if (faceLeft == false)
                {
                    player.transform.position = new Vector2(player.transform.position.x + 0.2f, player.transform.position.y);
                }
                else
                {
                    player.transform.position = new Vector2(player.transform.position.x - 0.2f, player.transform.position.y);
                }
            }

            //Отключение зависания рывка
            if (Dash.InstanceD.DoDash == true)
            {
                Dash.InstanceD.DoDash = false;
                if (Dash.InstanceD.DoRotate == true)// фикс поворота
                {
                    player.transform.localScale = new Vector2(-1, 1);
                    Dash.InstanceD.DoRotate = false;
                }

                Dash.InstanceD.time = Dash.InstanceD.ResetTime;
                Dash.InstanceD.TimerMin = Dash.InstanceD.ResetTimerMin;
                player.bodyType = RigidbodyType2D.Dynamic;
            }

            if (ColisionPL.Instance.PlAtack == true)
            {
                player.velocity = new Vector2(0, 0);
                AfterEnemy = true;
                ColisionPL.Instance.EnemyClass.PlAtack = false;
                ColisionPL.Instance.DoJ = false;
                ColisionPL.Instance.DoDoublleJ = true;
                ColisionPL.Instance.OnEnemy = false;
                DoublJump(0, false);
            }
            player.AddForce(vec * Jforce, ForceMode2D.Impulse);//Импульс

            if (ColisionPL.Instance.DoJ == false && AfterEnemy == false)
            {
                ColisionPL.Instance.DoDoublleJ = false;
            }
            //В классе ColisionPL присваевается переменой DoDoublleJ значение true после выхода из колизий, из-за чего есть возможность прыгнуть ещё раз в воздухе, после двойного прыжка мы больше не сделаем ещё одного

            if (ColisionPL.Instance.DoJ == false && ColisionPL.Instance.DoDoublleJ == false && AfterEnemy == false)
            {
                player.velocity = new Vector2(0, 0);
                player.AddForce(vec * Jforce, ForceMode2D.Impulse);
                DoublJump(2, true);
            }
            AfterEnemy = false;

            Trajectory.ResetTrajectory();
        }
    }

    private float oldPosPLy;
    private bool AnimUP = false;
    private bool AnimDown = false;
    private void AnimJ() //Функция на паденик и прыжкок
    {
        if (ColisionPL.Instance.animJ == true)
        {

            if (player.transform.position.y > oldPosPLy)
            {
                AnimUP = true;
                AnimDown = false;
            }
            else if (player.transform.position.y < oldPosPLy)
            {
                AnimUP = false;
                AnimDown = true;
            }
        }
        if (ColisionPL.Instance.animJ == false)
        {
            AnimUP = false;
            AnimDown = false;
        }
        oldPosPLy = player.transform.position.y;
    }

    public bool faceLeft;
    private float oldPosPLx;

    private void Reflect() //Функция поворота персонажа
    {
        if ((Math.Round(player.transform.position.x, 3) > Math.Round(oldPosPLx, 3) && faceLeft) || ((Math.Round(player.transform.position.x, 3) < Math.Round(oldPosPLx, 3) && !faceLeft)))
        {
            player.transform.localScale *= new Vector2(-1,1);
            faceLeft = !faceLeft;
        }
        oldPosPLx = player.transform.position.x;
    }

    private void FaceCheker()
    {
        if(player.transform.localScale.x == 1)
        {
            faceLeft = false;
        }
        else
        {
            faceLeft = true;
        }
    }

    private float delayPodkat = 0.6f;
    private float delayPodkatR = 0.6f;

    public float Velocity;
    public float StUPVel;//Переменная скорости на которой персонаж встанет
    public bool work = false;
    private void Podcat()//Функция подката
    {
        if (ColisionPL.Instance.animP == true && player.velocity.magnitude > Velocity)
        {
            anim.SetBool("DoPodkat", true);
            ChangeColider(1);
            work = true;
        }
        else if(ColisionPL.Instance.animP == false || player.velocity.magnitude < StUPVel)
        {
            if(work == true && (AnimUP == true || ColisionPL.Instance.animOnW == true))
            {
                delayPodkat = delayPodkatR;
                ChangeColider(0);
                work = false;
                anim.SetBool("DoPodkat", false);
            }
            else if(work == true)
            {
                delayPodkat -= Time.deltaTime;

                if (delayPodkat < 0)
                {
                    delayPodkat = delayPodkatR;
                    ChangeColider(0);
                    work = false;
                    anim.SetBool("DoPodkat", false);
                }
            }
        }
    }
    private void OnWall()
    {

        if (ColisionPL.Instance.animOnW == true)
        {
            Dash.InstanceD.DoDash = false;
            player.velocity = new Vector2(player.velocity.x, 0.2f);
            anim.SetBool("DoOnWall", true);
            if(ColisionPL.Instance.TurnLeft == true)
            {
                player.transform.localScale = new Vector2(-1, 1);
                faceLeft = true;
            }
            else
            {
                faceLeft = false;
                player.transform.localScale = new Vector2(1, 1);
            }
        }
        else
        {
            anim.SetBool("DoOnWall", false);
        }
    }

    public Transform pointThrow;
    public bool inDaubleAnimation;
    public bool inPodcatAnimation;
    public void ChangeColider(byte type) // 1 - подкат; 2 - двойной прыжок; 3 - рывок
    {
        if (type == 1) //подкат
        {
            pointThrow.localPosition = new Vector2(0.64f, 0.475f);
            PlCol.offset = new Vector2(0, -0.71f);
            PlCol.size = new Vector2(1, 0.87f);
            inDaubleAnimation = false;
            inPodcatAnimation = true;
        }
        else if(type == 2)//двойной прыжок 
        {
            PlCol.offset = new Vector2(0, 0.1f);
            PlCol.size = new Vector2(1.2f, 1.2f);
            inDaubleAnimation = true;
            inPodcatAnimation = false; 
        }
        else if (type == 3) // Рывок
        {
            PlCol.offset = new Vector2(0, 0);
            PlCol.size = new Vector2(1, 1.69f);
            inDaubleAnimation = false;
            inPodcatAnimation = false;
        }
        else if(type == 4)
        {
            PlCol.offset = new Vector2(0, -0.27f);
            PlCol.size = new Vector2(1, 1.69f);
            inDaubleAnimation = false;
            inPodcatAnimation = false;
        }
        else //Нормальное положение
        {
            pointThrow.localPosition = new Vector2(0.885f, 1.398f);
            PlCol.offset = new Vector2(0, 0);
            PlCol.size = new Vector2(1, 2.3f);
            inDaubleAnimation = false;
            inPodcatAnimation = false;
        }
    }
    public void DoublJump(byte SetType, bool AnimJ)
    {
        anim.SetBool("AnimDouble", AnimJ);
        ChangeColider(SetType);
    }
}
