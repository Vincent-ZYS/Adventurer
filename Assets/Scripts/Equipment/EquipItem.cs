using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipItem : MonoBehaviour {


     public Image sprite;
    public int id;
    void Awake()
    {

    }
    public void SetId(int id)
    {
        this.id = id;
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        SetInfo(info);
    }
    public void SetInfo(ObjectInfo info)
    {
        this.id = info.id;

        sprite.sprite = Resources.Load("UI/Sprite/" +info.icon_name, typeof(Sprite)) as Sprite;
        sprite.gameObject.transform.localScale = new Vector3(1f, 1f);
    }
    public void OnSelectItemClick()
    {
          
        EquipementDes.instance.Show(id);

       EquipmentUI.instance.tempId = id;
        EquipmentUI.instance.tempGo = this.gameObject;
     
    }
}
