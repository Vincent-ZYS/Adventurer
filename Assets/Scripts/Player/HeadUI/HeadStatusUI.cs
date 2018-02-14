using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeadStatusUI : MonoBehaviour {

    public Slider hpBar;
    public Slider mpBar;
    public Text hpLabel;
    public Text mpLabel;
    private PlayerStatus playStatus;
 private  Text name_level_label;
    public static HeadStatusUI instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        playStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        name_level_label = GameObject.Find("name_level_label").GetComponent<Text>();
        UpdateShow();
    }
    public void UpdateShow()
    {
      
        hpBar.value = playStatus.hp_remain / playStatus.hp+playStatus.hp_plus;
        mpBar.value = playStatus.mp_remain / playStatus.mp;

        hpLabel.text = playStatus.hp_remain + "/" + (playStatus.hp+ playStatus.hp_plus);
        mpLabel.text = playStatus.mp_remain + "/" + playStatus.mp;

        name_level_label.text ="Lv."+playStatus.level + " Player";

    }
}
