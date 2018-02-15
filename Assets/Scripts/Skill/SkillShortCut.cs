using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SkillShortCut : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public int thisId;
    public SkillInfo thisInfo=null;
    public float timer = 0;
    private bool isReleaseSkill = false;
    private bool isDown = false;
    public Image filledImage;
    public Text timer_text;
    [Header("长按延迟")]
    public float delay = 0.2f;
    [Header("是否进入冷却")]
    private bool isStart = false;


    private float lastIsDownTime;//最后一次点击技能按钮的时间
    void Start()
    {
        thisInfo = SkillsInfo.instance.GetSkillInfoById(thisId);
        Transform temp = transform.Find("FilledImage");
         filledImage=temp.gameObject.GetComponent<Image>();
         timer_text = temp.Find("timer").gameObject.GetComponent<Text>();
    }
    public void ReleaseSkillClick()
    {
        if (isReleaseSkill)
        {
            //TODO 产生粒子效果  

            isStart = true;
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
