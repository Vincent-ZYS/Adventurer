using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInfo : MonoBehaviour {
    public TextAsset skillsInfoText;
    public static SkillsInfo instance;
    private Dictionary<int, SkillInfo> skillInfoDict = new Dictionary<int, SkillInfo>();
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        InitSkillInfoDict();
    }
    public SkillInfo GetSkillInfoById(int id)
    {
        SkillInfo info = null;
        skillInfoDict.TryGetValue(id, out info);
   
        return info;
    }
    void InitSkillInfoDict()
    {
        string text = skillsInfoText.text;
        string[] skillinfoArray = text.Split('\n');
        foreach (string skillinfoStr in skillinfoArray)
        {
            string[] pa = skillinfoStr.Split(',');
            SkillInfo info = new SkillInfo();
            info.id = int.Parse(pa[0]);
            info.name = pa[1];
            info.icon_name = pa[2];
            info.des = pa[3];
            string str_applytype = pa[4];
            switch (str_applytype)
            {
                case "Passive":
                    info.applyType = ApplyType.Passive;
                    break;
                case "Buff":
                    info.applyType = ApplyType.Buff;
                    break;
                case "SingleTarget":
                    info.applyType = ApplyType.SingleTarget;
                    break;
                case "MultiTarget":
                    info.applyType = ApplyType.MultiTarget;
                    break;
                case "Call":info.applyType = ApplyType.Call;
                    break;

            }
            string str_applypro = pa[5];
            switch (str_applypro)
            {
                case "Attack":
                    info.applyProperty = ApplyProperty.Attack;
                    break;
                case "Def":
                    info.applyProperty = ApplyProperty.Def;
                    break;
                case "Speed":
                    info.applyProperty = ApplyProperty.Speed;
                    break;
                case "AttackSpeed":
                    info.applyProperty = ApplyProperty.AttackSpeed;
                    break;
                case "HP":
                    info.applyProperty = ApplyProperty.HP;
                    break;
                case "MP":
                    info.applyProperty = ApplyProperty.MP;
                    break;
                case "Partner":
                    info.applyProperty = ApplyProperty.Partner;
                    break;

            }
            info.applyValue = int.Parse(pa[6]);
            info.applyTime = int.Parse(pa[7]);
            info.mp = int.Parse(pa[8]);
            info.coldTime = int.Parse(pa[9]);
            info.level = int.Parse(pa[10]);
            switch (pa[11])
            {
                case "Self":
                    info.releaseType = ReleaseType.Self;
                    break;
                case "Enemy":
                    info.releaseType = ReleaseType.Enemy;
                    break;
                case "Position":
                    info.releaseType = ReleaseType.Position;
                    break;
            }
            info.distance =int.Parse(pa[12]);
            skillInfoDict.Add(info.id, info);
        }
    }
}
public enum ApplyType//作用类型
{
    Passive,
    Buff,
    SingleTarget,
    MultiTarget,
    Call
}
public enum ApplyProperty//作用属性
{
    Attack,
    Def,
    Speed,
    AttackSpeed,
    HP,
    MP,
    Partner
}
public enum ReleaseType
{
    Self,
    Enemy,
    Position
}
public class SkillInfo
{
    public int id;
    public string name;
    public string icon_name;
    public string des;
    public ApplyType applyType;
    public ApplyProperty applyProperty;
    public int applyValue;
    public int applyTime;
    public int mp;
    public int coldTime;
    public int level;
    public ReleaseType releaseType;
    public float distance;
}
