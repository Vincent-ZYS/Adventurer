using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShopDrugNpc : MonoBehaviour {
    [HideInInspector]
    public bool isMe = false;
    public static ShopDrugNpc instance;
    void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && (EventSystem.current.IsPointerOverGameObject() == false || (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)))
        {
         
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            
            if (isCollider&&hitInfo.collider.tag==Tag.npc)
            {
                isMe = true;
            }         
        }
        if (isMe == true)
            DSController.instance.ShowDialogue(DSController.instance.drug_Array[0], Resources.Load("UI/Sprite/Character", typeof(Sprite)) as Sprite, null, OnReceiveClick, OnRejectClick, Tag.npc, DSController.instance.drugButton[0], DSController.instance.drugButton[1]);
    }
  public  void OnReceiveClick()
    {
        ShopDrug.instance.TransformState();
        DSController.instance.OnCancelClick();
    }
    public void OnRejectClick()
    {

        DSController.instance.OnCancelClick();
    }
}
