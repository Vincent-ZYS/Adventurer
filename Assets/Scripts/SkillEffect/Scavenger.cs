using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scavenger : MonoBehaviour {

    public float speed = 5.0f;//飞行速度
    public List<ATKAndDamage> enemyList = new List<ATKAndDamage>();//将伤害过的敌人插入进来保证只伤害一次
    public float attack = 20.0f;
    public int skill_id;
    [Header("伤害数值显示")]
    public GameObject PopupDamgae;
    public static Scavenger instance;
    void Awake()
    {
        instance = this;
    }
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag ==Tags.soulBoss||col.tag==Tags.soulMonster)
        {
           ATKAndDamage enemy = col.GetComponent<ATKAndDamage>();
            int index = enemyList.IndexOf(enemy);
            if (index == -1)
            {
                SkillInfo info = SkillsInfo.instance.GetSkillInfoById(skill_id);
                enemy.TakeDamage(info.applyValue*(PlayerStatus.instance.attack+PlayerStatus.instance.attack_plus)/100);
                if (enemy.tag == Tags.soulBoss)
                {
                    enemy.GetComponent<SoulBossStatus>().hp_remain = enemy.GetComponent<SoulBossATKAndDamage>().hp;
                    enemy.GetComponent<SoulBossStatus>().UpdateShow();
                }
                if (enemy.tag == Tags.soulMonster)
                {
                    enemy.GetComponent<SoulMonsterStatus>().hp_remain = enemy.GetComponent<SoulMonsterATKAndDamage>().hp;
                    enemy.GetComponent<SoulMonsterStatus>().UpdateShow();
                }
                DamagePopup tempPop = PopupDamgae.GetComponent<DamagePopup>();
                tempPop.Value = info.applyValue* (PlayerStatus.instance.attack + PlayerStatus.instance.attack_plus)/100;
                tempPop.messageType = PopupType.doubleEnemy;
                GameObject.Instantiate(PopupDamgae, enemy.transform.position + Vector3.up, Quaternion.identity);
                enemyList.Add(enemy);
            }

        }
    }
    void Update()
    {
        transform.Translate(new Vector3(0,0,1)* speed * Time.deltaTime);
    }


}
