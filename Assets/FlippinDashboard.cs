using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippinDashboard : MonoBehaviour {
    private HeadGesture gesture;
    private GameObject dashboard;
    private bool isOpen = true;
    private Vector3 startRotation;
    private float timer = 0.0f;
    private float timerReset = 2.0f;
	// Use this for initialization
	void Start () {
        gesture = GetComponent<HeadGesture>();
        dashboard = GameObject.Find("Dashboard");
        startRotation = dashboard.transform.eulerAngles;
        closeDashboard();
	}
	
	// Update is called once per frame
	void Update () {
        if (gesture.isMovingDown)
        {
            openDashboard();
        }
        else if(!gesture.isFacingDown)
        {
            timer -= Time.deltaTime;
            if(timer <= 0.0f)
                closeDashboard();
        }
        else
        {
            timer = timerReset;
        }
	}

    private void closeDashboard()
    {
        if (isOpen)
        {
            dashboard.transform.eulerAngles = new Vector3(180.0f, startRotation.y, startRotation.z);
            isOpen = false;
        }
    }

    private void openDashboard()
    {
        if (!isOpen)
        {
            dashboard.transform.eulerAngles = startRotation;
            isOpen = true;
        }
    }
}
