using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoulBossStatus : MonoBehaviour {
   
    public float hp;
    public float hp_remain;
    public Slider hpBar;
    public void UpdateShow()
    {
        hpBar.value = hp_remain / hp;
    }
}
