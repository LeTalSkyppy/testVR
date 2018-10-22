using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViserGame : MonoBehaviour {
    new public Camera camera;
    public GameObject target;
    public float fireRate = 2f;
    public Canvas canvas;
    new public AudioSource audio;
    public float bpm;

    private float lastTime, deltaTime, timer;
    //public GameObject killEffect;

    private Text scoreText;
    private int score = 0;
    private float nextTargetTime = 0.0f;

    private Vector3 lastPos = new Vector3(0.0f,5.0f,15.0f);
    private Vector3 nextPosition = new Vector3(0.0f, 5.0f, 15.0f);
    private int index = 0;
    private bool heatMapDisplayed;
    private GameObject heatMap;
    // Use this for initialization
    void Start () {
        scoreText = canvas.transform.Find("Score").GetComponent<Text>();
        scoreText.text = "Score : 0";
        audio.Play();
        lastTime = 0f;
        deltaTime = 0f;
        timer = 0f;
        heatMapDisplayed = false;
        heatMap = GameObject.Find("heatMap");
    }
	
	// Update is called once per frame
	void Update () {

        deltaTime = audio.time - lastTime;
        timer += deltaTime;

        if (timer >= (60.0f/bpm))
        {
            Vector3 position = new Vector3(Random.Range(-5.0f,5.0f), Random.Range(0.0f, 10.0f), 15f);
            
            Instantiate(target, position, Quaternion.identity);
            index++;
            //SquarePattern();
            timer = 0;
        }

        lastTime = audio.time;

        //Ray ray;
        RaycastHit hit;
        //RaycastHit[] hits;
        GameObject hitObject;

        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        //Debug.DrawRay(rayOrigin, camera.transform.forward * 100.0f, Color.red);

        //ray = new Ray(camera.transform.position, camera.transform.rotation * Vector3.forward);
        //hits = Physics.RaycastAll(ray);

        //for (int i = 0; i < hits.Length; i++)
        //{
        //    RaycastHit hit = hits[i];
        //    hitObject = hit.collider.gameObject;
        //    if (hitObject.tag == "Target")
        //    {
        //        score++;
        //        scoreText.text = "Score : " + score.ToString();
        //        //Instantiate(killEffect, hitObject.transform.position, hitObject.transform.rotation);
        //        Destroy(hitObject);
        //    }
        //}

        if(Physics.Raycast(rayOrigin,camera.transform.forward, out hit, 100.0f))
        {
            hitObject = hit.collider.gameObject;
            if(hitObject.tag == "Target")
            {
                score++;
                scoreText.text = "Score : " + score.ToString();
                Destroy(hitObject);
            }
        }
        
        if(!audio.isPlaying && !heatMapDisplayed)
        {
            heatMap.transform.position = new Vector3(heatMap.transform.position.x, heatMap.transform.position.y, heatMap.transform.position.z - 2.0f);
            heatMapDisplayed = !heatMapDisplayed;
        }
    }


    private void SquarePattern()
    {
        lastPos = nextPosition;
        if (index <= 8)
        {            
            nextPosition = new Vector3(lastPos.x + 1.0f, lastPos.y, lastPos.z);
        }
        if(index > 8 && index <= 16)
        {
            nextPosition = new Vector3(lastPos.x, lastPos.y+1.0f, lastPos.z);
        }
        if(index > 16 && index <= 32)
        {
            nextPosition = new Vector3(lastPos.x - 1.0f, lastPos.y, lastPos.z);
        }
        if(index > 32 && index <= 42)
        {
            nextPosition = new Vector3(lastPos.x, lastPos.y-1.0f, lastPos.z);
        }
        if(index > 42)
        {
            index = 0;
            nextPosition = new Vector3(0.0f, 5.0f, 15.0f);
        }
    }
}
