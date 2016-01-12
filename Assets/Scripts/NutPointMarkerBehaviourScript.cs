using UnityEngine;
using System.Collections;

public class NutPointMarkerBehaviourScript : MonoBehaviour {

	int dieTimer = 0;
	TextMesh tm;
	Color c;
	SpriteRenderer clockSR;
	bool appeared = false;

	// Use this for initialization
	void Start () {
		tm = GetComponent<TextMesh>();
		c = tm.color;
		

	}

	public void setBlackColor(){
		c.r = 0f;
		c.g = 0f;
		c.b = 0f;
		GetComponent<TextMesh>().color = c;
		foreach (Transform child in transform){
			child.GetComponent<SpriteRenderer>().color = c;
		}
	}

	public void setClock(){

		foreach (Transform child in transform){
			clockSR = child.GetComponent<SpriteRenderer>();
		}
		clockSR.enabled = true;
	}

	// Update is called once per frame
	void Update () {
		dieTimer++;

		if (appeared == true){
			c.a = c.a-=0.02f;
		} else {
			c.a = c.a+=0.05f;
			if (c.a >= 1f){
				appeared = true;
			}
		}
		tm.color = c;
		if (clockSR != null){
			clockSR.color = c;
		}
		
		transform.localScale = new Vector3(transform.localScale.x+0.0002f, transform.localScale.y+0.0002f, 1f);

		if (dieTimer > 150){
			Destroy(gameObject);
		}
	}
}
