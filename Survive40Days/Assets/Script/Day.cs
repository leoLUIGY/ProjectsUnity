using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day : MonoBehaviour
{
    public int hour;
    public static int day;
    public Text dayText;
    public Text FPS;
    private float deltaTime;
    public GameObject rain;

    // Update is called once per frame
    void Update()
    {
        if (!Tutorial.tutorial)
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            FPS.text = Mathf.Ceil(fps).ToString();
            hour = Mathf.CeilToInt(Time.time);
            print(hour);
            if (hour % 30 == 0) Rain();
            if (hour != 0 && hour%180 == 0) StartCoroutine(Days());
        }
    }

    void Rain() {
        if (Random.Range(0f, 1f) > 0.5f) rain.SetActive(true);
        else rain.SetActive(false);
    }

    IEnumerator Days(){
        yield return new WaitForSeconds(1);
        day++;
        dayText.text = day.ToString();
        StopAllCoroutines();
    }
}
