using UnityEngine;
using System.Collections;

public class SnowBehaviourScript : MonoBehaviour {

	Rigidbody2D myrigidbody;

	public bool isRed;

	// Use this for initialization
	void Start () {

		myrigidbody = GetComponent<Rigidbody2D>();
		myrigidbody.velocity = new Vector2(-1.8f, -1.8f);
		isRed = false;

		StartCoroutine(checkOut());
	}

	// Update is called once per frame
	// void Update () {

	// }

	// Update is called once per frame
	public void startBleeping () {
		StartCoroutine(bleep());
	}

	IEnumerator bleep(){

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        // Color myC = sr.color;

		while (true){

			if (isRed){
				sr.color = new Color(1f, 1f, 1f, 1f); // Set to white
			} else {
				sr.color = new Color(1f, 0f, 0f, 1f); // Set to red
			}
			isRed = !isRed;

	    	yield return new WaitForSeconds(1f);
		}
    }

	IEnumerator checkOut(){
		while (true){

			if (transform.position.x > 60f || transform.position.y > 30f || transform.position.x < -60f || transform.position.y < -10f){
				Destroy(transform.gameObject);
			}

	    	yield return new WaitForSeconds(5f);
		}
    }

}
