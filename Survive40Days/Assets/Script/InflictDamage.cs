using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictDamage : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Enemy")) other.GetComponent<Enemies>().TakeDamage(damage);
    }
}
