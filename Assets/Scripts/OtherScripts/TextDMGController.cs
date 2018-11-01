using UnityEngine;

public class TextDMGController : MonoBehaviour {

    private float duration = 1;

	// Use this for initialization
	void Start () {
       
        if (transform.position.y > 0) {
            GetComponent<TextMesh>().color = Color.red;
            GetComponent<TextMesh>().text = transform.position.y + "";
        }
        else {
            GetComponent<TextMesh>().color = Color.green;
            GetComponent<TextMesh>().text = -transform.position.y + "";
        }

        if (transform.position.y == 1056) {
            GetComponent<TextMesh>().text = "Insufficient points for get this upgrade!";
        }

        transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        transform.LookAt(Camera.main.transform.forward*100);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
    }
	
	// Update is called once per frame
	void Update () {
        duration -= Time.deltaTime;
        transform.position += new Vector3(0, 0.05f, 0);
        transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        if (duration<0)
            Destroy(this.gameObject);
    }
}
