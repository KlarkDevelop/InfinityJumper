using UnityEngine;
using TMPro;

public class RecordWriter : MonoBehaviour
{
    public float record;
    private float oldPosX;

    [Header("Text")]
    public TMP_Text score;
    public TMP_Text bestRecord;

    private void Awake()
    {
        oldPosX = this.transform.position.x;
    }

    public bool doWriteRecord = true;
    private void Update()
    {
        if(doWriteRecord) WatchRecord();    
    }

    private void WatchRecord()
    {
        if(oldPosX < this.transform.position.x)
        {
            record = Mathf.Round(this.transform.position.x / 10);
            score.text = record.ToString();
            oldPosX = this.transform.position.x;
        }
    }
}
