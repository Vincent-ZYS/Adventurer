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
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;

        if (this.gameObject != GameObject.Find("Main Camera"))
        {
        }
        else {
            RotateView();
            ScrollWhell();
        }
    }
    void ScrollWhell()
    {
        distance = offset.magnitude;//获得原来的一个距离
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;
        distance = Mathf.Clamp(distance, 2, 18);
        offset = offset.normalized * distance;
    }
    void RotateView()
    {
        if (Input.GetMouseButtonDown(0)&&(EventSystem.current.IsPointerOverGameObject()==false))
        {
            isRotate = true;
        }
        if ( (Input.touchCount >  0&& Input.GetTouch(0).phase == TouchPhase.Began)&&(ETCInput.GetAxis("Horizontal")==0||ETCInput.GetAxis("Vertical")==0))
        {

            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)==false)
            {
                isRotate = true;
            } 
        }
        if ((Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Began))
        {

            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(1).fingerId)==false)
            {
                isRotate = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotate = false;
        }
        if (isRotate)
        {
            transform.RotateAround(player.position, player.up, Input.GetAxis("Mouse X") * rotateSpeed);
            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;

            transform.RotateAround(player.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
            float x = transform.eulerAngles.x;
            if (x < 10 || x > 80)
            {
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }
        }
        offset = transform.position - player.position;
    }
}
