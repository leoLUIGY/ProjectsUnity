using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw:MonoBehaviour {
    [SerializeField] GameObject throwable;
    [SerializeField] int vel;
    [SerializeField] float y, z;

    void NewAttack() {
        throwable.transform.localPosition = new Vector3(0, y, z);
        throwable.SetActive(true);
        StartCoroutine(Deactive());
    }

    IEnumerator Deactive() {
        yield return new WaitForSeconds(0.2f);
        throwable.SetActive(false);
    }

    void Update() {
        if (throwable.activeSelf) throwable.transform.Translate(Vector3.forward * vel * Time.deltaTime);
        if (!gameObject.GetComponent<Animator>().GetBool("attack")) throwable.SetActive(false);
    }
}
