using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class cubeTarget : ScriptableObject {

    public GameObject target;
    public float[] size = new float[9];
    public bool isAlive = false;
    public bool wantDisplayTargets;
    public bool displayTargets;
    public int index;
    public bool colorTarget;
    public float timerHit;
    public float timerStart;
    public bool start;
}
