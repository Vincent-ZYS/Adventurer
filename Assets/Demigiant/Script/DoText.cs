using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DoText : MonoBehaviour {
    private Text myText;
   
	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
        myText.DOText("是兄弟就来战斗，我是陈小春", 5);
        
    }
	
	// Update is called once per frame
	void Update () {
       
	}
}
