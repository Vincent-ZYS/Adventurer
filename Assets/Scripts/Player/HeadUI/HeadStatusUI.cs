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
    public static HeadStatusUI instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        playStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        UpdateShow();
    }
    public void UpdateShow()
    {
      
        hpBar.value = playStatus.hp_remain / playStatus.hp+playStatus.hp_plus;
        mpBar.value = playStatus.mp_remain / playStatus.mp;

        hpLabel.text = playStatus.hp_remain + "/" + (playStatus.hp+ playStatus.hp_plus);
        mpLabel.text = playStatus.mp_remain + "/" + playStatus.mp;

    }
}
