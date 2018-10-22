using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGame : MonoBehaviour {
    public GameObject ball;
    public float startHeight = 10.0f;
    public float fireRate = 5f;
    public Transform head;

    private float nextBallTime = 0.0f;
    private GameObject activeBall;
    private AudioSource audio;
    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextBallTime)
        {
            nextBallTime = Time.time + fireRate;
            audio.Play();
            Vector3 position = new Vector3(head.position.x,
              startHeight, head.position.z + 0.2f);
            activeBall = Instantiate(ball, position,
              Quaternion.identity) as GameObject;
        }
    }
}
