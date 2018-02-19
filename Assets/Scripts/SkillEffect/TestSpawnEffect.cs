using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnEffect : MonoBehaviour {

    // Use this for initialization
    public Transform Scavenger;
	void Start () {
        Scavenger.localPosition = transform.localPosition;
        Scavenger.gameObject.SetActive(true);
	}
	

}
