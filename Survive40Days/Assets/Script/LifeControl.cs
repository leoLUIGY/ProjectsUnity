using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeControl : MonoBehaviour
{
    public Image barraVidaUi;
    [SerializeField] private float maxLife = 100;
    [SerializeField] private float actualLife;
    [SerializeField] private GameObject[] enemy;
    [SerializeField]FamilyControl family;
    [SerializeField] private Text lifeValue;
    private bool isDead = false;
    private int myIndex;
    private int valueFor = 0;
    private float timeDuration; 

    void Start()
    {
        maxLife = 100;
        actualLife = maxLife;
        family = GetComponentInParent<FamilyControl>();
        StartCoroutine(HelpSeconds());
       
    }

    private void LateUpdate() {
        for (int v = 0; v < 3; v++) {
            if (transform.position == family.helpLight[v].transform.position) {
                gameObject.GetComponent<Animator>().SetBool("health", true);
                break;
            }
            else {
                gameObject.GetComponent<Animator>().SetBool("health", false);
            }

        }
    }
    void Update()
    {
        timeDuration += Time.fixedDeltaTime;
        barraVidaUi.rectTransform.sizeDelta = new Vector2(actualLife / maxLife * 50, 5);
        lifeValue.text = actualLife + "/" + maxLife.ToString();
        if (actualLife <= 0 && isDead == false)
        {
            isDead = true;
            FindValueToIndex();
           
        }

    }


    private void MovePlayerToRest()
    {
        FindMyIndex();

        family.PlayerSelected(family.placeRest[valueFor]);
        family.ChangePosition(family.familyPerson[myIndex]);
        myIndex = 0;

    }


    private void FindMyIndex()
    {
        for (int ii = 0; ii < family.familyPerson.Length; ii++)
        {
            if (gameObject.name != family.familyPerson[ii].name)
                myIndex++;
            else
                break;
        }

    }


    private void FindValueToIndex()
    {
        print("this is valueFor " + valueFor);

        for (int j = 0; j <=2; j++)
        {
            for (int i = 0; i < 6; i++)
            {
                if (family.familyPerson[i].transform.position != family.helpLight[j].transform.position)
                {
                    if (i == 5)
                    {
                        IndexOfRestPlace();
                        break;
                    }
                }
                else
                {
                    i = 6;
                }
            }
        }
        if (valueFor > 2 || valueFor <= 0)
            valueFor = 0;
        MovePlayerToRest();

    }

    private void IndexOfRestPlace()
    {
        for (int vv = 0; vv < 3; vv++)
        {
            for (int v = 0; v < family.placeRest.Length; v++)
            {
                if (family.placeRest[v].transform.position == family.helpLight[vv].transform.position)
                {
                    valueFor = v;
                    break;
                }
            }
        }
    }

    IEnumerator HelpSeconds()
    {
        if (actualLife >= maxLife)
            actualLife = maxLife;
       
        for (int v = 0; v < 3; v++)
        {
            if (transform.position == family.helpLight[v].transform.position )
            {
                actualLife++;
                isDead = true;
                break;
            }
            else {
                isDead = false;
            }

        }
        if (timeDuration >= Random.Range(0.5f, 1f) && !isDead && gameObject.GetComponent<Animator>().GetBool("attack") || Tutorial.testAtack)
        {
            actualLife--;
            timeDuration = 0;
        }
      
        yield return new WaitForSeconds(3);
        StartCoroutine(HelpSeconds());
    }
}
