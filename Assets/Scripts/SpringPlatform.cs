using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    public float angle;
    public Transform aim;

    public Transform direction;

    public float power;

    [HideInInspector]
    public Vector2 vector;

    private Animator springAnim;

    private void Awake()
    {
        springAnim = GetComponent<Animator>();

        if (transform.position.x < aim.position.x)
        {
            direction.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            direction.rotation = Quaternion.Euler(0, 0, -(180 + angle));
        }

        if (angle == 90)
        {
            vector = Vector2.up * power;
        }
        else if(angle == -90)
        {
            vector = Vector2.down * power;
        }
        else
        {
            float v = FindBalisticVector();

            vector = direction.right * v;
        }
    }

    private string currentAnim = "SpringIdle";
    public void ChangeAnim(string name)
    {
        if (name != currentAnim)
        {
            springAnim.Play(name);
            currentAnim = name;
        }
    }

    public float g = Physics.gravity.y;
    private float FindBalisticVector()
    {
        Vector2 vectorB = aim.position - transform.position;

        float x = vectorB.magnitude;
        float y = vectorB.y;

        float angleInRad = angle * Mathf.PI / 180;

        float v2 = (g * (x * x)) / (2 * (y - Mathf.Tan(angleInRad) * x) * Mathf.Pow(Mathf.Cos(angleInRad), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        return v;
    }

    [Header("Gizmo in scene")]
    public bool drowGizmo = true;
    private void OnDrawGizmos()
    {
        if (drowGizmo)
        {
            Gizmos.color = Color.white;

            if (transform.position.x < aim.position.x)
            {
                direction.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                direction.rotation = Quaternion.Euler(0, 0, -(180 + angle));
            }

            Gizmos.DrawRay(new Ray(transform.position, direction.right));
        }
    }
}
