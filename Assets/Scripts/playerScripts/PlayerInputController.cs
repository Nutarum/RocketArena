using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerInputController : NetworkBehaviour {

    #region DECLARACION DE VARIABLES

    [SyncVar]
    int rounds_won;


    int roundNumber;

    public Animator anim;

    private InputController inputController;
    private SyncVariablesController syncVarController;

    private GameObject player;
    private LineRenderer controllerLineRenderer;

    private Text M1Text;
    private Text M2Text;
    private Text SpaceText;
    private Text eText;
    private Text rText;
    private Text qText;
    private Text fText;

    private Text item1Text;

    private Text timerText;

    private Text statisticsText;
    internal int[] spells;
    internal int[] spellLevels;

    internal float fbdamagemod;
    internal float fbrangemod;
    internal float fbsizemod;
    internal float fblifesteal;
    internal bool fbdouble ;
    internal float fbcastTimeMod;

    internal float basicAOEradiusMOD;
    internal float basicAOECastingTimeMOD;
    internal float basicAOEDamageMOD;
    internal float basicAOERecivedDamageMOD;

    internal float teleportRangeMod ;
    internal float teleportCDMod ;
    internal bool teleportModDouble;
    internal float doubleTPController;

    internal float rushCDMod;
    internal float rushForceMod;
    internal float rushDamageMod ;
    internal Boolean directedRush;
    internal float inRush ;


    internal float reflectorCDMod ;
    internal float reflectorDurationMod;
    internal bool reflectorInvisibleMod;
    internal float reflectorMoveSpeed ;
    internal bool reflectorBreakMod;
    internal float inReflector;

    [Command]
    public void CmdSetReflectorBreakMod(Boolean b) {
        reflectorBreakMod = b;
    }

    internal float metallizeCDMod;
    internal float metallizeMovespeedMod ; //THE HIGHER, THE FASTER
    internal float metallizeDamageMod;
    internal float metallizeDurationMod;
    internal float metallizeDragMod ;
    internal float inMetallize;

    internal bool doubleBarrage;
    internal bool isInDoubleBarrage;
    internal float barrageLifeStealMod;
    internal float barrageNumberOfShotsMod;
    internal float missileBarrageCastingTimeMod;
    internal float barragedamagemod;
    internal float barrageRangemod;
    internal float barrageCDmod;
    internal Vector3 barrageStart = new Vector3();
    internal Vector3 barrageEnd = new Vector3();
    internal Vector3 barrageDir = new Vector3();

    internal float nextBarrageShot;

    internal float electricTrapDurationMod;
    internal float electricTrapAreaMod;
    internal float electricTrapDamageMod;
    internal float electricTrapCastingTimeMod;

    internal float isElectrified ;
    internal float electrifyDamage;
    internal float electrifyDuration;

    internal float timeBeaconDurationMod;
    internal float timeBeaconCDMod;
    internal bool timeBeaconThrowMod ;
    internal bool timeBeaconReduceAllCDMod ;
    internal bool timeBeaconSpeedMod;

    internal float timeBeaconX;
    internal float timeBeaconZ;
    internal float timeBeaconDuration ;
    internal float timeBeaconSpeedTime;
    internal float timeBeaconStartingHP;

    internal float confusionShotDurationMod;
    internal float confusionShotCDMod;
    internal float confusionShotDamageMod;
    internal float confusionShotRangeMod;
    internal bool confusionShotRandomKnockback;
    internal float inConfusion;

    internal int stimpakCharges;
    internal float stimpakTime ;

    bool inGameRound = false;

    internal int skillPoints;


    //NECESARIOS PARA CARGAR LOS PREFABS DESDE EL EDITOR
    public GameObject bolaFuego;
    public GameObject confusionShot;
    public GameObject electricTrap;
    public GameObject basicAOE;
    // public GameObject reflector;

    public GameObject explosion;

    Collider terrainCollider;

    public RectTransform castbar;
    public GameObject castbarCanvas;

    private float castbarmaxsize;

    float castingTime;
    //1 fireball
    public int castingType;


    private float m1SkillCD;
    private float m2SkillCD;
    private float spaceSkillCD;
    private float eSkillCD;
    private float rSkillCD;
    private float qSkillCD;
    private float fSkillCD;

    private float cameraX;
    private float cameraY;
    private float cameraZ;

    private float cameraHeight;
    private float checkEndTime;
    #endregion

    #region RESET ALL VALUES TO INITIAL
    [ClientRpc]
    public void RpcInitializeAllValues() {
        initializeAllValues();
    }
    private void initializeAllValues() {
            if (isServer && isLocalPlayer) {
                syncVarController.shopTimer = Constants.SHOP_TIME;
            }
            rounds_won = 0;
            roundNumber = 1;
            //M1 M2 SPACE Q E        
            spells = new int[] { 0, 5, -1, -1, -1, -1, -1 };
       
            //                      //0              5              10             15             20             25             30            
            spellLevels = new int[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            fbdamagemod = 1;
            fbrangemod = 1;
            fbsizemod = 1;
            fblifesteal = 0;
            fbdouble = false;
            fbcastTimeMod = 1;

            basicAOEradiusMOD = 1;
            basicAOECastingTimeMOD = 1;
            basicAOEDamageMOD = 1;
            basicAOERecivedDamageMOD = 1;

            teleportRangeMod = 1;
            teleportCDMod = 1;
            teleportModDouble = false;
            doubleTPController = -1;

            rushCDMod = 1;
            rushForceMod = 1;
            rushDamageMod = 1;
            directedRush = false;
            inRush = 0;


            reflectorCDMod = 1;
            reflectorDurationMod = 1;
            reflectorInvisibleMod = false;
            reflectorMoveSpeed = 1;
            reflectorBreakMod = false;
            inReflector = -10;

            metallizeCDMod = 1;
            metallizeMovespeedMod = 0.3f; //THE HIGHER, THE FASTER
            metallizeDamageMod = 0.5f;
            metallizeDurationMod = 1f;
            metallizeDragMod = 1f;
            inMetallize = 0;

            doubleBarrage = false;
            isInDoubleBarrage = false;
            barrageLifeStealMod = 0;
            barrageNumberOfShotsMod = 10;
            missileBarrageCastingTimeMod = 1;
            barragedamagemod = 1;
            barrageRangemod = 1;
            barrageCDmod = 1;

            nextBarrageShot = -1;

            electricTrapDurationMod = 1;
            electricTrapAreaMod = 1;
            electricTrapDamageMod = 1;
            electricTrapCastingTimeMod = 1;

            isElectrified = 0;
            electrifyDamage = 0;
            electrifyDuration = 0;

            timeBeaconDurationMod = 1;
            timeBeaconCDMod = 1;
            timeBeaconThrowMod = false;
            timeBeaconReduceAllCDMod = false;
            timeBeaconSpeedMod = false;

            timeBeaconX = 0;
            timeBeaconZ = 0;
            timeBeaconDuration = -10;
            timeBeaconSpeedTime = -10;
            timeBeaconStartingHP = 0;

            confusionShotDurationMod = 1;
            confusionShotCDMod = 1;
            confusionShotDamageMod = 1;
            confusionShotRangeMod = 1;
            confusionShotRandomKnockback = false;
            inConfusion = 0;

             stimpakCharges = Constants.STIMPAK_STARTING_CHARGES;
            stimpakTime = 0f;

            inGameRound = false;

            skillPoints = Constants.STARTING_SKILLPOINTS;
            castingTime = 0;
            //1 fireball
            castingType = 0;
            m1SkillCD = 0;
            m2SkillCD = 0;
            spaceSkillCD = 0;
            eSkillCD = 0;
            rSkillCD = 0;
            qSkillCD = 0;
            fSkillCD = 0;

        //aqui vamos a meter algunos cambios globales que van a afectar a la UI, y no son solo variables de este personajo
        //por lo que si un cliente que no es el dueño lo ejecuta, podria cambiar cosas de la UI y joderlas
        if (isLocalPlayer) {
            
            item1Text.text = "" + stimpakCharges;      
            GameObject.Find("spellBarImageItem1").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ItemSprites/itemicon1Normal");
            M1Text.text = "";
            M2Text.text = "";
            SpaceText.text = "";
            qText.text = "";
            eText.text = "";
            rText.text = "";
            fText.text = "";
            GameObject.Find("spellBarImage0").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon1Normal");
            GameObject.Find("spellBarImage1").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon5Normal");
            GameObject.Find("spellBarImage2").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/sprite_interrogante");
            GameObject.Find("spellBarImage3").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/sprite_interrogante");
            GameObject.Find("spellBarImage4").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/sprite_interrogante");
            GameObject.Find("spellBarImage5").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/sprite_interrogante");
            GameObject.Find("spellBarImage6").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/sprite_interrogante");
            GameObject.Find("SpellShopCanvas").GetComponent<SpellShopController>().ShowSpellShop();

            GameObject.Find("SpellShopCanvas").GetComponent<SpellShopController>().spellShopImages[2].sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/sprite_interrogante");
            GameObject.Find("SpellShopCanvas").GetComponent<SpellShopController>().spellShopImages[3].sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/sprite_interrogante");
            GameObject.Find("SpellShopCanvas").GetComponent<SpellShopController>().spellShopImages[4].sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/sprite_interrogante");
            GameObject.Find("SpellShopCanvas").GetComponent<SpellShopController>().spellShopImages[5].sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/sprite_interrogante");
            GameObject.Find("SpellShopCanvas").GetComponent<SpellShopController>().spellShopImages[6].sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/sprite_interrogante");

        }
    }
    #endregion

    #region COLISIONES

    void OnTriggerEnter(Collider collider) {
        if (collider.name.Contains("Player")) {
            if (inRush > 0) {
                CmdCollisionRushPlayerPlayer(collider.name, name);
            }
            else {
                if (GetComponent<Rigidbody>().velocity.magnitude > Constants.ALLOW_MOVEMENT_SPEED) {
                    Vector3 normal = (transform.position - collider.transform.position);
                    GetComponent<Rigidbody>().velocity = Vector3.RotateTowards(GetComponent<Rigidbody>().velocity, normal, 100, 0.0f);
                }
            }
        }
        else if (collider.name.Contains("obstacle")) {
            if (GetComponent<Rigidbody>().velocity.magnitude > Constants.ALLOW_MOVEMENT_SPEED) {
                GetComponent<Rigidbody>().velocity = Utils.getBounce(transform.position, GetComponent<Rigidbody>().velocity, collider);
            }
        }
    }
    [Command]
    public void CmdCollisionRushPlayerPlayer(string name2, string name) {
        RpcCollisionRushPlayerPlayer(name2);
        GameObject.Find(name2).GetComponent<PlayerInputController>().RpcCollisionRushPlayerPlayer(name);
    }
    [ClientRpc]
    public void RpcCollisionRushPlayerPlayer(string name) {
        if (inRush > 0) {
            inRush = 0;
            GetComponent<PlayerController>().CmdTargetClientRecibeGolpe(name, transform.GetComponent<Rigidbody>().velocity.normalized, Constants.RUSH_DAMAGE * rushDamageMod);
            transform.GetComponent<Rigidbody>().velocity = new Vector3();
        }
    }


    private void OnTriggerStay(Collider collider) {

        if (collider.name.Contains("obstacle")) {
            float distance = 1.7f;
            transform.position = (transform.position - collider.GetComponent<Transform>().position).normalized * distance + collider.GetComponent<Transform>().position;
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (collider.name.Contains("Player")) {
            CmdCollisionStayPlayerPlayer(collider.name, name);
        }
    }
    [Command]
    public void CmdCollisionStayPlayerPlayer(string name2, string name) {
        RpcCollisionStayPlayerPlayer(name2);
        GameObject.Find(name2).GetComponent<PlayerInputController>().RpcCollisionStayPlayerPlayer(name);
    }
    [ClientRpc]
    public void RpcCollisionStayPlayerPlayer(string name) {
        float distance = 1.5f;
        Collider c = GameObject.Find(name).GetComponent<Collider>();
        transform.position = (transform.position - c.GetComponent<Transform>().position).normalized * distance + c.GetComponent<Transform>().position;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    #endregion

    #region UTILS 
    private void changeColor(String color) {
        Material[] mats;
        Renderer childRend = transform.Find("CuerpoTEST_ANIMACION_2/Cube").GetComponent<Renderer>();
        mats = childRend.materials;
        mats[0] = Resources.Load<Material>(color);
        childRend.materials = mats;
    }
    #endregion

    #region START AND UPDATE




    void Awake() {
        terrainCollider = GameObject.Find("Terrain").GetComponent<Collider>();
        tag = "player";
    }

    // Use this for initialization
    void Start() {
     
        GetComponentInChildren<HPRegenParticleSystemScript>().setEnabled(false);
        GetComponentInChildren<ElectricityEffectScript>().setEnabled(false);
        GetComponentInChildren<ConfusionParticleSystemScript>().setEnabled(false);

        this.name += GetComponent<NetworkIdentity>().netId.ToString();
        if (!isLocalPlayer) {
            changeColor("Colors/magenta");
            castbarCanvas.SetActive(false);
            return;
        }
        //Ocultamos el boton de cerrar partida (para que no se pueda pulsar tampoco sin querer con la X)
        GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>().showGUI = false;

        changeColor("Colors/blue");

        GameObject.Find("SpellShopCanvas").GetComponent<SpellShopController>().setPlayer(this);

        M1Text = GameObject.Find("M1Text").GetComponent<Text>();
        M2Text = GameObject.Find("M2Text").GetComponent<Text>();
        SpaceText = GameObject.Find("SpaceText").GetComponent<Text>();
        qText = GameObject.Find("qText").GetComponent<Text>();
        eText = GameObject.Find("eText").GetComponent<Text>();
        rText = GameObject.Find("rText").GetComponent<Text>();
        fText = GameObject.Find("fText").GetComponent<Text>();
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        item1Text = GameObject.Find("item1Text").GetComponent<Text>();

        statisticsText = GameObject.Find("StatisticsText").GetComponent<Text>();

        item1Text.text = "" + stimpakCharges;

        controllerLineRenderer = GameObject.Find("ControllerLine").GetComponent<LineRenderer>();

        inputController = GameObject.Find("InputController").GetComponent<InputController>();
        syncVarController = GameObject.Find("SyncVariables").GetComponent<SyncVariablesController>();
        initializeAllValues();


        spawnPosition();

        

        cameraX = Camera.main.transform.position.x;
        cameraY = Camera.main.transform.position.y;              

        cameraZ = (float)-(cameraY / Math.Tan((Math.PI / 180) * Camera.main.transform.rotation.eulerAngles.x));
        Camera.main.transform.position = new Vector3(cameraX, cameraY, cameraZ);
        
        castbarCanvas.transform.LookAt(Camera.main.transform);
        castbarmaxsize = castbar.sizeDelta.x;


        

        checkEndTime = Time.time + 2;
    }


    // Update is called once per frame
    void Update() {

        mirar();

        if (!isLocalPlayer) {
            return;
        }

        //zoom();
        mover();

        if (transform.position.y < 900) {
            HechizoEspacio();
            HechizoM1();
            HechizoM2();
            HechizoQ();
            HechizoE();
            HechizoR();
            HechizoF();
            Item1();
            mostrarMenuEstadisticas();
        }


        DebugBalancing();

        castingTime -= Time.deltaTime;

        castingAnimation();
        UIupdate();

        checkEndOfGame();

    }

    #endregion

    #region GAME CONTROLER (end of rounds, spawns...)

    private void checkEndOfGame() {
        if (!isServer) {
            return;
        }
        if (!inGameRound) {
            return;
        }
        if (Time.time > checkEndTime) {
            int contadorJugadoresVivos = 0;
            int contadorJugadoresMuertos = 0;
            GameObject[] objectsn = GameObject.FindGameObjectsWithTag("player"); //get all objects of the same type as this players
            for (var f = 0; f < objectsn.Length; f++) {
                if (objectsn[f].GetComponent<PlayerInputController>().transform.position.y < 100) {
                    contadorJugadoresVivos++;
                }
                else {
                    contadorJugadoresMuertos++;
                }
            }
            //Compruebo tambien que no haya muertos para que los juegos con 1 jugador no se reinicien solos
            if (contadorJugadoresVivos <= 1 && contadorJugadoresMuertos > 0) {
                EndofRound();



            }
            checkEndTime += 2;
        }
    }

    private void UIupdate() {
        if (syncVarController.shopTimer > 0) {
            timerText.text = "Round " + roundNumber + "/" + Constants.ROUND_LIMIT + " starting in: " + (int)syncVarController.shopTimer;
            if (roundNumber > Constants.ROUND_LIMIT) {
                timerText.text = "Game ended, restarting in: " + (int)syncVarController.shopTimer;
            }
        }
        else {
            timerText.text = "";
        }
        if (isServer && syncVarController.shopTimer < 0 && syncVarController.shopTimer > -10 && !inGameRound) {    
            //si se ha alcanzado el limite de rondas, el servidor recorre todos los players y pide sus reinicios
            if(roundNumber > Constants.ROUND_LIMIT) {
                GameObject[] listaPlayers = GameObject.FindGameObjectsWithTag("player"); //get all objects of the same type as this players
                for (var f = 0; f < listaPlayers.Length; f++) {
                    listaPlayers[f].GetComponent<PlayerInputController>().RpcInitializeAllValues();
                }
            }else {
                StartofRound();
            }
        }
    }


    [ClientRpc]
    public void RpcStartOfRound() {
        if (!isLocalPlayer) {
            return;
        }
        GameObject.Find("SpellShopCanvas").GetComponent<SpellShopController>().HideSpellShop();

        castingType = 0;
        castingTime = 0;

        m1SkillCD = 0.01f;
        m2SkillCD = 0.01f;
        if (spells[2] != -1)
            spaceSkillCD = 0.01f;
        if (spells[3] != -1)
            qSkillCD = 0.01f;
        if (spells[4] != -1)
            eSkillCD = 0.01f;
        if (spells[5] != -1)
            rSkillCD = 0.01f;
        if (spells[6] != -1)
            fSkillCD = 0.01f;

        timeBeaconSpeedTime = -10;
        timeBeaconDuration = -10;
        inReflector = 0;
        inMetallize = 0;
        inRush = 0;
        GetComponent<Rigidbody>().velocity = new Vector3();


        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects) {
            if (go.activeInHierarchy) {
                if (go.name.Contains("Trap")) {
                    Destroy(go);
                }
                else if (go.name.Contains("FireBall")) {
                    Destroy(go);
                }
                else if (go.name.Contains("basicAOE")) {
                    Destroy(go);
                }
            }
        }
    }
    [ClientRpc]
    public void RpcEndOfRound() {
        GameObject.Find("SpellShopCanvas").GetComponent<SpellShopController>().ShowSpellShop();
    }

    private void StartofRound() {
        if (!isServer)
            return;
        syncVarController.shopTimer = -10;
        inGameRound = true;

        //reseteamos a los jugadores
        GameObject[] listaPlayers = GameObject.FindGameObjectsWithTag("player"); //get all objects of the same type as this players
        for (var f = 0; f < listaPlayers.Length; f++) {
            listaPlayers[f].GetComponent<PlayerController>().hp = listaPlayers[f].GetComponent<PlayerController>().maxhp;
            listaPlayers[f].GetComponent<PlayerInputController>().RpcspawnPosition();
            listaPlayers[f].GetComponent<PlayerInputController>().RpcStartOfRound();
        }

        //reseteamos las columnas
        GameObject[] listaColumnas = GameObject.FindGameObjectsWithTag("obstacle"); //get all objects of the same type as this players
        for (var f = 0; f < listaColumnas.Length; f++) {
            listaColumnas[f].GetComponent<ObstacleController>().reset();
        }

        GameObject.Find("SyncVariables").GetComponent<SyncVariablesController>().radiomapa = 30;

    }

    private void EndofRound() {
        if (!isServer)
            return;
        GameObject[] objectsn = GameObject.FindGameObjectsWithTag("player"); //get all objects of the same type as this players
        for (var f = 0; f < objectsn.Length; f++) {
            
            if (objectsn[f].GetComponent<PlayerInputController>().transform.position.y < 100) {
                objectsn[f].GetComponent<PlayerInputController>().rounds_won++;
            }

            objectsn[f].GetComponent<PlayerController>().hp = objectsn[f].GetComponent<PlayerController>().maxhp;
            objectsn[f].GetComponent<PlayerInputController>().RpcGiveSpellpointsAndSpawn();
        }

        syncVarController.radiomapa = 30;
        syncVarController.shopTimer = Constants.SHOP_TIME;
        inGameRound = false;
        RpcEndOfRound();
    }

    [ClientRpc]
    private void RpcGiveSpellpointsAndSpawn() {
        if (isLocalPlayer) {
            roundNumber++;
            skillPoints += ((roundNumber-2)/Constants.INCREASE_SKILL_POINT_EARNED_EVERY_X_ROUNDS) +  Constants.SKILLPOINTS_PER_ROUND;
            spawnPosition();
        }

    }

    [ClientRpc]
    private void RpcspawnPosition() {
        if (isLocalPlayer) {
            spawnPosition();
            timerText.text = "";
        }
    }

    private void spawnPosition() {
        transform.position = (Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), Vector3.up) * Vector3.right).normalized * 20;
        GameObject.Find("suelo").GetComponent<SueloController>().restartFaseNeblina();
    }
    #endregion

    private int debugHitDmg = 0;
    private void DebugBalancing() {
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            if (isServer) {
                debugHitDmg--;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            if (isServer) {
                debugHitDmg++;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            if (isServer) {
                GetComponent<PlayerController>().CmdClientRecibeGolpe(transform.forward, debugHitDmg);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            if (isServer) {
                if (!inGameRound) {
                    syncVarController.shopTimer = 30;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            if (isServer) {
                if (!inGameRound) {
                    StartofRound();
                }
                else {
                    EndofRound();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F12)) {
            if (isServer) {
                EndofRound(); EndofRound(); EndofRound();
                EndofRound(); EndofRound(); EndofRound();
                EndofRound(); EndofRound(); EndofRound();
                syncVarController.shopTimer = 5;
            }            
        }

        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            inputController.usingController = !inputController.usingController;
            controllerLineRenderer.enabled = !controllerLineRenderer.enabled;
        }
    }

    private void castingAnimation() {
        if (castingType == 1) { //bola de fuego casting 0.5f
            float unidad = castbarmaxsize / (Constants.FIREBALL_CASTING_TIME * fbcastTimeMod);
            castbar.sizeDelta = new Vector2(castbarmaxsize - (unidad * castingTime), castbar.sizeDelta.y);

        }
        else if (castingType == 2) {
            float unidad = castbarmaxsize / (Constants.BASICAOE_CASTING_TIME * basicAOECastingTimeMOD);
            castbar.sizeDelta = new Vector2(castbarmaxsize - (unidad * castingTime), castbar.sizeDelta.y);
        }
        else if (castingType == 15) {
            float unidad = castbarmaxsize / (Constants.MISSILE_BARRAGE_CASTING_TIME * missileBarrageCastingTimeMod);
            castbar.sizeDelta = new Vector2(castbarmaxsize - (unidad * castingTime), castbar.sizeDelta.y);
        }
        else if (castingType == 25) {
            float unidad = castbarmaxsize / (Constants.ELECTRIC_TRAP_CAST_TIME * electricTrapCastingTimeMod);
            castbar.sizeDelta = new Vector2(castbarmaxsize - (unidad * castingTime), castbar.sizeDelta.y);
        }
        else if (castingType == 30) {
            float unidad = castbarmaxsize / (Constants.CONFUSION_CASTING_TIME);
            castbar.sizeDelta = new Vector2(castbarmaxsize - (unidad * castingTime), castbar.sizeDelta.y);
        }
        else {
            castbar.sizeDelta = new Vector2(0, castbar.sizeDelta.y);
        }
    }

    #region HABILIDAD M1
    private void HechizoM1() {
        if (inputController.isM1Pressed()) {
            if (m1SkillCD < 0 && castingTime < 0 && castingType == 0) {
                castingTime = (Constants.FIREBALL_CASTING_TIME * fbcastTimeMod);
                castingType = 1;
            }
        }
        if (castingType == 1 && castingTime < 0) {

            GameObject.Find("spellBarImage0").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon1CD");


            if (fbdouble) {
                CmdSpawnBola(GetComponent<NetworkIdentity>().netId, Constants.FIREBALL_BASIC_DAMAGE * fbdamagemod, fbrangemod, fbsizemod, fblifesteal, transform.position + (transform.right), transform.forward + (transform.right / 25));
                CmdSpawnBola(GetComponent<NetworkIdentity>().netId, Constants.FIREBALL_BASIC_DAMAGE * fbdamagemod, fbrangemod, fbsizemod, fblifesteal, transform.position + (-transform.right), transform.forward + (-transform.right / 25));
            }
            else {
                CmdSpawnBola(GetComponent<NetworkIdentity>().netId, Constants.FIREBALL_BASIC_DAMAGE * fbdamagemod, fbrangemod, fbsizemod, fblifesteal, transform.position, transform.forward);

            }
            castingType = 0;
            m1SkillCD = Constants.FIREBALL_CD;
        }

        if (m1SkillCD > 0) {
            M1Text.text = (int)m1SkillCD + 1 + "";
        }

        m1SkillCD -= Time.deltaTime;

        if (m1SkillCD <= 0 && m1SkillCD > -10) {
            GameObject.Find("spellBarImage0").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon1Normal");
            M1Text.text = "";
            m1SkillCD = -10;
        }

    }

    [Command]
    private void CmdSpawnBola(NetworkInstanceId netId, float fbdamage, float fbrangemod, float fbsizemod, float fblifesteal, Vector3 TMPposition, Vector3 TMPdirection) {
        GameObject nuevaBola = Instantiate(bolaFuego, TMPposition + TMPdirection * 1.5f, Quaternion.identity);
        nuevaBola.GetComponent<Rigidbody>().velocity = (TMPdirection.normalized * Constants.FIREBALL_BASIC_VELOCITY);
        NetworkServer.Spawn(nuevaBola);
        nuevaBola.GetComponent<FireballScript>().RpcsetStartParams(netId, fbdamage, fbrangemod, fbsizemod, fblifesteal);
    }

    #endregion

    #region HABILIDAD M2


    private void HechizoM2() {
        if (inputController.isM2Pressed()) {
            if (m2SkillCD < 0 && castingTime < 0 && castingType == 0) {
                castingTime = Constants.BASICAOE_CASTING_TIME * basicAOECastingTimeMOD;
                castingType = 2;
            }
        }
        if (castingType == 2 && castingTime < 0) {

            GameObject.Find("spellBarImage1").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon5CD");

            CmdCalcularBasicAoe(name, basicAOEradiusMOD, basicAOEDamageMOD);

            GetComponent<PlayerController>().CmdClientRecibeGolpe(new Vector3(), Constants.BASICAOE_DAMAGE * basicAOERecivedDamageMOD);

            castingType = 0;
            m2SkillCD = Constants.BASICAOE_CD;
        }



        if (m2SkillCD > 0) {
            M2Text.text = (int)m2SkillCD + 1 + "";
        }

        m2SkillCD -= Time.deltaTime;

        if (m2SkillCD <= 0 && m2SkillCD > -10) {
            GameObject.Find("spellBarImage1").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon5Normal");
            M2Text.text = "";
            m2SkillCD = -10;
        }
    }

    [Command]
    private void CmdCalcularBasicAoe(String castername, float basicAOEradiusMODTMP, float basicAOEDamageMODTMP) {

        float CalculatebasicAOERadius = Constants.BASICAOE_RADIUS * basicAOEradiusMODTMP;

        GameObject[] objectsn = GameObject.FindGameObjectsWithTag("player"); //get all objects of the same type as this players
        GameObject caster = GameObject.Find(castername);
        for (var f = 0; f < objectsn.Length; f++) //filter the objects that don't match
        {
            if (!objectsn[f].name.Equals(castername)) {
                Vector3 dif = objectsn[f].transform.position - caster.transform.position;
                if (dif.magnitude < CalculatebasicAOERadius) {
                    objectsn[f].GetComponent<PlayerController>().CmdClientRecibeGolpe(dif.normalized, Constants.BASICAOE_DAMAGE * basicAOEDamageMODTMP);
                }
            }
        }

        GameObject nuevoAOE = Instantiate(basicAOE, new Vector3(caster.transform.position.x, -1.07f, caster.transform.position.z), Quaternion.identity);
        NetworkServer.Spawn(nuevoAOE);
        nuevoAOE.GetComponent<BasicAOEeffectController>().RpcsetStartParams(CalculatebasicAOERadius);


        RpcExplosion(new Vector3(caster.transform.position.x, -1.07f, caster.transform.position.z), CalculatebasicAOERadius);
    }
    [ClientRpc]
    private void RpcExplosion(Vector3 pos, float radius) {
        GameObject nuevaExplosion = Instantiate(explosion, pos, Quaternion.identity);
        nuevaExplosion.transform.localScale *= radius / 2;
        //NetworkServer.Spawn(nuevaExplosion);
    }
    #endregion

    #region HABILIDAD ESPACIO
    private void HechizoEspacio() {
        if (spells[2] == 10) {

            doubleTPController -= Time.deltaTime;
            if (doubleTPController > -1 && doubleTPController < 0) {
                doubleTPController = -1;
                spaceSkillCD = Constants.TELEPORT_CD * teleportCDMod;
                GameObject.Find("spellBarImage2").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + spells[2] + "CD");
            }

            if (inputController.isSpacePressed() && castingTime < 0) {
                if (spaceSkillCD <= 0) {

                    GameObject.Find("spellBarImage2").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + spells[2] + "CD");

                    if (teleportModDouble) {
                        if (doubleTPController > 0) {
                            doubleTPController = -1;
                            spaceSkillCD = Constants.TELEPORT_CD * teleportCDMod;

                        }
                        else {
                            spaceSkillCD = 0.5f;
                            doubleTPController = 2;
                        }
                    }
                    else {
                        spaceSkillCD = Constants.TELEPORT_CD * teleportCDMod;
                    }



                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(inputController.mousePosition());
                    if (terrainCollider.Raycast(ray, out hit, 100)) {

                        Vector3 vectorTP = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                        vectorTP -= transform.position;
                        if (vectorTP.magnitude > Constants.TELEPORT_RANGE * teleportRangeMod) {
                            vectorTP = vectorTP.normalized * Constants.TELEPORT_RANGE * teleportRangeMod;
                        }

                        transform.position += new Vector3(vectorTP.x, 0, vectorTP.z);
                    }
                }
            }
        }
        else if (spells[2] == 11) {

            if (inputController.isSpacePressed() && castingTime < 0) {
                if (spaceSkillCD <= 0) {
                    spaceSkillCD = Constants.RUSH_CD * rushCDMod;


                    GameObject.Find("spellBarImage2").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + spells[2] + "CD");

                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(inputController.mousePosition());
                    if (terrainCollider.Raycast(ray, out hit, 100)) {

                        Vector3 vectorTP = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                        vectorTP -= transform.position;

                        vectorTP = vectorTP.normalized * Constants.RUSH_FORCE * rushForceMod;

                        GetComponent<Rigidbody>().velocity += (vectorTP);
                        //transform.GetComponent<Rigidbody>().AddForce(vectorTP);
                        inRush = 0.1f;
                    }
                }
            }

            if (inRush > 0) {
                inRush += Time.deltaTime;
                if (transform.GetComponent<Rigidbody>().velocity.magnitude <= Constants.ALLOW_MOVEMENT_SPEED && inRush > 0.5f) {
                    inRush = 0;
                }

                if (directedRush) {

                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(inputController.mousePosition());
                    if (terrainCollider.Raycast(ray, out hit, 100)) {
                        Vector3 vectorDirectedRush = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                        vectorDirectedRush -= transform.position;
                        transform.GetComponent<Rigidbody>().velocity = Vector3.RotateTowards(transform.GetComponent<Rigidbody>().velocity, vectorDirectedRush, Time.deltaTime * 2, 0);
                    }

                }
            }

        }

        //we only want to change the images if we have a spell selected for this skill
        if (spells[2] != -1) {
            if (spaceSkillCD > 0) {
                SpaceText.text = (int)spaceSkillCD + 1 + "";
            }

            spaceSkillCD -= Time.deltaTime;

            if (spaceSkillCD <= 0 && spaceSkillCD > -10) {
                GameObject.Find("spellBarImage2").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + spells[2] + "Normal");
                SpaceText.text = "";
                spaceSkillCD = -10;
            }
        }

    }
    #endregion

    #region HABILIDAD Q
    private void HechizoQ() {

        if (spells[3] == 15) {

            if (inputController.isQPressed()) {
                if (qSkillCD < 0 && castingTime < 0) {
                    castingTime = Constants.MISSILE_BARRAGE_CASTING_TIME * missileBarrageCastingTimeMod;
                    castingType = 15;

                    qSkillCD = Constants.MISSILE_BARRAGE_CD * barrageCDmod;
                    GameObject.Find("spellBarImage3").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + spells[3] + "CD");

                    nextBarrageShot = castingTime;

                    //el 0.5 son radianes para el angulo del abanico
                    barrageStart = Vector3.RotateTowards(transform.forward, -transform.right, 0.5f, 10000);
                    barrageEnd = Vector3.RotateTowards(transform.forward, transform.right, 0.5f, 10000);
                    barrageDir = barrageStart;
                    isInDoubleBarrage = false;
                }
            }

            if (castingType == 15 && castingTime < nextBarrageShot) {
                nextBarrageShot -= (Constants.MISSILE_BARRAGE_CASTING_TIME * missileBarrageCastingTimeMod) / barrageNumberOfShotsMod;
                CmdSpawnBola(GetComponent<NetworkIdentity>().netId, Constants.MISSILE_BARRAGE_DAMAGE * barragedamagemod, Constants.MISSILE_BARRAGE_BASIC_RANGE_MOD * barrageRangemod, Constants.MISSILE_BARRAGE_SIZE_MOD, barrageLifeStealMod, transform.position, barrageDir);

                barrageDir = Vector3.RotateTowards(barrageDir, barrageEnd, 1f / barrageNumberOfShotsMod, 10000);
            }


            if (castingType == 15 && castingTime < 0) {
                if (doubleBarrage && isInDoubleBarrage == false) {
                    isInDoubleBarrage = true;
                    castingTime = Constants.MISSILE_BARRAGE_CASTING_TIME * missileBarrageCastingTimeMod;
                    nextBarrageShot = castingTime;

                    barrageDir = barrageEnd;
                    barrageEnd = barrageStart;
                    barrageStart = barrageDir;
                }
                else {
                    castingType = 0;
                }
            }

            //change images only if u have a skill on the q button 
            if (spells[3] != -1) {
                if (qSkillCD > 0) {
                    qText.text = (int)qSkillCD + 1 + "";
                }

                qSkillCD -= Time.deltaTime;

                if (qSkillCD < 0 && qSkillCD > -10) {
                    GameObject.Find("spellBarImage3").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + spells[3] + "Normal");
                    qText.text = "";
                    qSkillCD = -10;
                }
            }
        }
    }
    #endregion

    #region HABILIDAD E
    [Command]
    void CmdMetalGraphics(Boolean turnOn, float metalizeDmgMod, float metalizeDuration) {
        if (turnOn) {
            inMetallize = metalizeDuration;
        }
        else {
            inMetallize = 0;
        }
        RpcMetalGraphics(turnOn, metalizeDmgMod);
    }

    [ClientRpc]
    void RpcMetalGraphics(Boolean turnOn, float metalizeDmgMod) {
        if (turnOn) {
            changeColor("Colors/metal");
            this.metallizeDamageMod = metalizeDmgMod;
        }
        else {
            if (isLocalPlayer) {
                changeColor("Colors/blue");
            }
            else {
                changeColor("Colors/magenta");
            }
        }
    }

    [Command]
    public void CmdActivateReflector(Boolean turnOn, float duration, Boolean isInvisible) {
        RpcActivateReflector(turnOn, duration, isInvisible);
    }

    [ClientRpc]
    private void RpcActivateReflector(Boolean turnOn, float duration, Boolean isInvisible) {
        inReflector = duration;
        if (!isInvisible) {
            GetComponentInChildren<ReflectorScript>().GetComponent<Renderer>().enabled = turnOn;
        }
    }

    private void HechizoE() {
        //REFLECTOR
        if (spells[4] == 20) {

            if (inputController.isEPressed() && castingTime < 0) {
                if (eSkillCD <= 0) {
                    GameObject.Find("spellBarImage4").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + spells[4] + "CD");
                    eSkillCD = Constants.REFLECTOR_CD * reflectorCDMod;

                    inReflector = Constants.REFLECTOR_DURATION * reflectorDurationMod;
                    CmdActivateReflector(true, inReflector, reflectorInvisibleMod);
                    if (reflectorInvisibleMod) {
                        GetComponentInChildren<ReflectorScript>().GetComponent<Renderer>().enabled = true;
                        Material[] mats;
                        mats = GetComponentInChildren<ReflectorScript>().GetComponent<Renderer>().materials;
                        mats[0] = Resources.Load<Material>("Colors/ReflectorInvis");
                        GetComponentInChildren<ReflectorScript>().GetComponent<Renderer>().materials = mats;
                    }

                }
            }

            if (inReflector > 0) {
                inReflector -= Time.deltaTime;

            }
            if (inReflector <= 0 && inReflector > -10) {
                CmdActivateReflector(false, -10, false);
                inReflector = -10;
            }
        }
        //METALIZE
        else if (spells[4] == 21) {
            if (inputController.isEPressed() && castingTime < 0) {
                if (eSkillCD <= 0) {
                    eSkillCD = Constants.METALLIZE_CD * metallizeCDMod;
                    inMetallize = Constants.METALLIZE_DURATION * metallizeDurationMod;
                    CmdMetalGraphics(true, metallizeDamageMod, inMetallize);
                    GameObject.Find("spellBarImage4").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + spells[4] + "CD");

                }
            }
            inMetallize -= Time.deltaTime;
            if (inMetallize <= 0 && inMetallize > -10) {
                CmdMetalGraphics(false, metallizeDamageMod, 0);
                inMetallize = -10;
            }


        }


        if (spells[4] != -1) {

            if (eSkillCD > 0) {
                eText.text = (int)eSkillCD + 1 + "";
            }

            eSkillCD -= Time.deltaTime;

            if (eSkillCD < 0 && eSkillCD > -10) {

                GameObject.Find("spellBarImage4").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + spells[4] + "Normal");
                eText.text = "";
                eSkillCD = -10;
            }
        }
    }

    #endregion

    #region HABILIDAD R
    private void HechizoR() {
        //ELECTRIC TRAP
        if (spells[5] == 25) {

            if (inputController.isRPressed() && castingTime < 0) {
                if (rSkillCD <= 0) {
                    castingTime = Constants.ELECTRIC_TRAP_CAST_TIME * electricTrapCastingTimeMod;
                    castingType = 25;
                }
            }

            if (castingType == 25 && castingTime < 0) {
                GameObject.Find("spellBarImage5").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + spells[5] + "CD");
                rSkillCD = Constants.ELECTRIC_TRAP_CD;
                CmdSpawnElectricTrap(GetComponent<NetworkIdentity>().netId, electricTrapDamageMod, electricTrapDurationMod, electricTrapAreaMod, transform.position);
                castingType = 0;
            }
            //TIME BEACON
        }
        else if (spells[5] == 26) {
            if (inputController.isRPressed()) {
                if (rSkillCD <= 0 && timeBeaconDuration <= -10) {                 
                    GameObject.Find("spellBarImage5").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + spells[5] + "CD");
                    rSkillCD = Constants.TIME_BEACON_CD * timeBeaconCDMod;
                    if (timeBeaconThrowMod) {
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(inputController.mousePosition());
                        if (terrainCollider.Raycast(ray, out hit, 100)) {
                            Vector3 vectorThrowTimeBeacon = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                            vectorThrowTimeBeacon -= transform.position;
                            if (vectorThrowTimeBeacon.magnitude > Constants.TIME_BEACON_THROW_RANGE) {
                                vectorThrowTimeBeacon = vectorThrowTimeBeacon.normalized * Constants.TIME_BEACON_THROW_RANGE;
                            }
                            timeBeaconX = transform.position.x + vectorThrowTimeBeacon.x;
                            timeBeaconZ = transform.position.z + vectorThrowTimeBeacon.z;
                        }
                    }
                    else {
                        timeBeaconX = transform.position.x;
                        timeBeaconZ = transform.position.z;
                    }

                    timeBeaconStartingHP = GetComponent<PlayerController>().updatedHpForClient;
                    timeBeaconDuration = Constants.TIME_BEACON_DURATION * timeBeaconDurationMod;
                }
            }
            if (timeBeaconDuration > -10) {
              
                timeBeaconDuration -= Time.deltaTime;
                //we will return to the starting point when the time ends or when we press R again (and at least 0,5 seconds passed)
                if (timeBeaconDuration <= 0 || (inputController.isRPressed() && timeBeaconDuration < (Constants.TIME_BEACON_DURATION * timeBeaconDurationMod) - 0.5f)) {
                                        
                    //si sigue vivo (para que no se recoloce en el suelo despues de muerto)
                    if (GetComponent<PlayerController>().updatedHpForClient > 0) {                    

                        transform.position = new Vector3(timeBeaconX, transform.position.y, timeBeaconZ);
                        timeBeaconDuration = -10;

                        CmdSetHP(timeBeaconStartingHP);

                        //reduce all CDs except R skill cd
                        if (timeBeaconReduceAllCDMod) {
                            if (m1SkillCD > 0) {
                                m1SkillCD *= 0.5f;
                            }
                            if (m2SkillCD > 0) {
                                m2SkillCD *= 0.5f;
                            }
                            if (spaceSkillCD > 0) {
                                spaceSkillCD *= 0.5f;
                            }
                            if (qSkillCD > 0) {
                                qSkillCD *= 0.5f;
                            }
                            if (eSkillCD > 0) {
                                eSkillCD *= 0.5f;
                            }
                        }
                        if (timeBeaconSpeedMod) {
                            timeBeaconSpeedTime = Constants.TIME_BEACON_SPEED_DURATION;
                        }
                    }
                }
            }
        }

        if (spells[5] != -1) {
            if (rSkillCD > 0) {
                rText.text = (int)rSkillCD + 1 + "";
            }
            rSkillCD -= Time.deltaTime;
            if (rSkillCD < 0 && rSkillCD > -10) {
                GameObject.Find("spellBarImage5").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + spells[5] + "Normal");
                rText.text = "";
                rSkillCD = -10;
            }
        }
    }

    [Command]
    private void CmdSetHP(float timeBeaconStartingHP) {
        //si sigue vivo
        if (GetComponent<PlayerController>().hp > 0) {
            GetComponent<PlayerController>().hp = timeBeaconStartingHP;
        }
    }

    [Command]
    private void CmdSpawnElectricTrap(NetworkInstanceId netId, float electricTrapDamageMod, float electricTrapDurationMod, float electricTrapAreaMod, Vector3 TMPposition) {
        TMPposition = new Vector3(TMPposition.x, -0.9f, TMPposition.z);
        GameObject nuevaElectricTrap = Instantiate(electricTrap, TMPposition, Quaternion.identity);
        NetworkServer.Spawn(nuevaElectricTrap);
        nuevaElectricTrap.GetComponent<ElectricTrapScript>().RpcsetStartParams(netId, electricTrapDamageMod, electricTrapDurationMod, electricTrapAreaMod);
    }

    [Command]
    public void CmdsetElectrified(float duration, float damage) {
        RpcsetElectrified(duration, damage);
    }
    [ClientRpc]
    public void RpcsetElectrified(float duration, float damage) {
        isElectrified = duration;
        electrifyDamage = damage;
        electrifyDuration = duration;

        GetComponentInChildren<ElectricityEffectScript>().setEnabled(true);
    }
    [Command]
    public void CmdRemoveElectrifiedEffect() {
        RpcRemoveElectrifiedEffect();
    }
    [ClientRpc]
    public void RpcRemoveElectrifiedEffect() {

        GetComponentInChildren<ElectricityEffectScript>().setEnabled(false);
    }
    #endregion

    #region HABILIDAD F

    private void HechizoF() {

        if (inConfusion > 0) {
            inConfusion -= Time.deltaTime;
        }

        if (spells[6] == 30) {

            if (inputController.isFPressed()) {
                if (fSkillCD < 0 && castingTime < 0) {
                    castingTime = Constants.CONFUSION_CASTING_TIME;
                    castingType = 30;
                    GameObject.Find("spellBarImage6").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + spells[6] + "CD");
                }
            }


            if (castingType == 30 && castingTime < 0) {
                castingType = 0;
                fSkillCD = Constants.CONFUSION_CASTING_CD * confusionShotCDMod;
                CmdSpawnConfusionShot(GetComponent<NetworkIdentity>().netId, Constants.CONFUSIONSHOT_BASIC_DAMAGE * confusionShotDamageMod,confusionShotDurationMod, confusionShotRangeMod, confusionShotRandomKnockback, transform.position, transform.forward);
            }

            //change images only if u have a skill on the q button 
            if (spells[6] != -1) {
                if (fSkillCD > 0) {
                    fText.text = (int)fSkillCD + 1 + "";
                }

                fSkillCD -= Time.deltaTime;

                if (fSkillCD < 0 && fSkillCD > -10) {
                    GameObject.Find("spellBarImage6").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + spells[6] + "Normal");
                    fText.text = "";
                    fSkillCD = -10;
                }
            }
        }
    }

    [Command]
    private void CmdSpawnConfusionShot(NetworkInstanceId netId, float damagemod, float durationmod, float rangemod, bool teleport, Vector3 TMPposition, Vector3 TMPdirection) {
        GameObject nuevoConfusionShot = Instantiate(confusionShot, TMPposition + TMPdirection * 1.5f, Quaternion.identity);
        nuevoConfusionShot.GetComponent<Rigidbody>().velocity = (TMPdirection.normalized * Constants.CONFUSIONSHOT_BASIC_VELOCITY);
        NetworkServer.Spawn(nuevoConfusionShot);
        nuevoConfusionShot.GetComponent<ConfusionShotScript>().RpcsetStartParams(netId, damagemod, durationmod, rangemod, teleport);
    }

    [ClientRpc]
    public void RpcRecibeConfusion(float confusionDuration) {
        if (isLocalPlayer) {
            inConfusion = confusionDuration;
            CmdConfusionParticleSystemEnabled(true);
        }
    }


    [Command]
    private void CmdConfusionParticleSystemEnabled(bool b) {
        RpcConfusionParticleSystemEnabled(b);
    }

    [ClientRpc]
    private void RpcConfusionParticleSystemEnabled(bool b) {
        GetComponentInChildren<ConfusionParticleSystemScript>().setEnabled(b);
    }

    #endregion

    #region ITEM 1

    [Command]
    private void CmdHPRegenParticleSystemEnabled(bool b) {
        RpcHPRegenParticleSystemEnabled(b);
    }

    [ClientRpc]
    private void RpcHPRegenParticleSystemEnabled(bool b) {
        GetComponentInChildren<HPRegenParticleSystemScript>().setEnabled(b);
    }

    private void Item1() {
        if (inputController.isNumber1Pressed()) {
            if (stimpakCharges > 0 && stimpakTime < Constants.STIMPAK_DURATION - 1) {
                stimpakCharges--;
                stimpakTime = Constants.STIMPAK_DURATION;

                CmdHPRegenParticleSystemEnabled(true);

                if (stimpakCharges > 0) {
                    item1Text.text = "" + stimpakCharges;
                }
                else {
                    item1Text.text = "";
                    GameObject.Find("spellBarImageItem1").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ItemSprites/itemicon1CD");
                }
            }
        }

        if (stimpakTime > 0) {
            stimpakTime -= Time.deltaTime;
            GetComponent<PlayerController>().CmdClientRecibeGolpe(new Vector3(), -Time.deltaTime * (Constants.STIMPAK_HEALTH_REGEN / Constants.STIMPAK_DURATION));
        }
        else if (stimpakTime > -10) {
            CmdHPRegenParticleSystemEnabled(false);
            stimpakTime = -10;
        }

    }

    #endregion

    #region ESTADISTICAS
    private void mostrarMenuEstadisticas() {
        if (inputController.isTabPressed()) {
            if (!statisticsText.enabled) {
                statisticsText.enabled = true;

                String str = "";
                GameObject[] objectsn = GameObject.FindGameObjectsWithTag("player"); //get all objects of the same type as this players
                for (var f = 0; f < objectsn.Length; f++) {
                    bool itsMe = objectsn[f].GetComponent<PlayerInputController>().GetComponent<NetworkIdentity>().netId.ToString() == GetComponent<NetworkIdentity>().netId.ToString();

                    if (itsMe) {
                        str += "<color=#0000ffff>";
                    }
                    str += "ID: " + objectsn[f].GetComponent<PlayerInputController>().GetComponent<NetworkIdentity>().netId.ToString();
                    str += " - Rounds won: " + objectsn[f].GetComponent<PlayerInputController>().rounds_won;
                    if (itsMe) {
                        str += "</color>";
                    }
                    str += "\n";
                }
                statisticsText.text = str;
            }
        }
        else {
            statisticsText.enabled = false;
        }
    }

    #endregion

    #region UPDATE PLAYER AND CAMERA (zoom mirar mover...)
    private void zoom() {

        
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            cameraY -= 1;
            if (cameraY < 5)
                cameraY = 5;
            cameraZ = (float)-(cameraY / Math.Tan((Math.PI / 180) * Camera.main.transform.rotation.eulerAngles.x));


        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            cameraY += 1;
            if (cameraY > 50)
                cameraY = 50;
            cameraZ = (float)-(cameraY / Math.Tan((Math.PI / 180) * Camera.main.transform.rotation.eulerAngles.x));


        }

        if (Input.GetMouseButton(2)) {
            float angle = Camera.main.transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y");
            if (angle < 35) {
                angle = 35;
            }
            else if (angle > 90) {
                angle = 90;
            }
            Camera.main.transform.rotation = Quaternion.Euler(angle, 0, 0);
            cameraZ = (float)-(cameraY / Math.Tan((Math.PI / 180) * Camera.main.transform.rotation.eulerAngles.x));


        }
    }

    private void mirar() {
        if (isLocalPlayer) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(inputController.mousePosition());
            if (terrainCollider.Raycast(ray, out hit, 100)) {
                //girar al jugador
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));

                //mover la camara       
                Vector3 CameraDest = new Vector3(transform.position.x + cameraX, cameraY, transform.position.z + cameraZ);
                CameraDest += (new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position) / 4;
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, CameraDest, 0.1f);

                castbarCanvas.transform.LookAt(Camera.main.transform);
                castbarCanvas.transform.rotation = Quaternion.Euler(castbarCanvas.transform.rotation.eulerAngles.x, 180, castbarCanvas.transform.rotation.eulerAngles.z);


                if (inputController.usingController) {
                    controllerLineRenderer.SetPosition(0, new Vector3(transform.position.x, -0.9f, transform.position.z));
                    controllerLineRenderer.SetPosition(1, hit.point);
                }
            }


        }

        GetComponent<PlayerController>().updateHpbarRotation();
    }

    private void mover() {

        float drag = Constants.BASIC_DRAG;

       

        if (inRush > 0) {
            drag += Constants.IN_RUSH_DRAG;
        }
        if (inMetallize > 0) {
            drag += Constants.IN_METAL_DRAG * metallizeDragMod;
        }

        float old_vel_mag = transform.GetComponent<Rigidbody>().velocity.magnitude;
        //    float dragForceMagnitude = old_vel_mag*old_vel_mag * drag; // The variable 
        float dragForceMagnitude = old_vel_mag * drag; // The variable 
        GetComponent<Rigidbody>().AddForce(dragForceMagnitude * -GetComponent<Rigidbody>().velocity.normalized * Time.deltaTime * 3);
        if (old_vel_mag < Constants.ALLOW_MOVEMENT_SPEED) {
            GetComponent<Rigidbody>().velocity = new Vector3();
        }
        else {
            return;
        }

        var x = inputController.getAxisHorizontal();
        var z = inputController.getAxisVertical();

        if (x != 0 || z != 0) {
            anim.SetInteger("state", 1);
        }
        else {
            anim.SetInteger("state", 0);
        }

        Vector3 mov = new Vector3(x, 0, z);
        if (Math.Abs(mov.x) + Math.Abs(mov.z) > 1) {
            mov = mov.normalized;
        }

        float thisUpdatePlayerSpeed = Constants.PLAYER_BASE_SPEED;
        if (castingTime > 0) {
            thisUpdatePlayerSpeed *= 0.3f;
        }
        if (castingType == 2) {
            thisUpdatePlayerSpeed = 0;
        }
        if (castingType == 25) {
            thisUpdatePlayerSpeed = 0;
        }
        if (inMetallize > 0) {
            thisUpdatePlayerSpeed *= metallizeMovespeedMod;
        }

        if (inReflector > 0) {
            thisUpdatePlayerSpeed *= reflectorMoveSpeed;
        }

        if (timeBeaconSpeedTime > 0) {
            timeBeaconSpeedTime -= Time.deltaTime;
            thisUpdatePlayerSpeed *= Constants.TIME_BEACON_SPEED_MOD;
        }
        if (stimpakTime > 0) {
            thisUpdatePlayerSpeed *= Constants.STIMPAK_SPEED_BOOST;
        }

        if (isElectrified > 0) {
            castingType = 0;
            castingTime = 0;
            thisUpdatePlayerSpeed = 0;
            isElectrified -= Time.deltaTime;
            GetComponent<PlayerController>().CmdTakeDamage(electrifyDamage / electrifyDuration * Time.deltaTime);
        }
        else if (isElectrified > -10) {

            CmdRemoveElectrifiedEffect();
            isElectrified = -10;
        }


        mov = mov * Time.deltaTime * thisUpdatePlayerSpeed;

        if(inConfusion>-10 && inConfusion < 0){
            inConfusion = -10;
            CmdConfusionParticleSystemEnabled(false);
        }
        if (inConfusion > 0){
            transform.position = transform.position - mov;
        }
        else{
            transform.position = transform.position + mov;
        }
       
    }
    #endregion
}
