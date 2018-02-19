using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerAnimationAttack : MonoBehaviour {
    public Animator animator;
    public bool isCanAttackB = false;//判断能否进行连招
    public float fwdTime = 0.2f;//每次攻击向前进一步
    public bool isCanAttackRange = false;
    public static PlayerAnimationAttack instance;
    void Awake()
    {
        instance = this;
    }
     void Start()
    {
        //EventDelegate NormalAttackEvent = new EventDelegate(this, "OnNormalAttackClick");
        //GameObject.Find("NormalAttack").GetComponent<UIButton>().onClick.Add(NormalAttackEvent);

        //EventDelegate RangeAttackEvent = new EventDelegate(this, "OnRangeAttackClick");
        //GameObject.Find("RangeAttack").GetComponent<UIButton>().onClick.Add(RangeAttackEvent);

        //EventDelegate RedAttackEvent = new EventDelegate(this, "OnRedAttackClick");
        //GameObject redAttack = GameObject.Find("RedAttack");
        //redAttack.GetComponent<UIButton>().onClick.Add(RedAttackEvent);
        //redAttack.SetActive(false);
        GameObject.Find("AttackNormal").GetComponent<Button>().onClick.AddListener(OnNormalAttackClick);
     


    }

    public void OnNormalAttackClick()
    {
   
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackA") && isCanAttackB)
        {
            this.animator.SetTrigger("AttackB");
           
           
        }
      else  if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackB") && isCanAttackRange)
        {
            this.animator.SetTrigger("AttackRanger");

        }
        else  {
            this.animator.SetTrigger("AttackA");
        }
      
    }
    public void OnRangeAttackClick()
    {
        //StartCoroutine(AttackFwd());
        this.animator.SetTrigger("AttackRanger");
    }
    public void OnRedAttackClick()
    {
        this.animator.SetTrigger("AttackGun");
    }
    public void AttackB1()
    {
        isCanAttackB = true;
    }
    public void AttackB2()
    {
        isCanAttackB = false;
    
    }
    public void AttackRange1()
    {
     
        isCanAttackRange = true;
    }
    public void AttackRange2()
    {
        isCanAttackRange = false;
    }
}
