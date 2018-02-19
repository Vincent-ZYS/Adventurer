using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillItem : MonoBehaviour {
    public int id;
    public Image icon;
    public Text name_label;
    SkillInfo info = null;
    public void SetId(int id)//设置技能Item
    {
        this.id = id;
        info = SkillsInfo.instance.GetSkillInfoById(id);
        icon.sprite = Resources.Load(info.icon_name, typeof(Sprite)) as Sprite;
        name_label.text = info.name;
    }
    public void UpdateShow(int level)
    {
        if(info.level<=level)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    public void SelectSkillItem()
    {
        SkillDes.instance.skill_name_label.text = info.name;
        SkillDes.instance.skill_type_label.text ="Skill type:"+ info.applyType.ToString();
        SkillDes.instance.skill_mp_label.text = "Consume mp:"+info.mp.ToString();
        SkillDes.instance.skill_des_label.text ="Skill Describe:"+info.des;
        SkillDes.instance.current_id = id;
    }
}
