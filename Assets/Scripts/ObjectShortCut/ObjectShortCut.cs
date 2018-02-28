using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public enum ShortCutType
{
    Hp,
    Mp
}
public class ObjectShortCut : MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public ShortCutType drugType;
    public int id;
    public ObjectInfo thisInfo = null;
    public Image image;
    public Text number;
    private bool isStart = false;
    private bool isReleaseDrug = false;
    public float timer = 0;
    public Image filledImage;
    public Text timer_text;
    private bool isDown = false;
    private float lastIsDownTime;//最后一次点击技能按钮的时间
    [Header("长按延迟")]
    public float delay = 0.2f;
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        number = transform.Find("number").GetComponent<Text>();
        filledImage = transform.Find("FilledImage").GetComponent<Image>();
        timer_text = transform.Find("timer").GetComponent<Text>();
        image.gameObject.SetActive(false);
    }
    public void ReleaseDrug()
    {
        if(isStart==false&&isReleaseDrug)
        {
            OnDrugUseClick();
            isStart = true;
        }
    }
    public void OnDrugUseClick()//消耗药品
    {
        bool tempBool = Inventory.instance.MinusId(id, 1);
        if (tempBool)
        {
            ObjectInfo objectinfo = ObjectsInfo.instance.GetObjectInfoById(id);
       
            PlayerStatus.instance.GetDrug(objectinfo.hp, objectinfo.mp);
            MinusDrug();
            HeadStatusUI.instance.UpdateShow();
        }
    }
    void Update()
    {

        if (isStart)
        {
            timer += Time.deltaTime;

            filledImage.fillAmount = (thisInfo.coldTime - timer) / thisInfo.coldTime;
            timer_text.gameObject.SetActive(true);
            timer_text.text = ((int)(thisInfo.coldTime - timer + 1)).ToString();
            if (timer >= thisInfo.coldTime)
            {
                isStart = false;
                filledImage.fillAmount = 0;
                timer = 0;
                timer_text.gameObject.SetActive(false);
            }
        }
        if (isDown)
        {
            // 当前时间 -  按钮最后一次被按下的时间 > 延迟时间0.2秒  
            if (Time.time - lastIsDownTime > delay)
            {


                ShortCutsDes.instance.gameObject.SetActive(true);

                ShortCutsDes.instance.gameObject.transform.position = Input.mousePosition;
                ShortCutsDes.instance.name_label.text = thisInfo.name;
                ShortCutsDes.instance.mp_label.text = "Restore mp:" + thisInfo.mp;
                ShortCutsDes.instance.des_label.text = "Restore hp" + thisInfo.hp;
                lastIsDownTime = Time.time;
                isReleaseDrug = false;

            }
        }
    }
    void MinusDrug()
    {
        number.text =( int.Parse(number.text)-1).ToString();
        if (int.Parse(number.text) <= 0)
        {
            this.gameObject.SetActive(false);
        }

    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        lastIsDownTime = Time.time;
        isReleaseDrug = true;
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
