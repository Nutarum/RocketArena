using UnityEngine.Networking;
using UnityEngine;

public class SyncVariablesController : NetworkBehaviour {
    

    [SyncVar]
    internal float radiomapa = Constants.STARTING_MAP_RADIUS;

    [SyncVar]
    internal float shopTimer = Constants.SHOP_TIME;

    // Use this for initialization
    void Start () {
		
	}    
	
	// Update is called once per frame
	void Update () {
        if (isServer) {
            float radiomapreducido = Time.deltaTime / 8; 
            radiomapa -= radiomapreducido;
        }
        if (isServer) {
            shopTimer -= Time.deltaTime;
        }
    }
}
