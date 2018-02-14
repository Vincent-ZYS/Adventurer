using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillDes : MonoBehaviour {
    public static SkillDes instance;
    public Text skill_name_label;
    public Text skill_des_label;
    public Text skill_type_label;//描述技能类型
    public Text skill_mp_label;//描述技能所需要的MP
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        skill_des_label = GameObject.Find("Des_text").GetComponent<Text>();
        skill_mp_label = GameObject.Find("mp_label").GetComponent<Text>();
        skill_type_label = GameObject.Find("skill_type_label").GetComponent<Text>();
        skill_name_label = GameObject.Find("skill_name_label").GetComponent<Text>();
        ClearDescribe();
    }
    public void ClearDescribe()
    {
        skill_des_label.text = "";
        skill_mp_label.text = "";
        skill_type_label.text = "";
        skill_name_label.text = "";
    }
}
