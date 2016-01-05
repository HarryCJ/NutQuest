using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool isDead = false;
	public bool isProtected = false;
	public int lives = 1;
	public bool isBouncy = true;
	public int nutPoints = 0;
	public int jumpAmount = 16;
	public bool addToMultiplier = true;
	public string enemyType = "default";
	public int[] jumpLevels = new int[]{15, 16, 17, 18};

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

	public virtual void tryKill(){
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

	public IEnumerator blink(){

		SpriteRenderer sr = GetComponent<SpriteRenderer>();

		for (int i = 0; i < 12; i++) {

			sr.enabled = false;
	        yield return new WaitForSeconds(0.2f);
			sr.enabled = true;
	        yield return new WaitForSeconds(0.2f);
		}
    }

	public IEnumerator dieDelay(){
		Physics2D.IgnoreCollision(GameObject.Find("player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
        yield return new WaitForSeconds(5);
		Destroy(gameObject);
    }

}
