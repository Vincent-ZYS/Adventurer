using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestory : MonoBehaviour
{
    public float exitTime = 1;
    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, exitTime);
    }


}
