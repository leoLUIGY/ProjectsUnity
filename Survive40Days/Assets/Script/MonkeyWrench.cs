using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyWrench: MonoBehaviour
{
    [SerializeField] int vel, damage;

    private void Update() {
        transform.GetChild(0).Rotate(0, 0, Time.deltaTime * 1000, Space.Self);
        transform.Translate(Vector3.right * vel * Time.deltaTime);
    }

    public IEnumerator Turn() {
        transform.localPosition = new Vector3(0,1,7);
        vel = -Mathf.Abs(vel);
        yield return new WaitForSeconds(0.2f);
        vel *= -1;
        yield return new WaitForSeconds(0.2f);
        vel *= -1;
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Enemy")) other.GetComponent<Enemies>().TakeDamage(damage);
    }
}
