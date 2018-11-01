using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellUpgradeController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    PlayerInputController myplayer;

    public GameObject textoAvisos;
    public Text hoverText;

    public SpellShopController spellShopController;
    int spell;
    public Image b11, b21, b22, b31, b41, b42, b51, b61, b62, b63;
    public Canvas spellUpgradeCanvas;

    #region Descriptions and sprites for all upgrades
    //this are all the descriptions for the skill upgrades
    string[,] spellDescriptions = {{"+DMG \n +RNG", "+LS", "+DMG", "+DMG \n +RNG", "+LS", "++RNG", "+DMG \n +RNG", "+++++SIZE \n +++CAST TIME \n +++++DMG ++LS" , "++++DMG \n -----RNG" , "---DMG \n DOUBLE PROJ" }
    ,{"", "", "", "", "", "", "", "", "", ""}
    ,{"", "", "", "", "", "", "", "", "", ""}
    ,{"", "", "", "", "", "", "", "", "", ""}
    ,{"", "", "", "", "", "", "", "", "", ""}

        ,{"-Rec DMG \n +AOE", "-Cast time", "++DMG", "-Rec DMG \n +AOE", "+AOE \n +DMG", "-Cast time \n + DMG", "-Rec DMG \n +AOE", "---Cast time \n ---AOE", "+++AOE \n --DMG", "----Rec DMG"}
    ,{"", "", "", "", "", "", "", "", "", ""}
    ,{"", "", "", "", "", "", "", "", "", ""}
    ,{"", "", "", "", "", "", "", "", "", ""}
    ,{"", "", "", "", "", "", "", "", "", ""}

        ,{"+Range", "+Range", "-CD", "+Range", "+Range", "-CD", "+Range", "+++++Range", "---CD", "Double tp \n ++CD \n -----Range"}
     ,{"+DMG\n+FORCE", "-CD", "++DMG", "+DMG\n+FORCE", "-CD", "++DMG", "+DMG\n+FORCE", "---CD\n----FORCE", "++++DMG\n----FORCE", "Change direcction \nduring rush"}
            ,{"", "", "", "", "", "", "", "", "", ""}
            ,{"", "", "", "", "", "", "", "", "", ""}
            ,{"", "", "", "", "", "", "", "", "", ""}

        ,{"+DMG \n +RNG", "++DMG", "--CD", "+DMG \n +RNG", "++DMG", "--CD", "+DMG \n +RNG", "double barrage! \n +++CD \n--DMG", "++DMG \n++CD \n+++++life steal", "+++RNG \n--casting time \n --CD"}
            ,{"Q2", "Q2", "Q2", "Q2", "Q2", "Q2", "Q2", "Q2", "Q2", "Q2"}
            ,{"", "", "", "", "", "", "", "", "", ""}
            ,{"", "", "", "", "", "", "", "", "", ""}
            ,{"", "", "", "", "", "", "", "", "", ""}

        ,{"-CD", "-CD", "+duration", "-CD", "-CD", "+duration", "-CD", "Duplicate duration, but reflector breaks on first hit", "During reflection, you move faster", "Turns your reflector invisible for other players"}
            ,{"+duration", "+DMG reduction", "-CD", "+duration", "+DMG reduction", "-CD", "+duration", "Move normally while mettalized", "+++Knockback reduction", "+++ DMG reduction --Movespeed"}
            ,{"", "", "", "", "", "", "", "", "", ""}
           ,{"", "", "", "", "", "", "", "", "", ""}
            ,{"", "", "", "", "", "", "", "", "", ""}

        ,{"+Duration \n + DMG", "+Activation area", "++DMG", "+Duration \n + DMG", "+Activation area", "++DMG", "+Duration \n + DMG", "-50% casting time", "++++Duration \n No damage", "+++activation area \n-- DMG"}
            ,{"+Duration", "-CD", "+Duration", "+Duration", "-CD", "+Duration", "+Duration", "Throws the beacon", "Reduce other CDs by 50% on use", "Move faster after returning"}
            ,{"", "", "", "", "", "", "", "", "", ""}
           ,{"", "", "", "", "", "", "", "", "", ""}
            ,{"", "", "", "", "", "", "", "", "", ""}

             ,{"+Duration", "+DMG", "-CD", "+Duration", "+DMG", "-CD", "+Duration", "---Range\n No DMG \n +++++Duration", "+++DMG\n---Duration", "+++Knockback \n Random Knockback direction"}
             ,{"", "", "", "", "", "", "", "", "", ""}
             ,{"", "", "", "", "", "", "", "", "", ""}
            ,{"", "", "", "", "", "", "", "", "", ""}
            ,{"", "", "", "", "", "", "", "", "", ""}
    };

    //images for each spell upgrades
    //we are now using the skill image for each upgrade, but this array will make sense if in a future we have diferent images for upgrades of the same skill
    string[,] spellSprites = {{"Sprites/SpellButtonsSprites/spellicon1Normal", "Sprites/SpellButtonsSprites/spellicon1Normal", "Sprites/SpellButtonsSprites/spellicon1Normal", "Sprites/SpellButtonsSprites/spellicon1Normal", "Sprites/SpellButtonsSprites/spellicon1Normal", "Sprites/SpellButtonsSprites/spellicon1Normal", "Sprites/SpellButtonsSprites/spellicon1Normal", "Sprites/SpellButtonsSprites/spellicon1Normal" , "Sprites/SpellButtonsSprites/spellicon1Normal" , "Sprites/SpellButtonsSprites/spellicon1Normal" }
    ,{"", "", "", "", "", "", "", "", "", ""}
    ,{"", "", "", "", "", "", "", "", "", ""}
    ,{"", "", "", "", "", "", "", "", "", ""}
    ,{"", "", "", "", "", "", "", "", "", ""}
    ,{"Sprites/SpellButtonsSprites/spellicon5Normal", "Sprites/SpellButtonsSprites/spellicon5Normal", "Sprites/SpellButtonsSprites/spellicon5Normal", "Sprites/SpellButtonsSprites/spellicon5Normal", "Sprites/SpellButtonsSprites/spellicon5Normal", "Sprites/SpellButtonsSprites/spellicon5Normal", "Sprites/SpellButtonsSprites/spellicon5Normal", "Sprites/SpellButtonsSprites/spellicon5Normal" , "Sprites/SpellButtonsSprites/spellicon5Normal" , "Sprites/SpellButtonsSprites/spellicon5Normal" }
    ,{"", "", "", "", "", "", "", "", "", ""}
    ,{"", "", "", "", "", "", "", "", "", ""}
    ,{"", "", "", "", "", "", "", "", "", ""}
    ,{"", "", "", "", "", "", "", "", "", ""}
    ,{"Sprites/SpellButtonsSprites/spellicon10Normal", "Sprites/SpellButtonsSprites/spellicon10Normal", "Sprites/SpellButtonsSprites/spellicon10Normal", "Sprites/SpellButtonsSprites/spellicon10Normal", "Sprites/SpellButtonsSprites/spellicon10Normal", "Sprites/SpellButtonsSprites/spellicon10Normal", "Sprites/SpellButtonsSprites/spellicon10Normal", "Sprites/SpellButtonsSprites/spellicon10Normal" , "Sprites/SpellButtonsSprites/spellicon10Normal" , "Sprites/SpellButtonsSprites/spellicon10Normal" }
     ,{"Sprites/SpellButtonsSprites/spellicon11Normal", "Sprites/SpellButtonsSprites/spellicon11Normal", "Sprites/SpellButtonsSprites/spellicon11Normal", "Sprites/SpellButtonsSprites/spellicon11Normal", "Sprites/SpellButtonsSprites/spellicon11Normal", "Sprites/SpellButtonsSprites/spellicon11Normal", "Sprites/SpellButtonsSprites/spellicon11Normal", "Sprites/SpellButtonsSprites/spellicon11Normal" , "Sprites/SpellButtonsSprites/spellicon11Normal" , "Sprites/SpellButtonsSprites/spellicon11Normal" }
          ,{"", "", "", "", "", "", "", "", "", ""}
          ,{"", "", "", "", "", "", "", "", "", ""}
                ,{"", "", "", "", "", "", "", "", "", ""}
       ,{"Sprites/SpellButtonsSprites/spellicon15Normal", "Sprites/SpellButtonsSprites/spellicon15Normal", "Sprites/SpellButtonsSprites/spellicon15Normal", "Sprites/SpellButtonsSprites/spellicon15Normal", "Sprites/SpellButtonsSprites/spellicon15Normal", "Sprites/SpellButtonsSprites/spellicon15Normal", "Sprites/SpellButtonsSprites/spellicon15Normal", "Sprites/SpellButtonsSprites/spellicon15Normal" , "Sprites/SpellButtonsSprites/spellicon15Normal" , "Sprites/SpellButtonsSprites/spellicon15Normal" }
 ,{"Sprites/SpellButtonsSprites/spellicon16Normal", "Sprites/SpellButtonsSprites/spellicon16Normal", "Sprites/SpellButtonsSprites/spellicon16Normal", "Sprites/SpellButtonsSprites/spellicon16Normal", "Sprites/SpellButtonsSprites/spellicon16Normal", "Sprites/SpellButtonsSprites/spellicon16Normal", "Sprites/SpellButtonsSprites/spellicon16Normal", "Sprites/SpellButtonsSprites/spellicon16Normal" , "Sprites/SpellButtonsSprites/spellicon16Normal" , "Sprites/SpellButtonsSprites/spellicon16Normal" }
   ,{"", "", "", "", "", "", "", "", "", ""}
          ,{"", "", "", "", "", "", "", "", "", ""}
                ,{"", "", "", "", "", "", "", "", "", ""}
             ,{"Sprites/SpellButtonsSprites/spellicon20Normal", "Sprites/SpellButtonsSprites/spellicon20Normal", "Sprites/SpellButtonsSprites/spellicon20Normal", "Sprites/SpellButtonsSprites/spellicon20Normal", "Sprites/SpellButtonsSprites/spellicon20Normal", "Sprites/SpellButtonsSprites/spellicon20Normal", "Sprites/SpellButtonsSprites/spellicon20Normal", "Sprites/SpellButtonsSprites/spellicon20Normal" , "Sprites/SpellButtonsSprites/spellicon20Normal" , "Sprites/SpellButtonsSprites/spellicon20Normal" }

             ,{"Sprites/SpellButtonsSprites/spellicon21Normal", "Sprites/SpellButtonsSprites/spellicon21Normal", "Sprites/SpellButtonsSprites/spellicon21Normal", "Sprites/SpellButtonsSprites/spellicon21Normal", "Sprites/SpellButtonsSprites/spellicon21Normal", "Sprites/SpellButtonsSprites/spellicon21Normal", "Sprites/SpellButtonsSprites/spellicon21Normal", "Sprites/SpellButtonsSprites/spellicon21Normal" , "Sprites/SpellButtonsSprites/spellicon21Normal" , "Sprites/SpellButtonsSprites/spellicon21Normal" }
         ,{"", "", "", "", "", "", "", "", "", ""}
          ,{"", "", "", "", "", "", "", "", "", ""}
                ,{"", "", "", "", "", "", "", "", "", ""}

         ,{"Sprites/SpellButtonsSprites/spellicon25Normal", "Sprites/SpellButtonsSprites/spellicon25Normal", "Sprites/SpellButtonsSprites/spellicon25Normal", "Sprites/SpellButtonsSprites/spellicon25Normal", "Sprites/SpellButtonsSprites/spellicon25Normal", "Sprites/SpellButtonsSprites/spellicon25Normal", "Sprites/SpellButtonsSprites/spellicon25Normal", "Sprites/SpellButtonsSprites/spellicon25Normal" , "Sprites/SpellButtonsSprites/spellicon25Normal" , "Sprites/SpellButtonsSprites/spellicon25Normal" }
                     ,{"Sprites/SpellButtonsSprites/spellicon26Normal", "Sprites/SpellButtonsSprites/spellicon26Normal", "Sprites/SpellButtonsSprites/spellicon26Normal", "Sprites/SpellButtonsSprites/spellicon26Normal", "Sprites/SpellButtonsSprites/spellicon26Normal", "Sprites/SpellButtonsSprites/spellicon26Normal", "Sprites/SpellButtonsSprites/spellicon26Normal", "Sprites/SpellButtonsSprites/spellicon26Normal" , "Sprites/SpellButtonsSprites/spellicon26Normal" , "Sprites/SpellButtonsSprites/spellicon26Normal" }
         ,{"", "", "", "", "", "", "", "", "", ""}
          ,{"", "", "", "", "", "", "", "", "", ""}
                ,{"", "", "", "", "", "", "", "", "", ""}

                ,{"Sprites/SpellButtonsSprites/spellicon30Normal", "Sprites/SpellButtonsSprites/spellicon30Normal", "Sprites/SpellButtonsSprites/spellicon30Normal", "Sprites/SpellButtonsSprites/spellicon30Normal", "Sprites/SpellButtonsSprites/spellicon30Normal", "Sprites/SpellButtonsSprites/spellicon30Normal", "Sprites/SpellButtonsSprites/spellicon30Normal", "Sprites/SpellButtonsSprites/spellicon30Normal" , "Sprites/SpellButtonsSprites/spellicon30Normal" , "Sprites/SpellButtonsSprites/spellicon30Normal" }
            ,{"", "", "", "", "", "", "", "", "", ""}
        ,{"", "", "", "", "", "", "", "", "", ""}
          ,{"", "", "", "", "", "", "", "", "", ""}
                ,{"", "", "", "", "", "", "", "", "", ""}
    };

    #endregion

    // Use this for initialization
    void Start() {
        hoverText.enabled = false;
        spellUpgradeCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (hoverText.enabled) {
            hoverText.transform.position = new Vector3(Input.mousePosition.x + 70, Input.mousePosition.y - 50, 0);
        }
    }

    internal void setPlayer(PlayerInputController playerInputController) {
        myplayer = playerInputController;
    }
    
    //when the mouse enter a upgrade button
    public void OnPointerEnter(PointerEventData eventData) {
        hoverText.enabled = true;

        //show the description of the hovered upgrade
        if (eventData.hovered[0].ToString().StartsWith("b11")) {
            hoverText.text = spellDescriptions[spell, 0];
        }
        if (eventData.hovered[0].ToString().StartsWith("b21")) {
            hoverText.text = spellDescriptions[spell, 1];
        }
        if (eventData.hovered[0].ToString().StartsWith("b22")) {
            hoverText.text = spellDescriptions[spell, 2];
        }
        if (eventData.hovered[0].ToString().StartsWith("b31")) {
            hoverText.text = spellDescriptions[spell, 3];
        }
        if (eventData.hovered[0].ToString().StartsWith("b41")) {
            hoverText.text = spellDescriptions[spell, 4];
        }
        if (eventData.hovered[0].ToString().StartsWith("b42")) {
            hoverText.text = spellDescriptions[spell, 5];
        }
        if (eventData.hovered[0].ToString().StartsWith("b51")) {
            hoverText.text = spellDescriptions[spell, 6];
        }
        if (eventData.hovered[0].ToString().StartsWith("b61")) {
            hoverText.text = spellDescriptions[spell, 7];
        }
        if (eventData.hovered[0].ToString().StartsWith("b62")) {
            hoverText.text = spellDescriptions[spell, 8];
        }
        if (eventData.hovered[0].ToString().StartsWith("b63")) {
            hoverText.text = spellDescriptions[spell, 9];
        }
    }

    //when mouse go out of the buttom, hide the description
    public void OnPointerExit(PointerEventData eventData) {
        hoverText.text = "";
        hoverText.enabled = false;
    }

    //activate the spell upgrade menu
    public void ShowSpellUpgrade(int spell) {
        b11.GetComponent<Button>().interactable = false;
        b21.GetComponent<Button>().interactable = false;
        b22.GetComponent<Button>().interactable = false;
        b31.GetComponent<Button>().interactable = false;
        b41.GetComponent<Button>().interactable = false;
        b42.GetComponent<Button>().interactable = false;
        b51.GetComponent<Button>().interactable = false;
        b61.GetComponent<Button>().interactable = false;
        b62.GetComponent<Button>().interactable = false;
        b63.GetComponent<Button>().interactable = false;
        this.spell = spell;

        //load images
        b11.sprite = Resources.Load<Sprite>(spellSprites[spell, 0]);
        b21.sprite = Resources.Load<Sprite>(spellSprites[spell, 1]);
        b22.sprite = Resources.Load<Sprite>(spellSprites[spell, 2]);
        b31.sprite = Resources.Load<Sprite>(spellSprites[spell, 3]);
        b41.sprite = Resources.Load<Sprite>(spellSprites[spell, 4]);
        b42.sprite = Resources.Load<Sprite>(spellSprites[spell, 5]);
        b51.sprite = Resources.Load<Sprite>(spellSprites[spell, 6]);
        b61.sprite = Resources.Load<Sprite>(spellSprites[spell, 7]);
        b62.sprite = Resources.Load<Sprite>(spellSprites[spell, 8]);
        b63.sprite = Resources.Load<Sprite>(spellSprites[spell, 9]);

        //enable the active buttons depending on the skill level
        if (myplayer.spellLevels[spell] == 1) {
            b11.GetComponent<Button>().interactable = true;
        }
        if (myplayer.spellLevels[spell] == 2) {
            b21.GetComponent<Button>().interactable = true;
            b22.GetComponent<Button>().interactable = true;
        }
        if (myplayer.spellLevels[spell] == 3) {
            b31.GetComponent<Button>().interactable = true;
        }
        if (myplayer.spellLevels[spell] == 4) {
            b41.GetComponent<Button>().interactable = true;
            b42.GetComponent<Button>().interactable = true;
        }
        if (myplayer.spellLevels[spell] == 5) {
            b51.GetComponent<Button>().interactable = true;
        }
        if (myplayer.spellLevels[spell] == 6) {
            b61.GetComponent<Button>().interactable = true;
            b62.GetComponent<Button>().interactable = true;
            b63.GetComponent<Button>().interactable = true;
        }


        spellShopController.PointsUpdate();
        spellUpgradeCanvas.enabled = true;
    }


    public void HideSpellUpgrade() {
        spellUpgradeCanvas.enabled = false;
    }


    #region spellUpgrade Button clicks
    //Upgrade the skills, depending on the upgrade button pressed and the active skill 
    public void btnSpellUpgrade11() {
        if (myplayer.skillPoints >= Constants.SKILLPOINTS_COST_BASIC_UPGRADE) {
            myplayer.skillPoints -= Constants.SKILLPOINTS_COST_BASIC_UPGRADE;
            myplayer.spellLevels[spell]++;
        }
        else {
            Instantiate(textoAvisos, new Vector3(myplayer.transform.position.x, 1056, myplayer.transform.position.z), Quaternion.identity);
            return;
        }
        if (spell == 0) { //Bazooka
            myplayer.fbrangemod += 0.1f;
            myplayer.fbdamagemod += 0.1f;
        }
        else if (spell == 5) { //basicaoe
            myplayer.basicAOEradiusMOD += 0.07f;
            myplayer.basicAOERecivedDamageMOD -= 0.1f;
        }
        else if (spell == 10) { //teleport
            myplayer.teleportRangeMod += 0.1f;
        }
        else if (spell == 11) { //rush
            myplayer.rushDamageMod += 0.1f;
            myplayer.rushForceMod += 0.15f;
        }
        else if (spell == 15) { //barrage
            myplayer.barrageRangemod += 0.1f;
            myplayer.barragedamagemod += 0.1f;
        }
        else if (spell == 16) { //q2


        }
        else if (spell == 20) { //reflector
            myplayer.reflectorCDMod -= 0.1f;
        }
        else if (spell == 21) { //petrify
            myplayer.metallizeDurationMod += 0.2f;
        }
        else if (spell == 25) { //electric trap
            myplayer.electricTrapDamageMod += 0.1f;
            myplayer.electricTrapDurationMod += 0.1f;
        }
        else if (spell == 26) { //Time beacon
            myplayer.timeBeaconDurationMod += 0.1f;
        }
        else if (spell == 30) { //Confusion shot
            myplayer.confusionShotDurationMod += 0.15f;
        }
        ShowSpellUpgrade(spell);
    }
    public void btnSpellUpgrade21() {
        if (myplayer.skillPoints >= Constants.SKILLPOINTS_COST_BASIC_UPGRADE) {
            myplayer.skillPoints -= Constants.SKILLPOINTS_COST_BASIC_UPGRADE;
            myplayer.spellLevels[spell]++;
        }
        else {
            Instantiate(textoAvisos, new Vector3(myplayer.transform.position.x, 1056, myplayer.transform.position.z), Quaternion.identity);
            return;
        }
        if (spell == 0) { //Bazooka
            myplayer.fblifesteal += 0.1f;
        }
        else if (spell == 5) { //basicaoe
            myplayer.basicAOECastingTimeMOD -= 0.15f;
        }
        else if (spell == 10) { //teleport
            myplayer.teleportRangeMod += 0.1f;
        }
        else if (spell == 11) {
            myplayer.rushCDMod -= 0.15f;
        }
        else if (spell == 15) { //barrage
            myplayer.barragedamagemod += 0.2f;
        }
        else if (spell == 16) { //q1

        }
        else if (spell == 20) { //reflector
            myplayer.reflectorCDMod -= 0.1f;
        }
        else if (spell == 21) { //petrify
            myplayer.metallizeDamageMod -= 0.1f;
        }
        else if (spell == 25) { //electric trap
            myplayer.electricTrapAreaMod += 0.2f;
        }
        else if (spell == 26) { //time beacon
            myplayer.timeBeaconCDMod -= 0.1f;
        }
        else if (spell == 30) { //Confusion shot
            myplayer.confusionShotDamageMod += 0.3f;
        }
        ShowSpellUpgrade(spell);
    }
    public void btnSpellUpgrade22() {
        if (myplayer.skillPoints >= Constants.SKILLPOINTS_COST_BASIC_UPGRADE) {
            myplayer.skillPoints -= Constants.SKILLPOINTS_COST_BASIC_UPGRADE;
            myplayer.spellLevels[spell]++;
        }
        else {
            Instantiate(textoAvisos, new Vector3(myplayer.transform.position.x, 1056, myplayer.transform.position.z), Quaternion.identity);
            return;
        }
        if (spell == 0) { //Bazooka           
            myplayer.fbdamagemod += 0.1f;
        }
        else if (spell == 5) { //basicaoe
            myplayer.basicAOEDamageMOD += 0.2f;
        }
        else if (spell == 10) { //teleport
            myplayer.teleportCDMod -= 0.1f;
        }
        else if (spell == 11) {
            myplayer.rushDamageMod += 0.2f;
        }
        else if (spell == 15) { //barrage
            myplayer.barrageCDmod -= 0.2f;
        }
        else if (spell == 16) { //q1

        }
        else if (spell == 20) { //reflector
            myplayer.reflectorDurationMod += 0.1f;
        }
        else if (spell == 21) { //petrify
            myplayer.metallizeCDMod -= 0.1f;
        }
        else if (spell == 25) { //electric trap
            myplayer.electricTrapDamageMod += 0.2f;
        }
        else if (spell == 26) { //Time beacon
            myplayer.timeBeaconDurationMod += 0.1f;
        }
        else if (spell == 30) { //Confusion shot
            myplayer.confusionShotCDMod -= 0.08f;
        }
        ShowSpellUpgrade(spell);
    }
    public void btnSpellUpgrade31() {
        if (myplayer.skillPoints >= Constants.SKILLPOINTS_COST_BASIC_UPGRADE) {
            myplayer.skillPoints -= Constants.SKILLPOINTS_COST_BASIC_UPGRADE;
            myplayer.spellLevels[spell]++;
        }
        else {
            Instantiate(textoAvisos, new Vector3(myplayer.transform.position.x, 1056, myplayer.transform.position.z), Quaternion.identity);
            return;
        }
        if (spell == 0) { //Bazooka           
            myplayer.fbrangemod += 0.1f;
            myplayer.fbdamagemod += 0.1f;
        }
        else if (spell == 5) { //basicaoe
            myplayer.basicAOEradiusMOD += 0.07f;
            myplayer.basicAOERecivedDamageMOD -= 0.1f;
        }
        else if (spell == 10) { //teleport
            myplayer.teleportRangeMod += 0.1f;
        }
        else if (spell == 11) { //rush
            myplayer.rushDamageMod += 0.1f;
            myplayer.rushForceMod += 0.15f;
        }
        else if (spell == 15) { //barrage
            myplayer.barrageRangemod += 0.1f;
            myplayer.barragedamagemod += 0.1f;
        }
        else if (spell == 16) { //q1

        }
        else if (spell == 20) { //reflector
            myplayer.reflectorCDMod -= 0.1f;
        }
        else if (spell == 21) { //petrify
            myplayer.metallizeDurationMod += 0.2f;
        }
        else if (spell == 25) { //electric trap
            myplayer.electricTrapDamageMod += 0.1f;
            myplayer.electricTrapDurationMod += 0.1f;
        }
        else if (spell == 26) { //time beacon
            myplayer.timeBeaconDurationMod += 0.1f;
        }
        else if (spell == 30) { //Confusion shot
            myplayer.confusionShotDurationMod += 0.15f;
        }
        ShowSpellUpgrade(spell);
    }
    public void btnSpellUpgrade41() {
        if (myplayer.skillPoints >= Constants.SKILLPOINTS_COST_BASIC_UPGRADE) {
            myplayer.skillPoints -= Constants.SKILLPOINTS_COST_BASIC_UPGRADE;
            myplayer.spellLevels[spell]++;
        }
        else {
            Instantiate(textoAvisos, new Vector3(myplayer.transform.position.x, 1056, myplayer.transform.position.z), Quaternion.identity);
            return;
        }
        if (spell == 0) { //Bazooka

            myplayer.fblifesteal += 0.1f;

        }
        else if (spell == 5) { //basicaoe
            myplayer.basicAOEradiusMOD += 0.07f;
            myplayer.basicAOEDamageMOD += 0.1f;
        }
        else if (spell == 10) { //teleport
            myplayer.teleportRangeMod += 0.1f;
        }
        else if (spell == 11) {
            myplayer.rushCDMod -= 0.15f;
        }
        else if (spell == 15) { //barrage
            myplayer.barragedamagemod += 0.2f;
        }
        else if (spell == 16) { //q2

        }
        else if (spell == 20) { //reflector
            myplayer.reflectorCDMod -= 0.1f;
        }
        else if (spell == 21) { //petrify
            myplayer.metallizeDamageMod -= 0.1f;
        }
        else if (spell == 25) { //electric trap
            myplayer.electricTrapAreaMod += 0.2f;
        }
        else if (spell == 26) { //time beacon
            myplayer.timeBeaconCDMod -= 0.1f;
        }
        else if (spell == 30) { //Confusion shot
            myplayer.confusionShotDamageMod += 0.3f;
        }
        ShowSpellUpgrade(spell);
    }
    public void btnSpellUpgrade42() {
        if (myplayer.skillPoints >= Constants.SKILLPOINTS_COST_BASIC_UPGRADE) {
            myplayer.skillPoints -= Constants.SKILLPOINTS_COST_BASIC_UPGRADE;
            myplayer.spellLevels[spell]++;
        }
        else {
            Instantiate(textoAvisos, new Vector3(myplayer.transform.position.x, 1056, myplayer.transform.position.z), Quaternion.identity);
            return;
        }
        if (spell == 0) { //Bazooka
            myplayer.fbrangemod += 0.2f;

        }
        else if (spell == 5) { //basicaoe
            myplayer.basicAOECastingTimeMOD -= 0.1f;
            myplayer.basicAOEDamageMOD += 0.1f;
        }
        else if (spell == 10) { //teleport
            myplayer.teleportCDMod -= 0.1f;
        }
        else if (spell == 11) {
            myplayer.rushDamageMod += 0.2f;
        }
        else if (spell == 15) { //barrage
            myplayer.barrageCDmod -= 0.2f;
        }
        else if (spell == 16) { //q1

        }
        else if (spell == 20) { //reflector
            myplayer.reflectorDurationMod += 0.1f;
        }
        else if (spell == 21) { //petrify
            myplayer.metallizeCDMod -= 0.1f;
        }
        else if (spell == 25) { //electric trap
            myplayer.electricTrapDamageMod += 0.2f;
        }
        else if (spell == 26) { //Time beacon
            myplayer.timeBeaconDurationMod += 0.1f;
        }
        else if (spell == 30) { //Confusion shot
            myplayer.confusionShotCDMod -= 0.08f;
        }
        ShowSpellUpgrade(spell);
    }
    public void btnSpellUpgrade51() {
        if (myplayer.skillPoints >= Constants.SKILLPOINTS_COST_BASIC_UPGRADE) {
            myplayer.skillPoints -= Constants.SKILLPOINTS_COST_BASIC_UPGRADE;
            myplayer.spellLevels[spell]++;
        }
        else {
            Instantiate(textoAvisos, new Vector3(myplayer.transform.position.x, 1056, myplayer.transform.position.z), Quaternion.identity);
            return;
        }
        if (spell == 0) { //Bazooka
            myplayer.fbrangemod += 0.1f;
            myplayer.fbdamagemod += 0.1f;

        }
        else if (spell == 5) { //basicaoe
            myplayer.basicAOEradiusMOD += 0.07f;
            myplayer.basicAOERecivedDamageMOD -= 0.1f;
        }
        else if (spell == 10) { //teleport
            myplayer.teleportRangeMod += 0.1f;
        }
        else if (spell == 11) { //rush
            myplayer.rushDamageMod += 0.1f;
            myplayer.rushForceMod += 0.15f;
        }
        else if (spell == 15) { //barrage
            myplayer.barrageRangemod += 0.1f;
            myplayer.barragedamagemod += 0.1f;
        }
        else if (spell == 16) { //q1

        }
        else if (spell == 20) { //reflector
            myplayer.reflectorCDMod -= 0.1f;
        }
        else if (spell == 21) { //petrify
            myplayer.metallizeDurationMod += 0.2f;
        }
        else if (spell == 25) { //electric trap
            myplayer.electricTrapDamageMod += 0.1f;
            myplayer.electricTrapDurationMod += 0.1f;
        }
        else if (spell == 26) { //time beacon
            myplayer.timeBeaconDurationMod += 0.1f;
        }
        else if (spell == 30) { //Confusion shot
            myplayer.confusionShotDurationMod += 0.15f;
        }
        ShowSpellUpgrade(spell);
    }
    public void btnSpellUpgrade61() {
        if (myplayer.skillPoints >= Constants.SKILLPOINTS_COST_ULTIMATE_UPGRADE) {
            myplayer.skillPoints -= Constants.SKILLPOINTS_COST_ULTIMATE_UPGRADE;
            myplayer.spellLevels[spell]++;
        }
        else {
            Instantiate(textoAvisos, new Vector3(myplayer.transform.position.x, 1056, myplayer.transform.position.z), Quaternion.identity);
            return;
        }
        if (spell == 0) { //Bazooka
            myplayer.fbsizemod += 2f;
            myplayer.fblifesteal += 0.2f;
            myplayer.fbdamagemod += 0.5f;
            myplayer.fbcastTimeMod += 1.3f;
        }
        else if (spell == 5) { //basicaoe
            myplayer.basicAOECastingTimeMOD -= 0.4f;
            myplayer.basicAOEradiusMOD -= 0.45f;
        }
        else if (spell == 10) { //teleport
            myplayer.teleportRangeMod += 0.5f;
        }
        else if (spell == 11) {
            myplayer.rushForceMod -= 0.6f;
            myplayer.rushCDMod -= 0.3f;
        }
        else if (spell == 15) { //barrage
            myplayer.doubleBarrage = true;
            myplayer.barrageCDmod += 0.3f;
            myplayer.barragedamagemod -= 0.2f;
        }
        else if (spell == 16) { //q2

        }
        else if (spell == 20) { //reflector
            myplayer.reflectorDurationMod *= 2;
            myplayer.reflectorBreakMod = true;
            myplayer.CmdSetReflectorBreakMod(true);
        }
        else if (spell == 21) { //metallize
            myplayer.metallizeMovespeedMod = 1;
        }
        else if (spell == 25) { //electric trap
            myplayer.electricTrapCastingTimeMod -= 0.5f;
        }
        else if (spell == 26) { //Time beacon
            myplayer.timeBeaconThrowMod = true;
        }
        else if (spell == 30) { //Confusion shot
            myplayer.confusionShotRangeMod *= 0.6f;
            myplayer.confusionShotDamageMod *= 0f;
            myplayer.confusionShotDurationMod += 0.8f;
        }
        ShowSpellUpgrade(spell);
    }
    public void btnSpellUpgrade62() {
        if (myplayer.skillPoints >= Constants.SKILLPOINTS_COST_ULTIMATE_UPGRADE) {
            myplayer.skillPoints -= Constants.SKILLPOINTS_COST_ULTIMATE_UPGRADE;
            myplayer.spellLevels[spell]++;
        }
        else {
            Instantiate(textoAvisos, new Vector3(myplayer.transform.position.x, 1056, myplayer.transform.position.z), Quaternion.identity);
            return;
        }
        if (spell == 0) { //Bazooka
            myplayer.fbdamagemod += 0.4f;
            myplayer.fbrangemod -= 0.5f;

        }
        else if (spell == 5) { //basicaoe          
            myplayer.basicAOEradiusMOD += 0.23f;
            myplayer.basicAOEDamageMOD -= 0.2f;
        }
        else if (spell == 10) { //teleport
            myplayer.teleportCDMod -= 0.3f;
        }
        else if (spell == 11) {
            myplayer.rushForceMod -= 0.6f;
            myplayer.rushDamageMod += 0.4f;
        }
        else if (spell == 15) { //barrage
            myplayer.barragedamagemod += 0.2f;
            myplayer.barrageLifeStealMod += 0.5f;
            myplayer.barrageCDmod += 0.2f;
        }
        else if (spell == 16) { //q2

        }
        else if (spell == 20) { //reflector
            myplayer.reflectorMoveSpeed = 1.3f;
        }
        else if (spell == 21) { //petrify
            myplayer.metallizeDragMod = 3f;
        }
        else if (spell == 25) { //electric trap
            myplayer.electricTrapDamageMod = 0;
            myplayer.electricTrapDurationMod += 0.5f;
        }
        else if (spell == 26) { //time beacon
            myplayer.timeBeaconReduceAllCDMod = true;
        }
        else if (spell == 30) { //Confusion shot
            myplayer.confusionShotDamageMod += 1.4f;
            myplayer.confusionShotDurationMod -= 0.5f;
        }
        ShowSpellUpgrade(spell);
    }
    public void btnSpellUpgrade63() {
        if (myplayer.skillPoints >= Constants.SKILLPOINTS_COST_ULTIMATE_UPGRADE) {
            myplayer.skillPoints -= Constants.SKILLPOINTS_COST_ULTIMATE_UPGRADE;
            myplayer.spellLevels[spell]++;
        }
        else {
            Instantiate(textoAvisos, new Vector3(myplayer.transform.position.x, 1056, myplayer.transform.position.z), Quaternion.identity);
            return;
        }
        if (spell == 0) { //Bazooka
            myplayer.fbdamagemod -= 0.3f;
            myplayer.fbdouble = true;
        }
        else if (spell == 5) { //basicaoe
            myplayer.basicAOERecivedDamageMOD -= 0.7f;
        }
        else if (spell == 10) { //teleport
            myplayer.teleportModDouble = true;
            myplayer.teleportCDMod += 0.3f;
            myplayer.teleportRangeMod -= 0.5f;
        }
        else if (spell == 11) {
            myplayer.directedRush = true;
        }
        else if (spell == 15) { //barrage
            myplayer.barrageRangemod += 0.4f;
            myplayer.missileBarrageCastingTimeMod -= 0.2f;
            myplayer.barrageCDmod -= 0.2f;
        }
        else if (spell == 16) { //q2

        }
        else if (spell == 20) { //reflector
            myplayer.reflectorInvisibleMod = true;
        }
        else if (spell == 21) { //petrify
            myplayer.metallizeDamageMod -= 0.3f;
            myplayer.metallizeMovespeedMod -= 0.15f;
        }
        else if (spell == 25) { //electric trap
            myplayer.electricTrapDamageMod -= 0.2f;
            myplayer.electricTrapAreaMod += 0.6f;
        }
        else if (spell == 26) { //time beacon
            myplayer.timeBeaconSpeedMod = true;
        }
        else if (spell == 30) { //Confusion shot
            myplayer.confusionShotRandomKnockback = true;
        }
        ShowSpellUpgrade(spell);
    }

    #endregion

}
