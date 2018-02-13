using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PopupType
{
    normalEnmey,//普通对怪物攻击
    doubleEnemy,//暴击对怪物伤害
    player,//怪物对主角的伤害
    doublePlayer,//怪物暴击主角
    treatment,//治疗
    restoreMagic//恢复魔法
}
public class DamagePopup : MonoBehaviour {


    //目标位置  
    private Vector3 mTarget;
    //屏幕坐标  
    private Vector3 mScreen;
    //伤害数值  
    public int Value;

    //文本宽度  
    public float ContentWidth = 400;
    //文本高度  
    public float ContentHeight = 200;

    //GUI坐标  
    private Vector2 mPoint;

    //销毁时间  
    public float FreeTime = 1.5F;

    public Font guiskin;
    [HideInInspector]
    public GUIStyle mStyle=new GUIStyle();

    [Header("FontSize")]
    public int fontSize = 25;
    [HideInInspector]
    public PopupType messageType;
    void Start()
    {
        
        mStyle.font = guiskin;

        switch(messageType)
        {
            case PopupType.normalEnmey:
                mStyle.normal.textColor = Color.red;
                break;
            case PopupType.doubleEnemy:
                mStyle.normal.textColor = Color.yellow;
                break;
            case PopupType.player:mStyle.normal.textColor = Color.white;
                break;
            case PopupType.treatment:mStyle.normal.textColor = Color.green;
                break;
            case PopupType.restoreMagic:mStyle.normal.textColor = Color.blue;
                break;
        }
        mStyle.fontSize = fontSize;
        //获取目标位置  
        mTarget = transform.position;
        //获取屏幕坐标  
        mScreen = Camera.main.WorldToScreenPoint(mTarget);
        //将屏幕坐标转化为GUI坐标  
        mPoint = new Vector2(mScreen.x, Screen.height - mScreen.y);
        //开启自动销毁线程  
        StartCoroutine("Free");
    }

    void Update()
    {
        //使文本在垂直方向山产生一个偏移  
        transform.Translate(Vector3.up * 0.5F * Time.deltaTime);
        //重新计算坐标  
        mTarget = transform.position;
        //获取屏幕坐标  
        mScreen = Camera.main.WorldToScreenPoint(mTarget);
        //将屏幕坐标转化为GUI坐标  
        mPoint = new Vector2(mScreen.x, Screen.height - mScreen.y);
    }

    void OnGUI()
    {
        //保证目标在摄像机前方  
        if (mScreen.z > 0)
        {
            //内部使用GUI坐标进行绘制  
            GUI.Label(new Rect(mPoint.x, mPoint.y, ContentWidth, ContentHeight), Value.ToString(),mStyle);
            
        }
    }

    IEnumerator Free()
    {
        yield return new WaitForSeconds(FreeTime);
        Destroy(this.gameObject);
    }
}
