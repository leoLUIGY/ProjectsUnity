using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : Texts
{
    public Text stateText;
    public GameObject[] textsInScene;
    public Button[] sets;
    public GameObject text;
    public GameObject backgroundText;
    public FamilyControl.States state;
    [SerializeField] public static bool tutorial;
    public static bool testAtack = false;
    private int stateOfInicialization;
    private bool inInitializationComands = true;
  
    public Text[] texts;
    void Start()
    {
        TextsTutoria();
        for (int i = 0; i < textsLanguage.Length; i++)
            texts[i].text = textsLanguage[i];

        for (int j = 0; j < sets.Length; j++)
            sets[j].GetComponent<Material>().color = new Color(255,255,255);
    }

    void Update()
    {
        if (tutorial)
        {
            StateOfTutorial();
        }
        if ((Menu.choiceInitial) && tutorial )
        {
            stateOfInicialization = 0;
            Menu.choiceInitial = false;
            backgroundText.SetActive(true);
            foreach (GameObject scene in textsInScene)
            {
                scene.SetActive(false);
            }
        }

    }

    private void StateOfTutorial()
    {
        switch (FamilyControl.ActualState)
        { 
            case FamilyControl.States.initialize:
                if (inInitializationComands)
                {
                    inInitializationComands = false;
                    StartCoroutine(Initialize());
                }
                break;
            case FamilyControl.States.great:
                if (inInitializationComands)
                {
                    inInitializationComands = false;
                    StartCoroutine(Initialize());
                }
                break;
            case FamilyControl.States.firstStep:
                stateText.text = comands[0];
                //sets[1].GetComponent<Material>().color = new Color(255, 0, 0);
                break;

            case FamilyControl.States.secondStep:
                stateText.text = comands[1];
               // sets[1].GetComponent<Material>().color = new Color(255, 255, 255);
               // sets[2].GetComponent<Material>().color = new Color(255, 0, 0);
                textsInScene[1].SetActive(true);
                break;

            case FamilyControl.States.thirdStep:
                stateText.text = comands[0];
               // sets[4].GetComponent<Material>().color = new Color(255, 0,0);
                break;

            case FamilyControl.States.fourthStep:
               // sets[4].GetComponent<Material>().color = new Color(255, 255, 255);
                //sets[6].GetComponent<Material>().color = new Color(255, 0, 0);
                stateText.text = comands[2];
                break;

            default:
                tutorial = false;
                foreach (GameObject scene in textsInScene)
                {
                    scene.SetActive(true);
                }
                text.SetActive(false);
                backgroundText.SetActive(false);
                break;
        }
    }
    IEnumerator Initialize()
    {

        for (int i = stateOfInicialization; i <= initializeComands.Length; i++)
        {
            stateText.text = initializeComands[i];
            ControlSets();

            yield return new WaitForSeconds(6f);
            if (stateOfInicialization == 3)
            {
               // for (int j = 0; j < 6; j++)
                   // sets[j].GetComponent<Material>().color = new Color(255,255,255);

                inInitializationComands = true;
                FamilyControl.ActualState = FamilyControl.States.firstStep;
                stateOfInicialization++;
                break;
            }
            else if (stateOfInicialization == 6)
            {

                inInitializationComands = true;
                FamilyControl.ActualState = FamilyControl.States.thirdStep;
                stateOfInicialization++;
                break;
            }
            else if (stateOfInicialization == 9)
            {
                FamilyControl.ActualState = FamilyControl.States.finishTutorial;
                inInitializationComands = true;
                break;
            }
            else
            {
                stateOfInicialization++;
            }
        }
    }

    private void ControlSets()
    {
       /* if (stateOfInicialization == 1)
            sets[7].GetComponent<Material>().color = new Color(255,0,0);
        if (stateOfInicialization == 2)
        {
            sets[7].GetComponent<Material>().color = new Color(255,255,255);
            for(int i = 0; i < 6; i++)
                sets[i].GetComponent<Material>().color = new Color(255,0,0);
        }

        if (stateOfInicialization == 4) sets[2].GetComponent<Material>().color = new Color(255, 255,255);
        if (stateOfInicialization == 5) testAtack = true;
        else testAtack = false;
        if (stateOfInicialization == 7) 
        sets[6].GetComponent<Material>().color = new Color(255, 255,255);
        */
    }
}