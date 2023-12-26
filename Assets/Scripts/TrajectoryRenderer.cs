using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    private LineRenderer LineRen;
    void Start()
    {
        LineRen = GetComponent<LineRenderer>();
    }

    public void ShowDashTrajectory(Vector2 origin, Vector2 speed, float DashForce, int pointsCount, float pointDist, float DeathZone)
    {
        if (speed.magnitude > DeathZone)
        {
            Vector3[] points = new Vector3[pointsCount];
            LineRen.positionCount = points.Length;

            for (int i = 0; i < points.Length; i++)
            {
                float time = i * pointDist;
                points[i] = origin + (speed* DashForce) * time + Physics2D.gravity * time * time / 2;
            }
            LineRen.SetPositions(points);
        }
        else
        {
            LineRen.positionCount = 0;
        }
    }

    public void ShowTrajectory( Vector2 origin, Vector2 speed, int pointsCount)
    {
        Vector3[] points = new Vector3[pointsCount];
        LineRen.positionCount = points.Length;

        for(int i= 0; i < points.Length; i++)
        {
            float time = i * 0.1f;

            points[i] = origin + speed * time + Physics2D.gravity * time * time / 2;
        }
        LineRen.SetPositions(points);
    }

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    public void ChangeLineColor( Color color, float StartAlpha, float EndAlpha )
    {
        gradient = new Gradient();

        colorKey = new GradientColorKey[2];
        colorKey[0].color = color;
        colorKey[0].time = 0.0f;
        colorKey[1].color = color;
        colorKey[1].time = 1.0f;

        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = StartAlpha;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = EndAlpha;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);


        LineRen.colorGradient = gradient;
    }
    public void ResetTrajectory()
    {
        LineRen.positionCount = 0;
    }

}
