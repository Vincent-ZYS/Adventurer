using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkillsInfoCs : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        for (int i = 4001; i < 4006; i++)
        {
            SkillInfo info = SkillsInfo.instance.GetSkillInfoById(i);
            Debug.Log("skillInfoEffectName::" + info.skillEffect_name);
        }
    }
	
	
}
