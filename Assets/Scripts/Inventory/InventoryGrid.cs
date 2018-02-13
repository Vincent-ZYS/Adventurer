using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryGrid : MonoBehaviour {
    public int id = 0;
    private ObjectInfo info = null;
    public Text numLabel;
    public int num = 0;
    public void SetId(int id, int num = 1)
    {
        this.id = id;
        info = ObjectsInfo.instance.GetObjectInfoById(id);
        InventoryItem item = null;
        if (item == null)
        {

            item = this.GetComponentInChildren<InventoryItem>();
        }
       
        item.SetIconName(id, info.icon_name);
        if (numLabel == null)
        {
            numLabel = this.GetComponentInChildren<Text>();
        }
        numLabel.enabled = true;
        this.num = num;

        numLabel.text = num.ToString();
    }
    public void ClearInfo()
    {
        id = 0;
        info = null;
        this.num = 0;

        numLabel.enabled = false;

    }
    public void PlusNumber(int num = 1)
    {
        this.num += num;

        numLabel.text = this.num.ToString();

    }
    public bool MinusNumber(int num = 1)
    {
        if (this.num >= num)
        {
            this.num -= num;

            if (this.num == 0)
            {
               
                ClearInfo();
                GameObject.Destroy(this.GetComponentInChildren<InventoryItem>().gameObject);

                Inventory.instance.memorizeItem.Remove(id);
                InventoryDes.instance.gameObject.SetActive(false);
              
            }

            numLabel.text = this.num.ToString();
            return true;

        }
        else
        {
            ClearInfo();
            GameObject.Destroy(this.GetComponentInChildren<InventoryItem>().gameObject);

            Inventory.instance.memorizeItem.Remove(id);
            InventoryDes.instance.gameObject.SetActive(false);
        }
        return false;
    }
}
