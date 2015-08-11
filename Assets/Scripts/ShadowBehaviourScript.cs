using UnityEngine;
using System.Collections;

public class ShadowBehaviourScript : MonoBehaviour {

	// Use this for initialization
	SpriteRenderer sr;
	void Start () {

		sr = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {

		Ray landingRay = new Ray();
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.parent.position, Vector3.down);
		for (int i = 0; i < hits.Length; i++) {
            RaycastHit2D hit = hits[i];
			if (hit.collider.tag.Contains("env")){
				float dif = transform.parent.position.y - hit.point.y;
				transform.position = new Vector2(transform.parent.position.x, hit.point.y);
				transform.rotation = Quaternion.Euler(transform.parent.rotation.x, transform.parent.rotation.x, -transform.parent.rotation.z);
				Color myC = sr.color;
	    		myC.a = 0.3f-(dif/30);
	    		sr.color = myC;
			}
		}
		// Debug.Log(hit.collider.gameObject.name);
		// if (hit.collider.gameObject.tag == "env"){
		// 	Debug.Log(hit.collider.gameObject.name);
		// }
//
	}
}
