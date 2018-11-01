using UnityEngine;
using UnityEngine.UI;

public class DebugInfoScript : MonoBehaviour {
    public Text fpsText;
    public float deltaTime;   

    void Update() {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        string debugText = "";
        debugText += "Fps: " + Mathf.Ceil(fps).ToString() + "\n";             

        fpsText.text = debugText;
    }
}