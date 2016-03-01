using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class SpiderSackBehaviourScript : Enemy {

	HingeJoint2D spiderRopeJoint;
	float targetLength;

    List<Transform> spider_ropes = new List<Transform>();
    List<SpriteRenderer> spider_rope_sr = new List<SpriteRenderer>();

    bool reachedWebLength = false;
    // List<HingeJoint2D> spider_rope_joints = new List<HingeJoint2D>();

	// Use this for initialization
	void Start () {

		spiderRopeJoint = GetComponent<HingeJoint2D>();

		nutPoints = 1;
		lives = 2;
		jumpAmount = 5;
		addToMultiplier = false;
		targetLength = UnityEngine.Random.Range(2f, 3f);

        foreach (Transform child in transform){
            if (child.name == "spider_rope_1"){
				spider_ropes.Add(child);
				spider_rope_sr.Add(child.GetComponent<SpriteRenderer>());	
				// spider_rope_joints.Add(child.GetComponent<HingeJoint2D>());
            } else if (child.name == "spider_rope_2"){
				spider_ropes.Add(child);
				spider_rope_sr.Add(child.GetComponent<SpriteRenderer>());
				// spider_rope_joints.Add(child.GetComponent<HingeJoint2D>());
            } else if (child.name == "spider_rope_3"){
				spider_ropes.Add(child);
				spider_rope_sr.Add(child.GetComponent<SpriteRenderer>());
				// spider_rope_joints.Add(child.GetComponent<HingeJoint2D>());
            } else if (child.name == "spider_rope_4"){
				spider_ropes.Add(child);
				spider_rope_sr.Add(child.GetComponent<SpriteRenderer>());
				// spider_rope_joints.Add(child.GetComponent<HingeJoint2D>());
			} else if (child.name == "spider_rope_5"){
				spider_ropes.Add(child);
				spider_rope_sr.Add(child.GetComponent<SpriteRenderer>());
				// spider_rope_joints.Add(child.GetComponent<HingeJoint2D>());
            } else if (child.name == "spider_rope_6"){
				spider_ropes.Add(child);
				spider_rope_sr.Add(child.GetComponent<SpriteRenderer>());
				// spider_rope_joints.Add(child.GetComponent<HingeJoint2D>());
            } else if (child.name == "spider_rope_7"){
				spider_ropes.Add(child);
				spider_rope_sr.Add(child.GetComponent<SpriteRenderer>());
				// spider_rope_joints.Add(child.GetComponent<HingeJoint2D>());
			} else if (child.name == "spider_rope_8"){
				spider_ropes.Add(child);
				spider_rope_sr.Add(child.GetComponent<SpriteRenderer>());
				// spider_rope_joints.Add(child.GetComponent<HingeJoint2D>());
			}
		}

	}

    public IEnumerator dieDelayWeb(){

		for (int i = 0; i < 3; i++) {

			for(int x = 0; x < spider_rope_sr.Count; x++){
				spider_rope_sr[x].enabled = false;
			}
	        yield return new WaitForSeconds(0.5f);
			for(int x = 0; x < spider_rope_sr.Count; x++){
				spider_rope_sr[x].enabled = true;
			}
	        yield return new WaitForSeconds(0.5f);
		}

		for(int x = spider_ropes.Count-1; x >= 0; x--){
			spider_ropes[x].gameObject.SetActive(false);
		}
    }

	// Update is called once per frame
	void FixedUpdate () {
		if (reachedWebLength == false){
			for(int x = spider_ropes.Count-1; x >= 0; x--){
				spider_ropes[x].localScale = new Vector3(1f, spider_ropes[x].transform.localScale.y + UnityEngine.Random.Range(0.01f, 0.05f), 1f);
			}
			if (spider_ropes[0].localScale.y >= targetLength){
				reachedWebLength = true;
			}
		}
	}

	public override void tryKill(){

		lives--;
		if (isDead == false && lives <= 0){

			isDead = true;
			spiderRopeJoint.enabled = false;
	        StartCoroutine(dieDelay());
	        StartCoroutine(dieDelayWeb());
			StartCoroutine(blink());


		}
	}
}
