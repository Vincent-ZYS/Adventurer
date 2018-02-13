using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FromTween : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //transform.DOMoveX(5, 1).From();
        transform.DOMoveX(5, 4).From(true);//目标值与当前值变成相对值
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
