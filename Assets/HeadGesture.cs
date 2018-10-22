using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadGesture : MonoBehaviour {
    public bool isFacingDown = false;
    public bool isMovingDown = false;

    private float sweepRate = 100.0f;
    private float previousCameraAngle;
    public Transform camera;
	// Use this for initialization
	void Start () {
        previousCameraAngle = cameraAngle();
	}
	
	// Update is called once per frame
	void Update () {
        isFacingDown = DetectFacingDown();
        isMovingDown = DetectMovingDown();
	}

    private bool DetectFacingDown()
    {
        return (cameraAngle() < 60.0f);
    }

    private bool DetectMovingDown()
    {
        float angle = cameraAngle();
        float deltaAngle = previousCameraAngle - angle;
        float rate = deltaAngle / Time.deltaTime;
        previousCameraAngle = angle;
        return (rate >= sweepRate);
    }
    private float cameraAngle()
    {
        return Vector3.Angle(Vector3.down, camera.rotation * Vector3.forward);
    }
}
