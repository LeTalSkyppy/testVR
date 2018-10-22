using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startButton : MonoBehaviour {

    public cubeTarget scriptCT;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Image touchImage = transform.Find("touchImage").GetComponent<Image>();
        touchImage.transform.localScale = new Vector3((scriptCT.timerStart/2.0f)*1.5f, (scriptCT.timerStart / 2.0f)*1.5f, 1.0f);
        if(scriptCT.timerStart > 2.0f)
        {
            scriptCT.start = true;
            gameObject.SetActive(false);
        }
	}
}
