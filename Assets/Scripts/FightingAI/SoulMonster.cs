using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMonster : Enemy {

    private Transform player;
    public float attackDistance = 2.5f;
    public float speed = 2;
    public float memeorySpeed;
    private CharacterController cc;
    private Animator animator;
    public float maxDistance = 5.0f;//超过该距离，将不会继续追击
    [Header("Boss Attack Speed")]
    public float attackTime = 3;//Boss attack for 3 seconds
    public float attackTimer = 0;
    [Header("Enemy Run Animation Name")]
    public string walkName = "BossRun01";
    private Transform target;
    [Header("巡逻切换时间")]
    public  float timer=5.0f;
    public float time=0f;

    void Start()
    {
        memeorySpeed = speed;
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        cc = this.GetComponent<CharacterController>();
        this.animator = this.GetComponent<Animator>();
        attackTimer = attackTime;
    }
    void Update()
    {
        float distance = Vector3.Distance(GameObject.FindWithTag(Tags.player).transform.position, transform.position);
        if(distance<maxDistance)
        {
            AutoAttack();
        }
        else
        {
            time += Time.deltaTime;
            if(time>=timer)
            {
                RandomState();
                time= 0;
            }
        }
        if(this.animator.GetCurrentAnimatorStateInfo(0).IsName(walkName))
        {
            cc.SimpleMove(transform.forward * speed);
        }


    }
    void AutoAttack()
    {
        Vector3 targetPos = player.position;
        if (SkillShortCut.instance.currentPet != null)
        {
            if (Vector3.Distance(transform.position, targetPos) >= Vector3.Distance(transform.position, SkillShortCut.instance.currentPet.transform.position))
            {
                targetPos = SkillShortCut.instance.currentPet.transform.position;
            }
        }
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        float distance = Vector3.Distance(targetPos, transform.position);
        if (distance <= attackDistance)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackTime)
            {
                this.animator.SetTrigger("Attack");
                attackTimer = 0;

            }
            else
            {
                this.animator.SetBool("Walk", false);
            }

        }
        else
        {
            attackTimer = attackTime;//一追上就 进行攻击
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName(walkName))//判断当前是否是Run动画
            {
                cc.SimpleMove(transform.forward * speed);
            }
            this.animator.SetBool("Walk", true);

        }
    }

   void RandomState()
    {
        int value = Random.Range(0, 2);
        if (value == 0)
        {
            this.animator.SetBool("Walk", false);
        }
        else
        {
            if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName(walkName))
            {
                transform.Rotate(transform.up * Random.Range(0, 360));
            }
            this.animator.SetBool("Walk", true);
        }

      

        float distance = Vector3.Distance(GameObject.FindWithTag(Tags.player).transform.position, transform.position);
        if (distance < maxDistance)
        {

            AutoAttack();

        }
        if (SkillShortCut.instance.currentPet != null)
        {
            if (Vector3.Distance(transform.position, SkillShortCut.instance.currentPet.transform.position) < maxDistance)
            {
                AutoAttack();
            }
        }

    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if(hit.collider.tag==Tags.soulMonster)
        {
            this.animator.SetBool("Walk", false);
            speed = -speed;
        }
        else
        {
            speed = memeorySpeed;
        }
       
    }
}
