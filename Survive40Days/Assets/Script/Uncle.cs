using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uncle : MonoBehaviour{
    private void Update() {
        if (gameObject.GetComponentInParent<Animator>().GetBool("health")) {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Enemy")) {
            other.GetComponent<Enemies>().HalfSpeed();
            gameObject.GetComponentInParent<Animator>().SetBool("attack", true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag.Equals("Enemy")) {
            other.GetComponent<Enemies>().NormalSpeed();
            gameObject.GetComponentInParent<Animator>().SetBool("attack", false);
        }
    }
}