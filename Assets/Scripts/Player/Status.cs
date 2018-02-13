using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Status : MonoBehaviour {
    public static Status instance;
    private bool isShow = false;
    public DOTweenAnimation dotween;
    [Header("UI")]
    public Text attackLabel;
    public Text defLabel;
    public Text speedLabel;
    public Text hpLabel;
    public Text summaryLabel;
    public Text point_reaminLabel;
    public GameObject attackButtonGo;
    public GameObject attackButtonBack;
    public GameObject defButtonGo;
    public GameObject defButtonBack;
    public GameObject speedButtonGo;
    public GameObject speedButtonBack;
    public GameObject hpButtonGo;
    public GameObject hpButtonBack;
    private PlayerStatus playerStatus;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        dotween = this.GetComponent<DOTweenAnimation>();
        playerStatus = PlayerStatus.instance;
        attackLabel = GameObject.Find("attack").GetComponent<Text>();
        defLabel= GameObject.Find("def").GetComponent<Text>();
        speedLabel= GameObject.Find("speed").GetComponent<Text>();
        hpLabel=GameObject.Find("hp").GetComponent<Text>();
        point_reaminLabel = GameObject.Find("point_remain").GetComponent<Text>();
        summaryLabel = GameObject.Find("Summary").GetComponent<Text>();
        attackButtonGo = GameObject.Find("attack_plus_button");
        attackButtonBack = GameObject.Find("attack_minus_button");
        defButtonGo = GameObject.Find("def_plus_button");
        defButtonBack = GameObject.Find("def_minus_button");
        hpButtonGo = GameObject.Find("hp_plus_button");
       hpButtonBack= GameObject.Find("hp_minus_button");
        speedButtonGo = GameObject.Find("speed_plus_button");
        speedButtonBack = GameObject.Find("speed_minus_button");
    }
    public void TransformState()//开启和关闭
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
        UpdateShow();
        dotween.DOPlayForward();

    }
    public void Hide()
    {
        dotween.DOPlayBackwards();
        isShow = false;
        InventoryDes.instance.gameObject.SetActive(false);

    }
   public void UpdateShow()
    {
        attackLabel.text = playerStatus.attack + "+" + playerStatus.attack_plus;
        defLabel.text = playerStatus.def + "+" + playerStatus.def_plus;
        speedLabel.text = playerStatus.speed + "+" + playerStatus.speed_plus;
        hpLabel.text = playerStatus.hp + "+" + playerStatus.hp_plus;
        point_reaminLabel.text = playerStatus.point_remain.ToString();

        summaryLabel.text = "HP:"+(playerStatus.hp+playerStatus.hp_plus)+" "+"Attack:" + (playerStatus.attack + playerStatus.attack_plus) + " " + "Def:" + (playerStatus.def + playerStatus.def_plus) + " " + "Speed:" + (playerStatus.speed + playerStatus.speed_plus);
        if (playerStatus.point_remain > 0)
        {
            attackButtonGo.SetActive(true);
            defButtonGo.SetActive(true);
            speedButtonGo.SetActive(true);
            hpButtonGo.SetActive(true);
            hpButtonBack.SetActive(true);
            defButtonBack.SetActive(true);
            speedButtonBack.SetActive(true);
            attackButtonBack.SetActive(true);
        }
        else
        {

            attackButtonGo.SetActive(false);
            defButtonGo.SetActive(false);
            speedButtonGo.SetActive(false);
            hpButtonGo.SetActive(false);
            hpButtonBack.SetActive(false);
            defButtonBack.SetActive(false);
            speedButtonBack.SetActive(false);
            attackButtonBack.SetActive(false);
        }
    }
    #region 各属性加点
    public void OnDefPlusClick()//防御加点
    {
        bool success = playerStatus.GetPoint();
        if (success)
        {
            playerStatus.def_plus++;
            
            UpdateShow();
        }
    }
    public void OnDefMinusClick()
    {
        if (playerStatus.def_plus<= 0) return;
        playerStatus.def_plus--;
        playerStatus.point_remain++;
        UpdateShow();
    }
    public void OnAttackPlusClick()//攻击加点
    {
        bool success = playerStatus.GetPoint();
        if (success)
        {
            playerStatus.attack_plus++;
            UpdateShow();
        }
    }
    public void OnAttackMinusClick()
    {
        if (playerStatus.attack_plus<=0) return;
        playerStatus.attack_plus--;
        playerStatus.point_remain++;
        UpdateShow();
    }
    public void OnSpeedPlusClick()//速度加点
    {
      
        bool success = playerStatus.GetPoint();
        if (success)
        {
            playerStatus.speed_plus++;
            UpdateShow();
        }
    }
    public void OnSpeedMinusClick()
    {
        if (playerStatus.speed_plus <=0) return;
        playerStatus.speed_plus--;
        playerStatus.point_remain++;
        UpdateShow();
    }
    public void OnHpPlusClick()//体力加点
    {
        bool success = playerStatus.GetPoint();
        if(success)
        {
            playerStatus.hp_plus++;
            playerStatus.hp_remain++;
            UpdateShow();
            HeadStatusUI.instance.UpdateShow();
        }
    }
    public void OnHpMinusClick()
    {
        if (playerStatus.hp_plus <=0) return;
        playerStatus.hp_plus--;
        playerStatus.hp_remain--;
        playerStatus.point_remain++;
        UpdateShow();
        HeadStatusUI.instance.UpdateShow();
    }
    #endregion
  
}
