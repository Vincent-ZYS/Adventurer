using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
public class BarNpc : MonoBehaviour {

    [HideInInspector]
    public bool isMe = false;
    public static BarNpc instance;
    public GameObject DialogueUI;
    public DOTweenAnimation dotween;
    private bool isShow = false;
    private float killCount = 0;//击杀的数量
    private bool isInTask = false;//是否正在进行任务
    public GameObject okBtnGo;
    public GameObject acceptBtnGo;
    public GameObject cancelBtnGo;
    private Text desLabel;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        DialogueUI = GameObject.Find("DialogueUI");
        desLabel = GameObject.Find("Quest").transform.Find("MessageLabel").gameObject.GetComponent<Text>();
    }
    void Update()
    {
        #region DialogueBar
        if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && (EventSystem.current.IsPointerOverGameObject() == false || (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);

            if (isCollider && hitInfo.collider.tag == Tag.barNpc)
            {
              
                isMe = true;
            }
            if(isMe)
            {
                DSController.instance.ShowDialogueSingle("Can you help me ?", null);
            }


        }
        if (DSController.instance.isSingle == true&&isMe==true)
        {
               if(DialogueUI==null)
            { DialogueUI = GameObject.Find("DialogueUI"); }
            if (DialogueUI.activeInHierarchy && (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && (EventSystem.current.IsPointerOverGameObject() || (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))))
            {
                    DSController.instance.OnCancelClick();
                    DSController.instance.isSingle = false;
                ShowQuest();
                
            }
            
           
        }
        #endregion
    }
    private void ShowQuest()//查看任务当前状态
    {

        if (isInTask)
        {
            ShowTaskProgress();

        }
        else
        {
            ShowTaskDes();
        }
        TransformState();
    }
    public void TransformState()//开启任务面板
    {
        if (!isShow)
        {
            dotween.DOPlayForward();
            isShow = true;
        }
        else
        {
            dotween.DOPlayBackwards();
            isShow = false;
        }
    }
    public void OnKillEnemy()
    {
        if (isInTask)
        {
            killCount++;
        }
    }
    void ShowTaskProgress()//显示任务进度
    {
        desLabel.text = "任务:\n你已经杀死了" + killCount + "\\10只狼\n\n奖励:\n1000金币";
        okBtnGo.SetActive(true);
        acceptBtnGo.SetActive(false);
        cancelBtnGo.SetActive(false);


    }
    void ShowTaskDes()
    {
        desLabel.text = "任务:\n杀死了10只狼\n\n奖励:\n1000金币";
        okBtnGo.SetActive(false);
        acceptBtnGo.SetActive(true);
        cancelBtnGo.SetActive(true);
    }
    public void OnAcceptButtonClick()
    {
        ShowTaskProgress();
        isInTask = true;
    }
    public void OnOkButtonClick()
    {
        if (killCount >= 10)
        {
            //完成任务
            //status.GetCoins(1000);
            Inventory.instance.AddCoin(1000);
            ShowTaskDes();
        }
        else
        {
            //没有完成任务
            TransformState();
        }
    }
}
