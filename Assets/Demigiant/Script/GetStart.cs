using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GetStart : MonoBehaviour {
    public Vector3 vec = new Vector3(0, 0, 0);
    //public Transform cubeTransform;
    public RectTransform taskPanel;
	// Use this for initialization
	void Start () {
        DOTween.To(() => vec, x => vec = x, new Vector3(0, 0, 10), 2);
		
	}
	
	// Update is called once per frame
	void Update () {
        //cubeTransform.position = vec;
        taskPanel.localPosition = vec;
	}
}
