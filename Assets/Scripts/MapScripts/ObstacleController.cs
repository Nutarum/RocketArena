using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObstacleController : NetworkBehaviour {
    public GameObject explosion;
    public GameObject basicAOE;
    public GameObject textoDMG;

    float hp = Constants.OBSTACLE_HP;

    internal float positionY;
	// Use this for initialization
	void Start () {
        positionY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // ahora mismo esto solo recibe daño de colision con bola de fuego, por lo que solo se ejecutara esta funcion desde el server
    public void takeDamage(float dmg) {
        hp -= dmg;
        if (hp < 0) {
            explotar();
        }

        //no hago math abs porque aqui no se pueden curar las columnas (no hay daños negativos)
        if (dmg >= 1) {
            GameObject textoDaño = Instantiate(textoDMG, new Vector3(transform.position.x, (int)dmg, transform.position.z), Quaternion.identity);
            NetworkServer.Spawn(textoDaño);
        }        
    }

   
    //EJECUTADO SOLO POR SERVER
    public void explotar() {
        //busca jugadores en rango de explosion para meterles la ostia
        GameObject[] objectsn = GameObject.FindGameObjectsWithTag("player"); //get all objects of the same type as this players
        for (var f = 0; f < objectsn.Length; f++) //filter the objects that don't match
        {        
                Vector3 dif = objectsn[f].transform.position - transform.position;
                if (dif.magnitude < Constants.OBSTACLE_EXPLOSION_AOE) {
                    objectsn[f].GetComponent<PlayerController>().CmdClientRecibeGolpe(dif.normalized, Constants.OBSTACLE_EXPLOSION_DAMAGE);
                }            
        }

        //crea el efecto visual del area roja para todos los jugadores
        GameObject nuevoAOE = Instantiate(basicAOE, new Vector3(transform.position.x, -1.07f, transform.position.z), Quaternion.identity);
        NetworkServer.Spawn(nuevoAOE);
        nuevoAOE.GetComponent<BasicAOEeffectController>().RpcsetStartParams(Constants.OBSTACLE_EXPLOSION_AOE);

        RpcExplotar();
    }

    // desactivamos la columna para todos los jugadores
    [ClientRpc]
    void RpcExplotar() {    
        GameObject nuevaExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        nuevaExplosion.transform.localScale *= Constants.OBSTACLE_EXPLOSION_AOE/2;
        transform.position = transform.position - new Vector3(0,100,0);
    }

    //reseteamos la columna (SERVIDOR)
    public void reset() {
        hp = Constants.OBSTACLE_HP;
        RpcReset();
    }

    //reseteamos la columna para todos los jugadores
    [ClientRpc]
    void RpcReset() {
        transform.position = new Vector3(transform.position.x,positionY,transform.position.z);
    }
}
