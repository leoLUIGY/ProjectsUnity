using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FamilyControl : MonoBehaviour
{
    [SerializeField] public enum States { firstStep, secondStep, thirdStep, fourthStep,finishTutorial, great, initialize};
    private  States stateTemp;
    public static  States ActualState = States.initialize;
    [SerializeField] public static int houseLife;
    private float maxLife;
    [SerializeField] public Image[] lifeHouse;
    [SerializeField] public GameObject[] familyPerson;
    [SerializeField] public GameObject[] placeRest;
    [SerializeField] public GameObject[] helpLight;
    [SerializeField] public GameObject highlight;
    [SerializeField] private bool selected;
    [SerializeField] GameObject imageLose;
    [SerializeField] public AudioClip som;
    [SerializeField] private Text houseValue;
    private  Vector3 temporaryPosition;
    private Quaternion temporaryRotation;
    private GameObject temp;

    private void Awake() {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
  
        selected = false;
        houseLife = 1000;
        maxLife = houseLife;
        imageLose.SetActive(false);
        for (int i  = 0; i<3;i++) {
            helpLight[i].transform.position = placeRest[i].transform.position;
        }
        lifeHouse[1].rectTransform.sizeDelta = new Vector2(houseLife / maxLife * 300, 10);
    }

    // Update is called once per frame
    void Update()
    {

        lifeHouse[0].rectTransform.sizeDelta = new Vector2(houseLife / maxLife * 300, 10);
        houseValue.text = (houseLife / 10).ToString() + "/" + (maxLife/10).ToString();
        if (houseLife <= 0)
        {
            imageLose.SetActive(true);
            houseValue.text = "00/100";
            Time.timeScale = 0;
        }

    }

    public void SelectPeople(GameObject but)
    {
        GameObject[] arrayChoice = { };
        if (but.tag == "Player")
            arrayChoice = familyPerson;
        else if (but.tag == "EmptyPlace")
            arrayChoice = placeRest;

        PlayersInWindows(but, arrayChoice);
    }

    private void PlayersInWindows(GameObject hit, GameObject[] arrayName)
    {
        for (int i = 0; i < arrayName.Length; i++)
        {
            if (hit.name == arrayName[i].name )
            {
                print("im the "+ hit.name+ " and this is "+ arrayName[i].name);
                GetComponent<AudioSource>().PlayOneShot(som);
                if (!selected)
                {
                    print("yes");
                    selected = true;
                    PlayerSelected(arrayName[i]);
                }

                else if (selected)
                {
                    print("no");
                    selected = false;
                    ChangePosition(arrayName[i]);
                }
            }

        }
    }

    public void PlayerSelected(GameObject choicePerson)
    {       
        highlight.SetActive(true);
        temporaryPosition = choicePerson.transform.position;
        highlight.transform.position = temporaryPosition;
        temporaryRotation = choicePerson.transform.rotation;
        temp = choicePerson;
        if ((ActualState == States.firstStep && choicePerson.tag == "Player") || (ActualState == States.thirdStep && choicePerson.tag == "Player"))
            ActualState++;
    }

   
    public void ChangePosition( GameObject changePerson)
    {
        highlight.SetActive(false);
        temp.transform.position = changePerson.transform.position;
        temp.transform.rotation = changePerson.transform.rotation;
        changePerson.transform.position = temporaryPosition;
        changePerson.transform.rotation = temporaryRotation;
        LightHelp(changePerson, temp);
        if ((ActualState== States.secondStep && changePerson.tag == "Player") || (ActualState == States.fourthStep && changePerson.tag == "EmptyPlace")) {
            ActualState = States.great;
        }
    }

    private void LightHelp(GameObject person, GameObject otherPerson)
    { 
        for (int ii = 0; ii< helpLight.Length; ii++) {
            if ((otherPerson.tag == "Player" && otherPerson.transform.position ==helpLight[ii].transform.position) 
            || (person.tag =="Player" && person.transform.position == helpLight[ii].transform.position))
            {
                helpLight[ii].SetActive(true);
            }
            else if((otherPerson.tag == "EmptyPlace" && otherPerson.transform.position == helpLight[ii].transform.position) 
            ||(person.tag ==  "EmptyPlace" && person.transform.position == helpLight[ii].transform.position))
            {
                helpLight[ii].SetActive(false);
               
            }

        } 
    }

}
