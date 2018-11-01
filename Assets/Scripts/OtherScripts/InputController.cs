using System;
using UnityEngine;


public class InputController : MonoBehaviour {

    //TO CHANGE INPUT CONFIGURATION, FROM UNITY EDITOR: EDIT -> PROJECT SETTINGS -> INPUT
    //KEYBOARD - CONTROLLER
    // mouse 1 - R2
    // mouse 2 - Y
    // space   - L2
    //   Q     - R1
    //   E     - X
    //   R     - L1 
    // number 1- A
    // TAB     - select

    internal bool usingController = false;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool isM1Pressed() {
        if (Input.GetAxis("M1")>0 || Input.GetButton("M1")) {
            return true;
        }
        return false;
    }

    public bool isM2Pressed() {
        return Input.GetButton("M2");        
    }

    public bool isSpacePressed() {
        if (Input.GetAxis("SPACE") > 0 || Input.GetButton("SPACE")) {
            return true;
        }
        return false;
    }

    public bool isQPressed() {
        return Input.GetButton("Q");        
    }
    public bool isEPressed() {
        return Input.GetButton("E");
    }
    public bool isRPressed() {        
        return Input.GetButton("R");
    }
    public bool isFPressed() {
        return Input.GetButton("F");
    }

    internal bool isNumber1Pressed() {
        return Input.GetButton("Number1");
    }

    public float getAxisHorizontal() {       
        return Input.GetAxis("Horizontal");
    }
    public float getAxisVertical() {
        return Input.GetAxis("Vertical");
    }

    public bool isTabPressed() {
        return Input.GetButton("Tab");
    }

    private float viejoMirarX = 0;
    private float viejoMirarZ = 0;

    public Vector3 mousePosition() {
        if (usingController) {
            if (Math.Abs(Input.GetAxis("MirarX") + Input.GetAxis("MirarZ")) > 0.05) {
                viejoMirarX = Input.GetAxis("MirarX");
                viejoMirarZ = Input.GetAxis("MirarZ");
                return new Vector3(Screen.width/2 + Input.GetAxis("MirarX") * Screen.width / 2, Screen.height / 2 + Input.GetAxis("MirarZ") * Screen.height / 2, 0);
            }
            else {
                return new Vector3(Screen.width / 2 + viejoMirarX * Screen.width / 2, Screen.height / 2 + viejoMirarZ * Screen.height / 2, 0);
            }
           
        }
        else {
            return Input.mousePosition;
        }
    }

    
}
