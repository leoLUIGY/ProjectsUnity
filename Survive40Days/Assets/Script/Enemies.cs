using UnityEngine;
using System.Collections;

public class Enemies : MonoBehaviour {
    [SerializeField] private int attack, life;
    [SerializeField] private float vel;
    private Animator anim;
    private bool inWindow = false;
    private float timeDuration;
    private void Awake() {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }
    void Update() { 

        if (life <= 0) {
            GetComponent<CapsuleCollider>().center = new Vector3(90,90,90);
            StartCoroutine(Die());
        }
        transform.Translate(Vector3.right * -vel * Time.deltaTime);

        if (inWindow)
            DamageToHouse();
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Wall")) {
            inWindow = true;
            vel = 0;
            anim.SetBool("isAttack", true);
        }
    }
    public void TakeDamage(int damage) {
        life -= damage;
    }

    public void DamageToHouse() {
        timeDuration += Time.fixedTime;
        if (timeDuration >= 0.2f)
            FamilyControl.houseLife--;
            timeDuration = 0;
    }

    public void HalfSpeed() {
        vel /= 2;
    }

    public void NormalSpeed() {
        vel *= 2;
    }
}
