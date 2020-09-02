using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandmother : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject stick;
    private GameObject attackPoint;
    void Start()
    {
        attackPoint = transform.GetChild(0).gameObject;
    }

    void Attack() {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.transform.position, 1.2f);
        foreach (Collider enemy in hitEnemies) {
            if (enemy.gameObject.tag.Equals("Enemy")) enemy.GetComponent<Enemies>().TakeDamage(damage);
        }
    }

    void StickActive() {
        stick.SetActive(true);
    }

    void StickDeactive() {
        stick.SetActive(false);
    }
}
