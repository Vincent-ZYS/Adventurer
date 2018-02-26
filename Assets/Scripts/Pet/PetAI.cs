using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PetAI : ATKAndDamage
{
    public static PetAI instance;//单例模式
    public PetStatus status;
    public Transform followTarget;
    private float timer = 0;
    public float attackTime = 2.0f;
    public float maxDistance = 5.0f;//宠物距离检测范围
    public float maxFollowTargetDistance = 10;//超过该距离则宠物停止当前攻击，跟随Master
    public float minFollowTargetDistance = 3;//宠物和人的距离不会小于这个数
    [HideInInspector]
    public GameObject Master;

    public CharacterController cc;

    private GameObject enemy = null;//存储当前敌人

    [Header("伤害数值显示")]
    public GameObject PopupDamgae;

    // Use this for initialization
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        cc = this.GetComponent<CharacterController>();
        timer = attackTime;
        gameObject.tag = "Pet";
        Master = GameObject.FindGameObjectWithTag("Player");//这种实例的语句尽量放在Start
       
    }

    // Update is called once per frame
    void Update()
    {
        FindEnemy();
    }

    void FindEnemy()
    {
        if ((transform.position - Master.transform.position).magnitude <= maxFollowTargetDistance)//&& Vector3.Distance(Master.transform.position,enemy.transform.position)<=mastermaxDistance)//攻击敌人前提是，和主人之间有个最大距离
        {

            MinDistanceEnemy();//搜寻最近敌人

            if (enemy != null && (transform.position - enemy.transform.position).magnitude <= maxDistance)// Vector3.Distance(transform.position,enemy.transform.position) <= maxDistance )
            {
                followTarget = enemy.transform;//通过enemy当前敌人存储字段，赋给target
                if (followTarget.tag ==Tags.soulBoss||followTarget.tag==Tags.soulMonster)
                {
                   
                    MoveToward();
                    maxFollowTargetDistance = 15.0f;
                 
                }
            }
            else//当前没有敌人
            {

                followTarget = Master.transform;
                if (Vector3.Distance(transform.position, Master.transform.position) >minFollowTargetDistance)//当达到这个距离，就停下，不会一直往人物方向走
                {
                    if (followTarget.tag == "Player")
                    {
                        MoveToward();
                    }
                }
                else
                {
                    this.animator.SetBool("Walk", false);
                }
            }
        }
        else
        {
            this.animator.ResetTrigger("AttackClaws");
            this.animator.SetBool("Walk", true);
            maxFollowTargetDistance = 20.0f;//怎么想的
            followTarget = Master.transform;
            if (Vector3.Distance(transform.position, Master.transform.position) > minFollowTargetDistance)//已经超出与玩家最大距离，直接跟随没商量
            {
             
                if (followTarget.tag == "Player")
                {
                    MoveToward();
                    maxFollowTargetDistance = 10;

                }
            }
        }



    }


    void MoveToward()//不要进行重复判断操作
    {


        Vector3 pos = followTarget.position;
        pos.y = transform.position.y;
        transform.LookAt(followTarget.position);
        if (followTarget.tag == "Player")
        {
            if (Vector3.Distance(transform.position, followTarget.position) >= minFollowTargetDistance)
            {
                this.animator.SetBool("Walk", true);
                transform.position = Vector3.Lerp(transform.position, pos, 1 * Time.deltaTime);
            }

            else
            {
                this.animator.SetBool("Walk", false);
            }
        }
        if (followTarget.tag == Tags.soulBoss || followTarget.tag == Tags.soulMonster)
        {
            if (Vector3.Distance(transform.position, followTarget.position) <= attackDistance)
            {
                Attack();
            }
            else
            {

                animator.SetBool("Walk", true);
                transform.position = Vector3.Lerp(transform.position, pos, 1 * Time.deltaTime);
            }
        }

    }

    void Attack()
    {
        timer += Time.deltaTime;
        if (timer >= attackTime)
        {
            PetAnimationAttack.instance.AttackClaw();
            timer = 0;
        }
        else
        {
            this.animator.SetBool("Walk", false);
        }
        
        

    }
    public void AttackClaws()
    {
        if (enemy != null)
        {
            enemy.GetComponent<ATKAndDamage>().TakeDamage(status.attackStrength);
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
            tempPop.Value = (int)status.attackStrength;
            tempPop.messageType = PopupType.normalEnmey;
            GameObject.Instantiate(PopupDamgae, enemy.transform.position + Vector3.up, Quaternion.identity);
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }

    void MinDistanceEnemy()//通过迭代获取最近的敌人
    {
        maxDistance = 5.0f;
        foreach (GameObject go in SpawnManager.instance.enemyList)
        {
            float temp = Vector3.Distance(go.transform.position, transform.position);//最小的与上一个比较和宠物的距离
            if (temp < maxDistance)
            {
                enemy = go;
                maxDistance = temp;//得到的temp绝对是目前最小距离的

            }
        }

    }
}


