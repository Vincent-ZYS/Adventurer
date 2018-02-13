using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShakePosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.DOShakePosition(2, new Vector3(2,2,0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
