using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShortCutsDes : MonoBehaviour {
    public static ShortCutsDes instance;
    public Text name_label;
    public Text mp_label;
    public Text des_label;
    void Awake()
    {
        instance = this;
    }
    // Use this for initialization
	void Start () {
        name_label = transform.Find("name_label").GetComponent<Text>();
        mp_label = transform.Find("mp_label").GetComponent<Text>();
        des_label = transform.Find("des_label").GetComponent<Text>();
        this.gameObject.SetActive(false);
	}

}
