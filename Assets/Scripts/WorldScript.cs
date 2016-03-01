using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class WorldScript : MonoBehaviour {
    // public PolygonCollider2D pc;
    // Use this for initialization

    List<GameObject> gameObjects = new List<GameObject>();
    public int timer = 20;

    List<GameObject> waveDisplayUIText  = new List<GameObject>();
    List<Text> waveDisplayUITextText  = new List<Text>();

    public IDictionary<string, int> upgrades = new Dictionary<string, int>()
    {
        {"nuts_increase", 0},
        {"frogs_increase", 0},
        {"mush_increase", 0},
        {"frog_boost", 0},
        {"spider_boost", 0},
        {"bird_boost", 0},
        {"default_boost", 0}
    };

    GameObject storeUIBottle;
    GameObject storeUIItemTitleText;
    GameObject storeUIItemDescText;
    GameObject storeUIItemCostText;
    Image storeUIBottle_sr;
    Text storeUIItemTitleText_text;
    Text storeUIItemDescText_text;
    Text storeUIItemCostText_text;
    Sprite[] storeSprites;
    string current_upgrade_selected;
    int current_upgrade_cost;
    int current_upgrade_max;

    GameObject player;
    GameObject clouds;
    GameObject enemies;
    GameObject pickups;
    GameObject snow_falling;
    GameObject leaf_falling;
    GameObject leaves;
    GameObject leaves_transition_mask;
    GameObject tree_mask_fade;
    GameObject tree_mask_leaves;
    GameObject landscape_mask_snow;
    GameObject grass_foreground;
    GameObject landscape_background;
    PlayerBehaviourScript playerBS;
	Rigidbody2D playerRB;
	Collider2D playercollider;
    GameObject nutArea;
    GameObject nutAreaCross;
    Collider2D nutAreaCollider;
    Collider2D nutAreaCrossCollider;
    SpriteRenderer leavesSR;
    Color leavesColor;

    GameObject branches_foreground;
    GameObject branches_shadows;
    GameObject branch_shadow_template;

    List<Collider2D> branches_foreground_colliders  = new List<Collider2D>();

    SpriteRenderer landscape_mask_snow_sr;
    SpriteRenderer grass_foreground_sr;
    SpriteRenderer landscape_background_sr;

    public bool spawnedLeaves = false;
    public bool recededLeaves = false;

    public string season = "summer";
    public float nutDelay = 2f;
    public float frogDelay = 5f;
    public float mushDelay = 6f;
    public float spiderDelay = 20f;
    public float birdDelay = 5f;

    GameObject storeUI;

    void Start () {

        storeUI = GameObject.Find("storeUI");
        storeSprites = Resources.LoadAll<Sprite>(@"store");
        storeUIBottle = GameObject.Find("storeUIBottle");
        storeUIItemTitleText = GameObject.Find("storeUIItemTitleText");
        storeUIItemDescText = GameObject.Find("storeUIItemDescText");
        storeUIItemCostText = GameObject.Find("storeUIItemCostText");
        storeUIBottle_sr = storeUIBottle.GetComponent<Image>();
        storeUIItemTitleText_text = storeUIItemTitleText.GetComponent<Text>();
        storeUIItemDescText_text = storeUIItemDescText.GetComponent<Text>();
        storeUIItemCostText_text = storeUIItemCostText.GetComponent<Text>();

        nutArea = GameObject.Find("nutArea");
        nutAreaCross = GameObject.Find("nutAreaCross");
        nutAreaCollider = nutArea.GetComponent<Collider2D>();
        nutAreaCrossCollider = nutAreaCross.GetComponent<Collider2D>();

        clouds = GameObject.Find("clouds");
        enemies = GameObject.Find("enemies");
        pickups = GameObject.Find("pickups");

        snow_falling = GameObject.Find("snow_falling");
        leaf_falling = GameObject.Find("leaf_falling");
        player = GameObject.Find("player");
        leaves = GameObject.Find("leaves");
        leaves_transition_mask = GameObject.Find("leaves_transition_mask");
        tree_mask_fade = GameObject.Find("tree_mask_fade");
        tree_mask_leaves = GameObject.Find("tree_mask_leaves");
        landscape_mask_snow = GameObject.Find("landscape_mask_snow");
        grass_foreground = GameObject.Find("grass_foreground");
        landscape_background = GameObject.Find("landscape_background");
        playerBS = player.GetComponent<PlayerBehaviourScript>();
        playerRB = player.GetComponent<Rigidbody2D>();
        playercollider = player.GetComponent<Collider2D>();
        leavesSR = leaves.GetComponent<SpriteRenderer>();
        leavesColor = leavesSR.color;
        landscape_mask_snow_sr = landscape_mask_snow.GetComponent<SpriteRenderer>();
        grass_foreground_sr = grass_foreground.GetComponent<SpriteRenderer>();
        landscape_background_sr = landscape_background.GetComponent<SpriteRenderer>();

		waveDisplayUIText.Add(GameObject.Find("waveDisplayUIText1"));
		waveDisplayUITextText.Add(waveDisplayUIText[0].GetComponent<Text>());
		waveDisplayUIText.Add(GameObject.Find("waveDisplayUIText1.5"));
		waveDisplayUITextText.Add(waveDisplayUIText[1].GetComponent<Text>());
		waveDisplayUIText.Add(GameObject.Find("waveDisplayUIText2"));
		waveDisplayUITextText.Add(waveDisplayUIText[2].GetComponent<Text>());
		waveDisplayUIText.Add(GameObject.Find("waveDisplayUIText2.5"));
		waveDisplayUITextText.Add(waveDisplayUIText[3].GetComponent<Text>());
		waveDisplayUIText.Add(GameObject.Find("waveDisplayUIText3"));
		waveDisplayUITextText.Add(waveDisplayUIText[4].GetComponent<Text>());

        branches_foreground = GameObject.Find("branches_foreground");
        branches_shadows = GameObject.Find("branches_shadows");
        branch_shadow_template = GameObject.Find("branch_shadow_template");

        foreach (Transform child in branches_foreground.transform){

            branches_foreground_colliders.Add(child.GetComponent<Collider2D>());
        //     GameObject g = Instantiate(branch_shadow_template);//new GameObject();
        //     g.transform.parent = branches_shadows.transform;
        //     // g.transform.position = child.position;
        //     g.transform.position = new Vector3(child.position.x*1.1f, child.position.y-0.05f, 2f);
        //     g.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        //     SpriteRenderer sr = g.GetComponent<SpriteRenderer>();
        //     SpriteRenderer childSR = child.gameObject.GetComponent<SpriteRenderer>();
        //     sr.sprite = childSR.sprite;
        //     sr.enabled = true;
        //     // break;

        }

        // addSpider(new Vector2(0f, -4f));

        // addFrog(new Vector3(0, 10, 0), true);
        // addFrog(new Vector3(10, 10, 0), false);
        // addFrog(new Vector3(-10, 10, 0), true);

        //        GameObject newFrog = new GameObject("froggy");

        //        newFrog.transform.localScale = new Vector3(10, 10, 1);
        //        SpriteRenderer frogSR = newFrog.AddComponent<SpriteRenderer>();
        //        frogSR.sortingOrder = 100;
        //
        //        pc = newFrog.AddComponent<PolygonCollider2D>();
        //        pc.SetPath(0, new Vector2[] { new Vector2(0f, 0.03f), new Vector2(-0.05f, -0.01f), new Vector2(-0.06f, -0.04f), new Vector2(0.04f, -0.04f), new Vector2(0.06f, 0.04f)});
        //
        //        //BoxCollider2D bc = newFrog.AddComponent<BoxCollider2D>();
        //        //bc.size = new Vector3(0.12f, 0.08f, 1f);
        //
        //        Rigidbody2D rb = newFrog.AddComponent<Rigidbody2D>();
        //        rb.fixedAngle = true;
        //        rb.mass = 10;
        //        FrogBehaviourScript frogBS = newFrog.AddComponent<FrogBehaviourScript>();

        StartCoroutine(spawnBirds());
        StartCoroutine(spawnFrogs());
        StartCoroutine(spawnSpiders());
        StartCoroutine(spawnClouds());
        // StartCoroutine(brownLeaves());

        StartCoroutine(CountdownWave());
        StartCoroutine(SnowGenerator());
        StartCoroutine(spawnNuts());

        StartCoroutine(blowSnow());
        addRaven(new Vector3(0f, 0f, 0f), true);
        season = "summer";
        // timer = 3;
    }

    public void disableBranchCollider(Collider2D c){
        // for (int x = 0; x < branches_foreground_colliders.size; x++) {
        foreach (Collider2D cc in branches_foreground_colliders){
            Physics2D.IgnoreCollision(cc, c);
        }
    }

    public void enableBranchCollider(Collider2D c){
        // for (int x = 0; x < branches_foreground_colliders.size; x++) {
        foreach (Collider2D cc in branches_foreground_colliders){
            Physics2D.IgnoreCollision(cc, c);
        }
    }

    public void addClockPoints(int num){
        timer += num;
        StartCoroutine(WaveDisplayTransition());
    } 

    public void subtractClockPoints(int num){
        timer -= num;
        StartCoroutine(WaveDisplayTransition());
    } 

    IEnumerator spawnNuts(){

        Debug.Log ("spawnNuts");
        //Random rnd = new Random();
        int i = 0;
        int foundTenacity = 0;

        while (true)
        {
            float delay = UnityEngine.Random.Range(nutDelay, nutDelay+1f);
            // delay = UnityEngine.Random.Range(0.5f, 0.5f);
            yield return new WaitForSeconds(delay);

            //get random vector

            bool foundV = false;
            Vector3 newV = new Vector3();
            foundTenacity = 0;
            while (foundV != true && foundTenacity < 200){

                newV = new Vector3(UnityEngine.Random.Range(-16f, 16f), UnityEngine.Random.Range(0f, 22f), 0);

                if (nutAreaCollider.OverlapPoint(newV)){
                    foundV = true;
                }
                foundTenacity++;
            }

            addNut(newV);

            i++;
        }
    }

    void addNut(Vector3 pos){
        GameObject mynut = null;
        int dice = UnityEngine.Random.Range(0, 25);
        if (dice == 0) {
            mynut = Instantiate(Resources.Load("Prefabs/apple")) as GameObject;
        } else if (dice == 1) {
            mynut = Instantiate(Resources.Load("Prefabs/banana")) as GameObject;
        } else if (dice == 2) {
            mynut = Instantiate(Resources.Load("Prefabs/orange")) as GameObject;
        } else {
            mynut = Instantiate(Resources.Load("Prefabs/nut")) as GameObject;
        }
        mynut.transform.position = pos;
    }

    IEnumerator spawnFrogs()
    {
        while (true)
        {
            float delay = UnityEngine.Random.Range(frogDelay, frogDelay+1f);
            // delay = UnityEngine.Random.Range(0.5f, 0.5f);
            yield return new WaitForSeconds(delay);

            if (UnityEngine.Random.Range(0, 2) == 0){

                addFrog(new Vector3(25, -3, 0), false);

            } else {

                addFrog(new Vector3(-25, -3, 0), true);

            }

        }
    }

    IEnumerator blowSnow()
    {
        Debug.Log("blowSnow");
        yield return new WaitForSeconds(15f);
        Debug.Log("start");
        float targetX = snow_falling.transform.position.x-30f;
        for (int x = 0; x < 500; x++)
        {
            snow_falling.transform.position = Vector3.Lerp(snow_falling.transform.position, new Vector3(targetX, 0f, 0f), Time.deltaTime*2f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator spawnSpiders()
    {
        while (true)
        {
            float delay = UnityEngine.Random.Range(spiderDelay, spiderDelay+1f);
            // float delay = UnityEngine.Random.Range(5f, 5f);
            yield return new WaitForSeconds(delay);


            int foundTenacity = 0;

            // while (foundTenacity < 100){

            bool foundV = false;
            Vector3 newV = new Vector3();

            while (foundV != true){
                newV = new Vector3(UnityEngine.Random.Range(-16f, 16f), UnityEngine.Random.Range(0f, 22f), 0);

                foreach (Collider2D child in branches_foreground_colliders){
                    if (child.OverlapPoint(newV)){
                        foundV = true;
                    }
                    foundTenacity++;
                    Debug.Log(foundTenacity);
                }
            }

            Debug.Log("found");
            addSpider(newV);

        }
    }

    IEnumerator spawnBirds()
    {
        while (true)
        {
            float delay = UnityEngine.Random.Range(birdDelay, birdDelay+1f);
            // delay = UnityEngine.Random.Range(0.5f, 0.5f);
            yield return new WaitForSeconds(delay);

            if (UnityEngine.Random.Range(0, 2) == 0){

                addBird(new Vector3(25, 4, 0), false);

            } else {

                addBird(new Vector3(-25, 4, 0), true);

            }
        }
    }

    IEnumerator spawnClouds()
    {
        while (true)
        {
            float delay = UnityEngine.Random.Range(10f, 15f);
            yield return new WaitForSeconds(delay);

            GameObject mycloud = Instantiate(Resources.Load("Prefabs/cloud")) as GameObject;
            mycloud.transform.parent = clouds.transform;
            CloudBehaviourScript cloudBS = mycloud.GetComponent<CloudBehaviourScript>();

    		//pick sprite
    		SpriteRenderer sr = mycloud.GetComponent<SpriteRenderer>();
    		Sprite[] sprites = Resources.LoadAll<Sprite>(@"sprites");
    		string[] cloudNames = new string[]{"cloud_1", "cloud_2"};
    		int cloudSprite = UnityEngine.Random.Range(0, cloudNames.Length);
    		for (int x = 0; x < sprites.Length; x++) {
    			if (sprites[x].name == cloudNames[cloudSprite]){
    				sr.sprite = sprites[x];
    			}
    		}

    		//pick opacity
    		Color myC = sr.color;
    		myC.a = UnityEngine.Random.Range(0.2f, 0.9f);
    		sr.color = myC;

    		//pick speed
    		cloudBS.speed = UnityEngine.Random.Range(0.001f, 0.005f);

    		//pick location
    		cloudBS.mypos = new Vector3(-25f, UnityEngine.Random.Range(0.2f, 10f), 10f);

            //pick size
        }
    }

    private static System.Random rng = new System.Random();

    public void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    // IEnumerator spawnSnow()
    // {   
    //     bool snowedLandscaped = false;

    //     Sprite[] sprites = Resources.LoadAll<Sprite>(@"snow_sprites");
    //     string[] snowNames = new string[]{"snow_0", "snow_1", "snow_2", "snow_3", "snow_4", "snow_5"};
    //     List<int> snowPosList = new List<int>();
    //     for (int i = 0; i < 65; i++) {
    //         snowPosList.Add(i);
    //     }

    //     float delay = 2.5f;
    //     int skipSnowPos = 60;
    //     for (int snow_level = 0; snow_level < 30; snow_level++) {
    //         // float delay = UnityEngine.Random.Range(0.05f, 0.05f);

    //         Shuffle(snowPosList);

    //         for (int i = skipSnowPos; i < 45; i++) { //65
    //             GameObject mysnow = Instantiate(Resources.Load("Prefabs/snow_falling_particle")) as GameObject;
    //             mysnow.transform.parent = snow_falling.transform;
    //             // CloudBehaviourScript cloudBS = mysnow.GetComponent<CloudBehaviourScript>();

    //             // float posX = UnityEngine.Random.Range(-20f, 40f);
    //             float posX = (float) snowPosList[i]-20;
    //             // if (posX > 40f){
    //             //     posX = 40f;
    //             // } else if (posX > 30f){
    //             //     posX = 30f;
    //             // } else if (posX > 20f){
    //             //     posX = 20f;
    //             // } else if (posX > 10f){
    //             //     posX = 10f;
    //             // } else if (posX > 0f){
    //             //     posX = 0f;
    //             // } else if (posX > -10f){
    //             //     posX = -10f;
    //             // } else {
    //             //     posX = -20f;
    //             // }
    //             //(float) Math.Round(posX / 1f) * 1f
    //             mysnow.transform.position = new Vector3(posX, 20f, UnityEngine.Random.Range(6f, -15f));

    //             Rigidbody2D snowrigidbody = mysnow.GetComponent<Rigidbody2D>();

    //     		//pick sprite
    //     		SpriteRenderer sr = mysnow.GetComponent<SpriteRenderer>();
    //     		Color myC = sr.color;

    //             snowrigidbody.angularVelocity = UnityEngine.Random.Range(5f, 50f);

    //             int snowSprite = UnityEngine.Random.Range(0, snowNames.Length);
    //             for (int x = 0; x < sprites.Length; x++) {
    //                 if (sprites[x].name == snowNames[snowSprite]){
    //                     sr.sprite = sprites[x];
    //                 }
    //             }

    //             float opacity = 1f;

    //             if (mysnow.transform.position.z >= 5f){
    //                 mysnow.transform.position = new Vector3(mysnow.transform.position.x, mysnow.transform.position.y, 5f);
    //                 sr.sortingOrder = -105;
    //                 mysnow.transform.localScale = new Vector3(6.5f, 6.5f, 1f);
    //                 snowrigidbody.velocity = new Vector2(UnityEngine.Random.Range(-0.5f, -1.5f), UnityEngine.Random.Range(-0.5f, -1.5f));
    //                 myC.a = 1f;
    //             } else if (mysnow.transform.position.z >= 1f){
    //                 mysnow.transform.position = new Vector3(mysnow.transform.position.x, mysnow.transform.position.y, 1f);
    //                 sr.sortingOrder = -86;
    //                 mysnow.transform.localScale = new Vector3(7.5f, 7.5f, 1f);
    //                 snowrigidbody.velocity = new Vector2(UnityEngine.Random.Range(-1f, -2f), UnityEngine.Random.Range(-1f, -2f));
    //                 myC.a = 0.9f;
    //             } else if (mysnow.transform.position.z >= 0f){
    //                 mysnow.transform.position = new Vector3(mysnow.transform.position.x, mysnow.transform.position.y, 0f);
    //                 sr.sortingOrder = -1;
    //                 mysnow.transform.localScale = new Vector3(8f, 8f, 1f);
    //                 snowrigidbody.velocity = new Vector2(UnityEngine.Random.Range(-1.5f, -2.5f), UnityEngine.Random.Range(-1.5f, -2.5f));
    //                 myC.a = 0.8f;
    //             } else if (mysnow.transform.position.z > -12f){
    //                 mysnow.transform.position = new Vector3(mysnow.transform.position.x, mysnow.transform.position.y, -12f);
    //                 sr.sortingOrder = 900;
    //                 mysnow.transform.localScale = new Vector3(8.5f, 8.5f, 1f);
    //                 snowrigidbody.velocity = new Vector2(UnityEngine.Random.Range(-2f, -3f), UnityEngine.Random.Range(-2f, -3f));
    //                 myC.a = 0.7f;
    //             } else {
    //                 mysnow.transform.position = new Vector3(mysnow.transform.position.x, mysnow.transform.position.y, -15f);
    //                 sr.sortingOrder = 1005;
    //                 mysnow.transform.localScale = new Vector3(10f, 10f, 1f);
    //                 snowrigidbody.velocity = new Vector2(UnityEngine.Random.Range(-2.5f, -3.5f), UnityEngine.Random.Range(-2.5f, -3.5f));
    //                 myC.a = 0.6f;
    //             }
    //             sr.color = myC;

    //         }

    //         yield return new WaitForSeconds(delay);

    //         //space between rows
    //         if (snow_level <= 16){
    //             if (delay > 0.5f){
    //                 delay -= 0.5f;
    //             }
    //         } else{
    //             delay += 0.5f;
    //         }

    //         //number of particles
    //         if (snow_level <= 15){
    //             if (skipSnowPos > 0){
    //                 int x = 65 - skipSnowPos;
    //                 x = x + x/3;
    //                 skipSnowPos = 65 - x;
    //                 if (skipSnowPos < 0){
    //                     skipSnowPos = 0;
    //                 } else if (skipSnowPos >= 65-3){
    //                     skipSnowPos--;
    //                 }
    //             }
    //         } else {
    //             int x = skipSnowPos;
    //             x = x + x/2;
    //             skipSnowPos = x;
    //             if (skipSnowPos < 5){
    //                 skipSnowPos = 10;
    //             }
    //         }

    //         if (snow_level == 20 && snowedLandscaped == false){
    //             StartCoroutine(snowLandscape());
    //             snowedLandscaped = true;
    //         }

    //         if (snow_level == 20 && season == "winter"){
    //             snow_level = 10;
    //             skipSnowPos = 30;
    //         }

    // 		//pick speed
    // 		// cloudBS.speed = UnityEngine.Random.Range(0.001f, 0.005f);

    // 		//pick location
    // 		// cloudBS.mypos = new Vector3(-25f, UnityEngine.Random.Range(0.2f, 10f), 0.5f);

    //         //pick size
    //     }
    // }

    IEnumerator SnowGenerator()
    {   
        // bool snowedLandscaped = false;
        int t = timer;
        if (t < 0){
            t = 0;
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>(@"snow_sprites");
        string[] snowNames = new string[]{"snow_0", "snow_1", "snow_2", "snow_3", "snow_4", "snow_5"};
        List<int> snowPosList = new List<int>();
        for (int i = 0; i < 100; i++) {
            snowPosList.Add(i);
        }

        while(true){

            if (season != "winter" || timer < 10){

                yield return new WaitForSeconds(1f);

            } else {

                int skipSnowPos = 133 - timer;
                if (skipSnowPos < 0){
                    skipSnowPos = 0;
                } else if (skipSnowPos > 100){
                    skipSnowPos = 100;
                } else {
                    // skipSnowPos = skipSnowPos / 4;
                }
                // Debug.Log("skipSnowPos");
                // Debug.Log(skipSnowPos);

                // skipSnowPos = 30;

                Shuffle(snowPosList);

                for (int i = skipSnowPos; i < 100; i++) { //65
                    GameObject mysnow = Instantiate(Resources.Load("Prefabs/snow_falling_particle")) as GameObject;
                    mysnow.transform.parent = snow_falling.transform;

                    float posX = (float) snowPosList[i]-30;

                    mysnow.transform.position = new Vector3(posX, 20f, UnityEngine.Random.Range(6f, -15f));

                    Rigidbody2D snowrigidbody = mysnow.GetComponent<Rigidbody2D>();

                    //pick sprite
                    SpriteRenderer sr = mysnow.GetComponent<SpriteRenderer>();
                    Color myC = sr.color;

                    snowrigidbody.angularVelocity = UnityEngine.Random.Range(5f, 50f);

                    int snowSprite = UnityEngine.Random.Range(0, snowNames.Length);
                    for (int x = 0; x < sprites.Length; x++) {
                        if (sprites[x].name == snowNames[snowSprite]){
                            sr.sprite = sprites[x];
                        }
                    }

                    float opacity = 1f;

                    if (mysnow.transform.position.z >= 5f){
                        mysnow.transform.position = new Vector3(mysnow.transform.position.x, mysnow.transform.position.y, 5f);
                        sr.sortingOrder = -105;
                        mysnow.transform.localScale = new Vector3(6.5f, 6.5f, 1f);
                        snowrigidbody.velocity = new Vector2(UnityEngine.Random.Range(-0.5f, -1.5f), UnityEngine.Random.Range(-0.5f, -1.5f));
                        myC.a = 1f;
                    } else if (mysnow.transform.position.z >= 1f){
                        mysnow.transform.position = new Vector3(mysnow.transform.position.x, mysnow.transform.position.y, 1f);
                        sr.sortingOrder = -86;
                        mysnow.transform.localScale = new Vector3(7.5f, 7.5f, 1f);
                        snowrigidbody.velocity = new Vector2(UnityEngine.Random.Range(-1f, -2f), UnityEngine.Random.Range(-1f, -2f));
                        myC.a = 0.9f;
                    } else if (mysnow.transform.position.z >= 0f){
                        mysnow.transform.position = new Vector3(mysnow.transform.position.x, mysnow.transform.position.y, 0f);
                        sr.sortingOrder = -1;
                        mysnow.transform.localScale = new Vector3(8f, 8f, 1f);
                        snowrigidbody.velocity = new Vector2(UnityEngine.Random.Range(-1.5f, -2.5f), UnityEngine.Random.Range(-1.5f, -2.5f));
                        myC.a = 0.8f;
                    } else if (mysnow.transform.position.z > -12f){
                        mysnow.transform.position = new Vector3(mysnow.transform.position.x, mysnow.transform.position.y, -12f);
                        sr.sortingOrder = 900;
                        mysnow.transform.localScale = new Vector3(8.5f, 8.5f, 1f);
                        snowrigidbody.velocity = new Vector2(UnityEngine.Random.Range(-2f, -3f), UnityEngine.Random.Range(-2f, -3f));
                        myC.a = 0.7f;
                    } else {
                        mysnow.transform.position = new Vector3(mysnow.transform.position.x, mysnow.transform.position.y, -15f);
                        sr.sortingOrder = 1005;
                        mysnow.transform.localScale = new Vector3(10f, 10f, 1f);
                        snowrigidbody.velocity = new Vector2(UnityEngine.Random.Range(-2.5f, -3.5f), UnityEngine.Random.Range(-2.5f, -3.5f));
                        myC.a = 0.6f;
                    }
                    sr.color = myC;

                    // if (snow_level == 20 && snowedLandscaped == false){
                    //     StartCoroutine(snowLandscape());
                    //     snowedLandscaped = true;
                    // }
                }

                // Debug.Log("WaitForSeconds");
                // Debug.Log(t);
                // Debug.Log((t/30f).ToString("0.0000"));
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

	IEnumerator snowLandscape(){
        Debug.Log("snowLandscape");
        Color landscape_mask_snow_c = landscape_mask_snow_sr.color;
        Color grass_foreground_c = grass_foreground_sr.color;
        Color landscape_background_c = landscape_background_sr.color;
        // Debug.Log(grass_foreground_c.r);
        // Debug.Log(grass_foreground_c.g);
        // Debug.Log(grass_foreground_c.b);
        // Debug.Log(landscape_background_c.r);
        // Debug.Log(landscape_background_c.g);
        // Debug.Log(landscape_background_c.b);
        while (landscape_mask_snow.transform.position.y > -1.5f)
        {   
            landscape_mask_snow.transform.position = new Vector3(landscape_mask_snow.transform.position.x, landscape_mask_snow.transform.position.y-0.01f, landscape_mask_snow.transform.position.z);
            landscape_mask_snow_c.a = Mathf.Lerp(landscape_mask_snow_c.a, 1f, Time.deltaTime*0.75f);
            landscape_mask_snow_sr.color = landscape_mask_snow_c;

            grass_foreground_c.r = Mathf.Lerp(grass_foreground_c.r, 1f, Time.deltaTime*0.1f);
            grass_foreground_c.g = Mathf.Lerp(grass_foreground_c.g, 1f, Time.deltaTime*0.1f);
            grass_foreground_c.b = Mathf.Lerp(grass_foreground_c.b, 1f, Time.deltaTime*0.1f);
            grass_foreground_sr.color = grass_foreground_c;

            landscape_background_c.r = Mathf.Lerp(landscape_background_c.r, 0.9f, Time.deltaTime*0.1f);
            landscape_background_c.g = Mathf.Lerp(landscape_background_c.g, 0.9f, Time.deltaTime*0.1f);
            landscape_background_c.b = Mathf.Lerp(landscape_background_c.b, 0.9f, Time.deltaTime*0.1f);
            landscape_background_sr.color = landscape_background_c;
            yield return new WaitForSeconds(0.005f);
        }
    }

    IEnumerator unsnowLandscape(){
        Debug.Log("unsnowLandscape");
        Color landscape_mask_snow_c = landscape_mask_snow_sr.color;
        Color grass_foreground_c = grass_foreground_sr.color;
        Color landscape_background_c = landscape_background_sr.color;
        while (landscape_mask_snow.transform.localPosition.y < 2.25f)
        {
            // Debug.Log(landscape_mask_snow.transform.position.y);
            landscape_mask_snow.transform.position = new Vector3(landscape_mask_snow.transform.position.x, landscape_mask_snow.transform.position.y+0.02f, landscape_mask_snow.transform.position.z);
            landscape_mask_snow_c.a = Mathf.Lerp(landscape_mask_snow_c.a, 0f, Time.deltaTime*0.75f);
            landscape_mask_snow_sr.color = landscape_mask_snow_c;

            grass_foreground_c.r = Mathf.Lerp(grass_foreground_c.r, 0.2f, Time.deltaTime*0.15f);
            grass_foreground_c.g = Mathf.Lerp(grass_foreground_c.g, 0.560f, Time.deltaTime*0.15f);
            grass_foreground_c.b = Mathf.Lerp(grass_foreground_c.b, 0.152f, Time.deltaTime*0.15f);
            grass_foreground_sr.color = grass_foreground_c;

            landscape_background_c.r = Mathf.Lerp(landscape_background_c.r, 0.137f, Time.deltaTime*0.15f);
            landscape_background_c.g = Mathf.Lerp(landscape_background_c.g, 0.247f, Time.deltaTime*0.15f);
            landscape_background_c.b = Mathf.Lerp(landscape_background_c.b, 0.101f, Time.deltaTime*0.15f);
            landscape_background_sr.color = landscape_background_c;
            // Debug.Log(landscape_mask_snow.transform.position.y);
            yield return new WaitForSeconds(0.005f);
        }
    }

	IEnumerator brownLeaves(){

        spawnedLeaves = false;
        recededLeaves = false;

        while (leavesColor.r <= 0.8f && leavesColor.g >= 0.35f)
        {
    		//pick opacity
    		leavesColor.r = Mathf.Lerp(leavesColor.r, 0.85f, Time.deltaTime*2f);
    		leavesColor.g = Mathf.Lerp(leavesColor.g, 0.3f, Time.deltaTime*2f);
    		// leavesColor.b += 0.1f;
    		leavesSR.color = leavesColor;

            if (leavesColor.r > 0.6f && spawnedLeaves == false && recededLeaves == false) {

                spawnedLeaves = true;
                recededLeaves = true;

                StartCoroutine(spawnLeaves());
                StartCoroutine(recedeLeaves());
                // StartCoroutine(spawnSnow());
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

	IEnumerator recedeLeaves(){
        Vector3 targetLoc = new Vector3(leaves.transform.localPosition.x, 5f, leaves.transform.localPosition.z);
        while (leaves.transform.localPosition.y < 4.5f){

            // leavesColor.a = Mathf.Lerp(leavesColor.a, 0f, Time.deltaTime*0.5f);
            // leavesSR.color = leavesColor;
            // leaves_transition_mask.transform.position = new Vector3(leaves_transition_mask.transform.position.x, leaves_transition_mask.transform.position.y-0.1f, leaves_transition_mask.transform.position.z);
            // = new Vector3(leaves.transform.position.x, leaves.transform.position.y+0.05f, leaves.transform.position.z);
            if (leaves.transform.localPosition.y < 1f){
                leaves.transform.localPosition = Vector2.Lerp(leaves.transform.localPosition, targetLoc, Time.deltaTime/12);
            } else {
                leaves.transform.localPosition = Vector2.Lerp(leaves.transform.localPosition, targetLoc, Time.deltaTime/3);
            }
            tree_mask_fade.transform.localPosition = new Vector3(tree_mask_fade.transform.localPosition.x, tree_mask_fade.transform.localPosition.y+10f, tree_mask_fade.transform.localPosition.z);
            tree_mask_leaves.transform.localPosition = new Vector3(tree_mask_leaves.transform.localPosition.x, tree_mask_leaves.transform.localPosition.y+10f, tree_mask_leaves.transform.localPosition.z);

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator regrowLeaves(){

        leavesColor.r = 0.20f;
        leavesColor.g = 0.5607843f;
        // leavesColor.b += 0.1f;
        leavesSR.color = leavesColor;

        Vector3 targetLoc = new Vector3(leaves.transform.localPosition.x, -0.5f, leaves.transform.localPosition.z);
        while (leaves.transform.localPosition.y > 0f){

            // leavesColor.a = Mathf.Lerp(leavesColor.a, 0f, Time.deltaTime*0.5f);
            // leavesSR.color = leavesColor;
            // leaves_transition_mask.transform.position = new Vector3(leaves_transition_mask.transform.position.x, leaves_transition_mask.transform.position.y-0.1f, leaves_transition_mask.transform.position.z);
            // = new Vector3(leaves.transform.position.x, leaves.transform.position.y+0.05f, leaves.transform.position.z);
            // if (leaves.transform.localPosition.y < 1f){
            //     leaves.transform.localPosition = Vector2.Lerp(leaves.transform.localPosition, targetLoc, Time.deltaTime/12);
            // } else {
            leaves.transform.localPosition = Vector2.Lerp(leaves.transform.localPosition, targetLoc, Time.deltaTime/3);
            // }
            tree_mask_fade.transform.localPosition = new Vector3(tree_mask_fade.transform.localPosition.x, tree_mask_fade.transform.localPosition.y-10f, tree_mask_fade.transform.localPosition.z);
            tree_mask_leaves.transform.localPosition = new Vector3(tree_mask_leaves.transform.localPosition.x, tree_mask_leaves.transform.localPosition.y-10f, tree_mask_leaves.transform.localPosition.z);

            yield return new WaitForSeconds(0.01f);
        }
    }

	IEnumerator spawnLeaves(){

        int foundTenacity = 0;
        float waitTime = 0.005f;

        while (foundTenacity < 100){

            bool foundV = false;
            Vector3 newV = new Vector3();
            foundTenacity = 0;
            while (foundV != true && foundTenacity < 100){
                newV = new Vector3(UnityEngine.Random.Range(-16f, 16f), UnityEngine.Random.Range(0f, 22f), 0);

                if (nutAreaCollider.OverlapPoint(newV) && nutAreaCrossCollider.OverlapPoint(newV)){
                    foundV = true;
                }
                foundTenacity++;
                // Debug.Log(foundTenacity);
            }
            if (foundV == true){
                // Debug.Log("found");
                GameObject myleaf = Instantiate(Resources.Load("Prefabs/leaf_falling_particle")) as GameObject;
                myleaf.transform.parent = leaf_falling.transform;

                foreach (Transform child in myleaf.transform){
                    SpriteRenderer myleaf_sr = child.GetComponent<SpriteRenderer>();
                    myleaf_sr.color = leavesSR.color;
                }
                myleaf.transform.position = newV;

            } else {
                // Debug.Log("break");
                break;
            }

            // Debug.Log("yield");
            yield return new WaitForSeconds(waitTime);
            waitTime+=waitTime/50;
        }
    }

    public void setItemView(string s){
        // int current_item_level = upgrades[s];
        current_upgrade_selected = s;
        int[] upgrade_cost_levels = new int[]{};

        switch (s){

            case "nuts_increase":
                storeUIItemTitleText_text.text = "Increase Nuts";
                storeUIItemDescText_text.text = "Makes nuts grow faster!";
                current_upgrade_max = 3;
                upgrade_cost_levels = new int[]{25,50,100,200};
                break;

            case "frogs_increase":
                storeUIItemTitleText_text.text = "Increase Frogs";
                storeUIItemDescText_text.text = "Makes more frogs appear!";
                current_upgrade_max = 3;
                upgrade_cost_levels = new int[]{25,50,100,200};
                break;

            case "mush_increase":
                storeUIItemTitleText_text.text = "Increase Mushrooms";
                storeUIItemDescText_text.text = "Makes mushrooms grow more often!";
                current_upgrade_max = 3;
                upgrade_cost_levels = new int[]{25,50,100,200};
                break;

            case "frog_boost":
                storeUIItemTitleText_text.text = "Increase Frog Boost";
                storeUIItemDescText_text.text = "Makes jumping on frogs boost you higher!";
                current_upgrade_max = 3;
                upgrade_cost_levels = new int[]{25,50,100,200};
                break;

            default:
                Debug.Log("Error: option \""+s+"\" not recognised.");
                break;
        }

        current_upgrade_cost = upgrade_cost_levels[upgrades[current_upgrade_selected]];
        storeUIItemCostText_text.text = current_upgrade_cost.ToString();
        if (playerBS.nuts < upgrade_cost_levels[upgrades[current_upgrade_selected]]){
             storeUIItemCostText_text.color = new Color(0.846f, 0f, 0f);
         } else {
             storeUIItemCostText_text.color = new Color(0.149f, 0.149f, 0.149f);
         }

        for (int x = 0; x < storeSprites.Length; x++) {
            if (storeSprites[x].name == "bar_"+upgrades[current_upgrade_selected]+"_"+current_upgrade_max.ToString()){
                storeUIBottle_sr.sprite = storeSprites[x];
            }
        }
    }

    public void buyItemPress(){
        if (playerBS.nuts >= current_upgrade_cost && upgrades[current_upgrade_selected] < current_upgrade_max){
            
            upgrades[current_upgrade_selected] += 1;
            playerBS.subtractNutPoints(current_upgrade_cost);

            switch (current_upgrade_selected){

                case "nuts_increase":
                    if (nutDelay > 0.5f){
                        nutDelay = nutDelay - 0.5f;
                    }
                    break;
                
                case "frogs_increase":
                    if (frogDelay > 0.5f){
                        frogDelay = frogDelay - 0.5f;
                    }
                    break;
                
                case "mush_increase":
                    if (mushDelay > 0.5f){
                        mushDelay = mushDelay - 0.5f;
                    }
                    break;
                
                case "frog_boost":
                    if (playerBS.frogBoost > 0.5f){
                        playerBS.frogBoost = playerBS.frogBoost - 0.5f;
                    }
                    break;

                default:
                    Debug.Log("Error: option \""+current_upgrade_selected+"\" not recognised.");
                    break;
            }

            setItemView(current_upgrade_selected);
        }
    }

    public void nextWavePress(){
        StartCoroutine(startNextWave());
    }

    public void addFrog(Vector3 pos, bool directionIsRight){
      	GameObject myfrog = Instantiate(Resources.Load("Prefabs/frog")) as GameObject;
        myfrog.transform.parent = enemies.transform;
      	myfrog.transform.position = pos;
        FrogBehaviourScript frogBS = myfrog.GetComponent<FrogBehaviourScript>();
        disableBranchCollider(myfrog.GetComponent<Collider2D>());
        frogBS.directionIsRight = directionIsRight;
    }

    void addBird(Vector3 pos, bool directionIsRight){
      	GameObject mybird = Instantiate(Resources.Load("Prefabs/bird")) as GameObject;
        mybird.transform.parent = enemies.transform;
      	mybird.transform.position = pos;
        BirdBehaviourScript birdBS = mybird.GetComponent<BirdBehaviourScript>();
        birdBS.directionIsRight = directionIsRight;
    }

    void addRaven(Vector3 pos, bool directionIsRight){
        GameObject mybird = Instantiate(Resources.Load("Prefabs/raven")) as GameObject;
        disableBranchCollider(mybird.GetComponent<Collider2D>());
        mybird.transform.parent = enemies.transform;
        mybird.transform.position = pos;
        // BirdBehaviourScript birdBS = mybird.GetComponent<BirdBehaviourScript>();
        // birdBS.directionIsRight = directionIsRight;
    }

    void addSpider(Vector3 pos){
        // Vector2 pos = new Vector2(0f, -4f);
      	GameObject myspider = Instantiate(Resources.Load("Prefabs/spider_web")) as GameObject;
        myspider.transform.parent = enemies.transform;
      	myspider.transform.position = pos;
        SpiderBehaviourScript spiderBS = myspider.GetComponent<SpiderBehaviourScript>();

  //       foreach (Transform child in myspider.transform){
  //           if (child.name == "spider_rope_4"){
		// 		HingeJoint2D spider_rope_4_joint = child.GetComponent<HingeJoint2D>();
  //               spider_rope_4_joint.connectedAnchor = new Vector2(pos.x, pos.y+13f);
		// 	}
		// }

        // frogBS.directionIsRight = directionIsRight;
    }

    // Update is called once per frame
    void FixedUpdate () {
    }

	IEnumerator CountdownWave(){
        timer = 100;

        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
            StartCoroutine(WaveDisplayTransition());

            if (timer == 15){
                if (season == "summer"){
                } else if (season == "winter"){
                    // season = "summer";
                    // timer = 999;
                }
            }
        }

        if (season == "summer"){
            season = "winter";
            StartCoroutine(brownLeaves());
            StartCoroutine(snowLandscape());

        } else if (season == "winter"){
            season = "summer";
            StartCoroutine(regrowLeaves());
            StartCoroutine(unsnowLandscape());
        }

        while (storeUI.transform.localPosition.x < 0f){
            storeUI.transform.localPosition = Vector2.Lerp(storeUI.transform.localPosition, new Vector2(5f,0f), Time.deltaTime*5f);
            yield return new WaitForSeconds(0.01f);
        }
        storeUI.transform.localPosition = new Vector2(0f,0f);
        Time.timeScale = 0;
        // StartCoroutine(CountdownWave());
    }



    IEnumerator startNextWave(){

        Time.timeScale = 1;
        while (storeUI.transform.localPosition.x < 800f){
            storeUI.transform.localPosition = Vector2.Lerp(storeUI.transform.localPosition, new Vector2(805f,0f), Time.deltaTime*5f);
            yield return new WaitForSeconds(0.01f);
        }
        storeUI.transform.localPosition = new Vector2(-800f,0f);

        StartCoroutine(CountdownWave());
    }


	IEnumerator WaveDisplayTransition(){

		foreach (Text t in waveDisplayUITextText){
			t.text = "";
		}

		string waveTimerString = timer.ToString();
		if (waveTimerString.Length == 1){
			waveDisplayUITextText[2].text = waveTimerString;
		} else if (waveTimerString.Length == 2){
			waveDisplayUITextText[1].text = waveTimerString.Substring(0, 1);
			waveDisplayUITextText[3].text = waveTimerString.Substring(1, 1);
		} else if (waveTimerString.Length == 3){
			waveDisplayUITextText[0].text = waveTimerString.Substring(0, 1);
			waveDisplayUITextText[2].text = waveTimerString.Substring(1, 1);
			waveDisplayUITextText[4].text = waveTimerString.Substring(2, 1);
		}

		// waveDisplayUITextText[2].fontSize = 100;
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 80;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 84;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 88;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 92;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 96;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 100;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 96;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 92;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 88;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 84;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 80;
		}
	}
}
