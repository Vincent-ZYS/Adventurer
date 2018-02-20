using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SkillShortCut : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public static SkillShortCut instance;
    public int thisId;
    public SkillInfo thisInfo=null;
    public float timer = 0;
    private bool isReleaseSkill = false;
    private bool isDown = false;
    public Image filledImage;
    public Text timer_text;
    public bool isDodge = false;
    GameObject tempEffect;
    public Vector3 targetPosition;//你要闪避的目标位置
    GameObject player = null;
    [Header("长按延迟")]
    public float delay = 0.2f;
    [Header("是否进入冷却")]
    private bool isStart = false;
    public GameObject PopupDamgae;
    [HideInInspector]
    public Transform firePosition;//实例化的位置
    [Header("技能特效")]
    public GameObject Scanvenger;
    public GameObject Blessing;
    public GameObject Stregnthen;
    public GameObject dodgeEffect;
    private float lastIsDownTime;//最后一次点击技能按钮的时间
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        thisInfo = SkillsInfo.instance.GetSkillInfoById(thisId);
        Transform temp = transform.Find("FilledImage");
         filledImage=temp.gameObject.GetComponent<Image>();
         timer_text = temp.Find("timer").gameObject.GetComponent<Text>();
        firePosition = GameObject.FindWithTag(Tags.player).transform;
    }
   
    public void ReleaseSkillClick()
    {
        if (isReleaseSkill&&isStart==false)
        {
            //TODO 产生粒子效果  
         bool isSuccess=PlayerStatus.instance.TakeMP(thisInfo.mp);
            if (!isSuccess)
                return;
            if(this.thisInfo.applyType==ApplyType.MultiTarget)
            {
                PlayerAnimationAttack.instance.animator.SetTrigger("AttackRanger");
               
                    StartCoroutine(WaitSkillAnimation());
                
               
               
            }
            if(this.thisInfo.applyType==ApplyType.Passive)
            {
                if(this.thisInfo.applyProperty==ApplyProperty.HP)
                {
                    GameObject.Instantiate(Blessing, firePosition.localPosition, Quaternion.identity);
                    PlayerStatus.instance.GetDrug(thisInfo.applyValue, 0);
                    DamagePopup tempPop = PopupDamgae.GetComponent<DamagePopup>();
                    tempPop.Value = thisInfo.applyValue;
                    tempPop.messageType = PopupType.treatment;
                    GameObject.Instantiate(PopupDamgae,firePosition.localPosition, Quaternion.identity);
                    HeadStatusUI.instance.UpdateShow();
                }
            }
            if (this.thisInfo.applyType == ApplyType.Buff)
            {
                if (this.thisInfo.applyProperty == ApplyProperty.AttackSpeed)
                {
                    StartCoroutine(WaitStregnthen());
                    
                }
            }
            if(this.thisInfo.applyType==ApplyType.Dodge)
            {
                if(thisInfo.applyProperty==ApplyProperty.Dodge)
                {

                    player = GameObject.FindWithTag(Tags.player);

                    targetPosition = player.transform.position + player.transform.forward * 5;
                    tempEffect = GameObject.Instantiate(dodgeEffect, player.transform.position, player.transform.rotation);
                    tempEffect.transform.SetParent(player.transform);

                    isDodge = true;
                }
            }
            isStart = true;
        }
    }
    IEnumerator WaitStregnthen()
    {
      GameObject tempSkill=  GameObject.Instantiate(Stregnthen, firePosition.localPosition, Quaternion.identity);
        tempSkill.transform.SetParent(firePosition);
     
        PlayerStatus.instance.attack += thisInfo.applyValue* PlayerStatus.instance.attack/100;
        PlayerStatus.instance.speed += thisInfo.applyValue * PlayerStatus.instance.speed/100;
      
        yield return  new WaitForSeconds(thisInfo.applyTime);
        Destroy(tempSkill);
        PlayerStatus.instance.attack -= thisInfo.applyValue* PlayerStatus.instance.attack/100;
        PlayerStatus.instance.speed -= thisInfo.applyValue * PlayerStatus.instance.speed/100;

    }
    IEnumerator WaitSkillAnimation()
    {
        yield return new WaitForSeconds(0.6f);
     if(PlayerAnimationAttack.instance.animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackRange")){
            GameObject.Instantiate(Scanvenger, firePosition.localPosition + Vector3.forward, firePosition.localRotation); }
    }
    void LateUpdate()
    {
        if (isDodge)
        {
           
            if (Vector3.Distance(player.transform.position, targetPosition) >0.5f)
            {
                player.transform.position = Vector3.Lerp(player.transform.position, targetPosition, thisInfo.applyValue * Time.deltaTime);
               
            }
            else
            {
                isDodge = false;
                Destroy(tempEffect);
            }
        }
    }
    void Update()
    {

        if (isStart)
        {
            timer += Time.deltaTime;
        
            filledImage.fillAmount = (thisInfo.coldTime - timer) / thisInfo.coldTime;
            timer_text.gameObject.SetActive(true);
            timer_text.text =((int)(thisInfo.coldTime - timer+1)).ToString();
            if (timer >=thisInfo.coldTime)
            {
                isStart = false;
                filledImage.fillAmount = 0;
                timer = 0;
                timer_text.gameObject.SetActive(false);
            }
        }
        // 如果按钮是被按下状态  
        if (isDown)
        {
            // 当前时间 -  按钮最后一次被按下的时间 > 延迟时间0.2秒  
            if (Time.time - lastIsDownTime > delay)
            {


                ShortCutsDes.instance.gameObject.SetActive(true);
              
               ShortCutsDes.instance.gameObject.transform.position = Input.mousePosition;
                ShortCutsDes.instance.name_label.text = thisInfo.name;
                ShortCutsDes.instance.mp_label.text =  "Consume mp:"+ thisInfo.mp;
                ShortCutsDes.instance.des_label .text= "skill describe:"+thisInfo.des;
                lastIsDownTime = Time.time;
                isReleaseSkill = false;

            }
        }
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        lastIsDownTime = Time.time;
        isReleaseSkill = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        isDown = false;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
        ShortCutsDes.instance.gameObject.SetActive(false);
    }
}
