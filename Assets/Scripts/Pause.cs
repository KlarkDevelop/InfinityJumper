using UnityEngine;

public class Pause : MonoBehaviour
{
    [Header("Player")]
    public SpriteRenderer playerSpR;

    [Header("Joysticks")]
    public GameObject jumpJoy;
    public GameObject throwJoy;
    public GameObject dashJoy;

    [Header("Pause objects")]
    public GameObject pauseBatt;
    public GameObject pauseWindow;
    public GameObject startGartic;

    public ScenMenegment scene;

    private AudioSource audioS;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }
    public void doPause()
    {
        audioS.Play();
        if (startGartic != null) startGartic.SetActive(false);
        pauseBatt.SetActive(false);
        pauseWindow.SetActive(true);

        jumpJoy.SetActive(false);
        throwJoy.SetActive(false);
        dashJoy.SetActive(false);

        playerSpR.enabled = false;

        Time.timeScale = 0;
    }

    public void Continiue()
    {
        audioS.Play();
        if (startGartic != null) startGartic.SetActive(true);
        pauseBatt.SetActive(true);
        pauseWindow.SetActive(false);

        jumpJoy.SetActive(true);
        throwJoy.SetActive(true);
        dashJoy.SetActive(true);

        playerSpR.enabled = true;

        Time.timeScale = 1f;
    }

    public void Exit(int sceneNum)
    {
        Time.timeScale = 1f;
        scene.ChangeScene(sceneNum);
    }
}
