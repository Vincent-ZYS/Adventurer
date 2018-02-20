using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerATKAndDamage :ATKAndDamage {


    public float attackB = 80;
    public float attackRange = 100;
    public float attackGun = 100;
    public WeaponGun gun;
    public AudioClip shot;
    public AudioClip sword;
    [Header("伤害数值显示")]
    public GameObject PopupDamgae;
    GameObject enemy = null;
    void Start()
    {
        hp = PlayerStatus.instance.hp+PlayerStatus.instance.hp_plus;
        PlayerStatus.instance.hp_remain = (int)hp;
        HeadStatusUI.instance.UpdateShow();
        Status.instance.UpdateShow();
    }
    void Update()
    {
        if(enemy!=null)
        {
            transform.LookAt(enemy.transform);
            enemy = null;
        }
    }
    public void AttackA()
    {
      
      
        float distance = attackDistance;
        try
        {
            foreach (GameObject go in SpawnManager.instance.enemyList)
            {
                float temp = Vector3.Distance(go.transform.position, transform.position);
                if (temp < distance)
                {
                    enemy = go;
                    distance = temp;
                }
            }
            if (enemy != null)
            {

                Vector3 targetPos = enemy.transform.position;
                targetPos.y = transform.position.y;

                enemy.GetComponent<ATKAndDamage>().TakeDamage(normalAttack+ PlayerStatus.instance.attack + PlayerStatus.instance.attack_plus);
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
                //TODO其他怪物
                DamagePopup tempPop = PopupDamgae.GetComponent<DamagePopup>();
                tempPop.Value = (int)normalAttack + PlayerStatus.instance.attack + PlayerStatus.instance.attack_plus;
                tempPop.messageType = PopupType.normalEnmey;
                GameObject.Instantiate(PopupDamgae, enemy.transform.position + Vector3.up, Quaternion.identity);


            }
        }
        catch
        {
            
        }
    }
    public void AttackB()
    {

        //AudioSource.PlayClipAtPoint(sword, transform.position, 0.8f);
      
        float distance = attackDistance;
        try 
        {
            foreach (GameObject go in SpawnManager.instance.enemyList)
            {
                float temp = Vector3.Distance(go.transform.position, transform.position);
                if (temp < distance)
                {
                    enemy = go;
                    distance = temp;
                }
            }
            if (enemy != null)
            {
                Vector3 targetPos = enemy.transform.position;
                targetPos.y = transform.position.y;
                enemy.GetComponent<ATKAndDamage>().TakeDamage(attackB+PlayerStatus.instance.attack+PlayerStatus.instance.attack_plus);
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
                //TODO 其他敌人
                DamagePopup tempPop = PopupDamgae.GetComponent<DamagePopup>();
                tempPop.Value = (int)normalAttack + PlayerStatus.instance.attack + PlayerStatus.instance.attack_plus;
                tempPop.messageType = PopupType.normalEnmey;
                GameObject.Instantiate(PopupDamgae, enemy.transform.position + Vector3.up, Quaternion.identity);
               
            }
        }
        catch
        {

        }
    }
    public void AttackRange()
    {

        //AudioSource.PlayClipAtPoint(sword, transform.position, 0.8f);
        List<GameObject> enemyList = new List<GameObject>();
        try
        {
            foreach (GameObject go in SpawnManager.instance.enemyList)
            {
                float temp = Vector3.Distance(go.transform.position, transform.position);
                if (temp < attackDistance)
                {
                    enemyList.Add(go);

                }
            }
            foreach (GameObject go in enemyList)
            {
                go.GetComponent<ATKAndDamage>().TakeDamage(attackRange+ PlayerStatus.instance.attack + PlayerStatus.instance.attack_plus);
                if (go.tag == Tags.soulBoss)
                {
                    go.GetComponent<SoulBossStatus>().hp_remain = enemy.GetComponent<SoulBossATKAndDamage>().hp;
                    go.GetComponent<SoulBossStatus>().UpdateShow();
                }
                if (go.tag == Tags.soulMonster)
                {
                    go.GetComponent<SoulMonsterStatus>().hp_remain = enemy.GetComponent<SoulMonsterATKAndDamage>().hp;
                    go.GetComponent<SoulMonsterStatus>().UpdateShow();
                }
                //TODO 其他怪物
                DamagePopup tempPop = PopupDamgae.GetComponent<DamagePopup>();

                tempPop.Value = (int)normalAttack + PlayerStatus.instance.attack + PlayerStatus.instance.attack_plus;
                tempPop.messageType = PopupType.normalEnmey;
                GameObject.Instantiate(PopupDamgae, go.transform.position + Vector3.up, Quaternion.identity);

            }
        }
        catch
        {

        }
    }
    public void AttackGun()
    {
        gun.attack = attackGun;
        gun.Shot();
        AudioSource.PlayClipAtPoint(shot, transform.position, 0.8f);
    }
}
