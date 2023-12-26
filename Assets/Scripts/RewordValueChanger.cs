using TMPro;
using UnityEngine;

public class RewordValueChanger : MonoBehaviour
{
    public RewordedAd RewAd;
    private TMP_Text TXT;

    private void Awake()
    {
        TXT = GetComponent<TMP_Text>();
        TXT.text = RewAd.RevordCount.ToString();
    }
}
