using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerDialogue : MonoBehaviour {
    public GameObject DialogueUI;
	// Use this for initialization
	void Start () {
        DSController.instance.ShowDialogueSingle("My name is zhui", Resources.Load("UI/Sprite/Character", typeof(Sprite)) as Sprite);


    }
    void Update()
    {

        if (DSController.instance.isSingle == true)
        {
            if (DialogueUI.activeInHierarchy && (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && (EventSystem.current.IsPointerOverGameObject() || (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))))
            {
                DSController.instance.OnCancelClick();
                DSController.instance.isSingle = false;
            }
        }
    }	
	
}
