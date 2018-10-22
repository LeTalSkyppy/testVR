using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calibrationCube : MonoBehaviour {
    public cubeTarget scriptCT;
    public Transform cameraTransform;
    public GameObject[] gamePlane = new GameObject[9];

    private LineRenderer heading;
    private int previousIndex;
    private Vector3 offset;
    private Vector3 position;
    private Vector2 gazePointLeft;
    private Vector2 gazePointRight;
    private Vector2 gazePointCenter;
    private Vector3 standardViewportPoint = new Vector3(0.5f, 0.5f, 1f);
    private Camera sceneCamera;
    private float timer;
    private bool canDestroy;
    //private float timerHit;
    private bool targetsDisplayed;
    // Use this for initialization
    void Start () {
        sceneCamera = cameraTransform.gameObject.GetComponent<Camera>();
        //heading = cameraTransform.gameObject.GetComponent<LineRenderer>();
        for(int i = 0; i < 9; i++)
        {
            scriptCT.size[i] = 5;
        }
        timer = 0;
        canDestroy = true;
        scriptCT.displayTargets = false;
        scriptCT.isAlive = false;
        scriptCT.colorTarget = false;
        targetsDisplayed = false;
        scriptCT.wantDisplayTargets = false;
        scriptCT.displayTargets = false;
        scriptCT.start = false;
        scriptCT.timerStart = 0.0f;
    }


    private void OnEnable()
    {
        if (PupilTools.IsConnected)
        {
            PupilTools.IsGazing = true;
            PupilTools.SubscribeTo("gaze");
        }      
    }

    // Update is called once per frame
    void Update () {

        Vector3 viewportPoint = standardViewportPoint;

        if (PupilTools.IsConnected && PupilTools.IsGazing)
        {
            gazePointLeft = PupilData._2D.GetEyePosition(sceneCamera, PupilData.leftEyeID);
            gazePointRight = PupilData._2D.GetEyePosition(sceneCamera, PupilData.rightEyeID);
            gazePointCenter = PupilData._2D.GazePosition;
            viewportPoint = new Vector3(gazePointCenter.x, gazePointCenter.y, 1f);
        }

        if (!scriptCT.isAlive && !scriptCT.displayTargets && scriptCT.start)
        {
                previousIndex = scriptCT.index;
            
            do
            {
                scriptCT.index = Random.Range(0, 8);
            } while (scriptCT.index == previousIndex);

            offset = new Vector3(Random.Range(-1.0f,1.0f), Random.Range(-1.0f,1.0f), 0);
            position = gamePlane[scriptCT.index].transform.position;

            //Quaternion rotation = new Quaternion(gamePlane[(int)index].transform.rotation.x - 270, gamePlane[(int)index].transform.rotation.y, gamePlane[(int)index].transform.rotation.z, gamePlane[(int)index].transform.rotation.w);

            //Quaternion rotation = new Quaternion(cameraTransform.rotation.x+180.0f, cameraTransform.rotation.y, cameraTransform.rotation.z, cameraTransform.rotation.w);

            GameObject tg = Instantiate(scriptCT.target, (position+offset), cameraTransform.rotation, gamePlane[scriptCT.index].transform);
            tg.transform.localScale = new Vector3(tg.transform.localScale.x * scriptCT.size[scriptCT.index], tg.transform.localScale.y, tg.transform.localScale.z * scriptCT.size[scriptCT.index]);
            tg.transform.localRotation = new Quaternion(180, 0, 0, 0);
            scriptCT.isAlive = true;
        }

        //RaycastHit hit;
        //GameObject hitObject;

        Vector3 rayOrigin = sceneCamera.ViewportToWorldPoint(viewportPoint);

        //Debug.DrawRay(rayOrigin, cameraTransform.forward * 100.0f, Color.red);
        //if (Physics.Raycast(rayOrigin, cameraTransform.forward, out hit, 100.0f))
        //{
        //    hitObject = hit.collider.gameObject;
        //    if (hitObject.tag == "Target")
        //    {
        //        scriptCT.size -= 0.1f;
        //        scriptCT.isAlive = false;
        //        Destroy(hitObject);
        //    }
        //}

        //heading.SetPosition(0, sceneCamera.transform.position - sceneCamera.transform.up);

        Ray ray = sceneCamera.ViewportPointToRay(viewportPoint);
        RaycastHit hitH;
        if (Physics.Raycast(ray, out hitH))
        {
            if(hitH.collider.gameObject.tag == "Target" && canDestroy)
            {
                scriptCT.timerHit += Time.deltaTime;
                if(scriptCT.timerHit >= 0.3f)
                {
                    scriptCT.size[scriptCT.index] -= 0.5f;
                    scriptCT.isAlive = false;
                    Destroy(hitH.collider.gameObject);
                    scriptCT.timerHit = 0.0f;                    
                }
                scriptCT.colorTarget = true;
            }
            else
            {
                scriptCT.timerHit = 0.0f;
                scriptCT.colorTarget = false;
            }
            if(hitH.collider.gameObject.tag == "Button")
            {
                if(hitH.collider.gameObject.name == "startButton")
                {
                    scriptCT.timerStart += Time.deltaTime;
                }
            }
            else
            {
                scriptCT.timerStart = 0.0f;
            }
            //heading.SetPosition(1, hitH.point);
        }
        else
        {
            //heading.SetPosition(1, ray.origin + ray.direction * 50f);
        }

        timer += Time.deltaTime;
        if(timer > 30.0f && !targetsDisplayed)
        {
            scriptCT.wantDisplayTargets = true;
            if (scriptCT.displayTargets)
            {
                canDestroy = false;
                for (int i = 0; i < 9; i++)
                {
                    scriptCT.index = i;
                    position = gamePlane[scriptCT.index].transform.position;
                    GameObject tg = Instantiate(scriptCT.target, position, cameraTransform.rotation, gamePlane[scriptCT.index].transform);
                    tg.transform.localScale = new Vector3(tg.transform.localScale.x * scriptCT.size[scriptCT.index], tg.transform.localScale.y, tg.transform.localScale.z * scriptCT.size[scriptCT.index]);
                    tg.transform.localRotation = new Quaternion(180, 0, 0, 0);
                }
                targetsDisplayed = true;
            }            
        }
    }
}
