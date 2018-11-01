using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public GameObject deathAnimation;

    SyncVariablesController syncVarController;

    private float hpbarmaxsize;
    public RectTransform hpbar;
    public GameObject hpbarCanvas;

    public GameObject textoDMG;

    public float maxhp = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public float hp;

    internal float updatedHpForClient;

    private void Awake() {
        hpbarmaxsize = hpbar.sizeDelta.x;
    }

    // Use this for initialization
    void Start() {               
        syncVarController = GameObject.Find("SyncVariables").GetComponent<SyncVariablesController>();
        hpbarCanvas.transform.LookAt(Camera.main.transform);        
    }

    public override void OnStartServer() {
        TakeDamage(-maxhp);
    }

    public override void OnStartClient() {
        OnChangeHealth(hp);
    }
    
    // Update is called once per frame
    void Update() {
        if (isServer) {
            //Esto es el daño que nos hace la lava mientras estamos sobre ella
            if (Vector3.Distance(transform.position, new Vector3(0, 0, 0)) > syncVarController.radiomapa) {
                TakeDamage(8.5f * Time.deltaTime);
            }
        }     
    }
    
    //Ejecutada en el update de playerinputcontroller (para que se haga despues de girar al jugador, si es localplayer)
    public void updateHpbarRotation() {        
            hpbarCanvas.transform.LookAt(Camera.main.transform);
            hpbarCanvas.transform.rotation = Quaternion.Euler(hpbarCanvas.transform.rotation.eulerAngles.x, 180, hpbarCanvas.transform.rotation.eulerAngles.z);
                 }

    [Command]
    public void CmdTakeDamage(float amount) {
        TakeDamage(amount);
    }

    public void TakeDamage(float amount) {
        if (!isServer) {
            return;
        }
        if (GetComponent<PlayerInputController>().inMetallize > 0) {
            amount *= GetComponent<PlayerInputController>().metallizeDamageMod;
        }
        hp -= amount;
        if (hp > maxhp) {
            hp = maxhp;            
        }
        if (Math.Abs(amount) >= 1) {
            GameObject textoDaño = Instantiate(textoDMG, new Vector3(transform.position.x, (int)amount, transform.position.z), Quaternion.identity);
            NetworkServer.Spawn(textoDaño);
        }        
    }


    void OnChangeHealth(float currentHealth) {
        float unidad = hpbarmaxsize / 100;
        hpbar.sizeDelta = new Vector2(unidad * currentHealth, hpbar.sizeDelta.y);

        updatedHpForClient = currentHealth;


        if (currentHealth <= 0) {
            if (transform.position.y < 900) {
                
                Instantiate(deathAnimation, transform.position, Quaternion.identity);
                transform.position += new Vector3(0, 1000, 0);
            }               
            GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        //este elseif evita un bug gordo, despues de morir los jugadores solo "revivian" en su pantalla, para el resto eran invisibles, y recibian el daño de estar lejos del centro
        }else if (transform.position.y > 900) {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
    [ClientRpc]
    public void RpcConfusionKnockback(float damage) {        
        Vector3 forceDir = new Vector3(UnityEngine.Random.Range(-100, 100), 0, UnityEngine.Random.Range(-100, 100)).normalized;

        if (damage >= 0) {
            GetComponent<Rigidbody>().velocity += (forceDir * Constants.CONFUSIONSHOT_INCREASED_KNOCKBACK_MODIFIER);
        }

        TakeDamage(damage);
    }


    [ClientRpc]
    public void RpcRecibeGolpe(Vector3 forceDir, float damage) {

        float fuerza = (10 * (float)(Math.Log(3 + damage)))-5;
        if (fuerza < 0) {
            fuerza = 0;
        }
      
        if(damage>=0) {
            GetComponent<Rigidbody>().velocity += (forceDir * fuerza);
        }
    
        TakeDamage(damage);
            
    }

 
    [Command]
    public void CmdClientRecibeGolpe(Vector3 forceDir, float damage) {
        RpcRecibeGolpe(forceDir, damage);
    }

    [Command]
    public void CmdTargetClientRecibeGolpe(String name, Vector3 forceDir, float damage) {
        GameObject.Find(name).GetComponent<PlayerController>().RpcRecibeGolpe(forceDir, damage);      
    }
}
