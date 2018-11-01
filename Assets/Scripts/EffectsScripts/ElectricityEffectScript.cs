using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityEffectScript : MonoBehaviour {

    public void Start() {

    }

    public void Update() {

    }

    public void setEnabled(bool b) {
        if (b) {
            GetComponent<ParticleSystem>().Play();
        }
        else {
            GetComponent<ParticleSystem>().Stop();
        }
    }
}
