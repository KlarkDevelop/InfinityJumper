using UnityEngine;

public class getBestRecord : MonoBehaviour
{
    public SaveSystem save;

    private TMPro.TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMPro.TMP_Text>();

        text.text = save.PlayerRecord.bestRecord.text;
    }
}
