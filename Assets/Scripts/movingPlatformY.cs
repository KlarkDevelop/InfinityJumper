using UnityEngine;

public class movingPlatformY : MonoBehaviour
{
    public Transform point;
    public float speed = 4;

    private Vector3 startPoint;

    private bool StartTop = false;
    private void Awake()
    {
        startPoint = transform.position;
        resetDelay = delay;

        if (point.transform.position.y > transform.position.y)
        {
            StartTop = false;
        }
        else
        {
            StartTop = true;
        }
    }

    public bool moveBack = true;
    [HideInInspector]
    public bool work = true;
    public bool isArrived = false;
    private void Update()
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

    private void MoveOneWay()
    {
        if (StartTop == true && isArrived == false)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
        else if (StartTop == false && isArrived == false)
        {
            transform.position = new Vector2(transform.position.x , transform.position.y + speed * Time.deltaTime);
        }

        if (StartTop == true && transform.position.y < point.transform.position.y)
        {
            isArrived = true;
        }
        else if (StartTop == false && transform.position.y > point.transform.position.y)
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
            if (StartTop == true)
            {
                transform.position = new Vector2(transform.position.x , transform.position.y - speed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(transform.position.x , transform.position.y + speed * Time.deltaTime);
            }

        }
        else if (isArrived == true && work == true)
        {
            if (StartTop == true)
            {
                transform.position = new Vector2(transform.position.x , transform.position.y + speed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(transform.position.x , transform.position.y - speed * Time.deltaTime);
            }
        }

        if (StartTop == true)
        {
            if (transform.position.y < point.transform.position.y && isArrived == false)
            {
                work = false;
                isArrived = true;
            }
            else if (transform.position.y > startPoint.y && isArrived == true)
            {
                work = false;
                isArrived = false;
            }
        }
        else
        {
            if (transform.position.y > point.transform.position.y && isArrived == false)
            {
                work = false;
                isArrived = true;
            }
            else if (transform.position.y < startPoint.y && isArrived == true)
            {
                work = false;
                isArrived = false;
            }
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
            if(delay < 0)
            {
                delay = resetDelay;
                work = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
