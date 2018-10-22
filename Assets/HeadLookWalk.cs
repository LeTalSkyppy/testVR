using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLookWalk : MonoBehaviour {
    public float velocity = 0.7f;
    public Transform camera;
    public bool isWalking = false;

    private Clicker clicker = new Clicker();
    private CharacterController controller;
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (clicker.clicked())
        {
            isWalking = !isWalking;
        }
        if (isWalking)
        {
            controller.SimpleMove(camera.forward * velocity);
        }
        Vector3 moveDirecton = camera.forward;
        moveDirecton *= velocity * Time.deltaTime;
        moveDirecton.y = 0.0f;
        controller.Move(moveDirecton);
	}
}
