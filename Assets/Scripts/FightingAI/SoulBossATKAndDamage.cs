using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBossATKAndDamage :ATKAndDamage {

    private Transform player;
    public AudioClip bossAttack;
    public GameObject PopupDamgae;
    void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
    }


    public void Attack1()
    {
      
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            ATKAndDamage playerAtkandDamage = player.GetComponent<ATKAndDamage>();
            float temp = normalAttack * ((200.0f - (PlayerStatus.instance.def + PlayerStatus.instance.def_plus)) / 200.0f);
            if (temp < 1) { temp = 1; }


            player.GetComponent<ATKAndDamage>().TakeDamage(temp);
            PlayerStatus.instance.hp_remain = playerAtkandDamage.hp;
            HeadStatusUI.instance.UpdateShow();
            DamagePopup tempPop = PopupDamgae.GetComponent<DamagePopup>();
            tempPop.Value = (int)temp;
            tempPop.messageType = PopupType.player;
            GameObject.Instantiate(PopupDamgae, player.transform.position + Vector3.up, Quaternion.identity);
           
            Debug.Log("normalAttack_Monster::" +temp);
        }
    }
    public void Attack2()
    {
     
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            ATKAndDamage playerAtkandDamage = player.GetComponent<ATKAndDamage>();
            float temp= normalAttack * ((200 - PlayerStatus.instance.def + PlayerStatus.instance.def_plus) / 200);
            if (temp < 1) temp = 1;

            playerAtkandDamage.TakeDamage(temp);
            PlayerStatus.instance.hp_remain = playerAtkandDamage.hp;
            HeadStatusUI.instance.UpdateShow();
            DamagePopup tempPop = PopupDamgae.GetComponent<DamagePopup>();
            tempPop.Value = (int)temp;
            tempPop.messageType = PopupType.player;
            GameObject.Instantiate(PopupDamgae, player.transform.position + Vector3.up, Quaternion.identity);
            
        }
    }
}
