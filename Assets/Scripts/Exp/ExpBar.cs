using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpBar : MonoBehaviour {
    public Slider expBar;
    public static ExpBar instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        expBar = this.GetComponent<Slider>();
    }
    public void SetValue(float value)
    {
        expBar.value = value;
    }
}
