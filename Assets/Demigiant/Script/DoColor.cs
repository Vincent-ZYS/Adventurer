using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DoColor : MonoBehaviour {
    private Text myText;
	// Use this for initialization
	void Start () {
        myText=GetComponent<Text>();
        myText.DOColor(Color.red, 2);
        myText.DOFade(0, 4);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
