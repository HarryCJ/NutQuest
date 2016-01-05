using UnityEngine;
using System.Collections;

public class SnowBehaviourScript : MonoBehaviour {

	Rigidbody2D myrigidbody;

	// Use this for initialization
	void Start () {

		myrigidbody = GetComponent<Rigidbody2D>();
		myrigidbody.velocity = new Vector2(-1.8f, -1.8f);

		StartCoroutine(checkOut());
	}

	// Update is called once per frame
	void Update () {

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
