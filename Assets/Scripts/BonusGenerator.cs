using System;
using System.Collections.Generic;
using UnityEngine;

public class BonusGenerator : MonoBehaviour
{
    public List<GameObject> Bonuses;
    public List<int> Chanses;

    private void Awake()
    {
        randomGenerate();
    }

    private void randomGenerate()
    {
        int value = UnityEngine.Random.Range(0, 101);

        int startNothingChansValue = 0;
        foreach (int item in Chanses)
        {
            startNothingChansValue += item; 
        }
        if( startNothingChansValue < value && value <= 100)
        {
            Destroy(this.gameObject);
        }
        Chanses.Add((100 - startNothingChansValue));
        float startChansValue = 0;
        int i = 0;
        foreach(GameObject item in Bonuses)
        {
            if ((Chanses.Count >= 2 && (Chanses.Count - 1) == Bonuses.Count) ) {                
                if(startChansValue < value && value <= ( startChansValue + Chanses[i]))
                {
                    Instantiate(Bonuses[i], this.transform);
                }
                startChansValue = startChansValue + Chanses[i];
                i++;
            }
            else
            {
                throw new notEnoughChansesExeption("Нехватает элементов списка Chanses");
            }
        }
    }
    [Serializable]
    private class notEnoughChansesExeption : Exception
    {
        public notEnoughChansesExeption() { }

        public notEnoughChansesExeption(string message)
            : base(message) { Debug.Log(message); }
    }
}
