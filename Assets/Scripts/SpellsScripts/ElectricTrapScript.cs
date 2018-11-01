using System;
using UnityEngine;
using UnityEngine.Networking;

public class ElectricTrapScript : NetworkBehaviour {
    
    String netid = "";
    bool ownerSetup = false; 

    float electricTrapDamageMod;
    float electricTrapDurationMod;

    // Use this for initialization
    void Start () {
        GetComponent<Renderer>().enabled = false;      
    }
	
	// Update is called once per frame
	void Update () {
        if (netid.Length > 0 && !ownerSetup) {
            ownerSetup = true;
            GameObject owner = GameObject.Find("Player(Clone)" + netid);
            if (owner.GetComponent<PlayerInputController>().isLocalPlayer) {
                GetComponent<Renderer>().enabled = true;
            }
        }       
    }

    void OnTriggerEnter(Collider other) {
        //Para que la bola no choque con nosotros mismos
        if (other.name.Contains("Player(Clone)" + netid)) {
            return;
        }
        if (other.name.Contains("Player")) {
            other.GetComponent<PlayerInputController>().CmdsetElectrified(Constants.ELECTRIC_TRAP_DURATION * electricTrapDurationMod, Constants.ELECTRIC_TRAP_DAMAGE * electricTrapDamageMod);
            Destroy(this.gameObject);
        }

    }

    [ClientRpc]
    public void RpcsetStartParams(NetworkInstanceId nid, float electricTrapDamageMod, float electricTrapDurationMod, float electricTrapAreaMod) {
        netid = nid.ToString();

        transform.localScale *= electricTrapAreaMod;

        this.electricTrapDamageMod = electricTrapDamageMod;
        this.electricTrapDurationMod = electricTrapDurationMod;

    }

}
