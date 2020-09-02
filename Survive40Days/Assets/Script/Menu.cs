using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//otimizar
public class Menu :Texts
{
    public static bool language = true;
    public static bool choiceInitial = true;
    public Scrollbar sound;
    public AudioSource audioSource;
    public static float actualVolume;
    public GameObject[] textInScene;
    void Start()
    {
        audioSource = audioSource.GetComponent<AudioSource>();
        Application.systemLanguage.Equals(SystemLanguage.English);
        TextLanguageMethod(language);
      
    }

    void Update()
    {
            audioSource.volume = sound.value;
            actualVolume = sound.value;
    }

    public void LoadScene()
    {
        StartCoroutine(Play());
        Camera.main.GetComponent<Animation>().Play();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator Play() {
        yield return new WaitForSeconds(4);
        choiceInitial = true;
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Br()
    {
        language = false;
        TextLanguageMethod(language);

    }
    public void Ing()
    {
        language = true;
        TextLanguageMethod(language);
    }
  
    public void State(int state) {
        for (int i = 0; i < textInScene.Length; i++) 
            {
            if (state == i) textInScene[state].SetActive(true);
            else textInScene[i].SetActive(false);
        }
    }
}
