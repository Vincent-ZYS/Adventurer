﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ShopEquip : MonoBehaviour {
    private bool isShow = false;
    public static ShopEquip instance;
    public DOTweenAnimation dotween;

    //public GameObject numberDialog;
    public InputField number_input;
    public Text myMoney;
    private int buy_id;
    public Text price;
    public Text label;
    void Awake()
    {
        instance = this;
    }
    public void TransformState()
    {
        if (!isShow)
        {
            dotween.DOPlayForward();
            isShow = true;
        }
        else
        {
            dotween.DOPlayBackwards();
            isShow = false;
        }
    }
    void Buy(int id)
    {
        ObjectInfo tempInfo = ObjectsInfo.instance.GetObjectInfoById(buy_id);
        Debug.Log("tempInfo::" + id);
        //int temp=tempInfo.price_sell;
        buy_id = id;


        Show(buy_id);

    }
    public void OnPlussClick()
    {
        int temp = int.Parse(number_input.text);
        temp++;
        number_input.text = temp.ToString();
        if (buy_id == 1003)
        {
            int temp1 = int.Parse(number_input.text) * 60;
            price.text = temp1.ToString();

        }
        if (buy_id == 1004)
        {
            int temp1 = int.Parse(number_input.text) * 80;
            price.text = temp1.ToString();

        }
        if (buy_id == 1005)
        {
            int temp1 = int.Parse(number_input.text) * 80;
            price.text = temp1.ToString();

        }
        if (buy_id == 1006)
        {
            int temp1 = int.Parse(number_input.text) * 100;
            price.text = temp1.ToString();

        }
        if (buy_id == 1007)
        {
            int temp1 = int.Parse(number_input.text) * 50;
            price.text = temp1.ToString();

        }
        if (buy_id == 1008)
        {
            int temp1 = int.Parse(number_input.text) * 150;
            price.text = temp1.ToString();

        }
    }
    public void OnMinusClick()
    {
        int temp = int.Parse(number_input.text);
        temp--;
        number_input.text = temp.ToString();
        if (buy_id == 1003)
        {
            int temp1 = int.Parse(number_input.text) * 200;
            price.text = temp1.ToString();

        }
        if (buy_id == 1004)
        {
            int temp1 = int.Parse(number_input.text) * 70;
            price.text = temp1.ToString();

        }
        if (buy_id == 1005)
        {
            int temp1 = int.Parse(number_input.text) * 80;
            price.text = temp1.ToString();

        }
        if (buy_id == 1006)
        {
            int temp1 = int.Parse(number_input.text) * 100;
            price.text = temp1.ToString();

        }
        if (buy_id == 1007)
        {
            int temp1 = int.Parse(number_input.text) * 50;
            price.text = temp1.ToString();

        }
        if (buy_id == 1008)
        {
            int temp1 = int.Parse(number_input.text) * 150;
            price.text = temp1.ToString();

        }
    }
    public void OnBuyId1003()
    {
        int temp = int.Parse(number_input.text) * 200;
        price.text = temp.ToString();
        Buy(1003);
    }
    public void OnBuyId1004()
    {
        int temp = int.Parse(number_input.text) * 70;
        price.text = temp.ToString();
        Buy(1004);
    }
    public void OnBuyId1005()
    {
        int temp = int.Parse(number_input.text) * 80;
        price.text = temp.ToString();
        Buy(1005);
    }
    public void OnBuyId1006()
    {
        int temp = int.Parse(number_input.text) * 100;
        price.text = temp.ToString();
        Buy(1006);
    }
    public void OnBuyId1007()
    {
        int temp = int.Parse(number_input.text) * 50;
        price.text = temp.ToString();
        Buy(1007);
    }
    public void OnBuyId1008()
    {
        int temp = int.Parse(number_input.text) * 150;
        price.text = temp.ToString();
        Buy(1008);
    }
    public void OnOkButtonClick()
    {
        int count = int.Parse(number_input.text);

        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(buy_id);
        int price = info.price_buy;
        int price_total = price * count;
        bool success = Inventory.instance.GetCoin(price_total);
        myMoney.text = Inventory.instance.coinCount.ToString();
        if (success)
        {
            if (count > 0)
            {
                Inventory.instance.GetId(buy_id, count);
            }

        }

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