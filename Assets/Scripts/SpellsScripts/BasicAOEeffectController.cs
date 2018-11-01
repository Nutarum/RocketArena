
using System;
using UnityEngine;
using UnityEngine.Networking;

public class BasicAOEeffectController : NetworkBehaviour {

    float lifeTime = 0.5f;
    float time;
    // Use this for initialization
    void Start () {
        time = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > time + lifeTime) {
            Destroy(this.gameObject);
        }
    }
    void setScale(float scaleValue) {

    }
    [ClientRpc]
    internal void RpcsetStartParams(float calculatebasicAOERadius) {
        transform.localScale = new Vector3(calculatebasicAOERadius * 2, 0.1f, calculatebasicAOERadius * 2);
    }
}
