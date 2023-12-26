using System;
using System.Collections.Generic;
using UnityEngine;

public class ChankGenerator : MonoBehaviour
{
    public Transform BackWoll;
    public Vector2 screenPoint;
    public Transform Player;
    public RecordWriter RecordPlayer;
    [Header("Чанки")]
    public Chank StartChank;
    public Chank[] Chanks;

    private List<Chank> ChanksLvl0 = new List<Chank>();
    private List<Chank> ChanksLvl1 = new List<Chank>();
    private List<Chank> ChanksLvl2 = new List<Chank>();
    private List<Chank> ChanksLvl3 = new List<Chank>();
    private List<Chank> ChanksLvl4 = new List<Chank>();
    private List<Chank> ChanksLvl5 = new List<Chank>();
    private List<Chank> ChanksLvl6 = new List<Chank>();

    private List<Chank> ChanksInScene = new List<Chank>();

    private void Awake()
    {
        ChanksInScene.Add(StartChank);
        sortChanks();
        Debug.Log($"Сумма всех шансов:{ ChanceLvl[0] + ChanceLvl[1] + ChanceLvl[2] + ChanceLvl[3] + ChanceLvl[4] + ChanceLvl[5] + ChanceLvl[6] }");

    }
    private void Update()
    {
        balanceChance();

        if (Player.position.x > (ChanksInScene[ChanksInScene.Count - 1].EndPoint.position.x - 20))
        {
            generateChank();
        }

        findCurrentChank();

        screenPoint = Camera.main.WorldToViewportPoint(CurrentChank.WollPoint.position);
        if (screenPoint.x < 0)
        {
            BackWoll.position = CurrentChank.WollPoint.position;
            if (!(ChanksInScene[0].StartPoint.position.x < Player.position.x 
                && Player.position.x < ChanksInScene[0].EndPoint.position.x)
                && (ChanksInScene[0] != ChanksInScene[ChanksInScene.IndexOf(CurrentChank)]))
            {
                //Debug.Log(ChanksInScene[0].gameObject.name);
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
            if(chank != null)
            {
                if (chank.StartPoint.position.x < Player.position.x && Player.position.x < chank.EndPoint.position.x)
                {
                    CurrentChank = chank;
                    break;
                }
            }
            else
            {
                Debug.Log("Null chank detected and remuved");
                ChanksInScene.Remove(chank);
                break;
            }
        }
    }

    private List<List<Chank>> ListChanksLvl = new List<List<Chank>>();
    private void sortChanks()
    {
        foreach (Chank chank in Chanks)
        {
            switch (chank.difficult)
            {
                case 0: ChanksLvl0.Add(chank); break;
                case 1: ChanksLvl1.Add(chank); break;
                case 2: ChanksLvl2.Add(chank); break;
                case 3: ChanksLvl3.Add(chank); break;
                case 4: ChanksLvl4.Add(chank); break;
                case 5: ChanksLvl5.Add(chank); break;
                case 6: ChanksLvl6.Add(chank); break;
            }
        }
        

        ListChanksLvl.Add(ChanksLvl0);
        ListChanksLvl.Add(ChanksLvl1);
        ListChanksLvl.Add(ChanksLvl2);
        ListChanksLvl.Add(ChanksLvl3);
        ListChanksLvl.Add(ChanksLvl4);
        ListChanksLvl.Add(ChanksLvl5);
        ListChanksLvl.Add(ChanksLvl6);
    }

    // 5 - 1 чанк 
    public float RecordForHardBalanc = 250;
    private bool oneChange = true;
    public float RecordPerUpdate = 50;
    private float nextRecord = 30;
    public float step = 1;
    public float partForLvl0 = 4;

    // 0 - чанк из бонусов(Самый редкий), 1 - очень легко, 2 - легко,
    // 3 - средне, 4 - выше среднего, 5 - сложно, 6 - очень сложно
    [Header("Шанс появления чанка")]
    public float[] ChanceLvl = new float[7];

    // X = 100 - 2 стандартных чанка 
    private void balanceChance()
    {
        
        if(nextRecord < RecordPlayer.record)
        {
            Debug.Log($"Сумма всех шансов до:{ ChanceLvl[0] + ChanceLvl[1] + ChanceLvl[2] + ChanceLvl[3] + ChanceLvl[4] + ChanceLvl[5] + ChanceLvl[6] }");
            if (ChanceLvl[3] >= 10)
            {
                if(ChanceLvl[1] > 1)ChanceLvl[1] -= step;
                if(ChanceLvl[2] > 5)ChanceLvl[2] -= step;
                ChanceLvl[3] -= step;

                float reminderValue = 0;
                if(ChanceLvl[1] > 1 && ChanceLvl[2] > 5)
                {
                    reminderValue = step * 3;
                }
                else if(ChanceLvl[1] > 1 && ChanceLvl[2] <= 5)
                {
                    reminderValue = step * 2;
                }
                else if(ChanceLvl[1] <= 1)
                {
                    reminderValue = step;
                }

                float valueForLvl0 = reminderValue / partForLvl0;
                float valueForOthers = reminderValue - valueForLvl0;

                if(RecordPlayer.record <= RecordForHardBalanc-5)
                {
                    ChanceLvl[0] += valueForLvl0;

                    ChanceLvl[4] += (valueForOthers/ 3) * 2;
                    ChanceLvl[5] += (valueForOthers / 3) * 1;
                }
                else if(RecordPlayer.record >= RecordForHardBalanc)
                {
                    ChanceLvl[0] += valueForLvl0;
                    ChanceLvl[4] += valueForOthers  / 3;
                    ChanceLvl[5] += valueForOthers / 3;
                    ChanceLvl[6] += valueForOthers / 3;
                }
                nextRecord += RecordPerUpdate;
            }
            if(RecordPlayer.record > RecordForHardBalanc && oneChange == true)
            {
                ChanceLvl[0] = 1;
                ChanceLvl[1] = 36;
                ChanceLvl[2] = 26;
                ChanceLvl[3] = 21;
                ChanceLvl[4] = 10;
                ChanceLvl[5] = 5;
                ChanceLvl[6] = 1;

                oneChange = false;
            }
            Debug.Log($"Сумма всех шансов после:{ ChanceLvl[0] + ChanceLvl[1] + ChanceLvl[2] + ChanceLvl[3] + ChanceLvl[4] + ChanceLvl[5] + ChanceLvl[6] }");
        }
    }


    private Chank newChank;
    private void generateChank()
    {
        float random = (float)Math.Round(UnityEngine.Random.Range(0f, 101f), 2);
        //Debug.Log(random);

        //StartPeriodLvl0 = 0
        float[] StartPeriodLvl = new float[7];
        float[] EndPeriodLvl = new float[7];

        for (int i= 0; i < StartPeriodLvl.Length; i++)
        {
            if((i + 1) < 7) StartPeriodLvl[i+1] = StartPeriodLvl[i] + ChanceLvl[i];
            EndPeriodLvl[i] = StartPeriodLvl[i] + ChanceLvl[i];
            //Debug.Log($"Generator {i} || Start: {StartPeriodLvl[i]}, End: {EndPeriodLvl[i]} ");
        }

        int iter = 0;
        foreach(List<Chank> listLvl in ListChanksLvl)
        {
            if((StartPeriodLvl[iter] < random && random < EndPeriodLvl[iter]) && ChanceLvl[iter] > 0)
            {
                if (listLvl.Count != 0 )
                {
                    newChank = Instantiate(listLvl[UnityEngine.Random.Range(0, listLvl.Count)]);
                    newChank.transform.position = ChanksInScene[ChanksInScene.Count - 1].EndPoint.position - newChank.StartPoint.localPosition;
                    ChanksInScene.Add(newChank);
                }
                else
                {
                    Debug.Log($"Чанков сложности {iter} не существует или шанс появления чанка равен 0." +
                        $" Значение Random: {random} " +
                        $"Значения отрезков шансов: start - {StartPeriodLvl[iter]} , end {EndPeriodLvl[iter]}");
                    generateChank();
                }
            }
            iter++;
        }
    }
}
