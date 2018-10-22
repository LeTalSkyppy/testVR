using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour {
    public float lifeTime = 1.0f;
    public GameObject hitMiss;

    private GameObject heatMap;
    private float timer;
	// Use this for initialization
	void Start () {
        timer = 0.0f;
        heatMap = GameObject.Find("heatMap");
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > lifeTime)
        {
            Vector3 positionHit = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2.0f);
            Instantiate(hitMiss, positionHit, transform.rotation, heatMap.transform);
            Destroy(gameObject);
        }           
	}
}
