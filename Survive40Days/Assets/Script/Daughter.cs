using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daughter : MonoBehaviour
{
    [SerializeField] private GameObject bear;
    // Start is called before the first frame update

    private void Update() {
        if (!gameObject.GetComponent<Animator>().GetBool("attack")) {
            bear.SetActive(false);
        }
    }

    void Attack() {
        bear.transform.localPosition = new Vector3(0, 3, 5.5f);
        bear.SetActive(true);
        StartCoroutine(EndAttack());
    }
    IEnumerator EndAttack() {
        if (gameObject.GetComponent<Animator>().GetBool("attack") || Tutorial.testAtack) {
            yield return new WaitForSeconds(1.2f);
            bear.SetActive(false);
        }
    }
}
