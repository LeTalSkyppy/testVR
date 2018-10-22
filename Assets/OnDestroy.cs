using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnDestroy : MonoBehaviour {

    public float lifeTime;
    public cubeTarget scriptCT;
    public Material red;
    public Material white;
    private float timer;
	// Use this for initialization
	void Start () {
        timer = 0.0f;
        //transform.localScale = new Vector3(transform.localScale.x * scriptCT.size[scriptCT.index], transform.localScale.y * scriptCT.size[scriptCT.index], transform.localScale.z);
        if(transform.localPosition.y != 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        }          
        
	}
	
	// Update is called once per frame
	void Update () {
        if (scriptCT.colorTarget)
        {
            Canvas touchCanvas = transform.Find("touchCanvas").GetComponent<Canvas>();
            touchCanvas.gameObject.SetActive(true);
            Image touchImage = touchCanvas.transform.Find("touchImage").GetComponent<Image>();
            touchImage.transform.localScale = new Vector3((1 * (scriptCT.timerHit / 0.3f)),(1 * (scriptCT.timerHit / 0.3f)), 1);
        }
        else
        {
            Canvas touchCanvas = transform.Find("touchCanvas").GetComponent<Canvas>();
            touchCanvas.gameObject.SetActive(false);
        }
        
        timer += Time.deltaTime;
		if(timer > lifeTime && !scriptCT.displayTargets)
        {
            if (!scriptCT.wantDisplayTargets)
            {
                scriptCT.isAlive = false;
            }
            else
            {
                scriptCT.displayTargets = true;
            }  
            Destroy(gameObject);
        }
	}
}
