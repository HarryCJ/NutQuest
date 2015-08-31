using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool isDead = false;
	public bool isProtected = false;
	public int lives = 1;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

	public virtual void die(){
		Debug.Log("ENEMY DIES!!!");
	}

	public IEnumerator protect(){

		isProtected = true;

		SpriteRenderer sr = GetComponent<SpriteRenderer>();

		sr.enabled = false;
        yield return new WaitForSeconds(0.2f);
		sr.enabled = true;
        yield return new WaitForSeconds(0.2f);
		sr.enabled = false;
        yield return new WaitForSeconds(0.2f);
		sr.enabled = true;

		isProtected = false;
    }

}
