using System;
using UnityEngine;
using UnityEngine.Networking;

public class FireballScript : NetworkBehaviour {

    public GameObject explosion;

    float time;
    float lifeTime = Constants.FIREBALL_BASIC_RANGE;
    public String netid = "";

    float fbdamage;
    float fblifesteal;

    // Use this for initialization
    void Start() {
        time = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (Time.time > time + lifeTime) {
            Destroy(this.gameObject);
        }
    }

    [ClientRpc]
    public void RpcExplotar(float scale) {
        GameObject nuevaExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        nuevaExplosion.transform.localScale *= scale;
        Destroy(this.gameObject);
    }

    [ClientRpc]
    private void RpcReflectorHit(String netidOwner) {
        GetComponent<Rigidbody>().velocity *= -1;
        netid = netidOwner;
    }

    void OnTriggerEnter(Collider other) {
        if (!isServer) {
            return;
        }
        //Para que la bola no choque con nosotros mismos
        if (other.name.Contains("Player(Clone)" + netid)) {
            return;
        }
        if (other.name.Contains("Player")) {         
                Vector3 forceDir = (transform.GetComponent<Rigidbody>().velocity).normalized;
                other.GetComponent<PlayerController>().RpcRecibeGolpe(forceDir, fbdamage);
                if (fblifesteal > 0) {
                    GameObject.Find("Player(Clone)" + netid).GetComponent<PlayerController>().TakeDamage(-(fbdamage * fblifesteal));
                }
            RpcExplotar(transform.localScale.x);                   
        }

        if (other.name.Contains("FireBall")) {
            if (other.GetComponent<FireballScript>().netid != netid) {

                float sizeOther = other.GetComponent<FireballScript>().transform.localScale.x;

                //SI LA BOLA DE FUEGO ES DE TAMAÑO > 1.9 no es destruida
                //si las 2 son pequeñas
                if (sizeOther < 1.9 && transform.localScale.x < 1.9) {
                    other.GetComponent<FireballScript>().RpcExplotar(transform.localScale.x);
                    RpcExplotar(transform.localScale.x);
                    //si la otra es pequeña (y las 2 no son pequeñas) (la nuestra es grande)
                }
                else if (sizeOther <= 1.9) {
                    other.GetComponent<FireballScript>().RpcExplotar(transform.localScale.x);
                    //si ni las 2 son pequeñas ni la otra es pequeña (la otra es grande)
                }
                else {
                    RpcExplotar(transform.localScale.x);
                }
            }
        }

        if (other.name.Contains("ConfusionShot")) {


            if (other.GetComponent<ConfusionShotScript>().netid != netid) {

                float sizeOther = other.GetComponent<ConfusionShotScript>().transform.localScale.x;

                //SI LA BOLA DE FUEGO ES DE TAMAÑO > 1.9 no es destruida
                //si las 2 son pequeñas
                if (sizeOther < 1.9 && transform.localScale.x < 1.9) {
                    other.GetComponent<ConfusionShotScript>().RpcExplotar(transform.localScale.x);
                    RpcExplotar(transform.localScale.x);
                    //si la otra es pequeña (y las 2 no son pequeñas) (la nuestra es grande)
                }
                else if (sizeOther <= 1.9) {
                    other.GetComponent<ConfusionShotScript>().RpcExplotar(transform.localScale.x);
                    //si ni las 2 son pequeñas ni la otra es pequeña (la otra es grande)
                }
                else {
                    RpcExplotar(transform.localScale.x);
                }
            }
        }

        if (other.name.Contains("obstacle")) {
                other.GetComponent<ObstacleController>().takeDamage(fbdamage);
                RpcExplotar(transform.localScale.x);          
        }

        if (other.name.Contains("Reflector")) {
            PlayerInputController reflectorOwner = other.GetComponentInParent<PlayerInputController>();
            if (!reflectorOwner.name.Equals("Player(Clone)" + netid) && reflectorOwner.inReflector > 0) {              
                    RpcReflectorHit(reflectorOwner.netId + "");
                    if (reflectorOwner.reflectorBreakMod) {
                        reflectorOwner.CmdActivateReflector(false, -10, false);
                    }                 
            }         
        }
    }

    [ClientRpc]
    public void RpcsetStartParams(NetworkInstanceId nid, float fbdamagemod, float fbrangemod, float fbsizemod, float fblifesteal) {
        netid = nid.ToString();
        lifeTime *= fbrangemod;

        transform.localScale *= fbsizemod;

        this.fbdamage = fbdamagemod;        
        this.fblifesteal = fblifesteal;

    }
    
}
