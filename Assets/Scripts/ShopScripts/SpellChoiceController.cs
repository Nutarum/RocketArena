using UnityEngine.UI;
using UnityEngine;

public class SpellChoiceController : MonoBehaviour {

    PlayerInputController myplayer;
    public SpellShopController spellShopController;

    public GameObject textoAvisos;

    public Image choice1;
    public Image choice2;

    public Text choice1Text;
    public Text choice2Text;

    int boton;

    public Canvas spellChoiceCanvas;

    private string[] skillDescriptions = { "","","","","", //m1
         "","","","","", //m2
        "Teleport \nTeleports you in the targeted direction.",
        "Rush \nPushes you in the targeted direccion, \n damaging any enemy you hit whit your body.",
        "",
        "",
        "",

        "Barrage \nFires a fast barrage of small missiles.", //q
        "",
        "",
        "",
        "",

        "Reflector \nGrants a temporary shield that reflects enemy projectiles.", //e
        "Metalize \nTurn yourself into metal, reducing movespeed, recieved damage and knockback.",
        "",
        "",
        "",

        "Electric trap \nPlaces a trap that immobilizes and damages the first enemy that walks on it.", //r
        "Time beacon \nPlaces a Time beacon on the ground, after a short time, you return to the time beacon and regain all health lost since the placement.",
        "",
        "",
        "",

        "Confusion shot \nConfuses an enemy, reversing its direction of movement", //f
        "",
        "",
        "",
        ""
    };

    // Use this for initialization
    void Start () {
        spellChoiceCanvas.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    //learn the selected skill
    public void onClickSpellChoice(int spell) {
        //we recieve a number from 0-4, we have to convert this number depending on the button we are at
        //for example skills on Q buttom have the (15-19) numbers
        int convertedSpell = (boton * 5) + spell;

        //if we have the points to buy this skill
        if (myplayer.skillPoints >= Constants.SKILLPOINTS_COST_NEW_SPELL) {
            myplayer.skillPoints-= Constants.SKILLPOINTS_COST_NEW_SPELL;
            myplayer.spells[boton] = convertedSpell;
            myplayer.spellLevels[convertedSpell]++;
            
            spellShopController.spellShopImages[boton].sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon"+ convertedSpell + "Normal");

            GameObject.Find("spellBarImage" + boton).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + convertedSpell + "Normal");
            
            spellShopController.PointsUpdate();

            HideChoiceSpell();
        }
        else {
            Instantiate(textoAvisos, new Vector3(myplayer.transform.position.x, 1056, myplayer.transform.position.z), Quaternion.identity);
    
        }
      
    }



    //images and descriptions for each skill (repeated code cause all the skills doesnt have necessarily same number of choices)
    public void ShowChoiceSpell(int boton) {
        this.boton = boton;
        
        //if this skill exists (first skill for this button)
        if (!skillDescriptions[((boton * 5) + 0)].Equals("")) {
            choice1.enabled = true;
            choice1Text.enabled = true;
            choice1.sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + ((boton * 5) + 0) + "Normal");
            choice1Text.text = skillDescriptions[((boton * 5) + 0)];
        }
        else {
            choice1.enabled = false;
            choice1Text.enabled = false;
        }

        //if this skill exists (second skill for this button)
        if (!skillDescriptions[((boton * 5) + 1)].Equals("")) {
            choice2.enabled = true;
            choice2Text.enabled = true;
            choice2.sprite = Resources.Load<Sprite>("Sprites/SpellButtonsSprites/spellicon" + ((boton * 5) + 1) + "Normal");
            choice2Text.text = skillDescriptions[((boton * 5) + 1)];
        }
        else {
            choice2.enabled = false;
            choice2Text.enabled = false;
        }
           

      
        spellChoiceCanvas.enabled = true;
    }


    public void HideChoiceSpell() {
        spellChoiceCanvas.enabled = false;
    }


    internal void setPlayer(PlayerInputController playerInputController) {
        myplayer = playerInputController;
    }
}
