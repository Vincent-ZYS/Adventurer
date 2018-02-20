using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKAndDamage : MonoBehaviour {
    public float hp = 100;
    public float normalAttack = 50;
    public float attackDistance = 1;
    private Animator animator;
    public AudioClip death;
  protected  void Awake()
    {
        animator = this.GetComponent<Animator>();
    }
  
    public virtual void TakeDamage(float damage)
    {
        if(hp>0)
        {
            hp -= damage;
        }
        if(hp>0)
        {
            if (this.gameObject.tag == Tags.soulBoss || this.tag == Tags.soulMonster)
            {
                this.animator.SetTrigger("Damage");
            }
           
        }
        else
        {
            this.animator.SetTrigger("Dead");
            PlayerStatus.instance.GetExp((int)this.gameObject.GetComponent<Enemy>().getExp);//怪物死亡获得经验值
            if(this.tag==Tags.soulMonster)
            {
                BarNpc.instance.OnKillEnemy();
            }
            this.gameObject.GetComponent<Enemy>().spawn.MinusNumber();
            AudioSource.PlayClipAtPoint(death, transform.position, 0.5f);
            if (this.tag == Tags.soulBoss || this.tag == Tags.soulMonster)
            {
                SpawnManager.instance.enemyList.Remove(this.gameObject);//移除死亡元素
                SpawnAward();
                Destroy(this.gameObject, 1);
            }
        }
        if (this.tag == Tags.soulBoss)//判断是否为Boss,产生不同的打击特效
        {
            GameObject.Instantiate(Resources.Load("HitBoss"), transform.position + Vector3.up, transform.rotation);

        }
        else if (this.tag == Tags.soulMonster)
        {
            GameObject.Instantiate(Resources.Load("HitMonster"), transform.position + Vector3.up, transform.rotation);
        }
    }
    void SpawnAward()
    {
        int count = Random.Range(1, 3);
        for(int i=0;i<count;i++)
        {
            int index = Random.Range(0, 1);
            if(index==0)
            {
                GameObject.Instantiate(Resources.Load("Item-DualSword"), transform.position + Vector3.up, Quaternion.identity);
            }else if(index==1)
            {
                GameObject.Instantiate(Resources.Load("Item-Gun"), transform.position + Vector3.up, Quaternion.identity);
            }
        }
    }
}
