using UnityEngine;
using System.Collections;

public class NutPointMarkerBehaviourScript : MonoBehaviour {

	int dieTimer = 0;
	TextMesh tm;
	Color c;

	// Use this for initialization
	void Start () {
		tm = GetComponent<TextMesh>();
		c = tm.color;
	}

	// Update is called once per frame
	void Update () {
		dieTimer++;

		c.a = c.a-=0.01f;
		tm.color = c;
		transform.localScale = new Vector3(transform.localScale.x+0.0002f, transform.localScale.y+0.0002f, 1f);

		if (dieTimer > 90){
			Destroy(gameObject);
		}
	}
}
