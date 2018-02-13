using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MyButton : MonoBehaviour {
    public RectTransform taskPanel;
    // Use this for initialization
    bool isIn = false;
    void Start()
    {
        Tweener twenner = taskPanel.DOLocalMove(new Vector3(0, 0, 0), 1);
        twenner.SetAutoKill(false);
        twenner.Pause();

    }
public	void OnMove()
    {
        if (!isIn)
        {
            taskPanel.DOPlayForward();
            isIn = true;
        }
        else
        {
            taskPanel.DOPlayBackwards();
            isIn = false;
        }
    }
}
