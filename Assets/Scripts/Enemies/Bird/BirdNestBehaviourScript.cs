using UnityEngine;
using System.Collections;

public class BirdNestBehaviourScript : MonoBehaviour {

	Transform bird_nest_inside_trigger;
	bird_nest_inside_trigger_script bird_nest_inside_trigger_s;

	// Use this for initialization
	void Start () {

        foreach (Transform child in transform){
            if (child.name == "bird_nest_inside_trigger"){
                 bird_nest_inside_trigger = child;
                 bird_nest_inside_trigger_s = child.GetComponent<bird_nest_inside_trigger_script>();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
