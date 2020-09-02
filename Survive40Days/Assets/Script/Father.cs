using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father : MonoBehaviour
{
    [SerializeField] private GameObject monkeyWrench;

    void NewAttack() {
        monkeyWrench.SetActive(true);
        StartCoroutine(monkeyWrench.GetComponent<MonkeyWrench>().Turn());
    }
}
