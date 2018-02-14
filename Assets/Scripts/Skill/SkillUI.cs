using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SkillUI : MonoBehaviour {
    public static SkillUI instance;
    //public int[] SkillIdList;//技能id的集合
    public int[] idList;
    public Transform[] skillPositions;//预制技能生成的位置
    public GameObject skillItem;
    private DOTweenAnimation dotween;
    private bool isShow = false;//是否进行弹出
    [Header("TestLevel")]
  public  int testLevel = 1;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        dotween = this.GetComponent<DOTweenAnimation>();
        int i=0;//临时变量用于定义位置索引
        foreach (int id in idList)
        {
            
            GameObject itemGo = GameObject.Instantiate(skillItem, skillPositions[i].position,Quaternion.identity);
            itemGo.transform.SetParent(skillPositions[i]);
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.transform.localScale = new Vector2(0.4f, 0.4f);
            i++;
           itemGo.GetComponent<SkillItem>().SetId(id);

        }

    }
    void UpdateShow()//判断当前等级是否能学习技能
    {
       
        SkillItem[] items = this.GetComponentsInChildren<SkillItem>();
        foreach (SkillItem item in items)
        {
            item.UpdateShow(PlayerStatus.instance.level);
        }
    }
    public void TransformState()//转变任务栏状态
    {
        if (isShow == false)
        {
            UpdateShow();
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
    }

}
