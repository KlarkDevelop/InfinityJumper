using UnityEngine;

public class getScore : MonoBehaviour
{
    public RecordWriter score;

    private TMPro.TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMPro.TMP_Text>();

        text.text = score.record.ToString();
    }
}
