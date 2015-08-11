using UnityEngine;
using System.Collections;

public class CloudBehaviourScript : MonoBehaviour {

	public float speed = 0.001f;
	public Vector3 mypos = new Vector2(100f, 100f);

	// Use this for initialization
	void Start () {
		if (mypos.x == 100f && mypos.y == 100f){
			 mypos = new Vector2(transform.position.x, transform.position.y);
		}
		transform.position = mypos;
	}

	// Update is called once per frame
	void Update () {

		// Vector3 mypos = new Vector2(transform.position.x, transform.position.y);
		mypos.x += speed;
		transform.position = mypos;
	}
}
