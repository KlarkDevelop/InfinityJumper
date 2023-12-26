using UnityEngine;

public class Button : MonoBehaviour
{
    public bool ButtonDown = false;
    public Transform MovingObject;
    public float speed;
    public Transform PointForMove;
    private Animator anim;

    public bool reusable = false;
    public float reuseDelay = 0;
    private float reuseDelayReset;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        reuseDelayReset = reuseDelay;
    }

    void Update()
    {
        if (ButtonDown == true)
        {
            if (MovingObject.TryGetComponent(out movingPlatformX platformX))
            {
                platformX.enabled = false;
                platformX.isArrived = false;
            }
            if (MovingObject.TryGetComponent(out movingPlatformY platformY))
            {
                platformY.enabled = false;
                platformY.isArrived = false;
            }

            Vector2 dir = (PointForMove.transform.position - MovingObject.transform.position).normalized * speed * Time.deltaTime;
            MovingObject.Translate(dir, Space.World);
            ChangeAnim("ButtonDown");
        }

        if(reusable == true)
        {
            Reuse();
        }
    }

    private void Reuse()
    {
        if(ButtonDown == true)
        {
            reuseDelay -= Time.deltaTime;
        }
        if(reuseDelay < 0)
        {
            reuseDelay = reuseDelayReset;
            ButtonDown = false;

            if (MovingObject.TryGetComponent(out movingPlatformX platformX))
            {
                platformX.enabled = true;
            }
            if (MovingObject.TryGetComponent(out movingPlatformY platformY))
            {
                platformY.enabled = true;
            }

            ChangeAnim("ButtonUp");
        }
    }

    private string CurrentAnim = "ButtonUp";
    private void ChangeAnim(string animName)
    {
        if(CurrentAnim == animName)
        {
            return;
        }
        else
        {
            anim.Play(animName);
            CurrentAnim = animName;
        }
    }

    public void DoPress()
    {
        ButtonDown = true;
    }
}
