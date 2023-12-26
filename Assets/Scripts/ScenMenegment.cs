using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenMenegment : MonoBehaviour
{
    public SaveSystem save;
    public Settings settings ;
    public void ChangeScene(int SceneNumber)
    {
        if (save != null) save.DoSaveData();
        SceneManager.LoadScene(SceneNumber);
    }

    public void mainMenuButt()
    {
        if(((save != null) && save.trainingPass == true) && settings.trainingPass == true)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(3);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void RestartScene()
    {
        if (save != null) save.DoSaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
