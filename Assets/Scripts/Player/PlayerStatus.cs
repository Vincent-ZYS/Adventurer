using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    public static PlayerStatus instance;
    [Header("Player Status")]
    public int hp = 100;
 
    public int hp_plus = 0;
    public int mp = 100;
    public float hp_remain = 100;
    public float mp_remain = 100;
    public int level = 1;
    public float exp = 0;
    public int attack = 20;

    public int attack_plus = 0;
    public float def = 20;
 
    public float def_plus = 0;
    public int speed = 20;

    public int speed_plus = 0;
    public int point_remain = 0;//角色的剩余点数
    public int coin = 200;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
      
        GetExp(0);
    }
    public void GetCoins(int count)//获得金币
    {
        coin += count;
    }
    public bool GetPoint(int point = 1)
    {
        if (point_remain >= point)
        {
            point_remain -= point;
            return true;
        }
        return false;
    }
    public void GetDrug(int hp, int mp)//补血
    {
        hp_remain += hp;
        mp_remain += mp;
        if (hp_remain > this.hp+hp_plus)
        {
            hp_remain = this.hp+hp_plus;
        }
        if (mp_remain > mp)
        {
            mp_remain = this.mp;
        }
    }
    public void GetExp(int exp)//获得经验
    {
        this.exp += exp;
        int total_exp = 100 + level * 30;
        while (this.exp >= total_exp)
        {//TODO
            this.level++;
            HeadStatusUI.instance.UpdateShow();
            this.exp -= total_exp;
            total_exp = 100 + level * 30;
        }
        ExpBar.instance.SetValue(this.exp / total_exp);
    }
    public bool TakeMP(int count)
    {
        if (mp_remain >= count)
        {
            mp_remain -= count;
       
           HeadStatusUI.instance.UpdateShow();
            return true;
        }
        else
        {
            return false;
        }
    }
}
