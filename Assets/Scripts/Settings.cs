using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public SaveSystem saveSys;

    [Header("Settings")]
    public string language = "en";
    public bool trainingPass = false;

    [Header("UI")]
    public TMP_Text languageUI;
    public Animator trainingStateUI;
    public GameObject settingsWindow;
    public GameObject menuButtons;
    private AudioSource settingsButtSource;

    public Slider slider;
    private void Start()
    {
        language = saveSys.lang;
        trainingPass = saveSys.trainingPass;
        settingsButtSource = GetComponent<AudioSource>();
        slider.value = saveSys.volumeInSave;
    }

    private void showSettingsValue()
    {
        language = saveSys.lang;
        trainingPass = saveSys.trainingPass;


        if (language == "ru")
        {
            languageUI.text = "Русский";
        }
        else if (language == "en")
        {
            languageUI.text = "English";
        }

        if (saveSys.trainingPass == true)
        {
            trainingStateUI.Play("no");
        }
        else
        {
            trainingStateUI.Play("yes");
        }
    }
    
    public void changeLanguage()
    {
        settingsButtSource.Play();
        if (language == "en")
        {
            language = "ru";
            languageUI.text = "Русский";
        }
        else if(language == "ru")
        {
            language = "en";
            languageUI.text = "English";
        }
        saveSys.changeLanguage();
    }

    public void goBackTraining()
    {
        settingsButtSource.Play();
        if (trainingPass == true)
        {
            trainingStateUI.Play("yes");
            trainingPass = false;
        }
        else
        {
            saveSys.endTraining();
            trainingStateUI.Play("no");
            trainingPass = true;
        }
    }

    public void toggleSettingsWindow()
    {
        settingsButtSource.Play();
        if (settingsWindow.activeSelf == true)
        {
            settingsWindow.SetActive(false);
            menuButtons.SetActive(true);
        }
        else
        {
            settingsWindow.SetActive(true);
            menuButtons.SetActive(false);
            showSettingsValue();
        }
    }

    public AudioMixerGroup mixer;
    
    public void changeSoundVolume(float value)
    {
        mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, value));
        saveSys.changeVolume(value);
    }
}
