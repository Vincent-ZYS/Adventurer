using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Xml;
public class DSController : MonoBehaviour {
    public static DSController instance;
    public Text infomation;
    public GameObject DialogueUI;
    public Image head;
    public Button receiveButton;
    public Button rejectButton;
    public bool isSingle = false;
    public TextAsset DrugText;
    //public TextAsset EquipText;
    [HideInInspector]
   public  string[] drug_Array;
    [HideInInspector]
   public string[] drugButton;
    [HideInInspector]
    public string[] equip_Array;
    [HideInInspector]
    public string[] equipButton;
    [HideInInspector]
    public string[] bar_Array;
    int i = 0;
    void Start()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(DrugText.text);
        XmlElement mElement = xmlDoc.DocumentElement;//读取节点值
        XmlNodeList mNodeList = mElement.SelectNodes("/DrugNpc/Dialogue");//选取字节点
        drug_Array = new string[mNodeList.Count];
        for (int i = 0; i < drug_Array.Length; i++)
        {
            drug_Array[i] = mNodeList[i].InnerText;
        }
        XmlNodeList mNodeList1 = mElement.SelectNodes("/DrugNpc/Button");//选取字节点
        drugButton = new string[mNodeList1.Count];
        for(int i=0;i<drugButton.Length;i++)
        {
            drugButton[i] = mNodeList1[i].InnerText;
        }
        #region EquipDialogue
        XmlNodeList mNodeList_equip = mElement.SelectNodes("/DrugNpc/DialogueEquip");
        equip_Array = new string[mNodeList_equip.Count];
        for(int i=0;i<equip_Array.Length;i++)
        {
            equip_Array[i] = mNodeList_equip[i].InnerText;
        }
        XmlNodeList mNodeList_equip_button = mElement.SelectNodes("/DrugNpc/Equip");
        equipButton = new string[mNodeList_equip_button.Count];
        for (int i = 0; i < mNodeList_equip_button.Count; i++)
        {
            equipButton[i] = mNodeList_equip_button[i].InnerText;
        }
        #endregion

        #region BarDialogue
        XmlNodeList mNodeList_Bar = mElement.SelectNodes("/DrugNpc/DialogueBar");
        bar_Array = new string[mNodeList_Bar.Count];
        for (int i = 0; i < bar_Array.Length; i++)
        {
            bar_Array[i] = mNodeList_Bar[i].InnerText;
        }

        #endregion

    }
    void Awake()
    {
        instance = this;
    }
    public void ShowDialogue(string tempinfomation="",Sprite temphead=null,Sprite npcSprite=null,UnityAction Receive=null,UnityAction Reject=null,string Tag="",string receive="",string reject="")
    {
        if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && (EventSystem.current.IsPointerOverGameObject() == false || (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider && (hitInfo.collider.tag == Tag))
            {
               
                //this.infomation.text = tempinfomation;
                DialogueUI.SetActive(true);
                this.infomation.DOText(tempinfomation, 2);

                this.head.sprite = npcSprite;
                receiveButton.onClick.AddListener(Receive);
            }
        }
        if (DialogueUI.activeInHierarchy && (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && (EventSystem.current.IsPointerOverGameObject() || (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))))
        {
            if (Tag != "")
            {
              

                rejectButton.onClick.AddListener(Reject);
                this.head.sprite = temphead;
                this.infomation.gameObject.SetActive(false);
                receiveButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = receive;
                rejectButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = reject;
                receiveButton.gameObject.SetActive(true);
                rejectButton.gameObject.SetActive(true);
               

            }
        }


        
    }
    public void ShowDialogueSingle(string tempinfomation = "", Sprite temphead = null)
    {
        
        DialogueUI.SetActive(true);
        this.infomation.DOText(tempinfomation, 2);
        this.head.sprite = temphead;
        isSingle = true;
        
    }


    public void OnCancelClick()
    {
        receiveButton.onClick.RemoveAllListeners();
        rejectButton.onClick.RemoveAllListeners();
        receiveButton.gameObject.SetActive(false);
        rejectButton.gameObject.SetActive(false);
        infomation.gameObject.SetActive(true);
        infomation.text = "";
        infomation.DOKill(false);
        DialogueUI.SetActive(false);
        if (ShopDrugNpc.instance != null)
        {
            ShopDrugNpc.instance.isMe = false;
        }
        if (EquipNpc.instance != null)
        {
            EquipNpc.instance.isMe = false;
        }
        if (BarNpc.instance != null)
        {
            BarNpc.instance.isMe = false;
        }
    }
}
