using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class panel : MonoBehaviour {

	// Use this for initialization
	void Start () {
     Tweener twe=transform.DOLocalMoveX(0, 2);
        twe.SetEase(Ease.OutBounce);
        twe.SetLoops(2);
        twe.OnComplete(OnComplete);

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnComplete()
    {
        Debug.Log("动画以完成");
    }

}
