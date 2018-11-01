using UnityEngine;

public class SueloController : MonoBehaviour {

    public SyncVariablesController syncVarController;
    public GameObject neblina;

    GameObject nuevaNeblina;

    ParticleSystem.ShapeModule editableShape;
    ParticleSystem.EmissionModule editableEmission;

    float textureScaleInicial;
    float faseNeblina;
    // Use this for initialization 
    void Start () {
        textureScaleInicial = GetComponent<Renderer>().material.mainTextureScale.x;
        restartFaseNeblina();
    }
    
    bool reseted = false;
    public void restartFaseNeblina() {
        faseNeblina = Constants.STARTING_MAP_RADIUS;

        GameObject[] neblinas;
        neblinas = GameObject.FindGameObjectsWithTag("neblina");
        foreach (GameObject neb in neblinas) {
            Destroy(neb);
        }

        reseted = true;
    }
	
	// Update is called once per frame
	void Update () {

        // EL DAÑO DE LA LAVA ESTA EN LA CLASE PlayerController (carpeta playerScripts)
        // PARA CAMBIAR LA VELOCIDAD A LA QUE SE REDUCE EL MAPA MIRAR EN SyncVariablesController (carpeta OtherScripts)

        if (syncVarController.radiomapa > 0) {
            if (syncVarController.radiomapa < faseNeblina ) {

                if (reseted && syncVarController.radiomapa > Constants.STARTING_MAP_RADIUS-2.5f) {
                    reseted = false;
                }
                if (!reseted) {
                    nuevaNeblina = Instantiate(neblina);
                    nuevaNeblina.tag = "neblina";
                    editableShape = nuevaNeblina.GetComponent<ParticleSystem>().shape;
                    editableEmission = nuevaNeblina.GetComponent<ParticleSystem>().emission;
                    editableShape.radius = faseNeblina / 5;

                    editableEmission.rateOverTime = faseNeblina * 5;

                    faseNeblina -= 2.5f;
                }
               

            }

        transform.localScale = new Vector3(syncVarController.radiomapa * 2, 0.1f, syncVarController.radiomapa * 2);

        //EL NUMERO A DIVIDIR ES EL TILTING DEL MATERIAL * 2 (4*2=8)
        GetComponent<Renderer>().material.mainTextureScale = new Vector2(textureScaleInicial - Mathf.Abs(Constants.STARTING_MAP_RADIUS - syncVarController.radiomapa) / 8, textureScaleInicial - Mathf.Abs(Constants.STARTING_MAP_RADIUS - syncVarController.radiomapa) / 8);
        GetComponent<Renderer>().material.mainTextureOffset = -0.5f * GetComponent<Renderer>().material.mainTextureScale;
        }
    }
}
