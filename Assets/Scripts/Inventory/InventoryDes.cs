using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryDes : MonoBehaviour {
    public static InventoryDes instance;
    public Text label;
    public float timer = 0;
    public GameObject consume_button;

    void Awake()
    {
        instance = this;
        this.gameObject.SetActive(false);
    }
    public void CloseSeleItemClick()
    {
        InventoryDes.instance.gameObject.SetActive(false);
    }
    public void Show(int id)
    {
        this.gameObject.SetActive(true);
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);

        string des = "";
        switch (info.type)
        {
            case ObjcetType.Drug:
                des = GetDrugDes(info);
                break;
            case ObjcetType.Equip:
                des = GetEquipDes(info);
                break;
        }
        label.text = des;
    }
    private string GetDrugDes(ObjectInfo info)
    {
        string str = "";
        str += "Name:" + info.name + "\n";
        str += "+HP:" + info.hp + "\n";
        str += "+MP:" + info.mp + "\n";
        str += "Sell:" + info.price_sell + "\n";
        str += "Buy:" + info.price_buy;
        return str;
    }
    private string GetEquipDes(ObjectInfo info)
    {
        string str = "";
        str += "Name:" + info.name + "\n";
        switch (info.dressType)
        {
            case DressType.Headgear:
                str += "穿戴类型:头盔\n";
                break;
            case DressType.Armor:
                str += "穿戴类型：盔甲\n";
                break;
            case DressType.LeftHand:
                str += "穿戴类型：左手\n";
                break;
            case DressType.RightHand:
                str += "穿戴类型:右手\n";
                break;
            case DressType.Shoe:
                str += "穿戴类型:鞋子\n";
                break;
            case DressType.Accessory:
                str += "穿戴类型：饰品\n";
                break;
        }
        switch (info.applicationType)
        {
            case ApplicationType.Swordman:
                str += "适用类型:剑士\n";
                break;
            case ApplicationType.Magician:
                str += "适用类型:魔法师\n";
                break;
            case ApplicationType.Common:
                str += "适用类型:通用\n";
                break;
        }
        str += "伤害值:" + info.attack + "\n";
        str += "防御值:" + info.def + "\n";
        str += "速度值:" + info.speed + "\n";

        str += "Sell:" + info.price_sell + "\n";
        str += "Buy:" + info.price_buy;
        return str;
    }
    }
