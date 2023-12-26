using UnityEngine;

public class EnterDungeon : MonoBehaviour
{
    public SpriteRenderer Player;
    public Animator Gate;
    public GameObject Button;
    public ScenMenegment Scene;
    public int sceneNum;

    private AudioSource enterSource;
    private void Start()
    {
        enterSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Button.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Button.SetActive(false);
    }

    
    private bool isWork = false;
    public void EnterGate()
    {
        enterSource.Play();
        Gate.Play("GateDown");
        Player.enabled = false;
        isWork = true;
    }

    public float time = 3;
    private void Update()
    {
        if (isWork == true)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                Scene.ChangeScene(sceneNum);
            }
        }
    }
}
