using System.Collections.Generic;
using UnityEngine;

public class ButtonMulti : MonoBehaviour
{
    //[Header("Button settings")]
    private Animator buttAnim;
    private bool buttonIsPress = false;

    [Header("Moving Objects")]
    public float speed;
    public List<Transform> objectsM;

    [Header("Poits for move")]
    public List<Transform> points;

    [Header("Reusability")]
    public bool reusable = false;
    public float reuseDelay = 0;
    private float reuseDelayReset;

    private void Awake()
    {
        buttAnim = GetComponent<Animator>();
        reuseDelayReset = reuseDelay;
    }

    private void Update()
    {
        if(buttonIsPress == true)
        {
            ChangeAnim("ButtonDown");
            if (objectsM.Count == points.Count)
            {
                for(int i = 0; i < points.Count; i++)
                {
                    Vector2 dir = (points[i].transform.position - objectsM[i].transform.position).normalized * speed * Time.deltaTime;
                    objectsM[i].Translate(dir);

                    if (objectsM[i].TryGetComponent(out movingPlatformX platformX))
                    {
                        platformX.enabled = false;
                        platformX.isArrived = false;
                    }
                    if (objectsM[i].TryGetComponent(out movingPlatformY platformY))
                    {
                        platformY.enabled = false;
                        platformX.isArrived = false;
                    }

                }
            }
            else
            {
                throw new System.NullReferenceException($"Количество объектов и точек движения не совпадают: Objects{objectsM.Count} ; Points{points.Count}");
            }
        }

        if (reusable == true)
        {
            Reuse();
        }
    }

    private void Reuse()
    {
        if (buttonIsPress == true)
        {
            reuseDelay -= Time.deltaTime;
        }
        if (reuseDelay < 0)
        {
            reuseDelay = reuseDelayReset;
            buttonIsPress = false;
            for(int i = 0; i < objectsM.Count; i++)
            {
                if (objectsM[i].TryGetComponent(out movingPlatformX platformX))
                {
                    platformX.enabled = true;
                }
                if (objectsM[i].TryGetComponent(out movingPlatformY platformY))
                {
                    platformY.enabled = true;
                }
            }
            ChangeAnim("ButtonUp");
        }
    }

    public void DoPress()
    {
        buttonIsPress = true;
    }

    //ChangeAnim("ButtonDown");
    private string CurrentAnim = "ButtonUp";
    private void ChangeAnim(string animName)
    {
        if (CurrentAnim == animName)
        {
            return;
        }
        else
        {
            buttAnim.Play(animName);
            CurrentAnim = animName;
        }
    }
}
