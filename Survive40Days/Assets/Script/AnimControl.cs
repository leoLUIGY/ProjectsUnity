using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour{
    private void Update() {
        if (gameObject.GetComponentInParent<Animator>().GetBool("health")) {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
    private void OnTriggerStay(Collider other) {
        if (other.tag.Equals("Enemy")) gameObject.GetComponentInParent<Animator>().SetBool("attack", true);
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag.Equals("Enemy")) gameObject.GetComponentInParent<Animator>().SetBool("attack", false);
    }
}
