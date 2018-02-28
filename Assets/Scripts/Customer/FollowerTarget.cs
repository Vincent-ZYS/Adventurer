using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class FollowerTarget : MonoBehaviour {

    private Transform player;
    private Vector3 offset;//相机与主角位置的一个便偏移
    public float scrollSpeed = 10;//鼠标滑轮的速度
    public float distance = 0;//用来存储相机与主角的一个距离
    public float rotateSpeed = 10;
    private bool isRotate = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag(Tag.player).transform;
        float z= player.transform.position.z;
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;

    }

}
