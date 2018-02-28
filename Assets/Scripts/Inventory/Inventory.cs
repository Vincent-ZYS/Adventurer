using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public static Inventory _instance;
    public int coinCount = 1000;
    public List<InventoryGrid> itemGridList = new List<InventoryGrid>();
    private bool isShow = false;
    public static Inventory instance;
    public DOTweenAnimation dotween;
    public Text coinNumber;
    public GameObject inventoryItem;
    [HideInInspector]
    public List<int> memorizeItem = new List<int>();
    public List<int> memorizeShortCut = new List<int>();
    public GameObject drop_panel;
    public int tempId;
    [Header("ShortCut数组")]
    public ObjectShortCut[] shortCuts;
    void Awake()
    {
        instance = this;
        coinNumber.text = coinCount.ToString();
     
    }
    public void TransformState()//转变任务栏状态
    {
        if (isShow == false)
        {
            Show();
        }
        else
        {

            Hide();
        }
    }
    public void Show()
    {
        isShow = true;
        dotween.DOPlayForward();

    }
    public void Hide()
    {
        dotween.DOPlayBackwards();
        isShow = false;
        InventoryDes.instance.gameObject.SetActive(false);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetId(Random.Range(1001, 1009));
        }

    }
    public void OnPickClick()
    {
    
       
            GetId(Random.Range(1001, 1009));
        
    }
    public void GetId(int id, int count = 1)
    {
     
        memorizeItem.Add(id);
       
        InventoryGrid grid = null;
  
        foreach (InventoryGrid temp in itemGridList)
        {
            
            if (temp.id == id)
            {
                
                grid = temp;
                break;
            }
        }
        if (grid != null)
        {
            grid.PlusNumber(count);
        }
        else
        {
            foreach (InventoryGrid temp in itemGridList)
            {
                if (temp.id == 0)
                {
                  
                    grid = temp;
                    break;
                }
            }
            if (grid != null)
            {
                GameObject itemGo = Instantiate(inventoryItem.gameObject);
               itemGo.transform.parent= grid.gameObject.transform;
                inventoryItem.GetComponent<InventoryItem>().grid_item = grid;
                //GameObject itemGo = NGUITools.AddChild(grid.gameObject, inventoryItem);
                itemGo.transform.localPosition = Vector3.zero;
                itemGo.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                itemGo.transform.SetAsFirstSibling();
                grid.SetId(id, count);

            }
        }
    }
    public bool GetCoin(int count)//用来给外界提供取金币的
    {
        if (coinCount >= count)
        {
            coinCount -= count;
            coinNumber.text = coinCount.ToString();
            return true;
        }
        return false;
    }
    public bool MinusId(int id, int count = 1)
    {

          InventoryGrid grid = null;
        foreach (InventoryGrid temp in itemGridList)
        {
            if (temp.id == id)
            {
                grid = temp;
                break;
            }
        }
     
        if (grid == null)
        {
            return false;
           
        }
        else
        {

            bool isSuccess = grid.MinusNumber(count);
            return isSuccess;
        }
    }
    
 public void  OpenDropClick()
    {
        drop_panel.SetActive(true);
    }
    public void OnConfirmClick()
    {
        bool tempBool = MinusId(tempId, int.Parse(InputNumber.instance.number_text.text));
        InputNumber.instance.gameObject.SetActive(false);

    }
    public void OnDrugUseClick()//消耗药品
    {
        bool tempBool = MinusId(tempId, 1);
        if(tempBool)
        {
            ObjectInfo objectinfo = ObjectsInfo.instance.GetObjectInfoById(tempId);
            PlayerStatus.instance.GetDrug(objectinfo.hp, objectinfo.mp);
            HeadStatusUI.instance.UpdateShow();
        }
    }
    public void OnCloseClick()
    {
        InputNumber.instance.gameObject.SetActive(false);
    }
    public void OnDressItemClick()
    {
        bool success = EquipmentUI.instance.Dress(tempId);
        if (success)
        {
            foreach(InventoryGrid temp in itemGridList)
            {
                if(temp.id==tempId)
                {
                    temp.MinusNumber();


                }
            }

        }
        else
        {
            ObjectInfo tempObjectInfo = ObjectsInfo.instance.GetObjectInfoById(tempId);
            if(tempObjectInfo.type==ObjcetType.Drug)
            {
                
                if(tempId%2==0)//血药
                {
                    foreach (ObjectShortCut temp in shortCuts)
                    {
                        if (temp.drugType == ShortCutType.Hp)
                        {
                            temp.id = tempId;
                            temp.thisInfo = tempObjectInfo;
                            temp.gameObject.SetActive(true);
                            temp.image.sprite= Resources.Load("UI/Sprite/" + tempObjectInfo.icon_name, typeof(Sprite)) as Sprite;
                            foreach (InventoryGrid tempGrid in itemGridList)
                            {
                                if(tempGrid.id==tempId)
                                {
                                    temp.number.text = tempGrid.num.ToString();
                                }
                            }


                        }
                    }
                }
                else//蓝药
                {
                    foreach (ObjectShortCut temp in shortCuts)
                    {
                        if (temp.drugType == ShortCutType.Mp)
                        {
                            temp.id = tempId;
                            temp.id = tempId;
                            temp.thisInfo = tempObjectInfo;
                            temp.gameObject.SetActive(true);
                            temp.image.sprite = Resources.Load("UI/Sprite/" + tempObjectInfo.icon_name, typeof(Sprite)) as Sprite;
                            foreach (InventoryGrid tempGrid in itemGridList)
                            {
                                if (tempGrid.id == tempId)
                                {
                                    temp.number.text = tempGrid.num.ToString();
                                }
                            }

                        }
                    }
                }
            }
            
        }
    }
 
    public void PlussProperty(int temp_id)//穿上装备之后增加属性
    {
        ObjectInfo obj = ObjectsInfo.instance.GetObjectInfoById(temp_id);
        PlayerStatus.instance.attack += obj.attack;
        PlayerStatus.instance.def += obj.def;
        PlayerStatus.instance.speed += obj.speed;
        Status.instance.UpdateShow();
        HeadStatusUI.instance.UpdateShow();
    }
    public void MinusProperty(int temp_id)//穿上装备之后减去属性
    {
        ObjectInfo obj = ObjectsInfo.instance.GetObjectInfoById(temp_id);
        PlayerStatus.instance.attack -= obj.attack;
        PlayerStatus.instance.def -= obj.def;
        PlayerStatus.instance.speed -= obj.speed;
        Status.instance.UpdateShow();
        HeadStatusUI.instance.UpdateShow();

    }
    public void AddCoin(int count)//用来获取金币
    {
        coinCount += count;
        coinNumber.text = coinCount.ToString();
    }

}
