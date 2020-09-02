using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Initialization : MonoBehaviour
{
    public Text textRecord;
    public GameObject tutorialChoice;
    public int recordDays = 0;
    public AudioSource[] audioSource;
    void Start()
    {
        audioSource[0] = audioSource[0].GetComponent<AudioSource>();
        audioSource[1] = audioSource[1].GetComponent<AudioSource>();
        audioSource[0].volume = Menu.actualVolume;
        audioSource[1].volume = Menu.actualVolume;
        DataGame data = SaveSystem.Load();
        if (data != null)
        {
            recordDays = data.recordDays;
        }

        if (Menu.choiceInitial)
        {
            Time.timeScale = 0;
            tutorialChoice.SetActive(true);
        }
       else
           No();
    }

    void Update()
    {
        if (FamilyControl.houseLife <= 0) {
            SaveValues();
            textRecord.text = recordDays.ToString();
        }
    }
    public void yes()
    {
        Time.timeScale = 1;
        Tutorial.tutorial = true;
        Menu.choiceInitial = true;
        tutorialChoice.SetActive(false);
        
    }
    public void No()
    {
        Time.timeScale = 1;
        Tutorial.tutorial = false;
        tutorialChoice.SetActive(false);
    }

    public void TutorialChoice()
    {
        SceneManager.LoadScene(1);
        Menu.choiceInitial = true;

    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
        Menu.choiceInitial = false;
    }

    private void SaveValues()
    {
        if (Day.day >= recordDays)
        {
            recordDays = Day.day;
            SaveSystem.Save(this);
        }
    }
}
