using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Panel2 : MonoBehaviour {
    private DOTweenAnimation animat;
    private bool isShow = false;
	// Use this for initialization
	void Start () {
        animat = GetComponent<DOTweenAnimation>();
        animat.DOPlay();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
  public  void OnClick()
    {
        if(isShow)
        {
            animat.DOPlayForward();
            isShow = false;
        }
        else
        {
            animat.DOPlayBackwards();
            isShow = true;
        }
    }
}
