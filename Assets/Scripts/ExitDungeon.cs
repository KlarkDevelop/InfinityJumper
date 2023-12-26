using UnityEngine;

public class ExitDungeon : MonoBehaviour
{
    public GameObject Button;
    public ScenMenegment Scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Button.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Button.SetActive(false);
    }
    public void EnterGate()
    {
        Scene.ChangeScene(0);
    }
}
