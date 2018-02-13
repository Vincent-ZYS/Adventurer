using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EquipmentUI : MonoBehaviour {
    public static EquipmentUI instance;
    public DOTweenAnimation dotween;
    private bool isShow = false;
    public GameObject headgear;
    public GameObject armor;
    public GameObject RightHand;
    public GameObject leftHand;
    public GameObject shoe;
    public GameObject accessory;
    public GameObject EquipItem;
    public int tempId;
    public GameObject tempGo;
    void Awake()
    {
        instance = this;
    }
    public void TransformState()//转变任务栏状态
    {
        if (isShow == false)
        {
            Show();
        }
        else
        {

            Hide();
        }
    }
    public void Show()
    {
        isShow = true;
        dotween.DOPlayForward();

    }
    public void Hide()
    {
        dotween.DOPlayBackwards();
        isShow = false;
     
        EquipementDes.instance.gameObject.SetActive(false);
    }

    public void TakeOff(int id, GameObject go)
    {
        
        Inventory.instance.GetId(id);
        GameObject.Destroy(go);
        Inventory.instance.MinusProperty(id);
        EquipementDes.instance.gameObject.SetActive(false);
    }
    public void OnTakeOffClick()
    {
        tempGo.transform.parent.GetChild(0).gameObject.SetActive(true);
        TakeOff(tempId, tempGo);
       
    }
    public bool Dress(int id)
    {
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);

        GameObject parent = null;
        if (info.type == ObjcetType.Drug)
        {
            return false ;
        }
        switch (info.dressType)
        {
            case DressType.Headgear:
                parent = headgear;
                break;
            case DressType.Armor:
                parent = armor;
                break;
            case DressType.RightHand:
                parent = RightHand;
                break;
            case DressType.LeftHand:
                parent = leftHand;
                break;
            case DressType.Shoe:
                parent = shoe;
                break;
            case DressType.Accessory:
                parent = accessory;
                break;
        }
        EquipItem item = parent.GetComponentInChildren<EquipItem>();
        if (item != null)
        {
            Inventory.instance.GetId(item.id);
            item.SetInfo(info);

        }
        else
        {
            GameObject itemGo = Instantiate(EquipItem);
            itemGo.transform.parent = parent.gameObject.transform;
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.GetComponent<EquipItem>().SetInfo(info);
            itemGo.transform.parent.GetChild(0).gameObject.SetActive(false);
            Inventory.instance.PlussProperty(id);
        }
        return true;
    }

}
