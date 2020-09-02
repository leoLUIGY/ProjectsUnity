using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 vel;

    void Update(){
        transform.Rotate(Time.deltaTime * vel, Space.Self);
    }

}
