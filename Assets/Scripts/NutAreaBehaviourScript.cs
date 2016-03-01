using UnityEngine;
using System.Collections;

public class NutAreaBehaviourScript : MonoBehaviour {

	bool containsPlayer = false;
	SpriteRenderer sr;
	bool isFading = false;
	float fadeTarget = 1f;
	Color myC;

	// Use this for initialization
	void Start () {
		sr = transform.parent.gameObject.GetComponent<SpriteRenderer>();
		myC = sr.color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.name == "player" && containsPlayer == false){

	        containsPlayer = true;
	        fadeTarget = 0.6f;

	        if (isFading == false){
				StartCoroutine(fadeOpacity());
	        }
		}
    }

    void OnTriggerExit2D(Collider2D other)
    {
		if (other.name == "player" && containsPlayer == true){

	        containsPlayer = false;
	        fadeTarget = 1f;
	        
	        if (isFading == false){
				StartCoroutine(fadeOpacity());
	        }
		}
    }

    IEnumerator fadeOpacity(){

    	isFading = true;

    	while (sr.color.a != fadeTarget){
    		myC.a = Mathf.Lerp(myC.a, fadeTarget, Time.deltaTime*5f);
			sr.color = myC;
			yield return new WaitForSeconds(0.01f);
    	}

    	isFading = false;
    }
}
