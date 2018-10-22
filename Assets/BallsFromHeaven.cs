using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsFromHeaven : MonoBehaviour {
    public GameObject ball;
    public float startHeight = 10.0f;
    public float fireRate = 0.5f;

    private float nextBallTime = 0.0f;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > nextBallTime)
        {
            nextBallTime = Time.time + fireRate;
            Vector3 randomPosition = new Vector3(Random.Range(-4.0f, 4.0f), startHeight, Random.Range(-4.0f, 4.0f));
            Instantiate(ball, randomPosition, Quaternion.identity);
        }
	}
}
