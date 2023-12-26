using System.Collections.Generic;
using UnityEngine;

public class TrainingChanksM : MonoBehaviour
{
    public Transform BackWoll;
    private Vector2 screenPoint;
    public Transform Player;
    [Header("Чанки")]
    public Chank[] Chanks;

    private List<Chank> ChanksInScene = new List<Chank>();

    private void Awake()
    {
        foreach (Chank chank in Chanks)
        {
            ChanksInScene.Add(chank);
        }
    }

    private void Update()
    {
        findCurrentChank();

        screenPoint = Camera.main.WorldToViewportPoint(CurrentChank.WollPoint.position);
        if (screenPoint.x < 0)
        {
            BackWoll.position = CurrentChank.WollPoint.position;
            if (!(ChanksInScene[0].StartPoint.position.x < Player.position.x && Player.position.x < ChanksInScene[0].EndPoint.position.x))
            {
                Destroy(ChanksInScene[0].gameObject);
                ChanksInScene.RemoveAt(0);
            }
        }
    }

    private Chank CurrentChank;
    private void findCurrentChank()
    {
        foreach (Chank chank in ChanksInScene)
        {
            if (chank.StartPoint.position.x < Player.position.x && Player.position.x < chank.EndPoint.position.x)
            {
                CurrentChank = chank;
                break;
            }
        }
    }
}
