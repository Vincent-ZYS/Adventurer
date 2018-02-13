using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public static SpawnManager instance;
    public List<GameObject> enemyList = new List<GameObject>();
    void Awake()
    {
        instance = this;
    }
}
