using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour {

    public float speed=10;
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.up *speed);
	}
}
