using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPlace;
    [SerializeField] private GameObject[] enemies;

    private void Start() {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() {
        yield return new WaitForSeconds(Random.Range(5f, 6f));
        if (!Tutorial.tutorial)
        {
            int enemie;
            if (Day.day < 3) {
                enemie = 0;
            }
            else {
                enemie = Random.Range(0, enemies.Length);
            }
            int place = Random.Range(0, spawnPlace.Length);
            GameObject clone = Instantiate(enemies[enemie], spawnPlace[place].transform.position, spawnPlace[place].transform.rotation);
        }
        StartCoroutine(Spawn());
    }
}
