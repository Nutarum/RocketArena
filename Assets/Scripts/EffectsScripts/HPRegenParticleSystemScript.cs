﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRegenParticleSystemScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setEnabled(bool b) {
        if (b) {
            GetComponent<ParticleSystem>().Play();
        } else {
            GetComponent<ParticleSystem>().Stop();
        }       
    }
}
