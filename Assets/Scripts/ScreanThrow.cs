using UnityEngine;

public class ScreanThrow : MonoBehaviour
{
    public Vector2 vecT;
    public Joystick joystickT;
    public AudioSource playerSource;
    public AudioClip sound;

    public static ScreanThrow InstanceT { get; set; }
    public void Awake()
    {
        InstanceT = this;
    }

    public float ThrowForce;
    public TrajectoryRenderer Trajectory;
    public Color TrajectoryColor;
    public float TrajectoryStartAlpha;
    public float TrajectoryEndAlpha;
    void Update()
    {
        if (ColisionPL.Instance.TrowCount > 0)//Проверка на наличие бонуса
        {
            if (clickT == true)
            {
                vecT = MousePosT - new Vector2(joystickT.Horizontal, joystickT.Vertical);
                vecT *= ThrowForce;//Вектор для броска 
                Trajectory.ShowTrajectory(pointThrow.transform.position, vecT, 16);
                Trajectory.ChangeLineColor(TrajectoryColor, TrajectoryStartAlpha, TrajectoryEndAlpha);
            }
        }
    }

    private bool clickT = false;
    private Vector2 MousePosT = new Vector2(0,0);

    public void TrowScreanDown()
    {
        clickT = true;
    }


    public Transform pointThrow;
    public GameObject syric;
    public bool DoTrow = false;

    public void ThrowScreanUp()
    {
        clickT = false;
        if (ColisionPL.Instance.TrowCount > 0)
        {
            Instantiate(syric, pointThrow.transform.position, Quaternion.identity);
            playerSource.PlayOneShot(sound);
            DoTrow = true;
            ColisionPL.Instance.TrowCount -= 1;
            Trajectory.ResetTrajectory();
        }
    }
}
