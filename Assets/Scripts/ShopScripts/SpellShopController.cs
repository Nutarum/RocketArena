using UnityEngine;
using UnityEngine.UI;

public class SpellShopController : MonoBehaviour {

    PlayerInputController myplayer;
    public SpellUpgradeController spellUpgradeController;
    public SpellChoiceController spellChoiceController;

    public Canvas spellShopCanvas;
    public Canvas shopMenuImageCanvas;
    public Text spellpointsText;

    
    public Image[] spellShopImages = { null, null, null, null, null, null, null };

    // Use this for initialization
    void Start() {
        shopMenuImageCanvas.enabled = false;
        spellShopCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update() {

    }


    public void showShop() {
        if (shopMenuImageCanvas.enabled == true) {
            shopMenuImageCanvas.enabled = false;
            spellChoiceController.HideChoiceSpell();
            spellUpgradeController.HideSpellUpgrade();
        }
        else {
            shopMenuImageCanvas.enabled = true;
            spellChoiceController.HideChoiceSpell();
            spellUpgradeController.HideSpellUpgrade();
        }

    }


    // 0: M1 --- 1: M2 --- 2: Space --- 3: Q ---- 4: E ---- 5:R
    public void clickSpellUpgradeShop(int boton) {
        //if u still had no skill selected for that button, we open the skill choice panel
        if (myplayer.spells[boton] == -1) {
            spellChoiceController.ShowChoiceSpell(boton);
            spellUpgradeController.HideSpellUpgrade();
        }
        //if u already had that skill, we open the skill upgrade 
        else {
            int spell = myplayer.spells[boton];
            spellUpgradeController.ShowSpellUpgrade(spell);
            spellChoiceController.HideChoiceSpell();
        }
    }

    public void ShowSpellShop() {
        spellShopCanvas.enabled = true;
        shopMenuImageCanvas.enabled = false;
        spellChoiceController.HideChoiceSpell();
        spellUpgradeController.HideSpellUpgrade();
        PointsUpdate();
    }

    public void HideSpellShop() {
        spellChoiceController.HideChoiceSpell();
        spellUpgradeController.HideSpellUpgrade();
        spellShopCanvas.enabled = false;
    }

    public void PointsUpdate() {
        spellpointsText.text = "Skill points: " + myplayer.skillPoints;
    }

    //we set the player gameobject for all shop controller, this is called from playerInputController
    //this is needed because we cant set a prefab as a gameobject from the unity editor
    internal void setPlayer(PlayerInputController playerInputController) {
        myplayer = playerInputController;
        spellUpgradeController.setPlayer(myplayer);
        spellChoiceController.setPlayer(myplayer);
        spellShopCanvas.enabled = true;
        PointsUpdate();
    }

}