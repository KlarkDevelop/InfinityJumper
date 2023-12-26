using UnityEngine;
using TMPro;

public class Translator : MonoBehaviour
{
    public SaveSystem save;
    private TMP_Text text;
    [TextArea]public string newText;
    private string oldText;

    public void Awake()
    {
        text = GetComponent<TMP_Text>();
        oldText = text.text;
    }

    public void Update()
    {
        if (save.lang == "ru")
        {
            text.text = newText;
        }
        else
        {
            text.text = oldText;
        }
    }
}
