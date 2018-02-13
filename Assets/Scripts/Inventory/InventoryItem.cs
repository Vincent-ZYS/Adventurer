using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventoryItem : MonoBehaviour
{
    public Image sprite;
    public InventoryGrid grid_item;
    private int id;
    public bool isHover = false;
    void Awake()
    {
        sprite = this.GetComponent<Image>();
       
    }
    public void SetId(int id)
    {
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
    }
    public void SetIconName(int id, string icon_name)
    {

        sprite.sprite = Resources.Load("UI/Sprite/" + icon_name, typeof(Sprite)) as Sprite;

        this.id = id;
    }
    void Update()
    {
       
    }

  public void OnSeleItemClick()
    {

        InventoryDes.instance.Show(id);
         if(ObjectsInfo.instance.GetObjectInfoById(id).type== ObjcetType.Equip)
        {
            InventoryDes.instance.consume_button.SetActive(false);
        }
        else
        {
            InventoryDes.instance.consume_button.SetActive(true);
        }
        Inventory.instance.tempId = id;
      
    }

 

}