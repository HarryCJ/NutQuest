﻿using UnityEngine;
using System.Collections;

public class NutSpawnerBehaviourScript : MonoBehaviour {

    Collider2D mycollider;

    // Use this for initialization
    void Start () {

        mycollider = GetComponent<Collider2D>();
        StartCoroutine(spawnNuts());
    }

	// Update is called once per frame
    void Update()
    {
	}

	IEnumerator spawnNuts(){

		Debug.Log ("spawnNuts");
		//Random rnd = new Random();
		int i = 0;

        while (true)
        {
            float delay = UnityEngine.Random.Range(0.5f, 3);
            yield return new WaitForSeconds(delay);

			//get random vector

			bool foundV = false;
            Vector3 newV = new Vector3();
            while (foundV == false){
                newV = new Vector3(UnityEngine.Random.Range(-16f, 16f), UnityEngine.Random.Range(0f, 22f), 0f);

                if (mycollider.OverlapPoint(newV)){
					foundV = true;
				}
			}

            addNut(newV);

            i++;
        }
    }

    void addNut(Vector3 pos){
        GameObject mynut = null;
        if (UnityEngine.Random.Range(0, 20) == 0)
        {
            mynut = Instantiate(Resources.Load("apple")) as GameObject;
        } else
        {
            mynut = Instantiate(Resources.Load("nut")) as GameObject;
        }
        mynut.transform.position = pos;
    }
}