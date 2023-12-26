using UnityEngine;

public class movingPlatformX : MonoBehaviour
{
    public Transform point;
    public float speed = 4;

    public Vector3 startPoint;

    private bool StartLeft = false;
    private void Awake()
    {
        startPoint = transform.position;
        resetDelay = delay;

        if (transform.position.x < point.transform.position.x)
        {
            StartLeft = true;
        }
        else
        {
            StartLeft = false;
        }
        work = false;
    }

    public bool moveBack = true;
    [HideInInspector]
    public bool work = true;
    public bool isArrived = false;
    private void Update()
    {
        if(workFunk == true)
        {
            getStartPoint();
        }
        else
        {
            if (moveBack == true)
            {
                MoveInfinity();
            }
            else
            {
                MoveOneWay();
            }
            setTimeout();
        }
    }

    private void MoveOneWay()
    {
        if (StartLeft == true && isArrived == false)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else if (StartLeft == false && isArrived == false)
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }

        if (StartLeft == true && transform.position.x > point.transform.position.x)
        {
            isArrived = true;
        }
        else if (StartLeft == false && transform.position.x < point.transform.position.x)
        {
            isArrived = true;
        }
        else
        {
            isArrived = false;
        }
    }

    private void MoveInfinity()
    {
        if (isArrived == false && work == true)
        {
            if (StartLeft == true)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            }

        }
        else if (isArrived == true && work == true)
        {
            if (StartLeft == true)
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            }
        }

        if (StartLeft == true)
        {
            if (transform.position.x > point.transform.position.x && isArrived == false)
            {
                work = false;
                isArrived = true;
            }
            else if (transform.position.x < startPoint.x && isArrived == true)
            {
                work = false;
                isArrived = false;
            }
        }
        else
        {
            if (transform.position.x < point.transform.position.x && isArrived == false)
            {
                work = false;
                isArrived = true;
            }
            else if (transform.position.x > startPoint.x && isArrived == true)
            {
                work = false;
                isArrived = false;
            }
        }
    }

    private bool workFunk = true;
    private float startPointTimer = 0.3f;
    private void getStartPoint()
    {
        startPointTimer -= Time.deltaTime;
        if(startPointTimer < 0)
        {
            startPoint = transform.position;
            workFunk = false;
        }
    }

    [Header("Timer")]
    public float delay;
    private float resetDelay;
    private void setTimeout()
    {
        if (work == false)
        {
            delay -= Time.deltaTime;
            if (delay < 0)
            {
                delay = resetDelay;
                work = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.parent = transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
